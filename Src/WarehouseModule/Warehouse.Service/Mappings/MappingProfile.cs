using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Attribute, AttributeDto>();
        CreateMap<CreateAttributeDto, Domain.Entities.Attribute>();
        CreateMap<PatchAttributeDto, Domain.Entities.Attribute>().ReverseMap();

        CreateMap<AttributeEnumValue, AttributeEnumValueDto>();
        CreateMap<CreateAttributeEnumValueDto, AttributeEnumValue>();
        CreateMap<PatchAttributeEnumValueDto, AttributeEnumValue>().ReverseMap();

        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<PatchCategoryDto, Category>().ReverseMap();

        CreateMap<Item, ItemDto>();
        CreateMap<CreateItemDto, Item>();
        CreateMap<PatchItemDto, Item>().ReverseMap();

        CreateMap<UnitOfMeasurement, UnitOfMeasurementDto>();
        CreateMap<CreateUnitOfMeasurementDto, UnitOfMeasurement>();
        CreateMap<PatchUnitOfMeasurementDto, UnitOfMeasurement>().ReverseMap();

        CreateMap<UnitOfMeasurementConversion, UnitOfMeasurementConversionDto>();
        CreateMap<CreateUnitOfMeasurementConversionDto, UnitOfMeasurementConversion>();
        CreateMap<PatchUnitOfMeasurementConversionDto, UnitOfMeasurementConversion>().ReverseMap();
    }
}
