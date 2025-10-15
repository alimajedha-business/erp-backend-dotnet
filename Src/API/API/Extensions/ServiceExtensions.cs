// Ignore Spelling: Extentions Cors

using Accounting.Application;
using Accounting.Application.Interfaces.Repositories;
using Accounting.Application.Interfaces.Services;
using Accounting.Application.Mappings;
using Accounting.Application.Services;
using Accounting.Infrastructure.DataAccess;
using Accounting.Infrastructure.DataAccess.Repositories;
using Common.Application;
using Common.Application.Mappings;
using Common.Infrastructure.Logging;
using General.Application;
using General.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Persistence.DataAccess;
using System.Globalization;

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
            // Add other modules (e.g., services.AddWarehouseInfrastructure(configuration))
            return services;
        }

        public static IServiceCollection AddModuleControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
            }).AddApplicationPart(typeof(Accounting.Presentation.AssemblyReference).Assembly)
            .AddApplicationPart(typeof(General.Presentation.AssemblyReference).Assembly)
            .AddApplicationPart(typeof(Warehouse.Presentation.AssemblyReference).Assembly);
            return services;
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                foreach (var module in ProjectConstants.Modules)
                {
                    s.SwaggerDoc($"v1-{module.ToLower()}", new OpenApiInfo
                    {
                        Title = $"{module} API",
                        Version = "v1"
                    });
                }
            });
        }
        public static IServiceCollection AddModuleAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AccountingMappingProfile).Assembly);
            services.AddAutoMapper(typeof(CommonMappingProfile).Assembly);
            return services;
        }

        public static IServiceCollection AddMultiLanguage(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddPortableObjectLocalization(options =>
            {
                options.ResourcesPath = "Localization";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(ProjectConstants.SupportedCultures[0]);
                options.SupportedCultures = ProjectConstants.SupportedCultures.Select(c => new CultureInfo(c)).ToList();
                options.SupportedUICultures = ProjectConstants.SupportedCultures.Select(c => new CultureInfo(c)).ToList();
            });
            return services;
        }
    }
}
