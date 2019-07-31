using Hl.Identity.Domain.Authorization.Users;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Roles
{
    public class RoleManager : ManagerBase, IRoleManager
    {
        private readonly IDapperRepository<Role, long> _roleRepository;
        private readonly IDapperRepository<UserRole, long> _userRoleRepository;
        private readonly IDapperRepository<RolePermission, long> _rolePermissionRepository;

        public RoleManager(IDapperRepository<Role, long> roleRepository,
            IDapperRepository<UserRole, long> userRoleRepository,
            IDapperRepository<RolePermission, long> rolePermissionRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task DeleteRoleById(long roleId)
        {
            var roleUserCount = await _userRoleRepository.GetCountAsync(p=>p.RoleId == roleId);
            if (roleUserCount > 0)
            {
                throw new BusinessException("该角色被分配有用户,请先删除用户后再尝试");
            }
            await UnitOfWorkAsync(async (conn, trans) => {
                await _roleRepository.DeleteAsync(p => p.Id == roleId, conn, trans);
                await _rolePermissionRepository.DeleteAsync(p => p.RoleId == roleId, conn, trans);
            }, Connection);
        }
    }
}
