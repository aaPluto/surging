using Hl.Core.Enums;
using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.Permissions
{
    public class Permission : FullAuditedEntity<long>
    {
        public Permission()
        {
            Status = Status.Valid;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public PermissionMold Mold { get; set; }
        public string Memo { get; set; }
        public Status Status { get; set; }
    }
}
