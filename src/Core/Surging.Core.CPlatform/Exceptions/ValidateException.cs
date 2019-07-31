using Surging.Core.CPlatform.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Core.CPlatform.Exceptions
{
    public class ValidateException : CPlatformException
    {
        public ValidateException(string message, Exception innerException = null) : base(message, innerException, StatusCode.ValidateError)
        {
        }

        public ValidateException(string message) : base(message, StatusCode.ValidateError)
        {

        }
    }
}
