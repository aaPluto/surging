using System;

namespace Hl.Identity.IApplication.Users.Dtos
{
    public class GetUserOutput : UserDtoBase
    {
        public long Id { get; set; }
        public string UserName { get; set; }
    }
}
