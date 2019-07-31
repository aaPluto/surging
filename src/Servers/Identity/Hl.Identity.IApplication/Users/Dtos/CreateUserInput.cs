using System.ComponentModel.DataAnnotations;

namespace Hl.Identity.IApplication.Users.Dtos
{
    public class CreateUserInput : UserDtoBase
    {

        [Required(ErrorMessage = "用户名不允许为空")]
        [RegularExpression("^[a-zA-Z0-9_-]{4,16}$", ErrorMessage = "用户名不允许为空")]
        public string UserName { get; set; }
    }
}
