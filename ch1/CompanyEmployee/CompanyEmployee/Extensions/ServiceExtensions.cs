using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;
using System.Diagnostics;

namespace CompanyEmployee.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<IloggerManager, LoggerManager>();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            string? MachineName = Environment.MachineName;
            string? ConnectionString = configuration.GetConnectionString("sqlConnection");
            ConnectionString = ConnectionString!.Replace("??????", MachineName);

            services.AddDbContext<RepositoryContext>(opts
                => opts.UseSqlServer(ConnectionString));
        }

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder)
            => builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
    }
}
