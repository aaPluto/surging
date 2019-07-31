﻿using Autofac;
using Surging.Core.Caching.Configurations;
using Surging.Core.CPlatform.Cache;
using Surging.Core.ServiceHosting.Internal;
using System.Linq;

namespace Surging.Core.Caching
{
    public static class ServiceHostBuilderExtensions
    {
        public static IServiceHostBuilder UseServiceCache(this IServiceHostBuilder hostBuilder)
        {
            return hostBuilder.MapServices(mapper =>
            {
                var serviceCacheProvider = mapper.Resolve<ICacheNodeProvider>();
                var addressDescriptors = serviceCacheProvider.GetServiceCaches().ToList();
                mapper.Resolve<IServiceCacheManager>().SetCachesAsync(addressDescriptors);
                mapper.Resolve<IConfigurationWatchProvider>();
            });
        }
    }
}