﻿using System;

namespace Surging.Core.CPlatform.Filters.Implementation
{
    public abstract class FilterAttribute : Attribute, IFilter
    {
        private readonly bool _filterAttribute;

        protected FilterAttribute()
        {
            _filterAttribute = true;
        }

        public virtual bool AllowMultiple { get => _filterAttribute; }
    }
}