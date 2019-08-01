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

        public static RoundhouseAutomation For(string directory = null)
        {
            return new RoundhouseAutomation(directory);
        }

        private readonly string _directory;

        private RoundhouseAutomation(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = Helpers.FindDirectoryContainingDirectory(Directory.GetCurrentDirectory(), ".git");
            }

            _directory = directory;
        }

        public IEnumerable<string> Logs => _logs;

        public async Task Start(string connectionString)
        {
            _logs.Add("Bringing database up to date");
            await Task.Yield();
            _logs.Add(Roundhouse(connectionString));
        }

        private string Roundhouse(string connectionString)
        {
            //var roundhouse = Helpers.FindTool(Path.Combine(_directory, "tools"), "rh.exe", "rh.bat", "rh");

            var items = new Dictionary<string, string>()
            {
                { "-c", connectionString },
                { "-f", Path.Combine(_directory, "database") },
                { "-dt", "postgres" },
                // { "--version", new GitVersionAutomation().LegacySemVerPadded },
            };
            var arguments = "rh " + string.Join(" ", items.Select(x => $"{x.Key} \"{x.Value}\""));
            arguments += " --silent";

            var process = Process.Start(new ProcessStartInfo("dotnet")
            {
                Arguments = arguments,
                WorkingDirectory = _directory,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });

            var content = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return content;
        }
    }
}
