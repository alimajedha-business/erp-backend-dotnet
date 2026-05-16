using AutoMapper;

using NGErp.General.Domain.Entities;
using NGErp.General.Service.DTOs;
using NGErp.Shared.Domain.Entities;

namespace NGErp.General.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<Company, CompanyDto>();
           CreateMap<CompanyUnit, CompanyUnitDto>();

           CreateMap<Person, PersonDto>();
           CreateMap<Person, PersonBaseDto>();
           
        }
    }
}
