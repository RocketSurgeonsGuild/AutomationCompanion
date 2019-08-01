using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Rocket.Surgery.Extensions.Configuration;
using Rocket.Surgery.Extensions.DependencyInjection;

namespace Rocket.Surgery.Automation.Postgres
{
    public class PostgresConvention : IConfigurationConvention, IServiceConvention, IHostedService
    {
        public static PostgresConvention For(int port, string key, string database = null, TimeSpan timeout = default)
        {
            return new PostgresConvention("localhost", port, "myuser", "mypassword", database, new[] { key }, timeout);
        }

        public static PostgresConvention For(string host, int port, string username, string password, string database, string[] keys, TimeSpan timeout = default)
        {
            return new PostgresConvention(host, port, username, password, database, keys, timeout);
        }

        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly string[] _keys;
        private readonly NpgsqlConnectionStringBuilder _connectionStringBuilder;
        private bool _useRh;

        public RoundhouseAutomation Roundhouse { get; private set; }

        private string _databaseName;
        private readonly CancellationToken _token;

        public string ConnectionString => new NpgsqlConnectionStringBuilder(_connectionStringBuilder.ToString())
        {
            Database = _databaseName ?? _connectionStringBuilder.Database
        }.ToString();

        private PostgresConvention(string host, int port, string username, string password, string database, string[] keys, TimeSpan timeout)
        {
            _keys = keys;

            if (timeout == default) timeout = TimeSpan.FromMinutes(5);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(timeout);
            _token = cts.Token;

            _connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Username = username,
                Password = password,
                Database = database ?? "development",
            };
        }

        public PostgresConvention UseRoundhouse(string directory = null)
        {
            _useRh = true;
            Roundhouse = RoundhouseAutomation.For(directory);
            return this;
        }

        public PostgresConvention WithTestDatabase()
        {
            _databaseName = "pgsql" + Guid.NewGuid().ToString("N");
            return this;
        }

        public PostgresConvention WithDatabase(string name)
        {
            _databaseName = name;
            return this;
        }

        public void Register(IConfigurationConventionContext context)
        {
            context.AddInMemoryCollection(
                _keys.Distinct().ToDictionary(
                    x => x,
                    x => ConnectionString
                )
            );
        }

        public void Register(IServiceConventionContext context)
        {
            context.Services.AddSingleton<IHostedService>(this);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var token = CancellationTokenSource.CreateLinkedTokenSource(_token, cancellationToken).Token;
            await SemaphoreSlim.WaitAsync(token);
            await WaitForDatabaseToBeAvailable(token);
            await CreateDatabase(token);
            SemaphoreSlim.Release();
            if (_useRh)
            {
                await Roundhouse
                    .Start(ConnectionString);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await DropDatabase(cancellationToken);
        }

        async Task WaitForDatabaseToBeAvailable(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
                try
                {
                    using (var c = new NpgsqlConnection(_connectionStringBuilder.ToString()))
                    {
                        c.Open();
                        var command = c.CreateCommand();
                        command.CommandText = "SELECT * from pg_catalog.pg_tables;";
                        command.ExecuteNonQuery();
                    }
                    return;
                }
                catch
                {
                    await Task.Delay(50, cancellationToken);
                }
        }

        async Task CreateDatabase(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(_databaseName)) return;
            using (var c = new NpgsqlConnection(_connectionStringBuilder.ToString()))
            {
                c.Open();
                var cc = c.CreateCommand();
                cc.CommandText = $"SELECT EXISTS (SELECT 1 FROM pg_database WHERE datname = '{_databaseName}')";
                var hasDatabase = (bool)await cc.ExecuteScalarAsync(cancellationToken);
                if (!hasDatabase)
                {
                    var command = c.CreateCommand();
                    command.CommandText = $"CREATE DATABASE {_databaseName};";
                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }
        }

        public async Task DropDatabase(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(_databaseName)) return;

            using (var c = new NpgsqlConnection(_connectionStringBuilder.ToString()))
            {
                c.Open();
                var command = c.CreateCommand();
                command.CommandText = $@"
UPDATE pg_database SET datallowconn = 'false' WHERE datname = '{_databaseName}';
ALTER DATABASE {_databaseName} CONNECTION LIMIT 1;

SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{_databaseName}';

DROP DATABASE {_databaseName};";
                var result = await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }
}
