using Autofac;

namespace Surging.Core.ServiceHosting.Startup
{
    public interface IStartup
    {
        IContainer ConfigureServices(ContainerBuilder services);

        void Configure(IContainer app);
    }
}