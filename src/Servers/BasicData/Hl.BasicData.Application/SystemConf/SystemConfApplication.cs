using System.Threading.Tasks;
using Hl.BasicData.Common.SystemConf;
using Hl.BasicData.Domain;
using Hl.BasicData.IApplication;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Extensions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;

namespace Hl.BasicData.Application
{
    [ModuleName("basicdata.v1",Version = "v1")]
    public class SystemConfApplication : ProxyServiceBase, ISystemConfApplication
    {
        public async Task<GetSystemConfOutput> GetSysConfByName(string confName)
        {
            if (confName.IsNullOrEmpty())
            {
                throw new ValidateException("配置项名称不允许为空");
            }
            var sysConf = await GetService<IDapperRepository<SystemConf, long>>().SingleOrDefaultAsync(p=>p.ConfigName == confName);
            if (sysConf == null)
            {
                throw new BusinessException($"不存在{confName}的配置项");
            }
            return sysConf.MapTo<GetSystemConfOutput>();
        }
    }
}
