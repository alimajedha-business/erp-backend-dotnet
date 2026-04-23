using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ItemUnitOfMeasurementConversionMappingProfile : Profile
{
    public ItemUnitOfMeasurementConversionMappingProfile()
    {
        CreateMap<ItemUnitOfMeasurementConversion, ItemUnitOfMeasurementConversionDto>();
        CreateMap<CreateItemUnitOfMeasurementConversionDto, ItemUnitOfMeasurementConversion>();
    }
}
