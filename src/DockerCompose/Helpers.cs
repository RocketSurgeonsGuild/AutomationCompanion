using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Rocket.Surgery.Automation
{
    static class Helpers
    {
        public static string GetFullPath(string fileName)
        {
            if (File.Exists(fileName))
                return Path.GetFullPath(fileName);

            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(';'))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                    return fullPath;
            }
            return null;
        }

        public static string FindDirectoryContainingDirectory(string directory, string targetDirectoryName)
        {
            while (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(Path.Combine(directory, targetDirectoryName)))
            {
                directory = Path.GetDirectoryName(directory);
            }
            return directory;
        }

        public static string FindDirectoryContainingFile(string directory, string targetDirectoryName)
        {
            while (!string.IsNullOrWhiteSpace(directory) && !File.Exists(Path.Combine(directory, targetDirectoryName)))
            {
                directory = Path.GetDirectoryName(directory);
            }
            return directory;
        }

        public static string FindToolInDirectory(string directory, params string[] toolNames)
        {
            return toolNames
                .SelectMany(tool => Directory.EnumerateFiles(directory, tool, SearchOption.AllDirectories))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .FirstOrDefault();
        }

        public static string FindToolInPath(params string[] toolNames)
        {
            return toolNames
                .Select(GetFullPath)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .FirstOrDefault();
        }

        public static string FindTool(string directory, params string[] toolNames)
        {
            var tool = FindToolInDirectory(directory, toolNames);
            if (string.IsNullOrWhiteSpace(tool))
            {
                tool = FindToolInPath(toolNames);
            }
            return tool;
        }

        public static bool DoesAssemblyLookDebugLike(Assembly assembly)
        {

            if (assembly == null) return false;
            var debuggable = assembly.GetCustomAttribute<DebuggableAttribute>();
            if (debuggable == null) return false;
            if (debuggable.IsJITTrackingEnabled)
            {
                return true;
            }
            var configuration = assembly.GetCustomAttribute<AssemblyConfigurationAttribute>();
            if (configuration == null) return false;
            if (configuration.Configuration.Equals("debug", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
