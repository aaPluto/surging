﻿using Surging.Core.ApiGateWay.Configurations;

namespace Surging.Core.ApiGateWay
{
    public class Register
    {
        public RegisterProvider Provider { get; set; } = RegisterProvider.Consul;

        public string Address { get; set; } = "127.0.0.1:8500";
    }
}