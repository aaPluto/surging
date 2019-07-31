using Hl.Core.ClassMapper;

namespace Hl.Identity.Domain.Authorization.UserGroups.ClassMappers
{
    public class UserGroupRelationMapper : HlClassMapper<UserGroupRelation>
    {
        public UserGroupRelationMapper()
        {
            Table("auth_user_usergroup");
            AutoMap();
        }
    }
}
