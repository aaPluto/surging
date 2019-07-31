using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.IApplication.UserGroups.Dtos
{
    public class GetUserGroupOutput
    {
        public GetUserGroupOutput()
        {
            Children = new List<GetUserGroupOutput>();
        }

        public long Id { get; set; }

        public string GroupCode { get; set; }

        public string GroupName { get; set; }

        public ICollection<GetUserGroupOutput> Children { get; set; }
    }
}
