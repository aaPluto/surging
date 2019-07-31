using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.System.Intercept;
using Surging.Debug.Test1.IApplication.Demo.Dtos;
using System.Threading.Tasks;

namespace Surging.Debug.Test1.IApplication.Demo
{
    [ServiceBundle("v1/api/debug/demo/{service}")]
    public interface IDemoApplication : IServiceKey
    {
        Task<string> GetUserName(QueryUserInput input);

        [InterceptMethod(CachingMethod.Get, Key = "GetUser_id_{0}", Mode = CacheTargetType.Redis)]
        Task<string> GetUserId([CacheKey(1)]string id);

        Task CreatDemo(DemoInput input);

        Task<string> CreateUser();
    }
}
