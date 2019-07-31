using Surging.Core.Domain.Entities.Auditing;
using System;

namespace Hl.Identity.Domain.Authorization.UserGroups
{
    public class UserGroupRelation : AuditedEntity<long>
    {
        public long UserId { get; set; }
        public long UserGroupId { get; set; }
    }
}
