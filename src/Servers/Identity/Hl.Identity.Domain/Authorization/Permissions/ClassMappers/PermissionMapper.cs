using Hl.Core.ClassMapper;

namespace Hl.Identity.Domain.Authorization.Permissions.ClassMappers
{
    public class PermissionMapper : HlClassMapper<Permission>
    {
        public PermissionMapper()
        {
            Table("auth_permission");
            AutoMap();
        }
    }
}
