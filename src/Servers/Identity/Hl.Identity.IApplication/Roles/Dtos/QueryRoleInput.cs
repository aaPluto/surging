using Surging.Core.Domain.PagedAndSorted;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.IApplication.Roles.Dtos
{
    public class QueryRoleInput : PagedResultRequestDto
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
