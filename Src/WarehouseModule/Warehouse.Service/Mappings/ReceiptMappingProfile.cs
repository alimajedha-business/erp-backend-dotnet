using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptMappingProfile : Profile
{
    public ReceiptMappingProfile()
    {
        CreateMap<Receipt, ReceiptDto>();
        CreateMap<Receipt, ReceiptListDto>()
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptTypeTitle),
                opt => opt.MapFrom(src => src.ReceiptType.Title)
            )
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptLineCount),
                opt => opt.MapFrom(src => src.ReceiptLines.Count)
            )
            .ForCtorParam(
                nameof(ReceiptListDto.ReceiptFieldValues),
                opt => opt.MapFrom(src => src.ReceiptFieldValues
                    .Where(e => e.ReceiptLineId == null))
            );
        CreateMap<CreateReceiptDto, Receipt>()
            .ForMember(d => d.Number, o => o.MapFrom(s => s.Number!))
            .ForMember(d => d.ReceiptLines, o => o.Ignore())
            .ForMember(d => d.ReceiptFieldValues, o => o.Ignore());
    }
}
