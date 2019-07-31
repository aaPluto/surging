using Hl.BasicData.Common.HlDictionary;
using Hl.BasicData.Domain;
using Hl.BasicData.IApplication.Dictionary.Dtos;
using Hl.Core.Maintenance;
using Surging.Core.Caching;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.System.Intercept;
using System;
using System.Threading.Tasks;

namespace Hl.BasicData.IApplication
{
    [ServiceBundle("api/dict/{service}")]
    public interface IDictionaryApplication : IServiceKey
    {
        [Service(Director = Maintainer.Liuhll, Date = "2019-05-01", Name = "新增字典值")]
        Task<string> CreateDict(CreateDictInput input);

        [Service(Director = Maintainer.Liuhll, Date = "2019-05-01", Name = "通过dictkey获取字典值")]
        Task<HlDictionaryOutput> GetDictValByKey(string dictKey);

    }
}
