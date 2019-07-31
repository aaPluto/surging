using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class OperationDtoBase
    {
        [Required(ErrorMessage = "操作名称不允许为空")]
        [MaxLength(50, ErrorMessage = "操作名称长度不允许超过50")]
        public string Name { get; set; }

        public string Memo { get; set; }

        public IEnumerable<long> FunctionIds { get; set; }
    }
}
