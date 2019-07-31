using Hl.Identity.Domain.Authorization.Menus;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Permissions
{
    public class FunctionManager : ManagerBase, IFunctionManager
    {
        private readonly IDapperRepository<Function, long> _functionRepository;
        private readonly IDapperRepository<PermissionFunction, long> _permissionFunctionRepository;
        private readonly IDapperRepository<Permission, long> _permissionRepository;

        public FunctionManager(IDapperRepository<Function, long> functionRepository,
            IDapperRepository<PermissionFunction, long> permissionFunctionRepository,
            IDapperRepository<Permission, long> permissionRepository)
        {
            _functionRepository = functionRepository;
            _permissionFunctionRepository = permissionFunctionRepository;
            _permissionRepository = permissionRepository;
        }


        public async Task CreateOperation(Permission operation, IEnumerable<long> functionIds)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                var permissionId = await _permissionRepository.InsertAndGetIdAsync(operation, conn, trans);
                foreach (var funcId in functionIds)
                {
                    var permissionFunc = new PermissionFunction() {
                        FunctionId = funcId,
                        PermissionId = permissionId,
                    };
                    await _permissionFunctionRepository.InsertAsync(permissionFunc, conn, trans);
                }
            }, Connection);
        }

        public async Task UpdateOperation(Permission operation, IEnumerable<long> functionIds)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                await _permissionRepository.UpdateAsync(operation, conn, trans);
                await _permissionFunctionRepository.DeleteAsync(p => p.PermissionId == operation.Id, conn, trans);
                foreach (var funcId in functionIds)
                {
                    var permissionFunc = new PermissionFunction()
                    {
                        FunctionId = funcId,
                        PermissionId = operation.Id,
                    };
                    await _permissionFunctionRepository.InsertAsync(permissionFunc, conn, trans);
                }
            }, Connection);
        }
    }
}
