using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NGErp.Warehouse.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWarehouseInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IAttributeRepository, AttributeRepository>();
        services.AddScoped<IAttributeEnumValueRepository, AttributeEnumValueRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryAttributeRuleRepository, CategoryAttributeRuleRepository>();
        services.AddScoped<ICategoryLevelConstraintRepository, CategoryLevelConstraintRepository>();
        services.AddScoped<IInventoryMovementTypeRepository, InventoryMovementTypeRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemAttributeRepository, ItemAttributeRepository>();
        services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
        services.AddScoped<IItemUnitOfMeasurementRepository, ItemUnitOfMeasurementRepository>();
        services.AddScoped<IMeasurementDimensionRepository, MeasurementDimensionRepository>();
        services.AddScoped<IShippingCompanyRepository, ShippingCompanyRepository>();
        services.AddScoped<IUnitOfMeasurementConversionRepository, UnitOfMeasurementConversionRepository>();
        services.AddScoped<IUnitOfMeasurementRepository, UnitOfMeasurementRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IWarehouseLocationRepository, WarehouseLocationRepository>();
        services.AddScoped<IWarehouseTypeRepository, WarehouseTypeRepository>();

        return services;
    }
}
