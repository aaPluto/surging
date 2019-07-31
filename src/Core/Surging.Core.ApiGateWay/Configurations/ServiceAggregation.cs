﻿using System.Collections.Generic;

namespace Surging.Core.ApiGateWay.Configurations
{
    public class ServiceAggregation
    {
        public string RoutePath { get; set; }

        public string ServiceKey { get; set; }

        public Dictionary<string, object> Params { get; set; }

        public string Key { get; set; }
    }
}