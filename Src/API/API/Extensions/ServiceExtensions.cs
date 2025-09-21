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
using Persistence.DataAccess;
using Microsoft.OpenApi.Models;

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
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
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
            services.AddPersistenceInfrastructure(configuration);
            // Add other modules (e.g., services.AddWarehouseInfrastructure(configuration))
            return services;
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Noavaran ERP API",
                    Version = "v1"                   
                });
                
            });
        }
    }
}
