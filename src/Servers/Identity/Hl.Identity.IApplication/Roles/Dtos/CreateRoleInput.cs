using System;
using System.ComponentModel.DataAnnotations;

namespace Hl.Identity.IApplication.Roles.Dtos
{
    public class CreateRoleInput : RoleDtoBase
    {
        [Required(ErrorMessage = "角色编码不允许为空")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,16}$", ErrorMessage = "角色编码格式不正确")]
        public string Code { get; set; }
    }
}
