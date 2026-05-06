using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class RemittanceTypeMappingProfile : Profile
{
    public RemittanceTypeMappingProfile()
    {
        CreateMap<RemittanceType, RemittanceTypeDto>();
        CreateMap<RemittanceType, RemittanceTypeSlimDto>();
        CreateMap<CreateRemittanceTypeDto, RemittanceType>();
        CreateMap<PatchRemittanceTypeDto, RemittanceType>().ReverseMap();
    }
}
