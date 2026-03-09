using AutoMapper;

using NGErp.General.Domain.Entities;
using NGErp.General.Service.DTOs;

namespace NGErp.General.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<Company, CompanyDto>();
           CreateMap<CompanyUnit, CompanyUnitDto>();
        }
    }
}
