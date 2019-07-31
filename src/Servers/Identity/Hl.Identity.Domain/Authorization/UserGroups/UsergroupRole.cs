using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.UserGroups
{
    public class UserGroupRole : AuditedEntity<long>
    {
        public long UserGroupId { get; set; }
        public long RoleId { get; set; }
    }
}
