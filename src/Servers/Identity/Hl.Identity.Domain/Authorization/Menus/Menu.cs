using Hl.Core.Enums;
using Surging.Core.Domain.Entities.Auditing;
using System;
namespace Hl.Identity.Domain.Authorization.Menus
{
    public class Menu : FullAuditedEntity<long>
    {
        public Menu()
        {
            Status = Status.Valid;
        }

        public long PermissionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UrlPath { get; set; }
        public long ParentId { get; set; }
        public int Level { get; set; }
        public MenuMold Mold { get; set; }
        public string Icon { get; set; }
        public string FrontEndComponent { get; set; }
        public int? Sort { get; set; }
        public string Memo { get; set; }

        public Status Status { get; set; }
    }
}
