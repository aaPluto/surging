using System.ComponentModel.DataAnnotations;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class CreateMenuInput : MenuDtoBase
    {
        [Required(ErrorMessage = "菜单编码不允许为空")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,16}$", ErrorMessage = "菜单编码格式不正确")]
        public string Code { get; set; }

        public long ParentId { get; set; }

    }
}
