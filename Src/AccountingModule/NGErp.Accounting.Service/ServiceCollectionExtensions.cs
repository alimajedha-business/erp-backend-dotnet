using Microsoft.Extensions.DependencyInjection;

using NGErp.Accounting.Service.Mappings;
using NGErp.Accounting.Service.Services;


namespace NGErp.Accounting.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); });

            services.AddScoped<IPythonIntegrationService, PythonIntegrationService>();

            return services;
        }
    }
}
