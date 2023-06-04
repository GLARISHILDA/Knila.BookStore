using Autofac;
using Autofac.Extras.DynamicProxy;
using Knila.BookStore.Infrastructure.Logging;
using Knila.BookStore.RepositoryConcrete;
using Knila.BookStore.RepositoryInterface;
using Microsoft.AspNetCore.Authentication;
using System.Data.Common;
using System.Data.SqlClient;

namespace Knila.BookStore.WebAPI.IOC
{
    public class RepositoryIOCModule : Module
    {
        private readonly DbConnection _sqlConnection;
        private readonly string _lifeTime;

        public RepositoryIOCModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
            .RegisterType<BookRepository>().As<IBookRepository>()
            .InstancePerLifetimeScope()
            .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LogInterceptor));

            base.Load(builder);
        }
    }
}