using Hl.Identity.Domain.Authorization.Menus;
using Surging.Core.CPlatform.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Permissions
{
    public interface IFunctionManager : ITransientDependency
    {
      //  Task CreateFunction(Function function, Permission permission, long menuId);
        Task CreateOperation(Permission operation, IEnumerable<long> functionIds);
        Task UpdateOperation(Permission operation, IEnumerable<long> functionIds);
    }
}
