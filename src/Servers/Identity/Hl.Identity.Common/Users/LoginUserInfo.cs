using System;

namespace Hl.Identity.Domain.Shared.Users
{
    public class LoginUserInfo
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string ChineseName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
