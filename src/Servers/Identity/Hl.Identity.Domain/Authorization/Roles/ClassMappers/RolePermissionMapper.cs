using Hl.Core.ClassMapper;


namespace Hl.Identity.Domain.Authorization.Roles.ClassMappers
{
    public class RolePermissionMapper : HlClassMapper<RolePermission>
    {
        public RolePermissionMapper()
        {
            Table("auth_role_permission");
            AutoMap();
        }
    }
}
