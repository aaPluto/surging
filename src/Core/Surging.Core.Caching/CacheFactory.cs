using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Exceptions;
using CPlatformAppConfig = Surging.Core.CPlatform.AppConfig;

namespace Surging.Core.Caching
{
    public static class CacheFactory
    {
        private static CacheTargetType cacheType = CacheTargetType.Redis;
        private static string cacheSectionName = "ddlCache";
        static CacheFactory()
        {
            if (!string.IsNullOrEmpty(CPlatformAppConfig.CacheSectionOptions.CacheSectionName))
            {
                cacheSectionName = CPlatformAppConfig.CacheSectionOptions.CacheSectionName;
            }
            if (CPlatformAppConfig.CacheSectionOptions.CacheType != cacheType)
            {
                cacheType = CPlatformAppConfig.CacheSectionOptions.CacheType;
            }
        }

        public static ICacheProvider CreateCacheProvider()
        {
            ICacheProvider _cacheProvider;
            switch (cacheType)
            {
                case CacheTargetType.Redis:
                    _cacheProvider = CacheContainer.GetService<ICacheProvider>($"{cacheSectionName}.{cacheType.ToString()}");
                    break;
                default:
                    throw new CPlatformException("暂时只支持redis缓存类型", null);

            }
            return _cacheProvider;
        }

    }
}
