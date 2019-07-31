using Hl.Core.ClassMapper;
using System;


namespace Hl.BasicData.Domain
{
    public class SystemConfClassMapper : HlClassMapper<SystemConf>
    {
        public SystemConfClassMapper()
        {
            Table("bd_systemconf");
            AutoMap();
        }
    }
}
