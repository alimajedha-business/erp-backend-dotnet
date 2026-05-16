using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class SiUnitMappingProfile : Profile
{
    public SiUnitMappingProfile()
    {
        CreateMap<SiUnit, SiUnitDto>()
            .ForCtorParam(
                nameof(SiUnitDto.UnitDimensionTitle),
                opt => opt.MapFrom(src => SiUnitDto.GetUnitDimensionDescription(src.UnitDimension))
            );
        CreateMap<SiUnit, SiUnitSlimDto>().ForCtorParam(
                nameof(SiUnitSlimDto.UnitDimensionTitle),
                opt => opt.MapFrom(src => SiUnitDto.GetUnitDimensionDescription(src.UnitDimension))
            );
        CreateMap<SiUnit, SiUnitAsReferenceDto>();
    }
}
