using Microsoft.Extensions.DependencyInjection;
using NGErp.HCM.Service.Mappings;
using NGErp.HCM.Service.Services;



namespace NGErp.HCM.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHCMServices(this IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(MappingProfile));            
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();
            return services;
        }
    }
}
