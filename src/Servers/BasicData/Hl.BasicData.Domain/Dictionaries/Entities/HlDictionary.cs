using Surging.Core.Domain.Entities.Auditing;

namespace Hl.BasicData.Domain
{
    public class HlDictionary : FullAuditedEntity<long>
    {
        public HlDictionary()
        {
            SysPreSet = false;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public long ParentId { get; set; }

        public int Seq { get; set; }

        public string TypeName { get; set; }

        public bool HasChild { get; set; }

        public bool SysPreSet { get; set; }
    }
}
