using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptLineMappingProfile : Profile
{
    public ReceiptLineMappingProfile()
    {
        CreateMap<ReceiptLine, ReceiptLineDto>()
            .ForCtorParam(
                nameof(ReceiptLineDto.Weight),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(
                    src.Weight,
                    src.PreferredMassUnit
                ))
            )
            .ForCtorParam(
                nameof(ReceiptLineDto.Volume),
                opt => opt.MapFrom(src => MeasurementUnitConverter.ConvertFromBase(
                    src.Volume,
                    src.PreferredVolumeUnit
                ))
            );
        CreateMap<CreateReceiptLineDto, ReceiptLine>()
            .ForMember(d => d.ReceiptLineMeasurementValues, o => o.Ignore())
            .ForMember(d => d.ReceiptLineAttributeValues, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());

        CreateMap<ReceiptLineMeasurementValue, ReceiptLineMeasurementValueDto>();
        CreateMap<CreateReceiptLineMeasurementValueDto, ReceiptLineMeasurementValue>();
    }
}
