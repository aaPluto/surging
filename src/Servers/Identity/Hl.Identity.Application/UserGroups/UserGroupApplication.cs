using Hl.Core.Commons.Dtos;
using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.UserGroups;
using Hl.Identity.IApplication.UserGroups;
using Hl.Identity.IApplication.UserGroups.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hl.Identity.Application.UserGroups
{
    [ModuleName(ApiConsts.Identity.ServiceKey, Version = "v1")]
    public class UserGroupApplication : ProxyServiceBase, IUserGroupApplication
    {
        private readonly IDapperRepository<UserGroup, long> _userGroupRepository;
        private readonly IUserGroupManager _userGroupManager;
        public UserGroupApplication(IDapperRepository<UserGroup, long> userGroupRepository,
            IUserGroupManager userGroupManager)
        {
            _userGroupRepository = userGroupRepository;
            _userGroupManager = userGroupManager;
        }
        public async Task<string> Create(CreateUserGroupInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            if (input.ParentId != 0)
            {
                var parentUserGroup = await _userGroupRepository.SingleOrDefaultAsync(p => p.Id == input.ParentId);
                if (parentUserGroup == null)
                {
                    throw new BusinessException($"不存在父Id为{input.ParentId}的用户组");
                }
            }
            var existUserGroup = await _userGroupRepository.FirstOrDefaultAsync(p => p.GroupCode == input.GroupCode);
            if (existUserGroup != null)
            {
                throw new BusinessException($"已经存在{input.GroupCode}的用户组");
            }
            var userGroupEntity = input.MapTo<UserGroup>();
            await _userGroupRepository.InsertAsync(userGroupEntity);
            return "新增用户组成功";
        }

        public async Task<string> Update(UpdateUserGroupInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var userGroup = await _userGroupRepository.SingleOrDefaultAsync(p => p.Id == input.Id);
            if (userGroup == null)
            {
                throw new BusinessException($"不存在Id为{input.Id}的用户组");
            }      
            userGroup = input.MapTo(userGroup);
            await _userGroupRepository.UpdateAsync(userGroup);
            return "更新用户组成功";
        }

        public async Task<string> Delete(DeleteByIdInput input)
        {
            await _userGroupManager.DeleteUserGroupById(input.Id);
            return "删除用户组成功";
        }

        public async Task<ICollection<GetUserGroupOutput>> GetAll()
        {
            var topUserGroups = await _userGroupRepository.GetAllAsync(p => p.ParentId == 0);
            var topUserGroupOutputs = topUserGroups.MapTo<ICollection<GetUserGroupOutput>>();
            var allUserGroups = await _userGroupRepository.GetAllAsync();
            foreach (var userGroupOutput in topUserGroupOutputs)
            {
                userGroupOutput.Children = await GetUserGroupChildren(userGroupOutput.Id, allUserGroups);
            }
            return topUserGroupOutputs;
        }

        private async Task<ICollection<GetUserGroupOutput>> GetUserGroupChildren(long userGroupId,IEnumerable<UserGroup> allUserGroups)
        {
            var userGroupChildren = allUserGroups.Where(p => p.ParentId == userGroupId);
            var userGroupChildrenOutputs = userGroupChildren.MapTo<ICollection<GetUserGroupOutput>>();
            foreach (var userGroupOutput in userGroupChildrenOutputs)
            {
                userGroupOutput.Children = await GetUserGroupChildren(userGroupOutput.Id, allUserGroups);
            }
            return userGroupChildrenOutputs;
        }

    }
}
