using AutoMapper;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.Domain.Shared.Users;

namespace Hl.Identity.IApplication.Authorization.Dtos
{
    public class AccountProfiles : Profile
    {
        public AccountProfiles()
        {
            CreateMap<UserInfo, LoginUserInfo>();
        }
    }
}
