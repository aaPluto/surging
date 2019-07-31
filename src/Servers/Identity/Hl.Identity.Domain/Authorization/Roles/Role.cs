using Hl.Core.Enums;
using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.Roles
{
    public class Role : FullAuditedEntity<long>
    {
        public Role()
        {
            Status = Status.Valid;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; }

        public Status Status { get; set; }
    }
}
