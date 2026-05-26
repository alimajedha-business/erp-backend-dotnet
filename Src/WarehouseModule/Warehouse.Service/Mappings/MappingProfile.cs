using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.Service.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AttributeEnumValue, AttributeEnumValueDto>();
        CreateMap<AttributeEnumValue, AttributeEnumValueSlimDto>();
        CreateMap<AttributeEnumValue, AttributeEnumValueListDto>()
            .ForCtorParam(
                nameof(AttributeEnumValueListDto.AttributeTitle),
                opt => opt.MapFrom(src => src.Attribute.Title)
            );
        CreateMap<CreateAttributeEnumValueDto, AttributeEnumValue>();
        CreateMap<PatchAttributeEnumValueDto, AttributeEnumValue>().ReverseMap();

        CreateMap<Domain.Entities.Attribute, AttributeDto>()
            .ForCtorParam(
                nameof(AttributeDto.DataTypeDescription),
                opt => opt.MapFrom(src => AttributeDto.GetDataTypeDescription(src.DataType))
            )
            .ForCtorParam(
                nameof(AttributeDto.AttributeEntityDescription),
                opt => opt.MapFrom(src => AttributeDto.GetEntityDescription(src.AttributeEntity))
            );
        CreateMap<Domain.Entities.Attribute, AttributeSlimDto>();
        CreateMap<CreateAttributeDto, Domain.Entities.Attribute>();
        CreateMap<PatchAttributeDto, Domain.Entities.Attribute>().ReverseMap();

        CreateMap<CategoryAttributeRule, CategoryAttributeRuleDto>();
        CreateMap<CategoryAttributeRule, CategoryAttributeRuleSlimDto>();
        CreateMap<CategoryAttributeRule, CategoryAttributeRuleListDto>()
            .ForCtorParam(
                nameof(CategoryAttributeRuleListDto.CategoryTitle),
                opt => opt.MapFrom(src => src.Category.Title)
            )
            .ForCtorParam(
                nameof(CategoryAttributeRuleListDto.AttributeTitle),
                opt => opt.MapFrom(src => src.Attribute.Title)
            );
        CreateMap<CreateCategoryAttributeRuleDto, CategoryAttributeRule>();
        CreateMap<PatchCategoryAttributeRuleDto, CategoryAttributeRule>().ReverseMap();

        CreateMap<CategoryLevelConstraint, CategoryLevelConstraintDto>();
        CreateMap<CreateCategoryLevelConstraintDto, CategoryLevelConstraint>();
        CreateMap<PatchCategoryLevelConstraintDto, CategoryLevelConstraint>().ReverseMap();

        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategorySlimDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<PatchCategoryDto, Category>().ReverseMap();

        CreateMap<FeatureSettings, FeatureSettingsDto>()
            .ForCtorParam(
                nameof(FeatureSettingsDto.WarehouseSerialRuleDescription),
                opt => opt.MapFrom(src => FeatureSettingsDto
                    .GetWarehouseSerialRuleDescription(src.WarehouseSerialRule)
                )
            );
        CreateMap<CreateFeatureSettingsDto, FeatureSettings>();
        CreateMap<PatchFeatureSettingsDto, FeatureSettings>().ReverseMap();

        CreateMap<InventoryMovementType, InventoryMovementTypeDto>();
        CreateMap<CreateInventoryMovementTypeDto, InventoryMovementType>();
        CreateMap<PatchInventoryMovementTypeDto, InventoryMovementType>().ReverseMap();

        CreateMap<ItemAttribute, ItemAttributeDto>();
        CreateMap<CreateItemAttributeDto, ItemAttribute>();
        CreateMap<PatchItemAttributeDto, ItemAttribute>().ReverseMap();

        CreateMap<Item, ItemSlimDto>();
        CreateMap<Item, ItemListDto>()
            .ForCtorParam(
                nameof(ItemListDto.UnitOfMeasurementTitle),
                opt => opt.MapFrom(src => src.ItemUnitOfMeasurements
                    .OrderBy(x => x.UnitOrder)
                    .Select(x => x.UnitOfMeasurement.Title)
                    .FirstOrDefault() ?? string.Empty)
            )
            .ForCtorParam(
                nameof(ItemListDto.ItemTypeTitle),
                opt => opt.MapFrom(src => src.ItemType.Title)
            )
            .ForCtorParam(
                nameof(ItemListDto.CategoryTitle),
                opt => opt.MapFrom(src => src.Category.Title)
            );

        CreateMap<CreateItemDto, Item>()
            .ForMember(dst => dst.ItemAttributes, opt => opt.Ignore())
            .ForMember(dst => dst.ItemUnitOfMeasurements, opt => opt.Ignore())
            .ForMember(dst => dst.ItemUnitOfMeasurementConversions, opt => opt.Ignore())
            .ForMember(dst => dst.ItemWarehouses, opt => opt.Ignore());

        CreateMap<Item, PatchItemDto>()
            .ForMember(
                dst => dst.AttributeIds,
                opt => opt.MapFrom(src => src.ItemAttributes.Select(x => x.AttributeId))
            )
            .ForMember(
                dst => dst.ItemWarehouses,
                opt => opt.MapFrom(src => src.ItemWarehouses.Select(x => new CreateItemWarehouseDto
                {
                    WarehouseId = x.WarehouseId,
                    ReorderPoint = x.ReorderPoint,
                    CriticalPoint = x.CriticalPoint,
                    ReorderQuantity = x.ReorderQuantity,
                    MaxStockLevel = x.MaxStockLevel,
                    LocationIds = x.ItemWarehouseLocations
                        .Select(l => l.WarehouseLocationId)
                        .ToList()
                }))
            )
            .ForMember(dst => dst.ItemUnitOfMeasurementConversions, opt => opt.Ignore());

        CreateMap<PatchItemDto, Item>()
            .ForMember(dst => dst.ItemAttributes, opt => opt.Ignore())
            .ForMember(dst => dst.ItemUnitOfMeasurements, opt => opt.Ignore())
            .ForMember(dst => dst.ItemWarehouses, opt => opt.Ignore())
            .ForMember(dst => dst.ItemUnitOfMeasurementConversions, opt => opt.Ignore())
            .ForMember(dst => dst.CompanyId, opt => opt.Ignore())
            .ForMember(dst => dst.CategoryId, opt => opt.Ignore());

        CreateMap<ItemType, ItemTypeDto>();
        CreateMap<ItemType, ItemTypeSlimDto>();
        CreateMap<CreateItemTypeDto, ItemType>();
        CreateMap<PatchItemTypeDto, ItemType>().ReverseMap();

        CreateMap<ItemUnitOfMeasurement, ItemUnitOfMeasurementDto>()
            .ForCtorParam(
                nameof(ItemUnitOfMeasurementDto.UnitOfMeasurement),
                opt => opt.MapFrom(src => src.UnitOfMeasurement)
            );
        CreateMap<CreateItemUnitOfMeasurementDto, ItemUnitOfMeasurement>();
        CreateMap<PatchItemUnitOfMeasurementDto, ItemUnitOfMeasurement>().ReverseMap();

        CreateMap<ItemWarehouse, ItemWarehouseDto>()
            .ForCtorParam(
                nameof(ItemWarehouseDto.Locations),
                opt => opt.MapFrom(src => src.ItemWarehouseLocations.Select(s => s.WarehouseLocation))
            );
        CreateMap<CreateItemWarehouseDto, ItemWarehouse>();

        CreateMap<ReceiptFieldDefinition, ReceiptFieldDefinitionDto>()
            .ForCtorParam(
                nameof(ReceiptFieldDefinitionDto.DataTypeDescription),
                opt => opt.MapFrom(src =>
                    ReceiptFieldDefinitionDto.GetDataTypeDescription(src.DataType)
                )
            )
            .ForCtorParam(
                nameof(ReceiptFieldDefinitionDto.AllowedPlacementDescription),
                opt => opt.MapFrom(src =>
                    ReceiptFieldDefinitionDto.GetPlacementDescription(src.AllowedPlacement)
                )
            );
        CreateMap<ReceiptFieldDefinition, ReceiptFieldDefinitionListDto>();

        CreateMap<ReceiptFieldValue, ReceiptFieldValueDto>();
        CreateMap<ReceiptFieldValue, ReceiptHeaderFieldValueListDto>()
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.FieldDefinitionTitle),
                opt => opt.MapFrom(src => src.FieldDefinition.Title)
            )
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.FieldDefinitionKey),
                opt => opt.MapFrom(src => src.FieldDefinition.Key)
            )
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.DataType),
                opt => opt.MapFrom(src => src.FieldDefinition.DataType)
            )
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.DataTypeDescription),
                opt => opt.MapFrom(src =>
                    ReceiptFieldDefinitionDto.GetDataTypeDescription(src.FieldDefinition.DataType)
                )
            )
            .ForCtorParam(
                nameof(ReceiptHeaderFieldValueListDto.Value),
                opt => opt.MapFrom(src => GetReceiptFieldValue(src))
            );
        CreateMap<CreateReceiptFieldValueDto, ReceiptFieldValue>();

        CreateMap<ReceiptSourceOfSupply, ReceiptFieldValueReferenceDto>()
            .ForCtorParam(
                nameof(ReceiptFieldValueReferenceDto.Code),
                opt => opt.MapFrom(src => src.Code)
            )
            .ForCtorParam(
                nameof(ReceiptFieldValueReferenceDto.Title),
                opt => opt.MapFrom(src => src.Title)
            );

        CreateMap<ReceiptLineAttributeValue, ReceiptLineAttributeValueDto>();
        CreateMap<CreateReceiptLineAttributeValueDto, ReceiptLineAttributeValue>();

        CreateMap<ReceiptLine, ReceiptLineDto>()
            .ForCtorParam(
                nameof(ReceiptLineDto.Weight),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(
                    src.Weight,
                    src.PreferredMassUnit
                ))
            )
            .ForCtorParam(
                nameof(ReceiptLineDto.Volume),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(
                    src.Volume,
                    src.PreferredVolumeUnit
                ))
            );
        CreateMap<CreateReceiptLineDto, ReceiptLine>()
            .ForMember(d => d.ReceiptLineMeasurementValues, o => o.Ignore())
            .ForMember(d => d.ReceiptLineAttributeValues, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());

        CreateMap<ReceiptLineMeasurementValue, ReceiptLineMeasurementValueDto>();
        CreateMap<CreateReceiptLineMeasurementValueDto, ReceiptLineMeasurementValue>();

        CreateMap<Receipt, ReceiptDto>();
        CreateMap<Receipt, ReceiptListDto>()
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptTypeTitle),
                opt => opt.MapFrom(src => src.ReceiptType.Title)
            )
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptLineCount),
                opt => opt.MapFrom(src => src.ReceiptLines.Count)
            )
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptFieldValues),
                opt => opt.MapFrom(src => src.ReceiptFieldValues
                    .Where(e => e.ReceiptLineId == null))
            );
        CreateMap<CreateReceiptDto, Receipt>()
            .ForMember(d => d.Number, o => o.MapFrom(s => s.Number!))
            .ForMember(d => d.ReceiptLines, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());

        CreateMap<ReceiptSourceOfSupply, ReceiptSourceOfSupplyDto>();
        CreateMap<ReceiptSourceOfSupply, ReceiptSourceOfSupplySlimDto>();
        CreateMap<CreateReceiptSourceOfSupplyDto, ReceiptSourceOfSupply>();
        CreateMap<PatchReceiptSourceOfSupplyDto, ReceiptSourceOfSupply>().ReverseMap();

        CreateMap<ReceiptTypeConfiguration, ReceiptTypeConfigurationDto>();
        CreateMap<ReceiptTypeConfiguration, ReceiptTypeConfigurationListDto>()
            .ForCtorParam(
                nameof(ReceiptTypeConfigurationListDto.ReceiptTypeTitle),
                opt => opt.MapFrom(src => src.ReceiptType.Title)
            )
            .ForCtorParam(
                nameof(ReceiptTypeConfigurationListDto.FieldConfigurationCount),
                opt => opt.MapFrom(src => src.FieldConfigurations.Count)
            );
        CreateMap<CreateReceiptTypeConfigurationDto, ReceiptTypeConfiguration>();

        CreateMap<ReceiptTypeFieldConfiguration, ReceiptTypeFieldConfigurationDto>()
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.ReceiptTypeId),
                opt => opt.MapFrom(src => src.ReceiptTypeConfiguration.ReceiptTypeId)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationDto.PlacementDescription),
                opt => opt.MapFrom(src =>
                    ReceiptTypeFieldConfigurationDto.GetPlacementDescription(src.Placement)
                )
            );

        CreateMap<ReceiptTypeFieldConfiguration, ReceiptTypeFieldConfigurationListDto>()
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationListDto.FieldDefinitionKey),
                opt => opt.MapFrom(src => src.FieldDefinition.Key)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationListDto.FieldDefinitionTitle),
                opt => opt.MapFrom(src => src.FieldDefinition.Title)
            )
            .ForCtorParam(
                nameof(ReceiptTypeFieldConfigurationListDto.PlacementDescription),
                opt => opt.MapFrom(src =>
                    ReceiptTypeFieldConfigurationDto.GetPlacementDescription(src.Placement)
                )
            );
        CreateMap<CreateReceiptTypeFieldConfigurationDto, ReceiptTypeFieldConfiguration>();
        CreateMap<PatchReceiptTypeFieldConfigurationDto, ReceiptTypeFieldConfiguration>()
            .ReverseMap();

        CreateMap<ReceiptType, ReceiptTypeDto>();
        CreateMap<ReceiptType, ReceiptTypeSlimDto>();
        CreateMap<CreateReceiptTypeDto, ReceiptType>();
        CreateMap<PatchReceiptTypeDto, ReceiptType>().ReverseMap();

        CreateMap<RemittanceType, RemittanceTypeDto>();
        CreateMap<RemittanceType, RemittanceTypeSlimDto>();
        CreateMap<CreateRemittanceTypeDto, RemittanceType>();
        CreateMap<PatchRemittanceTypeDto, RemittanceType>().ReverseMap();

        CreateMap<ShippingCompany, ShippingCompanyDto>();
        CreateMap<ShippingCompany, ShippingCompanySlimDto>();
        CreateMap<ShippingCompany, ShippingCompanyListDto>()
            .ForCtorParam(
                nameof(ShippingCompanyListDto.ManagerFullName),
                opt => opt.MapFrom(src => src.ManagerFirstName + ' ' + src.ManagerLastName)
            );
        CreateMap<CreateShippingCompanyDto, ShippingCompany>();
        CreateMap<PatchShippingCompanyDto, ShippingCompany>().ReverseMap();

        CreateMap<SiUnit, SiUnitDto>()
            .ForCtorParam(
                nameof(SiUnitDto.UnitDimensionTitle),
                opt => opt.MapFrom(src => SiUnitDto.GetUnitDimensionDescription(src.UnitDimension))
            );
        CreateMap<SiUnit, SiUnitSlimDto>()
            .ForCtorParam(
                nameof(SiUnitSlimDto.UnitDimensionTitle),
                opt => opt.MapFrom(src => SiUnitDto.GetUnitDimensionDescription(src.UnitDimension))
            );
        CreateMap<SiUnit, SiUnitAsReferenceDto>();

        CreateMap<UnitOfMeasurement, UnitOfMeasurementDto>();
        CreateMap<UnitOfMeasurement, UnitOfMeasurementSlimDto>();
        CreateMap<CreateUnitOfMeasurementDto, UnitOfMeasurement>();
        CreateMap<PatchUnitOfMeasurementDto, UnitOfMeasurement>().ReverseMap();

        CreateMap<WarehouseLocation, WarehouseLocationDto>()
            .ForCtorParam(
                nameof(WarehouseLocationDto.Length),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Length, src.PreferredLengthUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationDto.Width),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Width, src.PreferredLengthUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationDto.Height),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Height, src.PreferredLengthUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationDto.MaxMass),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.MaxMass, src.PreferredMassUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationDto.MaxVolume),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.MaxVolume, src.PreferredVolumeUnit)
                )
            );
        CreateMap<WarehouseLocation, WarehouseLocationSlimDto>();
        CreateMap<WarehouseLocation, WarehouseLocationListDto>()
            .ForCtorParam(
                nameof(WarehouseLocationListDto.Length),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Length, src.PreferredLengthUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.Width),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Width, src.PreferredLengthUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.Height),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Height, src.PreferredLengthUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.MaxMass),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.MaxMass, src.PreferredMassUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.MaxVolume),
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.MaxVolume, src.PreferredVolumeUnit)
                )
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.WarehouseTitle),
                opt => opt.MapFrom(src => src.Warehouse.Title)
            );
        CreateMap<CreateWarehouseLocationDto, WarehouseLocation>();
        CreateMap<WarehouseLocation, PatchWarehouseLocationDto>()
            .ForMember(
                d => d.Length,
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Length, src.PreferredLengthUnit)
                )
            )
            .ForMember(
                d => d.Width,
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Width, src.PreferredLengthUnit)
                )
            )
            .ForMember(
                d => d.Height,
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.Height, src.PreferredLengthUnit)
                )
            )
            .ForMember(
                d => d.MaxMass,
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.MaxMass, src.PreferredMassUnit)
                )
            )
            .ForMember(
                d => d.MaxVolume,
                opt => opt.MapFrom(src =>
                    MeasurementUnitConverter.ConvertFromBase(src.MaxVolume, src.PreferredVolumeUnit)
                )
            );
        CreateMap<PatchWarehouseLocationDto, WarehouseLocation>();

        CreateMap<Domain.Entities.Warehouse, WarehouseDto>();
        CreateMap<Domain.Entities.Warehouse, WarehouseSlimDto>();
        CreateMap<Domain.Entities.Warehouse, WarehouseListDto>()
            .ForCtorParam(
                nameof(WarehouseListDto.WarehouseTypeTitle),
                opt => opt.MapFrom(src => src.WarehouseType.Title)
            )
            .ForCtorParam(
                nameof(WarehouseListDto.CompanyUnitTitle),
                opt => opt.MapFrom(src => src.CompanyUnit.Name)
            );
        CreateMap<CreateWarehouseDto, Domain.Entities.Warehouse>();
        CreateMap<PatchWarehouseDto, Domain.Entities.Warehouse>().ReverseMap();

        CreateMap<WarehouseType, WarehouseTypeDto>();
        CreateMap<WarehouseType, WarehouseTypeSlimDto>();
        CreateMap<CreateWarehouseTypeDto, WarehouseType>();
        CreateMap<PatchWarehouseTypeDto, WarehouseType>().ReverseMap();
    }

    private static object? GetReceiptFieldValue(ReceiptFieldValue fieldValue)
    {
        return fieldValue.FieldDefinition.DataType switch
        {
            ReceiptFieldDataType.String => fieldValue.StringValue,
            ReceiptFieldDataType.Integer => fieldValue.IntegerValue,
            ReceiptFieldDataType.Decimal => fieldValue.DecimalValue,
            ReceiptFieldDataType.Date => fieldValue.DateValue,
            ReceiptFieldDataType.Boolean => fieldValue.BooleanValue,
            ReceiptFieldDataType.Reference => fieldValue.ReferenceDisplayValue,
            _ => null
        };
    }
}
