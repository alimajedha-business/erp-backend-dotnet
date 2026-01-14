using Microsoft.Extensions.DependencyInjection;



namespace NGErp.HCM.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHCMServices(this IServiceCollection services)
        {
            
            //services.AddAutoMapper(typeof(MappingProfile));
            //services.AddScoped<IExceptionLocalizer<GeneralResource>, ExceptionLocalizer<GeneralResource>>();
            //services.AddScoped<IPythonIntegrationService, PythonIntegrationService>();
            return services;
        }
    }
}
