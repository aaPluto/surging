using DapperExtensions.Mapper;
using Hl.Core.ClassMapper;

namespace Hl.Identity.Domain.Authorization.UserGroups.ClassMappers
{
    public class UserGroupMapper : HlClassMapper<UserGroup>
    {
        public UserGroupMapper()
        {
            Table("auth_usergroup");
            AutoMap();
        }
    }
}
