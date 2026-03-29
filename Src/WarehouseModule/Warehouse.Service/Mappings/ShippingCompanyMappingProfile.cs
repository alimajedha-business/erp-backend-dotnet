using AutoMapper;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Mappings;

public class ShippingCompanyMappingProfile : Profile
{
    public ShippingCompanyMappingProfile()
    {
        CreateMap<ShippingCompany, ShippingCompanyDto>();
        CreateMap<ShippingCompany, ShippingCompanyListDto>()
            .ForCtorParam(
                nameof(ShippingCompanyListDto.ManagerFullName),
                opt => opt.MapFrom(src => src.ManagerFirstName + ' ' + src.ManagerLastName)
            );
        CreateMap<CreateShippingCompanyDto, ShippingCompany>();
        CreateMap<PatchShippingCompanyDto, ShippingCompany>().ReverseMap();
    }
}
