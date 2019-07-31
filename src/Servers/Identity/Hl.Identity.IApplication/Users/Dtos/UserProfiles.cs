using AutoMapper;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.IApplication.Users.Dtos;

namespace Hl.Identity.IApplication.Employees.Dtos
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<CreateUserInput, UserInfo>();
            CreateMap<UpdateUserInput, UserInfo>();
            CreateMap<UserInfo, GetUserOutput>();
        }
    }
}
