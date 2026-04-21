using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ItemWarehouseMappingProfile : Profile
{
    public ItemWarehouseMappingProfile()
    {
        CreateMap<ItemWarehouse, ItemWarehouseDto>();
        CreateMap<CreateItemWarehouseDto, ItemWarehouse>();
    }
}
