using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class UnitOfMeasurementConversionMappingProfile : Profile
{
    public UnitOfMeasurementConversionMappingProfile()
    {
        CreateMap<UnitOfMeasurementConversion, UnitOfMeasurementConversionDto>();
        CreateMap<UnitOfMeasurementConversion, UnitOfMeasurementConversionSlimDto>();
        CreateMap<UnitOfMeasurementConversion, UnitOfMeasurementConversionListDto>()
            .ForCtorParam(
                nameof(UnitOfMeasurementConversionListDto.FromUnitOfMeasurementTitle),
                opt => opt.MapFrom(src => src.FromUnitOfMeasurement.Title)
            )
            .ForCtorParam(
                nameof(UnitOfMeasurementConversionListDto.ToUnitOfMeasurementTitle),
                opt => opt.MapFrom(src => src.ToUnitOfMeasurement.Title)
            );
        CreateMap<CreateUnitOfMeasurementConversionDto, UnitOfMeasurementConversion>();
        CreateMap<PatchUnitOfMeasurementConversionDto, UnitOfMeasurementConversion>().ReverseMap();
    }
}
