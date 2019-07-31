﻿namespace Surging.Core.CPlatform.Engines.Implementation
{
    public abstract class VirtualPathProviderServiceEngine : IServiceEngine
    {
        public string[] ModuleServiceLocationFormats { get; set; }

        public string[] ComponentServiceLocationFormats { get; set; }
    }
}