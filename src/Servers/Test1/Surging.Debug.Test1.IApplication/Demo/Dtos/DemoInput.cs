using AutoMapper.Attributes;
using Surging.Debug.Test1.Domain.Demo.Entities;

namespace Surging.Debug.Test1.IApplication.Demo.Dtos
{
    [MapsTo(typeof(DemoEntity))]
    public class DemoInput
    {
        public string Filed1 { get; set; }
    }
}
