
using Hl.Core.ClassMapper;
using Hl.Identity.Domain.Authorization.Roles;

namespace Hl.Identity.Domain.Authorization.Users.ClassMappers
{
    public class RoleMapper : HlClassMapper<Role>
    {
        public RoleMapper()
        {
            Table("auth_role");
            AutoMap();
        }
    }
}
