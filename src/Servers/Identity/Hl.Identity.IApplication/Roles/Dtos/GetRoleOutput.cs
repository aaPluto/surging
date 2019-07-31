using System;

namespace Hl.Identity.IApplication.Roles.Dtos
{
   public class GetRoleOutput : RoleDtoBase
   {
        public long Id { get; set; }

        public string Code { get; set; }
    }
}
