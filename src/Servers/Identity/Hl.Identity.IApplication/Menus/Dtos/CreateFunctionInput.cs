using Hl.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hl.Identity.IApplication.Menus.Dtos
{
    public class CreateFunctionInput : FunctionDtoBase
    {
        public long ParentId { get; set; }
    }
}
