using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.Core.Commons.Dtos;
using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Roles;
using Hl.Identity.IApplication.Roles;
using Hl.Identity.IApplication.Roles.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.Domain.PagedAndSorted;
using Surging.Core.Domain.PagedAndSorted.Extensions;
using Surging.Core.ProxyGenerator;

namespace Hl.Identity.Application.Roles
{
    [ModuleName(ApiConsts.Identity.ServiceKey, Version = "v1")]
    public class RoleApplication : ProxyServiceBase, IRoleApplication
    {
        private readonly IDapperRepository<Role, long> _roleRepository;
        private readonly IRoleManager _roleManager;

        public RoleApplication(IDapperRepository<Role, long> roleRepository,
            IRoleManager roleManager)
        {
            _roleRepository = roleRepository;
            _roleManager = roleManager;
        }

        public async Task<string> Create(CreateRoleInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var exsitRole = await _roleRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
            if (exsitRole != null)
            {
                throw new UserFriendlyException($"已经存在{input.Code}的角色信息");
            }
            await _roleRepository.InsertAsync(input.MapTo<Role>());
            return "新增角色成功";
        }


        public async Task<string> Update(UpdateRoleInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var role = await _roleRepository.SingleOrDefaultAsync(p => p.Id == input.Id);
            if (role == null)
            {
                throw new UserFriendlyException($"不存在{input.Id}的角色信息");
            }
            role = input.MapTo(role);
            await _roleRepository.UpdateAsync(role);
            return "更新角色成功";
        }

        public async Task<string> Delete(DeleteByIdInput input)
        {
            await _roleManager.DeleteRoleById(input.Id);
            return "删除角色成功";
        }

        public async Task<IPagedResult<GetRoleOutput>> Query(QueryRoleInput input)
        {  
            var queryResult = await _roleRepository.GetAllAsync(p => p.Code.Contains(input.Code) && p.Name.Contains(input.Name));
            return queryResult.MapTo<IEnumerable<GetRoleOutput>>().PageBy(input);
        }
    }
}
