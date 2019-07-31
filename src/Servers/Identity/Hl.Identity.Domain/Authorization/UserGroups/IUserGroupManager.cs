using Surging.Core.CPlatform.Ioc;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.UserGroups
{
    public interface IUserGroupManager : ITransientDependency
    {
        Task DeleteUserGroupById(long userGroupId);
    }
}
