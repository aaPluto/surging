using Surging.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.Permissions
{
    public class PermissionFile : AuditedEntity<long>
    {
        public long PermissionId { get; set; }
        public long FileId { get; set; }
    }
}
