using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Service.Interfaces;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Service.Mappings;
using NGErp.Warehouse.Service.Resources;


namespace NGErp.Warehouse.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWarehouseServices(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IExceptionLocalizer<WarehouseResources>, ExceptionLocalizer<WarehouseResources>>();
        services.AddScoped<IPythonIntegrationService, PythonIntegrationService>();
        return services;
    }
}
