using AutoMapper;
using Hl.Identity.Domain.Authorization.UserGroups;

namespace Hl.Identity.IApplication.UserGroups.Dtos
{
    public class UserGroupProfile : Profile
    {
        public UserGroupProfile()
        {
            CreateMap<CreateUserGroupInput, UserGroup>();
            CreateMap<UpdateUserGroupInput, UserGroup>();
            CreateMap<UserGroup, GetUserGroupOutput>().ForMember(s => s.Children, opt => opt.Ignore());
        }
    }
}
