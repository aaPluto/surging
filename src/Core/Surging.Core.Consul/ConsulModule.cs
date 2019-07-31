﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surging.Core.Consul.Configurations;
using Surging.Core.Consul.WatcherProvider;
using Surging.Core.Consul.WatcherProvider.Implementation;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Module;
using Surging.Core.CPlatform.Mqtt;
using Surging.Core.CPlatform.Routing;
using Surging.Core.CPlatform.Runtime.Client;
using Surging.Core.CPlatform.Runtime.Server;
using Surging.Core.CPlatform.Serialization;
using Surging.Core.CPlatform.Support;
using System;

namespace Surging.Core.Consul
{
    public class ConsulModule : EnginePartModule
    {
        public override void Initialize(CPlatformContainer serviceProvider)
        {
            base.Initialize(serviceProvider);
        }

        /// <summary>
        /// Inject dependent third-party components
        /// </summary>
        /// <param name="builder"></param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
            var configInfo = new ConfigInfo(null);
            UseConsulRouteManager(builder, configInfo)
               .UseConsulServiceSubscribeManager(builder, configInfo)
              .UseConsulCommandManager(builder, configInfo)
              .UseConsulCacheManager(builder, configInfo)
              .UseConsulWatch(builder, configInfo)
              .UseConsulMqttRouteManager(builder, configInfo);
        }

        public ConsulModule UseConsulRouteManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseRouteManager(builder, provider =>
           new ConsulServiceRouteManager(
               GetConfigInfo(configInfo),
            provider.GetRequiredService<ISerializer<byte[]>>(),
              provider.GetRequiredService<ISerializer<string>>(),
              provider.GetRequiredService<IClientWatchManager>(),
              provider.GetRequiredService<IServiceRouteFactory>(),
              provider.GetRequiredService<ILogger<ConsulServiceRouteManager>>(),
               provider.GetRequiredService<IServiceHeartbeatManager>()));
            return this;
        }

        public ConsulModule UseConsulCacheManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseCacheManager(builder, provider =>
          new ConsulServiceCacheManager(
              GetConfigInfo(configInfo),
           provider.GetRequiredService<ISerializer<byte[]>>(),
             provider.GetRequiredService<ISerializer<string>>(),
             provider.GetRequiredService<IClientWatchManager>(),
             provider.GetRequiredService<IServiceCacheFactory>(),
             provider.GetRequiredService<ILogger<ConsulServiceCacheManager>>()));
            return this;
        }

        /// <summary>
        /// 设置服务命令管理者。
        /// </summary>
        /// <param name="builder">Rpc服务构建者。</param>
        /// <param name="configInfo">ZooKeeper设置信息。</param>
        /// <returns>服务构建者。</returns>
        public ConsulModule UseConsulCommandManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseCommandManager(builder, provider =>
          {
              var result = new ConsulServiceCommandManager(
                   GetConfigInfo(configInfo),
                provider.GetRequiredService<ISerializer<byte[]>>(),
                  provider.GetRequiredService<ISerializer<string>>(),
                  provider.GetRequiredService<IServiceRouteManager>(),
                  provider.GetRequiredService<IClientWatchManager>(),
                  provider.GetRequiredService<IServiceEntryManager>(),
                  provider.GetRequiredService<ILogger<ConsulServiceCommandManager>>(),
                    provider.GetRequiredService<IServiceHeartbeatManager>());
              return result;
          });
            return this;
        }

        public ConsulModule UseConsulServiceSubscribeManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseSubscribeManager(builder, provider =>
          {
              var result = new ConsulServiceSubscribeManager(
                  GetConfigInfo(configInfo),
                  provider.GetRequiredService<ISerializer<byte[]>>(),
                  provider.GetRequiredService<ISerializer<string>>(),
                  provider.GetRequiredService<IClientWatchManager>(),
                  provider.GetRequiredService<IServiceSubscriberFactory>(),
                  provider.GetRequiredService<ILogger<ConsulServiceSubscribeManager>>());
              return result;
          });
            return this;
        }

        public ConsulModule UseConsulMqttRouteManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseMqttRouteManager(builder, provider =>
           new ConsulMqttServiceRouteManager(
               GetConfigInfo(configInfo),
            provider.GetRequiredService<ISerializer<byte[]>>(),
              provider.GetRequiredService<ISerializer<string>>(),
              provider.GetRequiredService<IClientWatchManager>(),
              provider.GetRequiredService<IMqttServiceFactory>(),
              provider.GetRequiredService<ILogger<ConsulMqttServiceRouteManager>>(),
              provider.GetRequiredService<IServiceHeartbeatManager>()));
            return this;
        }

        /// <summary>
        /// 设置使用基于Consul的Watch机制
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public ConsulModule UseConsulWatch(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            builder.Register(provider =>
            {
                return new ClientWatchManager(configInfo);
            }).As<IClientWatchManager>().SingleInstance();
            return this;
        }

        public ContainerBuilderWrapper UseSubscribeManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IServiceSubscribeManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        public ContainerBuilderWrapper UseCommandManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IServiceCommandManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        public ContainerBuilderWrapper UseCacheManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IServiceCacheManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        public ContainerBuilderWrapper UseRouteManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IServiceRouteManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        public ContainerBuilderWrapper UseMqttRouteManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IMqttServiceRouteManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        private ConfigInfo GetConfigInfo(ConfigInfo config)
        {
            ConsulOption option = null;
            var section = CPlatform.AppConfig.GetSection("Consul");
            if (section.Exists())
                option = section.Get<ConsulOption>();
            else if (AppConfig.Configuration != null)
                option = AppConfig.Configuration.Get<ConsulOption>();
            if (option != null)
            {
                var sessionTimeout = config.SessionTimeout.TotalSeconds;
                Double.TryParse(option.SessionTimeout, out sessionTimeout);
                config = new ConfigInfo(
                   option.ConnectionString,
                    TimeSpan.FromSeconds(sessionTimeout),
                    option.RoutePath ?? config.RoutePath,
                    option.SubscriberPath ?? config.SubscriberPath,
                    option.CommandPath ?? config.CommandPath,
                    option.CachePath ?? config.CachePath,
                    option.MqttRoutePath ?? config.MqttRoutePath,
                   option.ReloadOnChange != null ? bool.Parse(option.ReloadOnChange) :
                    config.ReloadOnChange,
                    option.EnableChildrenMonitor != null ? bool.Parse(option.EnableChildrenMonitor) :
                    config.EnableChildrenMonitor
                   );
            }
            return config;
        }
    }
}