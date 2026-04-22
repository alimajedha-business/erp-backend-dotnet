using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class WarehouseLocationMappingProfile : Profile
{
    public WarehouseLocationMappingProfile()
    {
        CreateMap<WarehouseLocation, WarehouseLocationDto>();
        CreateMap<WarehouseLocation, WarehouseLocationSlimDto>();
        CreateMap<WarehouseLocation, WarehouseLocationListDto>()
            .ForCtorParam(
                nameof(WarehouseLocationListDto.WarehouseTitle),
                opt => opt.MapFrom(src => src.Warehouse.Title)
            );
        CreateMap<CreateWarehouseLocationDto, WarehouseLocation>();
        CreateMap<PatchWarehouseLocationDto, WarehouseLocation>().ReverseMap();
    }
}
