using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptTypeMappingProfile : Profile
{
    public ReceiptTypeMappingProfile()
    {
        CreateMap<ReceiptType, ReceiptTypeDto>();
        CreateMap<ReceiptType, ReceiptTypeSlimDto>();
        CreateMap<CreateReceiptTypeDto, ReceiptType>();
        CreateMap<PatchReceiptTypeDto, ReceiptType>().ReverseMap();
    }
}
