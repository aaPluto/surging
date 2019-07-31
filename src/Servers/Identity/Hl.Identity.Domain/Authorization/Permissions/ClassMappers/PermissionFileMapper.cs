using Hl.Core.ClassMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.Permissions.ClassMappers
{
    public class PermissionFileMapper : HlClassMapper<PermissionFile>
    {
        public PermissionFileMapper()
        {
            Table("auth_permission_file");
            AutoMap();
        }
    }
}
