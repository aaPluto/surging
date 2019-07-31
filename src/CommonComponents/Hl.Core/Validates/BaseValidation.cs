﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Core.Validates
{
    public abstract class BaseValidation
    {
        /// <summary>
        /// 有效标识
        /// </summary>
        public bool IsValid { get; protected set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public ICollection<string> ErrorMessages { get; protected set; }

        /// <summary>
        /// 主错误信息
        /// </summary>
        public string PrimaryErrorMessage { get; protected set; }

        protected BaseValidation()
        {
            ErrorMessages = new List<string>();
        }
    }
}
