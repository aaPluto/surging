using Hl.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hl.Identity.IApplication.UserGroups.Dtos
{
    public abstract class UserGroupDtoBase
    {

        [Required(ErrorMessage = "用户组名称不允许为空")]
        [MaxLength(50, ErrorMessage = "用户组名称最长不允许超过50")]
        public string GroupName { get; set; }
        public Status Status { get; set; }
    }
}
