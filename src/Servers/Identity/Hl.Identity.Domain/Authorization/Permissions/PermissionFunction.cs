using Surging.Core.Domain.Entities.Auditing;
using System;
namespace Hl.Identity.Domain.Authorization.Permissions
{
    public class PermissionFunction : AuditedEntity<long>
    {
        public long PermissionId { get; set; }
        public long FunctionId { get; set; }
    }
}
