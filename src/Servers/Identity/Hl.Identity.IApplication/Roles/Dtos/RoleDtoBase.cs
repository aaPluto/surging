using Hl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hl.Identity.IApplication.Roles.Dtos
{
    public abstract class RoleDtoBase
    {

        [Required(ErrorMessage = "角色名称不允许为空")]
        [MaxLength(50,ErrorMessage = "角色名称长度不允许超过50")]
        public string Name { get; set; }
        public string Memo { get; set; }
        public Status Status { get; set; }
    }
}
