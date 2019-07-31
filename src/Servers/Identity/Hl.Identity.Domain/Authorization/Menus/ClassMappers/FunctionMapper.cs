using Hl.Core.ClassMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.Menus.ClassMappers
{
    public class FunctionMapper : HlClassMapper<Function>
    {
        public FunctionMapper()
        {
            Table("auth_function");
            AutoMap();
        }
    }
}
