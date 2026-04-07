using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ItemMappingProfile : Profile
{
    public ItemMappingProfile()
    {
        CreateMap<Item, ItemDto>();
        CreateMap<Item, ItemListDto>()
            .ForCtorParam(
                nameof(ItemListDto.ItemTypeTitle),
                opt => opt.MapFrom(src => src.ItemType.Title)
            )
            .ForCtorParam(
                nameof(ItemListDto.PrimaryUnitOfMeasurementTitle),
                opt => opt.MapFrom(src => src.PrimaryUnitOfMeasurement.Title)
            )
            .ForCtorParam(
                nameof(ItemListDto.CategoryTitle),
                opt => opt.MapFrom(src => src.Category.Title)
            );
        CreateMap<CreateItemDto, Item>();
        CreateMap<PatchItemDto, Item>().ReverseMap();
    }
}
