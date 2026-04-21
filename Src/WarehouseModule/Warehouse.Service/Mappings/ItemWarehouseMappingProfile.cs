using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ItemWarehouseMappingProfile : Profile
{
    public ItemWarehouseMappingProfile()
    {
        CreateMap<ItemWarehouse, ItemWarehouseDto>()
            .ForCtorParam(
                nameof(ItemWarehouseDto.Locations),
                opt => opt.MapFrom(src => src.ItemWarehouseLocations.Select(s => s.WarehouseLocation))
            );
        CreateMap<CreateItemWarehouseDto, ItemWarehouse>();
    }
}
