using DapperExtensions.Mapper;
using Surging.Debug.Test1.Domain.Demo.Entities;

namespace Surging.Debug.Test1.Domain.Demo.ClassMappers
{
    public class DemoClassMapper : ClassMapper<DemoEntity>
    {
        public DemoClassMapper()
        {
            Map(p => p.Id).Column("Id").Key(KeyType.Assigned);
            Map(p => p.Filed1).Column("Filed1");
            Table("demo");
        }
    }
}
