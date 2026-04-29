using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ItemMappingProfile : Profile
{
    public ItemMappingProfile()
    {
        CreateMap<Item, ItemDto>()
            .ForCtorParam(
                nameof(ItemDto.ItemAttributes),
                opt => opt.MapFrom(src => src.ItemAttributes.Select(s => s.Attribute))
            )
            .ForCtorParam(
                nameof(ItemDto.ItemUnitOfMeasurements),
                opt => opt.MapFrom(src => src.ItemUnitOfMeasurements.Select(s => s.UnitOfMeasurement))
            );
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
        CreateMap<CreateItemDto, Item>()
            .ForMember(dst => dst.ItemAttributes, opt => opt.Ignore())
            .ForMember(dst => dst.ItemUnitOfMeasurements, opt => opt.Ignore())
            .ForMember(dst => dst.ItemUnitOfMeasurementConversions, opt => opt.Ignore())
            .ForMember(dst => dst.ItemWarehouses, opt => opt.Ignore());
        CreateMap<PatchItemDto, Item>().ReverseMap();
    }
}
