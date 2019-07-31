using AutoMapper;
using Hl.Identity.Domain.Authorization.Roles;

namespace Hl.Identity.IApplication.Roles.Dtos
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleInput, Role>();
            CreateMap<UpdateRoleInput, Role>();
            CreateMap<Role,GetRoleOutput>();

        }
    }
}
