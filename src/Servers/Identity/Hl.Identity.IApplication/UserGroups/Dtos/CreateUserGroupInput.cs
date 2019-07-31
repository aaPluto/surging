using System.ComponentModel.DataAnnotations;

namespace Hl.Identity.IApplication.UserGroups.Dtos
{
    public class CreateUserGroupInput : UserGroupDtoBase
    {
        [Required(ErrorMessage = "父级用户组Id不允许为空,默认值为0")]
        public long ParentId { get; set; }

        [Required(ErrorMessage = "用户组名称不允许为空")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,50}$", ErrorMessage = "用户组编码格式不正确")]
        public string GroupCode { get; set; }
    }
}
