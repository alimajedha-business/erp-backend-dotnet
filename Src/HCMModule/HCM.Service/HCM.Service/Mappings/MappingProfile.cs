using AutoMapper;

using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;

namespace NGErp.HCM.Service.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentDto>();
        CreateMap<Department, CreateDepartmentDto>().ReverseMap();
        CreateMap<Position, PositionDto>();
        CreateMap<Position, CreatePositionDto>().ReverseMap();
        CreateMap<OrganizationalStructure, OrganizationalStructureDto>();
    }
}