using System;
using System.Reflection;
using Rocket.Surgery.Conventions;

namespace Rocket.Surgery.Automation.Postgres
{
    public static class PostgresConventionExtensions
    {
        public static IConventionHostBuilder UseDevelopmentPostgres(this IConventionHostBuilder builder, int port, string key, string database = null, TimeSpan timeout = default, Action<PostgresConvention> options = null) 
        {
            if (Helpers.DoesAssemblyLookDebugLike(Assembly.GetCallingAssembly()) || Helpers.DoesAssemblyLookDebugLike(Assembly.GetEntryAssembly()))
            {
                var convention = PostgresConvention.For(port, key, database, timeout);
                options?.Invoke(convention);
                builder.Scanner.AppendConvention(convention);
            }
            return builder;
        }
        public static IConventionHostBuilder UseDevelopmentPostgres(this IConventionHostBuilder builder, string host, int port, string username, string password, string database, string[] keys, TimeSpan timeout = default, Action<PostgresConvention> options = null) 
        {
            if (Helpers.DoesAssemblyLookDebugLike(Assembly.GetCallingAssembly()) || Helpers.DoesAssemblyLookDebugLike(Assembly.GetEntryAssembly()))
            {
                var convention = PostgresConvention.For(host, port, username, password, database, keys, timeout);
                options?.Invoke(convention);
                builder.Scanner.AppendConvention(convention);
            }
            return builder;
        }
    }
}
