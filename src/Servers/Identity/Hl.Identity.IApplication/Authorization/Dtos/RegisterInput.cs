using AutoMapper.Attributes;
using Hl.Identity.Domain.Authorization.Users;

namespace Hl.Identity.IApplication.Authorization.Dtos
{
    [MapsTo(typeof(UserInfo))]
    [IgnoreMapToProperties(typeof(UserInfo), nameof(RepeatedPassword))]
    public class RegisterInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string RepeatedPassword { get; set; }

        public string ChineseName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string QQ { get; set; }

        public string Wechat { get; set; }
    }
}
