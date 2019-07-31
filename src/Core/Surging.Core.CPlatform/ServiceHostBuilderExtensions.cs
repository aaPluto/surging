﻿using Autofac;
using Microsoft.Extensions.Configuration;
using Surging.Core.CPlatform.Address;
using Surging.Core.CPlatform.Configurations;
using Surging.Core.CPlatform.Engines;
using Surging.Core.CPlatform.Module;
using Surging.Core.CPlatform.Routing;
using Surging.Core.CPlatform.Runtime.Client;
using Surging.Core.CPlatform.Runtime.Server;
using Surging.Core.CPlatform.Support;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.ServiceHosting.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Surging.Core.CPlatform
{
    public static class ServiceHostBuilderExtensions
    {
        public static IServiceHostBuilder UseServer(this IServiceHostBuilder hostBuilder, string ip, int port, string token = "True")
        {
            return hostBuilder.MapServices(mapper =>
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", AppConfig.ServerOptions.Environment.ToString());
                BuildServiceEngine(mapper);
                mapper.Resolve<IServiceCommandManager>().SetServiceCommandsAsync();
                string serviceToken = mapper.Resolve<IServiceTokenGenerator>().GeneratorToken(token);
                int _port = AppConfig.ServerOptions.Port = AppConfig.ServerOptions.Port == 0 ? port : AppConfig.ServerOptions.Port;
                string _ip = AppConfig.ServerOptions.Ip = AppConfig.ServerOptions.Ip ?? ip;
                _port = AppConfig.ServerOptions.Port = AppConfig.ServerOptions.IpEndpoint?.Port ?? _port;
                _ip = AppConfig.ServerOptions.Ip = AppConfig.ServerOptions.IpEndpoint?.Address.ToString() ?? _ip;
                _ip = NetUtils.GetHostAddress(_ip);

                ConfigureRoute(mapper, serviceToken);
                mapper.Resolve<IModuleProvider>().Initialize();
                var serviceHosts = mapper.Resolve<IList<Runtime.Server.IServiceHost>>();
                Task.Factory.StartNew(async () =>
                {
                    foreach (var serviceHost in serviceHosts)
                        await serviceHost.StartAsync(_ip, _port);
                    mapper.Resolve<IServiceEngineLifetime>().NotifyStarted();
                }).Wait();
            });
        }

        public static IServiceHostBuilder UseServer(this IServiceHostBuilder hostBuilder, Action<SurgingServerOptions> options)
        {
            var serverOptions = new SurgingServerOptions();
            options.Invoke(serverOptions);
            AppConfig.ServerOptions = serverOptions;
            return hostBuilder.UseServer(serverOptions.Ip, serverOptions.Port, serverOptions.Token);
        }

        public static IServiceHostBuilder UseClient(this IServiceHostBuilder hostBuilder)
        {
            return hostBuilder.MapServices(mapper =>
            {
                var serviceEntryManager = mapper.Resolve<IServiceEntryManager>();
                var addressDescriptors = serviceEntryManager.GetEntries().Select(i =>
                {
                    i.Descriptor.Metadatas = null;
                    return new ServiceSubscriber
                    {
                        Address = new[] { new IpAddressModel {
                     Ip = Dns.GetHostEntry(Dns.GetHostName())
                 .AddressList.FirstOrDefault<IPAddress>
                 (a => a.AddressFamily.ToString().Equals("InterNetwork")).ToString() } },
                        ServiceDescriptor = i.Descriptor
                    };
                }).ToList();
                mapper.Resolve<IServiceSubscribeManager>().SetSubscribersAsync(addressDescriptors);
                mapper.Resolve<IModuleProvider>().Initialize();
            });
        }

        public static void BuildServiceEngine(IContainer container)
        {
            if (container.IsRegistered<IServiceEngine>())
            {
                var builder = new ContainerBuilder();

                container.Resolve<IServiceEngineBuilder>().Build(builder);
                var configBuilder = container.Resolve<IConfigurationBuilder>();
                var appSettingPath = Path.Combine(AppConfig.ServerOptions.RootPath, "appsettings.json");
                configBuilder.AddCPlatformFile("${appsettingspath}|" + appSettingPath, optional: false, reloadOnChange: true);
                builder.Update(container);
            }
        }

        public static void ConfigureRoute(IContainer mapper, string serviceToken)
        {
            if (AppConfig.ServerOptions.Protocol == CommunicationProtocol.Tcp ||
             AppConfig.ServerOptions.Protocol == CommunicationProtocol.None)
            {
                var routeProvider = mapper.Resolve<IServiceRouteProvider>();
                if (AppConfig.ServerOptions.EnableRouteWatch)
                    new ServiceRouteWatch(mapper.Resolve<CPlatformContainer>(),
                        () => routeProvider.RegisterRoutes(
                        Math.Round(Convert.ToDecimal(Process.GetCurrentProcess().TotalProcessorTime.TotalSeconds), 2, MidpointRounding.AwayFromZero)));
                else
                    routeProvider.RegisterRoutes(0);
            }
        }
    }
}