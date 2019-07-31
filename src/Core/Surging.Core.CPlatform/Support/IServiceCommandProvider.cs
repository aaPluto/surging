﻿using System.Threading.Tasks;

namespace Surging.Core.CPlatform.Support
{
    public interface IServiceCommandProvider
    {
        ValueTask<ServiceCommand> GetCommand(string serviceId);

        Task<object> Run(string text, params string[] InjectionNamespaces);
    }
}