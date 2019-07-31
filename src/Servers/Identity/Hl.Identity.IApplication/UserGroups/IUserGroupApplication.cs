using Hl.Core.Commons.Dtos;
using Hl.Core.Maintenance;
using Hl.Identity.Domain;
using Hl.Identity.IApplication.UserGroups.Dtos;
using Surging.Core.Caching;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.System.Intercept;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hl.Identity.IApplication.UserGroups
{
    [ServiceBundle("v1/api/usergroup/{service}")]
    public interface IUserGroupApplication : IServiceKey
    {
        /// <summary>
        /// 新增用户组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Service(Name = "新增用户组",Director = Maintainer.Liuhll, Date = "2019-05-14")]
        Task<string> Create(CreateUserGroupInput input);

        /// <summary>
        /// 更新用户组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Service(Name = "更新用户组", Director = Maintainer.Liuhll, Date = "2019-05-14")]
        Task<string> Update(UpdateUserGroupInput input);

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Service(Name = "删除用户组", Director = Maintainer.Liuhll, Date = "2019-05-14")]
        Task<string> Delete(DeleteByIdInput input);

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <returns></returns>
        [Service(Name = "获取用户组", Director = Maintainer.Liuhll, Date = "2019-05-14")]
        Task<ICollection<GetUserGroupOutput>> GetAll();
    }
}
