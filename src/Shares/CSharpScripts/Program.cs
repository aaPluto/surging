using Autofac;
using Microsoft.Extensions.Logging;
using Surging.Core.Caching.Configurations;
using Surging.Core.Consul.Configurations;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Configurations;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.Dapper;
using Surging.Core.EventBusRabbitMQ.Configurations;
using Surging.Core.ProxyGenerator;
using Surging.Core.ServiceHosting;
using Surging.Core.ServiceHosting.Internal.Implementation;
using Surging.Core.System.Intercept;
using System;

namespace Hl.ServiceHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = new ServiceHostBuilder()
                 .RegisterServices(builder =>
                 {
                     builder.AddMicroService(option =>
                      {
                          option.AddServiceRuntime()
                           .AddClientProxy()
                           .AddRelateServiceRuntime()
                           .AddConfigurationWatch()
                           .AddClientIntercepted(typeof(CacheProviderInterceptor))
                           .AddServiceEngine(typeof(SurgingServiceEngine))
                           ;

                          builder.Register(p => new CPlatformContainer(ServiceLocator.Current));
                      });
                 })
                 .ConfigureLogging(loggging =>
                 {
                     loggging.AddConfiguration(
                         AppConfig.GetSection("Logging"));
                 })
                 .UseServer(options => { })
                 .UseConsoleLifetime()
                 .Configure(build =>
                 {
#if DEBUG
                     build.AddCacheFile("${cachePath}|/app/configs/cacheSettings.json", optional: false, reloadOnChange: true);
                     build.AddCPlatformFile("${surgingPath}|/app/configs/surgingSettings.json", optional: false, reloadOnChange: true);
                     build.AddEventBusFile("${eventBusPath}|/app/configs/eventBusSettings.json", optional: false);
                     build.AddConsulFile("${consulPath}|/app/configs/consul.json", optional: false, reloadOnChange: true);


#else
                    build.AddCacheFile("${cachePath}|configs/cacheSettings.json", optional: false, reloadOnChange: true);                      
                    build.AddCPlatformFile("${surgingPath}|configs/surgingSettings.json", optional: false,reloadOnChange: true);                    
                    build.AddEventBusFile("configs/eventBusSettings.json", optional: false);
                    build.AddConsulFile("configs/consul.json", optional: false, reloadOnChange: true);
#endif
                 })
                 .UseProxy()
                 .UseStartup<Startup>()
                 .Build();

            using (host.Run())
            {
                Console.WriteLine($"����������ɹ���{DateTime.Now}��");
            }
        }
    }
}