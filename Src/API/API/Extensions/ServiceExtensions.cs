// Ignore Spelling: Extentions Cors Localizers

using Accounting.Application;
using Accounting.Application.Interfaces.Repositories;
using Accounting.Application.Interfaces.Services;
using Accounting.Application.Mappings;
using Accounting.Application.Services;
using Accounting.Infrastructure.DataAccess;
using Accounting.Infrastructure.DataAccess.Repositories;
using Accounting.Resources;
using Common.Application;
using Common.Application.Mappings;
using Common.Application.Services;
using Common.Infrastructure.Logging;
using Common.Resources;
using General.Resources;
using General.Application;
using General.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Morcatko.AspNetCore.JsonMergePatch;
using System.Globalization;
using HCM.Resources;
using HCM.Application;
using HCM.Infrastructure.DataAccess;


namespace API.Extensions
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
            //
            services.AddHCMApplication();
        }

        public static IServiceCollection AddInfrastructures(this IServiceCollection services, IConfiguration configuration)
        {
            // Module infrastructure
            services.AddAccountingInfrastructure(configuration);
            services.AddGeneralInfrastructure(configuration);
            // Add other modules (e.g., services.AddWarehouseInfrastructure(configuration))
            services.AddHCMlInfrastructure(configuration);
            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
            })
                .AddSystemTextJsonMergePatch()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var module = ProjectConstants.Modules
                        .FirstOrDefault(m => type.Namespace?.Contains(m) == true);
                        if (module is not null)
                        {
                            var resourceTypeName = $"{module}.Resources.{module}Resource";
                            var resourceType = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .FirstOrDefault(t => t.FullName == resourceTypeName);
                            if (resourceType is not null)
                                return factory.Create(resourceType);
                        }

                        // Fallback to shared/common resource
                        return factory.Create(typeof(CommonResource));
                    };
                })
                .AddApplicationPart(typeof(Accounting.Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(General.Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(Warehouse.Presentation.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(HCM.Presentation.AssemblyReference).Assembly);

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
                        Version = "v1",
                        Description = $"{module} Module API"
                    });
                }

                // Add security definition if using authentication
                //s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme.",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});

                //s.OperationFilter<AuthorizeCheckOperationFilter>(); // Custom filter if needed
            });

        }

        public static void ConfigureLocalization(this IServiceCollection services)
        {
            services.AddLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(ProjectConstants.SupportedCultures[0]);
                options.SupportedCultures = ProjectConstants.SupportedCultures.Select(c => new CultureInfo(c)).ToList();
                options.SupportedUICultures = ProjectConstants.SupportedCultures.Select(c => new CultureInfo(c)).ToList();

                // Derive culture from Accept-Language header
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                {
                    var lang = context.Request.Headers["Accept-Language"].FirstOrDefault();
                    var culture = !string.IsNullOrEmpty(lang) ? lang : "fa";
                    return await Task.FromResult(new ProviderCultureResult(culture));
                }));
            });
        }

    }
}
