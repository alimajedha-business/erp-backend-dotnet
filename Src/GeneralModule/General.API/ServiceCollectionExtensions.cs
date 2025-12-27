using ERP.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace NGErp.General.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGeneralApiServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            
            return services;
        }
    }
}
