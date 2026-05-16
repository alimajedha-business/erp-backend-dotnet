using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptMappingProfile : Profile
{
    public ReceiptMappingProfile()
    {
        CreateMap<Receipt, ReceiptListDto>()
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptTypeTitle),
                opt => opt.MapFrom(src => src.ReceiptType.Title)
            )
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptLineCount),
                opt => opt.MapFrom(src => src.ReceiptLines.Count)
            );

        CreateMap<Receipt, ReceiptDto>();
        CreateMap<ReceiptLine, ReceiptLineDto>();
        CreateMap<ReceiptLineAttributeValue, ReceiptLineAttributeValueDto>();
        CreateMap<ReceiptLineMeasurementValue, ReceiptLineMeasurementValueDto>();
        CreateMap<ReceiptFieldValue, ReceiptFieldValueDto>();

        CreateMap<CreateReceiptDto, Receipt>()
            .ForMember(d => d.Number, o => o.MapFrom(s => s.Number!))
            .ForMember(d => d.ReceiptLines, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());

        CreateMap<CreateReceiptLineDto, ReceiptLine>()
            .ForMember(d => d.ReceiptLineMeasurementValues, o => o.Ignore())
            .ForMember(d => d.ReceiptLineAttributeValues, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());

        CreateMap<CreateReceiptLineAttributeValueDto, ReceiptLineAttributeValue>();
        CreateMap<CreateReceiptLineMeasurementValueDto, ReceiptLineMeasurementValue>();

        CreateMap<CreateReceiptFieldValueDto, ReceiptFieldValue>();
    }
}
