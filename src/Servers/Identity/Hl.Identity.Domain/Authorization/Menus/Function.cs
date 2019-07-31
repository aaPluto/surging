using Hl.Core.Enums;
using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.Menus
{
    public class Function : FullAuditedEntity<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string WebApi { get; set; }
        public Status Status { get; set; }
        public HttpMethod Method { get; set; }
        public long ParentId { get; set; }
        public string Memo { get; set; }

    }
}
