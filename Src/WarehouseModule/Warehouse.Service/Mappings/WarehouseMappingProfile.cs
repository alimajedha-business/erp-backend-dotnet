using AutoMapper;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class WarehouseMappingProfile : Profile
{
    public WarehouseMappingProfile()
    {
        CreateMap<Domain.Entities.Warehouse, WarehouseDto>();
        CreateMap<Domain.Entities.Warehouse, WarehouseListDto>()
            .ForCtorParam(
                nameof(WarehouseListDto.WarehouseTypeTitle),
                opt => opt.MapFrom(src => src.WarehouseType.Title)
            )
            .ForCtorParam(
                nameof(WarehouseListDto.CompanyUnitTitle),
                opt => opt.MapFrom(src => src.CompanyUnit.Name)
            );
        CreateMap<CreateWarehouseDto, Domain.Entities.Warehouse>();
        CreateMap<PatchWarehouseDto, Domain.Entities.Warehouse>().ReverseMap();
    }
}
