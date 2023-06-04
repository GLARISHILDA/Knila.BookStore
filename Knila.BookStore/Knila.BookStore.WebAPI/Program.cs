using Autofac.Extensions.DependencyInjection;
using Autofac;
using Knila.BookStore.WebAPI.Extensions.AutoMapper;
using Knila.BookStore.WebAPI.Extensions.CustomFilters;
using NLog.Web;
using Knila.BookStore.ServiceConcrete;
using Knila.BookStore.ServiceInterface;
using Knila.BookStore.Infrastructure.DbConnection;
using NLog;
using Knila.BookStore.Infrastructure.Logging;
using Knila.BookStore.WebAPI.IOC;

namespace Knila.BookStore.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(LoggingActionFilter));
            }).AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                }); ;

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureContainer<ContainerBuilder>(builder =>
                    {
                        builder.RegisterInstance<IConfiguration>(configuration);
                        builder.Register(c => new LogInterceptor(logger)).SingleInstance();
                        builder.RegisterType<DapperConnectionProvider>().As<IDapperConnectionProvider>();
                        builder.RegisterModule(new ServiceIOCModule());
                        builder.RegisterModule(new RepositoryIOCModule());
                    });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); // Service (Auto Mapper)
            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}