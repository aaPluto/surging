using Surging.Core.CPlatform.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Core.CPlatform.Exceptions
{
    public class BusinessException : CPlatformException
    {
        public BusinessException(string message, Exception innerException = null) : base(message, innerException, StatusCode.BusinessError)
        {
        }

        public BusinessException(string message) : base(message, StatusCode.BusinessError)
        {

        }
    }
}
