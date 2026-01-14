using System.Globalization;

using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;

using Morcatko.AspNetCore.JsonMergePatch;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Infrastructure;
using NGErp.Base.Service;
using NGErp.Base.Service.Resources;
using NGErp.General.API;
using NGErp.General.Infrastructure.DataAccess;
using NGErp.General.Service;
using NGErp.Warehouse.Infrastructure.DataAccess;
using NGErp.Warehouse.Service;
using NGErp.Warehouse.Infrastructure.DataAccess;
using NGErp.Warehouse.Service;
using NGErp.HCM.Infrastructure.DataAccess;
using NGErp.HCM.Service;

namespace NGErp.API.Extensions
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

        public static void AddServices(this IServiceCollection services)
        {
            services.AddBaseServices();
            services.AddGeneralServices();
            services.AddGeneralApiServices();
            services.AddWarehouseServices();
            services.AddHCMServices();
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBaseInfrastructureServices(configuration);
            services.AddGeneralInfrastructureServices(configuration);
            services.AddWarehouseInfrastructureServices(configuration);
            services.AddHCMInfrastructureServices(configuration);

            return services;
        }

        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
                config.Filters.Add<ValidationFilterAttribute>();
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
                        return factory.Create(typeof(BaseResource));
                    };
                })
                .AddApplicationPart(typeof(ValidationFilterAttribute).Assembly)
                .AddApplicationPart(typeof(NGErp.General.API.AssemblyReference).Assembly)
                .AddApplicationPart(typeof(NGErp.Base.API.AssemblyReference).Assembly);
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

                // Add JWT Bearer token authentication
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                // Add Cookie authentication
                s.AddSecurityDefinition("Cookie", new OpenApiSecurityScheme
                {
                    Description = "Cookie-based authentication. The authentication cookie will be sent automatically.",
                    Name = "Cookie",
                    In = ParameterLocation.Cookie,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Cookie"
                });

                // Add security requirement - supports both Bearer and Cookie
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            },
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Cookie"
                    },
                    Name = "Cookie",
                    In = ParameterLocation.Cookie,
                },
                new List<string>()
            }
         });

                //Enable XML comments if available
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    s.IncludeXmlComments(xmlPath);
                }
            }
        );
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
