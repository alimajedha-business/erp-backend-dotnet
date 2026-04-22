using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class WarehouseTypeMappingProfile : Profile
{
    public WarehouseTypeMappingProfile()
    {
        CreateMap<WarehouseType, WarehouseTypeDto>();
        CreateMap<WarehouseType, WarehouseTypeSlimDto>();
        CreateMap<CreateWarehouseTypeDto, WarehouseType>();
        CreateMap<PatchWarehouseTypeDto, WarehouseType>().ReverseMap();
    }
}
