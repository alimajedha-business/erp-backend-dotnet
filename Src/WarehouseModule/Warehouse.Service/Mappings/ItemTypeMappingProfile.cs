using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

internal class ItemTypeMappingProfile : Profile
{
    public ItemTypeMappingProfile()
    {
        CreateMap<ItemType, ItemTypeDto>();
        CreateMap<CreateItemTypeDto, ItemType>();
        CreateMap<PatchItemTypeDto, ItemType>().ReverseMap();
    }
}
