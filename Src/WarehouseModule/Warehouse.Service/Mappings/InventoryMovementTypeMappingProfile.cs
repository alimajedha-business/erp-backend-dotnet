using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class InventoryMovementTypeMappingProfile : Profile
{
    public InventoryMovementTypeMappingProfile()
    {
        CreateMap<InventoryMovementType, InventoryMovementTypeDto>();
        CreateMap<CreateInventoryMovementTypeDto, InventoryMovementType>();
        CreateMap<PatchInventoryMovementTypeDto, InventoryMovementType>().ReverseMap();
    }
}
