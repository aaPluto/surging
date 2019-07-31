
using Hl.Core.ClassMapper;

namespace Hl.BasicData.Domain.ClassMapper
{
    class HlDictionaryClassMapper : HlClassMapper<HlDictionary>
    {
        public HlDictionaryClassMapper()
        {
            Table("bd_dictionary");
            AutoMap();
        }
    }
}
