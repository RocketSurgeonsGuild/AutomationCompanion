using System.Diagnostics;
using System.IO;
using Rocket.Surgery.Extensions.Configuration;

namespace Rocket.Surgery.Automation.DockerCompose
{
    public class DockerComposeConvention : IConfigurationConvention
    {
        private readonly string _composeFile;

        public static DockerComposeConvention For(string composeFile)
        {
            return new DockerComposeConvention(composeFile);
        }

        private DockerComposeConvention(string composeFile)
        {
            this._composeFile = composeFile;
        }

        public void Register(IConfigurationConventionContext context)
        {
            var _directory = Helpers.FindDirectoryContainingDirectory(Directory.GetCurrentDirectory(), ".git");
            if (string.IsNullOrWhiteSpace(_directory)) return;
            var process = Process.Start(new ProcessStartInfo("docker-compose")
            {
                Arguments = $"-f {_composeFile} up -d",
                WorkingDirectory = _directory,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });

            // var content = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
    }


}
