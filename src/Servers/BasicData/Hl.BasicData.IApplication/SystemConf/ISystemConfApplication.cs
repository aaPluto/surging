using Hl.BasicData.Common.SystemConf;
using Hl.BasicData.Domain;
using Hl.Core.Maintenance;
using Surging.Core.Caching;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.System.Intercept;
using System.Threading.Tasks;

namespace Hl.BasicData.IApplication
{
    [ServiceBundle("v1/sysconf/{service}")]
    public interface ISystemConfApplication : IServiceKey
    {
        [Service(Director = Maintainer.Liuhll, Date = "2019-05-02", Name = "通过配置配置名称获取系统配置项")]
        Task<GetSystemConfOutput> GetSysConfByName(string confName);
    }
}
