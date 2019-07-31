using Hl.Core.ClassMapper;

namespace Hl.Identity.Domain.Authorization.Users.ClassMappers
{
    class UserInfoMapper : HlClassMapper<UserInfo>
    {
        public UserInfoMapper()
        {
            Table("auth_userinfo");
            AutoMap();
        }
    }
}
