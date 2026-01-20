using Microsoft.Extensions.DependencyInjection;

using NGErp.Warehouse.Service.Mappings;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWarehouseServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<ICategoryService,  CategoryService>();
        return services;
    }
}
