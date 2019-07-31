using Surging.Core.CPlatform.Exceptions;
using System;

namespace Hl.Core.Utils
{
    public static class ConvertHelper
    {
        public static T ParseEnum<T>(string value) where T :struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception ex)
            {
                throw new CPlatformException("类型转换失败",ex);
            }
            
        }
    }
}
