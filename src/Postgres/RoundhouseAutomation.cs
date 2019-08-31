using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rocket.Surgery.Automation.Postgres
{
    public class RoundhouseAutomation
    {
        private readonly List<string> _logs = new List<string>();
        private readonly List<string> _args = new List<string>();

        public static RoundhouseAutomation For(string? directory = null, string? environment = null)
        {
            return new RoundhouseAutomation(directory, environment);
        }

        private readonly string _directory;
        private readonly string _environment;

        private RoundhouseAutomation(string? directory, string? environment)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = Path.Combine(
                    Helpers.FindDirectoryContainingDirectory(Directory.GetCurrentDirectory(), ".git"), "database");
            }

            _directory = Path.GetFullPath(directory);
            _environment = environment ?? "UnitTest";
        }

        public IEnumerable<string> Logs => _logs;

        public RoundhouseAutomation AddArgument(string arg)
        {
            _args.Add(arg);
            return this;
        }

        public async Task Start(string connectionString)
        {
            _logs.Add("Bringing database up to date");
            await Task.Yield();
            _logs.Add(Roundhouse(connectionString));
        }

        private string Roundhouse(string connectionString)
        {
            var rootDirectory = Helpers.FindDirectoryContainingDirectory(Directory.GetCurrentDirectory(), ".git");
            var dotnetTools = Path.Combine(rootDirectory, @".config", "dotnet-tools.json");
            var localTool = Helpers.GetDotNetTools(dotnetTools).Tools.TryGetValue("dotnet-roundhouse", out var config);
            var cmd = localTool ?
                config.Commands.First() :
                Helpers.FindTool(Path.Combine(_directory, "tools"), "rh.exe", "rh.bat", "rh") ??
                Helpers.FindToolInPath("rh.exe", "rh.bat", "rh");

            _args.AddRange(new[]
            {
                "-c", $"\"{connectionString}\"",  "-f", $"\"{_directory}\"", "-dt", "postgres", "--dc", "--env", _environment
            });

            var arguments = string.Join(" ", _args);
            arguments += " --silent";
            if (localTool)
            {
                arguments = cmd + " " + arguments;
            }

            var process = Process.Start(new ProcessStartInfo(localTool ? "dotnet" : cmd)
            {
                Arguments = arguments,
                WorkingDirectory = rootDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });

            var content = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            return content;
        }
    }
}
