using Surging.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Debug.Test1.Domain.UserInfo
{
    public class UserInfo : FullAuditedEntity<long>
    {
        public UserInfo()
        {
            //Roles = new List<UserRole>();

            Status = 1;
        }

        public long EmployeeId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


        public int Status { get; set; }

        //public EmployeeAggregate Employee { get; set; }

        //public virtual ICollection<UserRole> Roles { get; set; }
    }
}
