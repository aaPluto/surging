using Hl.Core.ClassMapper;

namespace Hl.Identity.Domain.Authorization.Users.ClassMappers
{
    public class UserRoleMapper : HlClassMapper<UserRole>
    {
        public UserRoleMapper()
        {
            Table("auth_user_role");      
            AutoMap();
        }

    }
}
