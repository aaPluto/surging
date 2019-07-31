using Hl.Identity.Domain.Authorization.Permissions;
using Surging.Core.CPlatform.Ioc;
using System;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Menus
{
    public interface IMenuManager : ITransientDependency
    {
        Task CreateMenu(Menu menu, Permission permission);
        Task UpdateMenu(Menu menu, Permission permission);
        Task DeleteMenu(long id);
    }
}
