using Surging.Core.CPlatform.Ioc;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Roles
{
    public interface IRoleManager : ITransientDependency
    {
        Task DeleteRoleById(long roleId);
    }
}
