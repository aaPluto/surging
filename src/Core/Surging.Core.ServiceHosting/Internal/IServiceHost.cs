using Autofac;
using System;

namespace Surging.Core.ServiceHosting.Internal
{
    public interface IServiceHost : IDisposable
    {
        IDisposable Run();

        IContainer Initialize();
    }
}