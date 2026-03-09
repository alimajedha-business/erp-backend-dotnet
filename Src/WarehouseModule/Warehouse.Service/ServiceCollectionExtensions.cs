using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Domain.EntitySchemas;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.EntitySchemas;
using NGErp.Warehouse.Service.Mappings;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWarehouseServices(this IServiceCollection services)
    {
        services.AddScoped<
            IExceptionLocalizer<WarehouseResource>, ExceptionLocalizer<WarehouseResource>
        >();

        services.AddHttpContextAccessor();

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddSingleton<IFilterSchema<Domain.Entities.Attribute>, AttributeSchema>();
        services.AddSingleton<IFilterSchema<AttributeEnumValue>, AttributeEnumValueSchema>();
        services.AddSingleton<IFilterSchema<Category>, CategorySchema>();
        services.AddSingleton<IFilterSchema<InventoryMovementType>, InventoryMovementTypeSchema>();
        services.AddSingleton<IFilterSchema<Item>, ItemSchema>();
        services.AddSingleton<IFilterSchema<MeasurementDimension>, MeasurementDimensionSchema>();
        services.AddSingleton<IFilterSchema<UnitOfMeasurementConversion>, UnitOfMeasurementConversionSchema>();
        services.AddSingleton<IFilterSchema<UnitOfMeasurement>, UnitOfMeasurementSchema>();
        services.AddSingleton<IFilterSchema<Domain.Entities.Warehouse>, WarehouseSchema>();

        services.AddScoped<IAttributeService, AttributeService>();
        services.AddScoped<IAttributeEnumValueService, AttributeEnumValueService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryAttributeRuleService, CategoryAttributeRuleService>();
        services.AddScoped<IInventoryMovementTypeService, InventoryMovementTypeService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IMeasurementDimensionService, MeasurementDimensionService>();
        services.AddScoped<IUnitOfMeasurementConversionService, UnitOfMeasurementConversionService>();
        services.AddScoped<IUnitOfMeasurementService, UnitOfMeasurementService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IWarehouseTypeService, WarehouseTypeService>();

        return services;
    }
}
