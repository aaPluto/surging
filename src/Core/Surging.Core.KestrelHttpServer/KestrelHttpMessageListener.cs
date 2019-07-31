using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surging.Core.CPlatform.Engines;
using Surging.Core.CPlatform.Jwt;
using Surging.Core.CPlatform.Serialization;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.KestrelHttpServer.Internal;
using Surging.Core.KestrelHttpServer.Middlewares;
using Surging.Core.Swagger;
using Surging.Core.Swagger.Builder;
using Surging.Core.Swagger.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Surging.Core.KestrelHttpServer
{
    public class KestrelHttpMessageListener : HttpMessageListener, IDisposable
    {
        private readonly ILogger<KestrelHttpMessageListener> _logger;
        private IWebHost _host;
        private readonly ISerializer<string> _serializer;
        private readonly IServiceSchemaProvider _serviceSchemaProvider;
        private readonly IServiceEngineLifetime _lifetime;
        public KestrelHttpMessageListener(ILogger<KestrelHttpMessageListener> logger,
            ISerializer<string> serializer,
            IServiceSchemaProvider serviceSchemaProvider,
            IServiceEngineLifetime lifetime) : base(logger, serializer)
        {
            _logger = logger;
            _serializer = serializer;
            _serviceSchemaProvider = serviceSchemaProvider;
            _lifetime = lifetime;
        }

        public async Task StartAsync(EndPoint endPoint)
        {
            var ipEndPoint = endPoint as IPEndPoint;
            try
            {
                var hostBuilder = new WebHostBuilder()
                  .UseContentRoot(Directory.GetCurrentDirectory())
                  .UseKestrel(options =>
                  {
                      options.Listen(ipEndPoint);
                  })
                  .ConfigureServices(ConfigureServices)
                  .ConfigureLogging((logger) =>
                  {
                      logger.AddConfiguration(
                             CPlatform.AppConfig.GetSection("Logging"));
                  })
                  .Configure(AppResolve);

                if (Directory.Exists(CPlatform.AppConfig.ServerOptions.WebRootPath))
                    hostBuilder = hostBuilder.UseWebRoot(CPlatform.AppConfig.ServerOptions.WebRootPath);
                _host = hostBuilder.Build();
                _lifetime.ServiceEngineStarted.Register(async () =>
                {
                    await _host.RunAsync();
                });
            }
            catch
            {
                _logger.LogError($"http服务主机启动失败，监听地址：{endPoint}。 ");
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            if (AppConfig.SwaggerOptions != null)
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(AppConfig.SwaggerOptions.Version, AppConfig.SwaggerOptions);
                    var annotationXmlDir = string.Empty;
                    if (!string.IsNullOrEmpty(AppConfig.SwaggerOptions.AnnotationXmlDir))
                    {
                        annotationXmlDir = EnvironmentHelper.GetEnvironmentVariable(AppConfig.SwaggerOptions.AnnotationXmlDir);
                    }
                    var xmlPaths = _serviceSchemaProvider.GetSchemaFilesPath(annotationXmlDir, AppConfig.SwaggerOptions.DtoXmls);
                    foreach (var xmlPath in xmlPaths)
                    {
                        options.IncludeXmlComments(xmlPath);
                    }

                    if (AppConfig.SwaggerOptions.Authorization.EnableAuthorization)
                    {
                        options.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                        {
                            Description = "请输入Jwt类型的Token",
                            Name = "Authorization",
                            In = "header",
                            Type = "apiKey"
                        });
                        options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>() { { "Bearer", Enumerable.Empty<string>() } });
                    }
                });
            }
        }

        private void AppResolve(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc();
            if (AppConfig.SwaggerOptions != null)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/{AppConfig.SwaggerOptions.Version}/swagger.json", AppConfig.SwaggerOptions.Title);
                });
                if (AppConfig.SwaggerOptions.Authorization.EnableAuthorization)
                {
                    app.UseAuthentication();
                }
            }

            app.Run(async (context) =>
            {
                var sender = new HttpServerMessageSender(_serializer, context);
                await OnReceived(sender, context);
            });
        }

        public void Dispose()
        {
            _host.Dispose();
        }
    }
}