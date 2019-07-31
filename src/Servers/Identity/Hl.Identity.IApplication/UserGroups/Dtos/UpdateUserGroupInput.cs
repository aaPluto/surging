using Surging.Core.System.Intercept;
using System;


namespace Hl.Identity.IApplication.UserGroups.Dtos
{
    public class UpdateUserGroupInput : UserGroupDtoBase
    {
        [CacheKey(1)]
        public long Id { get; set; }
    }
}
