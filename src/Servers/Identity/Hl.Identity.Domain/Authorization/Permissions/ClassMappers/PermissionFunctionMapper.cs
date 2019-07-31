using Hl.Core.ClassMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.Permissions.ClassMappers
{
    public class PermissionFunctionMapper : HlClassMapper<PermissionFunction>
    {
        public PermissionFunctionMapper()
        {
            Table("auth_permission_function");
            AutoMap();
        }
    }
}
