using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Surging.Core.ServiceHosting.Internal;
using Surging.Core.ServiceHosting.Internal.Implementation;
using Surging.Core.ServiceHosting.Startup;
using Surging.Core.ServiceHosting.Startup.Implementation;
using System;
using System.Reflection;

namespace Surging.Core.ServiceHosting
{
    public static class ServiceHostBuilderExtensions
    {
        public static IServiceHostBuilder UseStartup(this IServiceHostBuilder hostBuilder, Type startupType)
        {
            return hostBuilder
                .ConfigureServices(services =>
                {
                    if (typeof(IStartup).GetTypeInfo().IsAssignableFrom(startupType.GetTypeInfo()))
                    {
                        services.AddSingleton(typeof(IStartup), startupType);
                    }
                    else
                    {
                        services.AddSingleton(typeof(IStartup), sp =>
                        {
                            var config = sp.GetService<IConfigurationBuilder>();
                            return new ConventionBasedStartup(StartupLoader.LoadMethods(sp, config, startupType, ""));
                        });
                    }
                });
        }

        public static IServiceHostBuilder UseStartup<TStartup>(this IServiceHostBuilder hostBuilder) where TStartup : class
        {
            return hostBuilder.UseStartup(typeof(TStartup));
        }

        public static IServiceHostBuilder UseConsoleLifetime(this IServiceHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((collection) =>
            {
                collection.AddSingleton<IApplicationLifetime, ApplicationLifetime>();
                collection.AddSingleton<IHostLifetime, ConsoleLifetime>();
            });
        }
    }
}