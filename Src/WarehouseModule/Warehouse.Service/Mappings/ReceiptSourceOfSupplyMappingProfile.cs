using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ReceiptSourceOfSupplyMappingProfile : Profile
{
    public ReceiptSourceOfSupplyMappingProfile()
    {
        CreateMap<ReceiptSourceOfSupply, ReceiptSourceOfSupplyDto>();
        CreateMap<ReceiptSourceOfSupply, ReceiptSourceOfSupplySlimDto>();
        CreateMap<CreateReceiptSourceOfSupplyDto, ReceiptSourceOfSupply>();
        CreateMap<PatchReceiptSourceOfSupplyDto, ReceiptSourceOfSupply>().ReverseMap();
    }
}
