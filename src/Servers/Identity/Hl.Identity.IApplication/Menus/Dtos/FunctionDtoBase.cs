using Hl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class FunctionDtoBase
    {
        [Required(ErrorMessage = "功能名称不允许为空")]
        [MaxLength(50, ErrorMessage = "功能名称长度不允许超过50")]
        public string Name { get; set; }
        [RegularExpression("(/\\w+){1,}(/?)", ErrorMessage = "WebApi格式不正确")]
        public string WebApi { get; set; }
        public Status Status { get; set; }
        public HttpMethod Method { get; set; }
        public string Memo { get; set; }
    }
}
