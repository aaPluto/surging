using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class CreateOperationInput : OperationDtoBase
    {
        [Required(ErrorMessage = "操作编码不允许为空")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,16}$", ErrorMessage = "操作编码格式不正确")]
        public string Code { get; set; }

    }
}
