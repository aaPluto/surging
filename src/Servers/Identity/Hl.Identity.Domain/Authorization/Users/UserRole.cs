using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserRole : AuditedEntity<long>
    {
        public long UserId { get; set; }

        public long RoleId { get; set; }

    }
}
