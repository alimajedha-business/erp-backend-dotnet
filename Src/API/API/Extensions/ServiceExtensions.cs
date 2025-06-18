using Accounting.Application;
using Accounting.Application.Interfaces.Repositories;
using Accounting.Application.Interfaces.Services;
using Accounting.Application.Services;
using Accounting.Infrastructure.DataAccess;
using Accounting.Infrastructure.DataAccess.Repositories;
using Common.Infrastructure.Logging;
using General.Application;
using General.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace API.Extentions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {
            });

          public static void AddModuleApplications(this IServiceCollection services) 
        {
            services.AddAccountingApplication();
            services.AddGeneralApplication();
        }        

        public static IServiceCollection AddInfrastructures(this IServiceCollection services, IConfiguration configuration)
        {
            // Module infrastructure
            services.AddAccountingInfrastructure(configuration);
            services.AddGeneralInfrastructure(configuration);
            // Add other modules (e.g., services.AddWarehouseInfrastructure(configuration))
            return services;
        }
    }
}
