using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptLineMappingProfile : Profile
{
    public ReceiptLineMappingProfile()
    {
        CreateMap<CreateReceiptLineDto, ReceiptLine>();
    }
}
