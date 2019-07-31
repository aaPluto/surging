﻿using System.Collections.Generic;

namespace Surging.Core.ApiGateWay.Configurations
{
    public class ServicePart
    {
        public string MainPath { get; set; } = "part/service/aggregation";

        public bool EnableAuthorization { get; set; }

        public List<Services> Services { get; set; } = new List<Services>();
    }
}