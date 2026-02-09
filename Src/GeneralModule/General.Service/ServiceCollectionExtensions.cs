using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Service.Services;
using NGErp.General.Service.Mappings;
using NGErp.General.Service.Resources;
using NGErp.General.Service.Services;


namespace NGErp.General.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGeneralServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IExceptionLocalizer<GeneralResource>, ExceptionLocalizer<GeneralResource>>();

            services.AddScoped<IPythonIntegrationService, PythonIntegrationService>();
            services.AddScoped<ICompanyService, CompanyService>();

            return services;
        }
    }
}
