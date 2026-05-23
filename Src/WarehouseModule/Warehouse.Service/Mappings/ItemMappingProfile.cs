using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ItemMappingProfile : Profile
{
    public ItemMappingProfile()
    {
        CreateMap<Item, ItemSlimDto>();
        CreateMap<Item, ItemListDto>()
            .ForCtorParam(
                nameof(ItemListDto.UnitOfMeasurementTitle),
                opt => opt.MapFrom(src => src.ItemUnitOfMeasurements
                    .OrderBy(x => x.UnitOrder)
                    .Select(x => x.UnitOfMeasurement.Title)
                    .FirstOrDefault() ?? string.Empty)
            )
            .ForCtorParam(
                nameof(ItemListDto.ItemTypeTitle),
                opt => opt.MapFrom(src => src.ItemType.Title)
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

		CreateMap<Item, PatchItemDto>()
            .ForMember(
                dst => dst.AttributeIds,
                opt => opt.MapFrom(src => src.ItemAttributes.Select(x => x.AttributeId))
            )
            .ForMember(
                dst => dst.ItemWarehouses,
                opt => opt.MapFrom(src => src.ItemWarehouses.Select(x => new CreateItemWarehouseDto
                {
                    WarehouseId = x.WarehouseId,
                    ReorderPoint = x.ReorderPoint,
                    CriticalPoint = x.CriticalPoint,
                    ReorderQuantity = x.ReorderQuantity,
                    MaxStockLevel = x.MaxStockLevel,
                    LocationIds = x.ItemWarehouseLocations
                        .Select(l => l.WarehouseLocationId)
                        .ToList()
                }))
            )
            .ForMember(dst => dst.ItemUnitOfMeasurementConversions, opt => opt.Ignore());

        CreateMap<PatchItemDto, Item>()
            // ignore collection navigation props
            .ForMember(dst => dst.ItemAttributes, opt => opt.Ignore())
            .ForMember(dst => dst.ItemUnitOfMeasurements, opt => opt.Ignore())
            .ForMember(dst => dst.ItemWarehouses, opt => opt.Ignore())
            .ForMember(dst => dst.ItemUnitOfMeasurementConversions, opt => opt.Ignore())
            // ignore reference navigation props
            .ForMember(dst => dst.CompanyId, opt => opt.Ignore())
            .ForMember(dst => dst.CategoryId, opt => opt.Ignore());
    }
}
