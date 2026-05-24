using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Domain.EntitySchemas;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.EntitySchemas;
using NGErp.Warehouse.Service.Mappings;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;
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

        services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();

        services.AddSingleton<IFilterSchema<Domain.Entities.Attribute>, AttributeSchema>();
        services.AddSingleton<IFilterSchema<AttributeEnumValue>, AttributeEnumValueSchema>();
        services.AddSingleton<IFilterSchema<Category>, CategorySchema>();
        services.AddSingleton<IFilterSchema<CategoryLevelConstraint>, CategoryLevelConstraintSchema>();
        services.AddSingleton<IFilterSchema<FeatureSettings>, FeatureSettingsSchema>();
        services.AddSingleton<IFilterSchema<InventoryMovementType>, InventoryMovementTypeSchema>();
        services.AddSingleton<IFilterSchema<Item>, ItemSchema>();
        services.AddSingleton<IFilterSchema<ItemAttribute>, ItemAttributeSchema>();
        services.AddSingleton<IFilterSchema<ItemType>, ItemTypeSchema>();
        services.AddSingleton<IFilterSchema<ItemUnitOfMeasurement>, ItemUnitOfMeasurementSchema>();
        services.AddSingleton<IFilterSchema<RemittanceType>, RemittanceTypeSchema>();
        services.AddSingleton<IFilterSchema<Receipt>, ReceiptSchema>();
        services.AddSingleton<IFilterSchema<ReceiptFieldDefinition>, ReceiptFieldDefinitionSchema>();
        services.AddSingleton<IFilterSchema<ReceiptSourceOfSupply>, ReceiptSourceOfSupplySchema>();
        services.AddSingleton<IFilterSchema<ReceiptType>, ReceiptTypeSchema>();
        services.AddSingleton<IFilterSchema<ReceiptTypeConfiguration>, ReceiptTypeConfigurationSchema>();
        services.AddSingleton<
            IFilterSchema<ReceiptTypeFieldConfiguration>,
            ReceiptTypeFieldConfigurationSchema
        >();
        services.AddSingleton<IFilterSchema<ShippingCompany>, ShippingCompanySchema>();
        services.AddSingleton<IFilterSchema<SiUnit>, SiUnitSchema>();
        services.AddSingleton<IFilterSchema<UnitOfMeasurement>, UnitOfMeasurementSchema>();
        services.AddSingleton<IFilterSchema<Domain.Entities.Warehouse>, WarehouseSchema>();
        services.AddSingleton<IFilterSchema<WarehouseLocation>, WarehouseLocationSchema>();
        services.AddSingleton<IFilterSchema<WarehouseType>, WarehouseTypeSchema>();

        services.AddScoped<IAttributeService, AttributeService>();
        services.AddScoped<IAttributeEnumValueService, AttributeEnumValueService>();
        services.AddScoped<IAttributeBusinessRuleValidator, AttributeBusinessRuleValidator>();
        services.AddScoped<ICategoryBusinessRuleValidator, CategoryBusinessRuleValidator>();
        services.AddScoped<
            ICategoryLevelConstraintBusinessRuleValidator,
            CategoryLevelConstraintBusinessRuleValidator
        >();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryAttributeRuleService, CategoryAttributeRuleService>();
        services.AddScoped<ICategoryLevelConstraintService, CategoryLevelConstraintService>();
        services.AddScoped<IFeatureSettingsService, FeatureSettingsService>();
        services.AddScoped<IInventoryMovementTypeService, InventoryMovementTypeService>();
        services.AddScoped<IItemBusinessRuleValidator, ItemBusinessRuleValidator>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IItemTypeService, ItemTypeService>();
        services.AddScoped<IRemittanceTypeBusinessRuleValidator, RemittanceTypeBusinessRuleValidator>();
        services.AddScoped<IRemittanceTypeService, RemittanceTypeService>();
        services.AddScoped<IReceiptBusinessRuleValidator, ReceiptBusinessRuleValidator>();
        services.AddScoped<IReceiptFieldDefinitionService, ReceiptFieldDefinitionService>();
        services.AddScoped<IReceiptFieldValueService, ReceiptFieldValueService>();
        services.AddScoped<
            IReceiptSourceOfSupplyBusinessRuleValidator,
            ReceiptSourceOfSupplyBusinessRuleValidator
        >();
        services.AddScoped<IReceiptSourceOfSupplyService, ReceiptSourceOfSupplyService>();
        services.AddScoped<IReceiptInventoryProjectionService, ReceiptInventoryProjectionService>();
        services.AddScoped<IReceiptLineContextService, ReceiptLineContextService>();
        services.AddScoped<IReceiptTypeBusinessRuleValidator, ReceiptTypeBusinessRuleValidator>();
        services.AddScoped<
            IReceiptTypeConfigurationBusinessRuleValidator,
            ReceiptTypeConfigurationBusinessRuleValidator
        >();
        services.AddScoped<IReceiptTypeConfigurationService, ReceiptTypeConfigurationService>();
        services.AddScoped<
            IReceiptTypeFieldConfigurationService,
            ReceiptTypeFieldConfigurationService
        >();
        services.AddScoped<IReceiptService, ReceiptService>();
        services.AddScoped<IReceiptTypeService, ReceiptTypeService>();
        services.AddScoped<IShippingCompanyService, ShippingCompanyService>();
        services.AddScoped<ISiUnitService, SiUnitService>();
        services.AddScoped<IUnitOfMeasurementService, UnitOfMeasurementService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IWarehouseLocationService, WarehouseLocationService>();
        services.AddScoped<IWarehouseBusinessRuleValidator, WarehouseBusinessRuleValidator>();
        services.AddScoped<
            IWarehouseLocationBusinessRuleValidator,
            WarehouseLocationBusinessRuleValidator
        >();
        services.AddScoped<IWarehouseTypeBusinessRuleValidator, WarehouseTypeBusinessRuleValidator>();
        services.AddScoped<IWarehouseTypeService, WarehouseTypeService>();

        services.AddScoped<IExcelExportService, ExcelExportService<WarehouseResource>>();

        return services;
    }
}
