using System;
using System.Reflection;
using Rocket.Surgery.Conventions;

namespace Rocket.Surgery.Automation.Azurite
{
    public static class AzureiteConventionExtensions
    {
        public static IConventionHostBuilder UseDevelopmentAzurite(this IConventionHostBuilder builder, int port, Action<AzureiteConvention> options = null, params string[] keys) 
        {
            if (Helpers.DoesAssemblyLookDebugLike(Assembly.GetCallingAssembly()) || Helpers.DoesAssemblyLookDebugLike(Assembly.GetEntryAssembly()))
            {
                var convention = AzureiteConvention.For(port, keys);
                options?.Invoke(convention);
                builder.Scanner.AppendConvention(convention);
            }
            return builder;
        }
    }


}
