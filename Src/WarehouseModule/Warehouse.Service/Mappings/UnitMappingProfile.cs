using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class UnitMappingProfile : Profile
{
    public UnitMappingProfile()
    {
        CreateMap<Unit, UnitDto>()
            .ForCtorParam(
                nameof(UnitDto.UnitDimensionTitle),
                opt => opt.MapFrom(src => UnitDto.GetUnitDimensionDescription(src.UnitDimension))
            );
        CreateMap<Unit, UnitSlimDto>().ForCtorParam(
                nameof(UnitSlimDto.UnitDimensionTitle),
                opt => opt.MapFrom(src => UnitDto.GetUnitDimensionDescription(src.UnitDimension))
            );
        CreateMap<Unit, UnitAsReferenceDto>();
    }
}
