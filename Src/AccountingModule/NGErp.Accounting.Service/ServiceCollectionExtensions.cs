using Microsoft.Extensions.DependencyInjection;

using NGErp.Accounting.Service.Services;


namespace NGErp.Accounting.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountingServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(ServiceCollectionExtensions).Assembly);
            });

            services.AddScoped<IPythonIntegrationService, PythonIntegrationService>();

            return services;
        }
    }
}
