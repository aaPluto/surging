﻿using Microsoft.Extensions.Configuration;
using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Configurations;
using Surging.Core.CPlatform.Runtime.Client.Address.Resolvers.Implementation.Selectors.Implementation;
using System;

namespace Surging.Core.CPlatform
{
    public class AppConfig
    {
        #region 字段

        private static AddressSelectorMode _loadBalanceMode = AddressSelectorMode.Polling;
        private static SurgingServerOptions _serverOptions = new SurgingServerOptions();

        #endregion 字段

        internal static IConfigurationRoot Configuration { get; set; }

        public static AddressSelectorMode LoadBalanceMode
        {
            get
            {
                AddressSelectorMode mode = _loadBalanceMode; ;
                if (Configuration != null
                    && Configuration["AccessTokenExpireTimeSpan"] != null
                    && !Enum.TryParse(Configuration["AccessTokenExpireTimeSpan"], out mode))
                {
                    mode = _loadBalanceMode;
                }
                return mode;
            }
            internal set
            {
                _loadBalanceMode = value;
            }
        }

        public static IConfigurationSection GetSection(string name)
        {
            return Configuration?.GetSection(name);
        }

        public static SurgingServerOptions ServerOptions
        {
            get
            {
                return _serverOptions;
            }
            internal set
            {
                _serverOptions = value;
            }
        }

        public static CacheSectionOptions CacheSectionOptions { get; internal set; }
    }
}