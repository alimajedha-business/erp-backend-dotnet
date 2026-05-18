using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.Service.Mappings;

public class WarehouseLocationMappingProfile : Profile
{
    public WarehouseLocationMappingProfile()
    {
        CreateMap<WarehouseLocation, WarehouseLocationDto>()
            .ForCtorParam(
                nameof(WarehouseLocationDto.Length),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Length, src.PreferredLengthUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationDto.Width),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Width, src.PreferredLengthUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationDto.Height),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Height, src.PreferredLengthUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationDto.MaxMass),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.MaxMass, src.PreferredMassUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationDto.MaxVolume),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.MaxVolume, src.PreferredVolumeUnit))
            );
        CreateMap<WarehouseLocation, WarehouseLocationSlimDto>();
        CreateMap<WarehouseLocation, WarehouseLocationListDto>()
            .ForCtorParam(
                nameof(WarehouseLocationListDto.Length),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Length, src.PreferredLengthUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.Width),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Width, src.PreferredLengthUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.Height),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Height, src.PreferredLengthUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.MaxMass),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.MaxMass, src.PreferredMassUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.MaxVolume),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.MaxVolume, src.PreferredVolumeUnit))
            )
            .ForCtorParam(
                nameof(WarehouseLocationListDto.WarehouseTitle),
                opt => opt.MapFrom(src => src.Warehouse.Title)
            );
        CreateMap<CreateWarehouseLocationDto, WarehouseLocation>();
        CreateMap<WarehouseLocation, PatchWarehouseLocationDto>()
            .ForMember(
                d => d.Length,
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Length, src.PreferredLengthUnit))
            )
            .ForMember(
                d => d.Width,
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Width, src.PreferredLengthUnit))
            )
            .ForMember(
                d => d.Height,
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.Height, src.PreferredLengthUnit))
            )
            .ForMember(
                d => d.MaxMass,
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.MaxMass, src.PreferredMassUnit))
            )
            .ForMember(
                d => d.MaxVolume,
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(src.MaxVolume, src.PreferredVolumeUnit))
            );
        CreateMap<PatchWarehouseLocationDto, WarehouseLocation>();
    }
}
