using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Service.Interfaces;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.Mappings;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWarehouseServices(this IServiceCollection services)
    {
        services.AddScoped<IExceptionLocalizer<WarehouseResource>, ExceptionLocalizer<WarehouseResource>>();
        services.AddHttpContextAccessor();

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<ICategoryService,  CategoryService>();
        services.AddScoped<IItemService, ItemService>();

        return services;
    }
}
