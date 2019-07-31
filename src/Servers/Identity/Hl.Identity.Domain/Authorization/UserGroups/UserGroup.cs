using Hl.Core.Enums;
using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.UserGroups
{
    public class UserGroup : FullAuditedEntity<long>
    {
        public UserGroup()
        {
            Status = Status.Valid;
            ParentId = 0;
        }

        public long ParentId { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public Status Status { get; set; }

    }
}
