using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class UnitOfMeasurementMappingProfile : Profile
{
    public UnitOfMeasurementMappingProfile()
    {
        CreateMap<UnitOfMeasurement, UnitOfMeasurementDto>();
        CreateMap<UnitOfMeasurement, UnitOfMeasurementListDto>();
        CreateMap<CreateUnitOfMeasurementDto, UnitOfMeasurement>();
        CreateMap<PatchUnitOfMeasurementDto, UnitOfMeasurement>().ReverseMap();
    }
}
