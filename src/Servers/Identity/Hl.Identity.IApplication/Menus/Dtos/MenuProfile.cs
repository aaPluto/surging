using AutoMapper;
using Hl.Identity.Domain.Authorization.Menus;
using System;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<CreateMenuInput, Menu>().ForMember(p => p.Memo, opt => opt.Ignore());
            CreateMap<UpdateMenuInput, Menu>().ForMember(p => p.Memo, opt => opt.Ignore());
            CreateMap<CreateFunctionInput, Function>();
            CreateMap<Function, QueryFunctionOutput>();
        }
    }
}
