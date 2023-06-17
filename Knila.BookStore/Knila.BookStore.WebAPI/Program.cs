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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                        builder.Register(c => new LogInterceptor(logger)).SingleInstance(); // Logging Service Layer and Repository Layer Register
                        builder.RegisterType<DapperConnectionProvider>().As<IDapperConnectionProvider>(); // Inject DB Connection to Repository Layer
                        builder.RegisterModule(new ServiceIOCModule());
                        builder.RegisterModule(new RepositoryIOCModule(configuration));
                    });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); // Service (Auto Mapper)

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    // Adding Jwt Bearer
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = "https://localhost:7282/",
            ValidIssuer = "https://localhost:7282/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzUxMiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTYzNjI4MDE5NCwiaWF0IjoxNjM2MjgwMTk0fQ.1Kv_-41VhLaf5QDkfu2tMgsUIGV_lZsWfaZlmSap55CSAz3fGHQ9Qzf0PmxAaUfractckzOS5bjoB9EaxRLVbQ")),
            //ClockSkew = TimeSpan.Zero
        };
    });

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
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}