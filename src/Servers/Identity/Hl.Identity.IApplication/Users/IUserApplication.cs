
using Hl.Core.Commons.Dtos;
using Hl.Core.Maintenance;
using Hl.Identity.IApplication.Users.Dtos;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.Domain.PagedAndSorted;
using System.Threading.Tasks;

namespace Hl.Identity.IApplication.Employees
{
    [ServiceBundle("v1/api/user/{service}")]
    public interface IUserApplication : IServiceKey
    {
        /// <summary>
        /// 创建员工信息接口
        /// </summary>
        /// <param name="input">员工信息</param>
        /// <returns></returns>
        [Service(Director = Maintainer.Liuhll, Date = "2019-4-30", Name = "创建员工接口")]
        Task<string> Create(CreateUserInput input);

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Service(Director = Maintainer.Liuhll, Date = "2019-5-16", Name = "修改员工")]
        Task<string> Update(UpdateUserInput input);

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Service(Director = Maintainer.Liuhll, Date = "2019-5-16", Name = "删除员工")]
        Task<string> Delete(DeleteByIdInput input);

        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Service(Director = Maintainer.Liuhll, Date = "2019-5-16", Name = "查询员工列表")]
        Task<IPagedResult<GetUserOutput>> Query(QueryUserInput query);
    }
}
