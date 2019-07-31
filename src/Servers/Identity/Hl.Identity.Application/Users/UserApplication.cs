using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.Core.Commons.Dtos;
using Hl.Core.ServiceApi;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.IApplication.Employees;
using Hl.Identity.IApplication.Users.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.Dapper.Repositories;
using Surging.Core.Domain.PagedAndSorted;
using Surging.Core.Domain.PagedAndSorted.Extensions;
using Surging.Core.ProxyGenerator;


namespace Hl.Identity.Application.Employees
{
    [ModuleName(ApiConsts.Identity.ServiceKey, Version = "v1")]
    public class UserApplication : ProxyServiceBase, IUserApplication
    {

        private readonly IDapperRepository<UserInfo, long> _userRepository;
        private readonly IUserManager _userManager;
        public UserApplication(
            IDapperRepository<UserInfo, long> userRepository,
            IUserManager userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<string> Create(CreateUserInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var exsitUserInfo = await _userRepository.FirstOrDefaultAsync(p => p.UserName == input.UserName
            || p.Email == input.Email
            || p.Phone == input.Phone);
            if (exsitUserInfo != null)
            {
                throw new BusinessException("已经存在该员工信息,请检查员工账号信息");
            }
            var userInfo = input.MapTo<UserInfo>();

            await _userManager.CreateUserInfo(userInfo);
            return "新增员工成功";
        }

        public async Task<string> Delete(DeleteByIdInput input)
        {
            await _userManager.DeleteByUserId(input.Id);
            return "删除员工成功";
        }

        public async Task<string> Update(UpdateUserInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();

            var userInfo = await _userRepository.SingleOrDefaultAsync(p => p.Id == input.Id);
            if (userInfo == null)
            {
                throw new BusinessException($"不存在Id为{input.Id}的用户信息");
            }
            if (input.Email != userInfo.Email)
            {

                var exsitUser = await _userRepository.FirstOrDefaultAsync(p => p.Email == input.Email);
                if (exsitUser != null)
                {
                    throw new BusinessException($"系统中已经存在{input.Email}的用户信息");
                }
            }
            if (input.Phone != userInfo.Phone)
            {
                var exsitUser = await _userRepository.FirstOrDefaultAsync(p => p.Phone == input.Phone);
                if (exsitUser != null)
                {
                    throw new BusinessException($"系统中已经存在{input.Phone}的用户信息");
                }
            }
            userInfo = input.MapTo(userInfo);
            await _userRepository.UpdateAsync(userInfo);
            return "更新员工信息成功";
        }

        public async Task<IPagedResult<GetUserOutput>> Query(QueryUserInput query)
        {
            var userList = await _userRepository.GetAllAsync(p => p.UserName.Contains(query.UserName)
               && p.ChineseName.Contains(query.ChineseName)
               && p.Email.Contains(query.Email)
               && p.Phone.Contains(query.Phone));
            return userList.MapTo<IEnumerable<GetUserOutput>>().PageBy(query);
        }
    }
}
