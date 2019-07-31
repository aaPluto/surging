using Surging.Core.CPlatform.Ioc;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Authorization.Users
{
    public interface IUserManager : ITransientDependency
    {
        Task DeleteByUserId(long userId);
        Task CreateUserInfo(UserInfo userInfo);
        Task<UserInfo> GetUserInfoByUserId(long userId);
    }
}
