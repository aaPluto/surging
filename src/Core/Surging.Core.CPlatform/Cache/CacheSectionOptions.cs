namespace Surging.Core.CPlatform.Cache
{
    public class CacheSectionOptions
    {
        public bool IsEnableRepositoryCache { get; set; }

        public string CacheSectionName { get; set; } = "ddlCache";

        public CacheTargetType CacheType { get; set; } = CacheTargetType.Redis;
    }
}
