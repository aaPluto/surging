using System;

namespace Hl.Identity.Domain
{
    public static class CacheKeyConstants
    {
        public const string GetUserGroupByIdKey = "GetUserGroup_{0}";

        public const string GetUserGroupsKey = "GetUserGroups";

        public const string GetAllUsersKey = "GetAllUsers";

        public const string QueryUsersKey = "QueryUsers_{0}_{1}_{2}_{3}";
    }
}
