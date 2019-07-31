﻿
using System;

namespace Surging.Core.CPlatform.Utilities
{
    public static class GuidGenerator
    {
        public static Guid Create()
        {
            return Guid.NewGuid();
        }

        public static string CreateGuidStr()
        {
            return Create().ToString();
        }

        public static string CreateGuidStrWithNoUnderline()
        {
            return Create().ToString("N");
        }
    }
}
