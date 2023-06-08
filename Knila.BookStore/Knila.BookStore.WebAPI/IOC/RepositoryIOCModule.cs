using Autofac;
using Autofac.Extras.DynamicProxy;
using Insight.Database;
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

        public RepositoryIOCModule(IConfiguration configuration)
        {
            string connectionString = configuration["SQLConnectionString"];
            this._sqlConnection = new SqlConnection(connectionString);
        }

        // Corresponding Repository related with Interface and Concrete
        // Log Interceptor Automatically attached to the Interceptor Methods
        protected override void Load(ContainerBuilder builder)
        {
            SqlInsightDbProvider.RegisterProvider();

            builder
            .Register(b => this._sqlConnection.AsParallel<IBookRepository>())
            .InstancePerLifetimeScope()
            .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LogInterceptor));

            base.Load(builder);
        }
    }
}