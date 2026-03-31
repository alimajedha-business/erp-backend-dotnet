using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class MeasurementDimensionMappingProfile : Profile
{
    public MeasurementDimensionMappingProfile()
    {
        CreateMap<MeasurementDimension, MeasurementDimensionDto>();
        CreateMap<CreateMeasurementDimensionDto, MeasurementDimension>();
        CreateMap<PatchMeasurementDimensionDto, MeasurementDimension>().ReverseMap();
    }
}
