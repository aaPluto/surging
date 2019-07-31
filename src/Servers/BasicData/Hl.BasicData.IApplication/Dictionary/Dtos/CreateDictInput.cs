using System;
using System.ComponentModel.DataAnnotations;

namespace Hl.BasicData.IApplication.Dictionary.Dtos
{
    public class CreateDictInput
    {
        [Required(ErrorMessage = "字典编码不允许为空")]
        public string Code { get; set; }

        [Required(ErrorMessage = "字典名称不允许为空")]
        public string Name { get; set; }

        [Required(ErrorMessage = "字典值不允许为空")]
        public string Value { get; set; }

        public int ParentId { get; set; } = 0;

        public int Seq { get; set; } = 0;

        public string TypeName { get; set; }

        public bool HasChild { get; set; }
    }
}
