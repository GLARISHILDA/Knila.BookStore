using Autofac;
using Autofac.Extras.DynamicProxy;
using Knila.BookStore.Infrastructure.Logging;
using Knila.BookStore.ServiceConcrete;
using Knila.BookStore.ServiceInterface;

namespace Knila.BookStore.WebAPI.IOC
{
    public class ServiceIOCModule : Module
    {
        public ServiceIOCModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                 .RegisterType<BookService>().As<IBookService>()
                  .InstancePerLifetimeScope()
                  .EnableInterfaceInterceptors()
                  .InterceptedBy(typeof(LogInterceptor));

            builder
                 .RegisterType<IpAddressService>().As<IIpAddressService>()
                  .InstancePerLifetimeScope()
                  .EnableInterfaceInterceptors()
                  .InterceptedBy(typeof(LogInterceptor));

            base.Load(builder);
        }
    }
}