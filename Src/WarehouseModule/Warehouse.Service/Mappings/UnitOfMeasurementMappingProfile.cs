using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class UnitOfMeasurementMappingProfile : Profile
{
    public UnitOfMeasurementMappingProfile()
    {
        CreateMap<UnitOfMeasurement, UnitOfMeasurementDto>();
        CreateMap<UnitOfMeasurement, UnitOfMeasurementSlimDto>();
        CreateMap<UnitOfMeasurement, UnitOfMeasurementListDto>()
            .ForCtorParam(
                nameof(UnitOfMeasurementListDto.MeasurementDimensionTitle),
                opt => opt.MapFrom(src => src.MeasurementDimension.Title)
            );
        CreateMap<CreateUnitOfMeasurementDto, UnitOfMeasurement>();
        CreateMap<PatchUnitOfMeasurementDto, UnitOfMeasurement>().ReverseMap();
    }
}
