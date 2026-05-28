using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptLineAttributeValueMappingProfile : Profile
{
    public ReceiptLineAttributeValueMappingProfile()
    {
        CreateMap<ReceiptLineAttributeValue, ReceiptLineAttributeValueDto>();
        CreateMap<CreateReceiptLineAttributeValueDto, ReceiptLineAttributeValue>();
    }
}
