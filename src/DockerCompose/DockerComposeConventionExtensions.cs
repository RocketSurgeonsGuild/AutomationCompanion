using System;
using System.Reflection;
using Rocket.Surgery.Conventions;

namespace Rocket.Surgery.Automation.DockerCompose
{
    public static class DockerComposeConventionExtensions
    {
        public static IConventionHostBuilder UseDevelopmentDockerCompose(this IConventionHostBuilder builder, string composeFile, Action<DockerComposeConvention> options = null)
        {
            if (Helpers.DoesAssemblyLookDebugLike(Assembly.GetCallingAssembly()) || Helpers.DoesAssemblyLookDebugLike(Assembly.GetEntryAssembly()))
            {
                var convention = DockerComposeConvention.For(composeFile);
                options?.Invoke(convention);
                builder.Scanner.PrependConvention(convention);
            }
            return builder;
        }
    }


}
