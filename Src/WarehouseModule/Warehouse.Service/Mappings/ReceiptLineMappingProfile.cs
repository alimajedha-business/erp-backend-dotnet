using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptLineMappingProfile : Profile
{
    public ReceiptLineMappingProfile()
    {
        CreateMap<CreateReceiptLineDto, ReceiptLine>()
            .ForMember(d => d.ReceiptLineMeasurementValues, o => o.Ignore())
            .ForMember(d => d.ReceiptLineAttributeValues, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());

        CreateMap<ReceiptLineMeasurementValue, ReceiptLineMeasurementValueDto>();
        CreateMap<CreateReceiptLineMeasurementValueDto, ReceiptLineMeasurementValue>();
    }
}
