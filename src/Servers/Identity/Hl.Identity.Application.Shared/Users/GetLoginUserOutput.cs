using System;


namespace Hl.Identity.Application.Shared.Users
{
    public class GetLoginUserOutput
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ChineseName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birth { get; set; }

        public string NativePlace { get; set; }

        public string Address { get; set; }

        public string Folk { get; set; }

        public PoliticalStatus PoliticalStatus { get; set; }

        public string GraduateInstitutions { get; set; }

        public string Education { get; set; }

        public string Major { get; set; }

        public string Resume { get; set; }

        public string Memo { get; set; }

        public Status Status { get; set; }

        public int LoginFailedCount { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}
