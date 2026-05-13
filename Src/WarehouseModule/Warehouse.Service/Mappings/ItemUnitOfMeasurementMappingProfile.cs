using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ItemUnitOfMeasurementMappingProfile : Profile
{
    public ItemUnitOfMeasurementMappingProfile()
    {
        CreateMap<ItemUnitOfMeasurement, ItemUnitOfMeasurementDto>()
            .ForCtorParam(
                nameof(ItemUnitOfMeasurementDto.UnitOfMeasurement),
                opt => opt.MapFrom(src => src.UnitOfMeasurement)
            );
        CreateMap<CreateItemUnitOfMeasurementDto, ItemUnitOfMeasurement>();
        CreateMap<PatchItemUnitOfMeasurementDto, ItemUnitOfMeasurement>().ReverseMap();
    }
}
