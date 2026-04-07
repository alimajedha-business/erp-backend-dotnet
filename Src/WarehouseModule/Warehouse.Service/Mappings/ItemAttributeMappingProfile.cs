using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ItemAttributeMappingProfile : Profile
{
    public ItemAttributeMappingProfile()
    {
        CreateMap<ItemAttribute, ItemAttributeDto>();
        CreateMap<CreateItemAttributeDto, ItemAttribute>();
        CreateMap<PatchItemAttributeDto, ItemAttribute>().ReverseMap();
    }
}
