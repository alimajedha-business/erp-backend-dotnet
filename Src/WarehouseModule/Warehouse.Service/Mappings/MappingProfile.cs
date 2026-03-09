using AutoMapper;

using Microsoft.AspNetCore.Mvc.TagHelpers;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Attribute, AttributeDto>();
        CreateMap<Domain.Entities.Attribute, AttributeSlimDto>();
        CreateMap<CreateAttributeDto, Domain.Entities.Attribute>();
        CreateMap<PatchAttributeDto, Domain.Entities.Attribute>().ReverseMap();

        CreateMap<AttributeEnumValue, AttributeEnumValueDto>();
        CreateMap<AttributeEnumValue, AttributeEnumValueListDto>()
            .ForCtorParam(
                nameof(AttributeEnumValueListDto.AttributeTitle),
                opt => opt.MapFrom(src => src.Attribute.Title)
            );
        CreateMap<CreateAttributeEnumValueDto, AttributeEnumValue>();
        CreateMap<PatchAttributeEnumValueDto, AttributeEnumValue>().ReverseMap();

        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategorySlimDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<PatchCategoryDto, Category>().ReverseMap();

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

        CreateMap<InventoryMovementType, InventoryMovementTypeDto>();
        CreateMap<CreateInventoryMovementTypeDto, InventoryMovementType>();
        CreateMap<PatchInventoryMovementTypeDto, InventoryMovementType>().ReverseMap();

        CreateMap<Item, ItemDto>();
        CreateMap<CreateItemDto, Item>();
        CreateMap<PatchItemDto, Item>().ReverseMap();

        CreateMap<MeasurementDimension, MeasurementDimensionDto>();
        CreateMap<CreateMeasurementDimensionDto, MeasurementDimension>();
        CreateMap<PatchMeasurementDimensionDto, MeasurementDimension>().ReverseMap();

        CreateMap<UnitOfMeasurement, UnitOfMeasurementDto>();
        CreateMap<UnitOfMeasurement, UnitOfMeasurementTitleDto>();
        CreateMap<CreateUnitOfMeasurementDto, UnitOfMeasurement>();
        CreateMap<PatchUnitOfMeasurementDto, UnitOfMeasurement>().ReverseMap();

        CreateMap<UnitOfMeasurementConversion, UnitOfMeasurementConversionDto>();
        CreateMap<UnitOfMeasurementConversion, UnitOfMeasurementConversionSlimDto>();
        CreateMap<UnitOfMeasurementConversion, UnitOfMeasurementConversionListDto>()
            .ForCtorParam(
                nameof(UnitOfMeasurementConversionListDto.FromUnitOfMeasurementTitle), 
                opt => opt.MapFrom(src => src.FromUnitOfMeasurement.Title)
            )
            .ForCtorParam(
                nameof(UnitOfMeasurementConversionListDto.ToUnitOfMeasurementTitle), 
                opt => opt.MapFrom(src => src.ToUnitOfMeasurement.Title)
            );
        CreateMap<CreateUnitOfMeasurementConversionDto, UnitOfMeasurementConversion>();
        CreateMap<PatchUnitOfMeasurementConversionDto, UnitOfMeasurementConversion>().ReverseMap();

        CreateMap<Domain.Entities.Warehouse, WarehouseDto>();
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
        CreateMap<CreateWarehouseTypeDto, WarehouseType>();
        CreateMap<PatchWarehouseTypeDto, WarehouseType>().ReverseMap();
    }
}
