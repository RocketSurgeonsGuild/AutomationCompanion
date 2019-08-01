using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Rocket.Surgery.Extensions.Configuration;

namespace Rocket.Surgery.Automation.Azurite
{
    public class AzureiteConvention : IConfigurationConvention
    {
        public static AzureiteConvention For(int port, params string[] keys)
        {
            return new AzureiteConvention(port, keys);
        }

        private const int DevelopmentPort = 10000;
        private readonly int _port;
        private readonly string[] _keys;

        private AzureiteConvention(int port, string[] keys)
        {
            _port = port;
            _keys = keys
                .Concat(new[] {
                    "AzureWebJobsStorage",
                    "AzureWebJobsDashboard"
                })
                .ToArray();
        }

        public void Register(IConfigurationConventionContext context)
        {
            context.AddInMemoryCollection(_keys.Distinct().ToDictionary(x => x, x => GetConnectionString(_port)));
        }

        private static string GetConnectionString(int port)
        {
            var sb = new StringBuilder();
            sb.Append($"DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;");
            sb.Append($"AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;");
            sb.Append($"BlobEndpoint=http://127.0.0.1:{port}/devstoreaccount1;");
            sb.Append($"TableEndpoint=http://127.0.0.1:{port + 2}/devstoreaccount1;");
            sb.Append($"QueueEndpoint=http://127.0.0.1:{port + 1}/devstoreaccount1;");
            return sb.ToString();
        }
    }


}
