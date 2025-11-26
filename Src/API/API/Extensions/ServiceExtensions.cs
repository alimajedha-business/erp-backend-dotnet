using NGErp.Base.Service.Services;
using NGErp.Base.Service.Interfaces;
using NGErp.Base.Service.Resources;
using NGErp.General.Service;
using NGErp.General.API;
using NGErp.General.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;
using NGErp.Base.Infrastructure.DataAccess;
using System.Globalization;
using NGErp.Base.Service;
using NGErp.Base.API.ActionFilters;
using Morcatko.AspNetCore.JsonMergePatch;

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
            services.AddScoped<IExceptionLocalizer<BaseResource>, ExceptionLocalizer<BaseResource>>();
            services.AddGeneralServices();
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBaseInfrastructureServices(configuration);
            services.AddGeneralInfrastructureServices(configuration);

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
                .AddApplicationPart(typeof(NGErp.General.API.AssemblyReference).Assembly);
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
