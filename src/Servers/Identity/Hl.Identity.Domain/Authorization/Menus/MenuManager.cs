using Hl.Identity.Domain.Authorization.Permissions;
using Hl.Identity.Domain.Authorization.Roles;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Menus
{
    public class MenuManager : ManagerBase, IMenuManager
    {
        private readonly IDapperRepository<Menu, long> _menuRepository;
        private readonly IDapperRepository<Permission, long> _permissionRepository;
        private readonly IDapperRepository<RolePermission, long> _rolePermissionRepository;

        public MenuManager(IDapperRepository<Menu, long> menuRepository,
            IDapperRepository<Permission, long> permissionRepository,
            IDapperRepository<RolePermission, long> rolePermissionRepository)
        {
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task CreateMenu(Menu menu, Permission permission)
        {
            await UnitOfWorkAsync(async (conn, trans) => {              
                var permissionId = await _permissionRepository.InsertAndGetIdAsync(permission, conn, trans);
                menu.PermissionId = permissionId;
                await _menuRepository.InsertAsync(menu, conn, trans);
            },Connection);
        }

        public async Task DeleteMenu(long id)
        {
            var menu = await _menuRepository.GetAsync(id);
            var childrenMenus = await _menuRepository.GetAllAsync(p => p.ParentId == id);
            if (childrenMenus.Any())
            {
                throw new BusinessException("存在子菜单,请先删除子菜单");
            }
            var rolePermissions = await _rolePermissionRepository.GetAllAsync(p => p.PerssionId == menu.PermissionId);
            if (rolePermissions.Any())
            {
                throw new BusinessException("该菜单被分配给角色,请先删除关系后再尝试");
            }
            await UnitOfWorkAsync(async (conn, trans) => {
                await _permissionRepository.DeleteAsync(p=>p.Id == menu.PermissionId, conn, trans);
                await _menuRepository.DeleteAsync(menu, conn, trans);
            }, Connection);
        }

        public async Task UpdateMenu(Menu menu, Permission permission)
        {
            await UnitOfWorkAsync(async (conn, trans) => {
                await _permissionRepository.UpdateAsync(permission, conn, trans);
                await _menuRepository.UpdateAsync(menu, conn, trans);
            }, Connection);
        }
    }
}
