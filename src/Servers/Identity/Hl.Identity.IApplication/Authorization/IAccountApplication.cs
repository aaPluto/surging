using Hl.Core.Maintenance;
using Hl.Identity.Domain.Shared.Users;
using Hl.Identity.IApplication.Authorization.Dtos;
using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hl.Identity.IApplication.Authorization
{
    [ServiceBundle("v1/api/account/{service}")]
    public interface IAccountApplication : IServiceKey
    {
        [Service(Name = "用户登录接口", EnableAuthorization = false, Date = "2018-12-18")]
        Task<LoginResult> Login(LoginInput input);

        [Service(Name = "用户登录接口", Director = Maintainer.Liuhll, Date = "2019-06-26")]
        Task<LoginUserInfo> GetLoginUserInfo();

    }
}
