using Hl.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class PasswordHelper : IPasswordHelper
    {
        public string EncryptPassword(string userName, string plainPassword)
        {
            return EncryptHelper.Md5(EncryptHelper.Md5(userName + plainPassword));
        }
    }
}
