using Surging.Core.Domain.Entities.Auditing;

namespace Hl.BasicData.Domain
{
    public class SystemConf : FullAuditedEntity<long>
    {
        public string ConfigName { get; set; }

        public string ConfigValue { get; set; }

        public string Memo { get; set; }

        public int Seq { get; set; }

        public bool SysPreSet { get; set; }
    }
}
