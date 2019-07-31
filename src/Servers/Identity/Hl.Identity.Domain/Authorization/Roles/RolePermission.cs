using Surging.Core.Domain.Entities.Auditing;
using System;

namespace Hl.Identity.Domain.Authorization.Roles
{
    public class RolePermission : AuditedEntity<long>
    {
        public long RoleId { get; set; }
        public long PerssionId { get; set; }
    }
}
