using Surging.Core.CPlatform.Exceptions;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.UserGroups
{
    public class UserGroupManager : ManagerBase, IUserGroupManager
    {
        private readonly IDapperRepository<UserGroup, long> _userGroupRepository;
        private readonly IDapperRepository<UserGroupRelation, long> _userGroupRelationRepository;
        private readonly IDapperRepository<UserGroupRole, long> _userGroupRoleRepository;

        public UserGroupManager(IDapperRepository<UserGroup, long> userGroupRepository,
            IDapperRepository<UserGroupRelation, long> userGroupRelationRepository,
            IDapperRepository<UserGroupRole, long> userGroupRoleRepository)
        {
            _userGroupRelationRepository = userGroupRelationRepository;
            _userGroupRepository = userGroupRepository;
            _userGroupRoleRepository = userGroupRoleRepository;
        }

        public async Task DeleteUserGroupById(long userGroupId)
        {
            var userGroupchildrenCount = await _userGroupRepository.GetCountAsync(p =>p.ParentId == userGroupId);
            if (userGroupchildrenCount > 0)
            {
                throw new BusinessException("要删除的用户组存在子节点,请先删除子用户组");
            }
            var deleteUserGroupRoleCount = await _userGroupRoleRepository.GetCountAsync(p => p.UserGroupId == userGroupId);
            if (deleteUserGroupRoleCount > 0)
            {
                throw new BusinessException("要删除的用户组分配了角色,无法删除,请先取消角色授权");
            }
            await UnitOfWorkAsync(async (conn, trans) => {
                await _userGroupRepository.DeleteAsync(p => p.Id == userGroupId, conn, trans);
                await _userGroupRelationRepository.DeleteAsync(p => p.UserGroupId == userGroupId,conn,trans);
            },Connection);
        }
    }
}
