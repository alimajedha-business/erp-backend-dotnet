using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Domain.EntitySchemas;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.EntitySchemas;
using NGErp.Warehouse.Service.Mappings;
using NGErp.Warehouse.Service.RequestValidators;
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

        services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

        services.AddSingleton<IFilterSchema<Domain.Entities.Attribute>, AttributeSchema>();
        services.AddSingleton<IFilterSchema<AttributeEnumValue>, AttributeEnumValueSchema>();
        services.AddSingleton<IFilterSchema<Category>, CategorySchema>();
        services.AddSingleton<IFilterSchema<CategoryLevelConstraint>, CategoryLevelConstraintSchema>();
        services.AddSingleton<IFilterSchema<InventoryMovementType>, InventoryMovementTypeSchema>();
        services.AddSingleton<IFilterSchema<Item>, ItemSchema>();
        services.AddSingleton<IFilterSchema<ItemType>, ItemTypeSchema>();
        services.AddSingleton<IFilterSchema<ShippingCompany>, ShippingCompanySchema>();
        services.AddSingleton<IFilterSchema<UnitOfMeasurementConversion>, UnitOfMeasurementConversionSchema>();
        services.AddSingleton<IFilterSchema<UnitOfMeasurement>, UnitOfMeasurementSchema>();
        services.AddSingleton<IFilterSchema<Domain.Entities.Warehouse>, WarehouseSchema>();
        services.AddSingleton<IFilterSchema<WarehouseLocation>, WarehouseLocationSchema>();
        services.AddSingleton<IFilterSchema<WarehouseType>, WarehouseTypeSchema>();

        services.AddScoped<IAttributeService, AttributeService>();
        services.AddScoped<IAttributeEnumValueService, AttributeEnumValueService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryAttributeRuleService, CategoryAttributeRuleService>();
        services.AddScoped<ICategoryLevelConstraintService, CategoryLevelConstraintService>();
        services.AddScoped<IInventoryMovementTypeService, InventoryMovementTypeService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IItemTypeService, ItemTypeService>();
        services.AddScoped<IMeasurementDimensionService, MeasurementDimensionService>();
        services.AddScoped<IShippingCompanyService, ShippingCompanyService>();
        services.AddScoped<IUnitOfMeasurementConversionService, UnitOfMeasurementConversionService>();
        services.AddScoped<IUnitOfMeasurementService, UnitOfMeasurementService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IWarehouseLocationService, WarehouseLocationService>();
        services.AddScoped<IWarehouseTypeService, WarehouseTypeService>();

        services.AddScoped<IExcelExportService, ExcelExportService<WarehouseResource>>();

        return services;
    }
}
