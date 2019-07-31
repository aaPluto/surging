using Surging.Core.Domain.PagedAndSorted;

namespace Hl.Identity.IApplication.Users.Dtos
{
    public class QueryUserInput : PagedResultRequestDto
    {
        public string UserName { get; set; }

        public string ChineseName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
