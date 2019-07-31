using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.Domain.Shared.Users;
using Hl.Identity.IApplication.Authorization;
using Hl.Identity.IApplication.Authorization.Dtos;
using Hl.Identity.IApplication.Authorization.Validators;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Session;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hl.Identity.Application.Authorization
{
    [ModuleName("v1identity",Version = "v1")]
    public class AccountApplication : ProxyServiceBase, IAccountApplication
    {
        private readonly IUserManager _userManager;
        private readonly LoginManager _loginManager;


        public AccountApplication(IUserManager userManager,
            LoginManager loginManager)
        {
            _userManager = userManager;
            _loginManager = loginManager;
        }


        public async Task<LoginResult> Login(LoginInput input)
        {
            LoginResult loginResult = null;
            try
            {
                loginResult = new LoginResult()
                {
                    ResultType = LoginResultType.Success,
                    PayLoad = await _loginManager.Login(input.UserName, input.Password)
                };

            }
            catch (AuthException ex)
            {
                loginResult = new LoginResult()
                {
                    ResultType = LoginResultType.Fail,
                    ErrorMessage = ex.GetExceptionMessage()
                };
            }
            catch (Exception ex)
            {
                loginResult = new LoginResult()
                {
                    ResultType = LoginResultType.Error,
                    ErrorMessage = ex.GetExceptionMessage()
                };
            }

            return loginResult;
        }

        public async Task<LoginUserInfo> GetLoginUserInfo()
        {
            var session = NullSurgingSession.Instance;
            if (session == null)
            {
                throw new UserFriendlyException("获取登录用户信息失败,您还未登录系统");
            }
            var loginUserInfo = await _userManager.GetUserInfoByUserId(session.UserId.Value);
            return session.MapTo<LoginUserInfo>();
        }
    }
}
