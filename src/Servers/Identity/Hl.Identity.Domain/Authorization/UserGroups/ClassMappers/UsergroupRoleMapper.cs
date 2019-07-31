using Hl.Core.ClassMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.UserGroups.ClassMappers
{
    public class UserGroupRoleMapper : HlClassMapper<UserGroupRole>
    {
        public UserGroupRoleMapper()
        {
            Table("auth_usergroup_role");
            AutoMap();
        }
    }
}
