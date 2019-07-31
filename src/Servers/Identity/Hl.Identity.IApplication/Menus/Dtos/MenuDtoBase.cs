using Hl.Identity.Domain.Authorization.Menus;
using System.ComponentModel.DataAnnotations;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class MenuDtoBase
    {
        [Required(ErrorMessage = "菜单名称不允许为空")]
        [MaxLength(50, ErrorMessage = "菜单名称长度不允许超过50")]
        public string Name { get; set; }
        [RegularExpression("(/\\w+){1,}(/?)", ErrorMessage = "UrlPath格式不正确")]
        public string UrlPath { get; set; }
        public int Level { get; set; }
        public MenuMold Mold { get; set; }
        public string Icon { get; set; }
        public string Memo { get; set; }
        public string FrontEndComponent { get; set; }
        public int? Sort { get; set; }
    }
}
