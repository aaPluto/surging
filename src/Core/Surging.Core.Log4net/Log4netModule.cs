﻿using Microsoft.Extensions.Logging;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Module;
using Surging.Core.CPlatform.Utilities;

namespace Surging.Core.Log4net
{
    public class Log4netModule : EnginePartModule
    {
        private string log4NetConfigFile = "${LogConfigPath}|log4net.config";

        public override void Initialize(CPlatformContainer serviceProvider)
        {
            base.Initialize(serviceProvider);
            var section = CPlatform.AppConfig.GetSection("Logging");
            log4NetConfigFile = EnvironmentHelper.GetEnvironmentVariable(log4NetConfigFile);
            serviceProvider.GetInstances<ILoggerFactory>().AddProvider(new Log4NetProvider(log4NetConfigFile));
        }

        /// <summary>
        /// Inject dependent third-party components
        /// </summary>
        /// <param name="builder"></param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
        }
    }
}