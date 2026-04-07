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
        CreateMap<Department, PatchDepartmentDto>().ReverseMap();

        CreateMap<Position, PositionDto>();
        CreateMap<Position, CreatePositionDto>().ReverseMap();
        CreateMap<Position, PatchPositionDto>().ReverseMap();

        CreateMap<OrganizationalStructure, OrganizationalStructureDto>();

        CreateMap<EmploymentGroup, EmploymentGroupDto>().ReverseMap();
        CreateMap<EmploymentGroupSpecification, EmploymentGroupSpecificationDto>().ReverseMap();
    }
}