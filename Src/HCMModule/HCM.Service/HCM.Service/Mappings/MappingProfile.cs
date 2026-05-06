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

        CreateMap<EducationalStatus, EducationalStatusDto>();
        CreateMap<EducationalStatus, CreateEducationalStatusDto>().ReverseMap();
        CreateMap<EducationalStatus, PatchEducationalStatusDto>().ReverseMap();

        CreateMap<EducationField, EducationFieldDto>();
        CreateMap<EducationField, CreateEducationFieldDto>().ReverseMap();
        CreateMap<EducationField, PatchEducationFieldDto>().ReverseMap();

        CreateMap<EducationLevel, EducationLevelDto>();
        CreateMap<EducationLevel, CreateEducationLevelDto>().ReverseMap();
        CreateMap<EducationLevel, PatchEducationLevelDto>().ReverseMap();

        CreateMap<MaritalStatus, MaritalStatusDto>();
        CreateMap<MaritalStatus, CreateMaritalStatusDto>().ReverseMap();
        CreateMap<MaritalStatus, PatchMaritalStatusDto>().ReverseMap();

        CreateMap<MilitaryServiceStatus, MilitaryServiceStatusDto>();
        CreateMap<MilitaryServiceStatus, CreateMilitaryServiceStatusDto>().ReverseMap();
        CreateMap<MilitaryServiceStatus, PatchMilitaryServiceStatusDto>().ReverseMap();

        CreateMap<Position, PositionDto>();
        CreateMap<Position, CreatePositionDto>().ReverseMap();
        CreateMap<Position, PatchPositionDto>().ReverseMap();

        CreateMap<RelativeType, RelativeTypeDto>();
        CreateMap<RelativeType, CreateRelativeTypeDto>().ReverseMap();
        CreateMap<RelativeType, PatchRelativeTypeDto>().ReverseMap();

        CreateMap<OrganizationalStructure, OrganizationalStructureDto>();

        CreateMap<EmploymentGroup, EmploymentGroupDto>();
        CreateMap<EmploymentGroup, EmploymentGroupDetailDto>()
            .ForMember(
                d => d.Specifications,
                opt => opt.MapFrom(s => s.Specifications.OrderBy(x => x.ValidFrom))
            );
        CreateMap<CreateEmploymentGroupDto, EmploymentGroup>();
        CreateMap<EmploymentGroupSpecification, EmploymentGroupSpecificationDto>();
        CreateMap<OrganizationNode, OrganizationNodeTreeDto>().ReverseMap();

        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<Employee, EmployeeBaseDto>().ReverseMap();
        CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
        CreateMap<Employee, PatchEmployeeDto>().ReverseMap();

        CreateMap<EmployeeEducation, EmployeeEducationDto>();
        CreateMap<CreateEmployeeEducationDto, EmployeeEducation>();
        CreateMap<EmployeeEducation, PatchEmployeeEducationDto>();

        CreateMap<EmployeeWorkExperience, EmployeeWorkExperienceDto>().ReverseMap();
        CreateMap<CreateEmployeeWorkExperienceDto, EmployeeWorkExperience>();
        CreateMap<EmployeeWorkExperience, PatchEmployeeWorkExperienceDto>().ReverseMap();

        CreateMap<EmployeeWarriorRecord, EmployeeWarriorRecordDto>().ReverseMap();
        CreateMap<CreateEmployeeWarriorRecordDto, EmployeeWarriorRecord>();
        CreateMap<EmployeeWarriorRecord, PatchEmployeeWarriorRecordDto>().ReverseMap();

        CreateMap<EmployeeRelative, EmployeeRelativeDto>();
        CreateMap<CreateEmployeeRelativeDto, EmployeeRelative>();
        CreateMap<EmployeeRelative, PatchEmployeeRelativeDto>();
        
        CreateMap<EducationLevel, EducationLevelDto>();
        CreateMap<CreateEducationLevelDto, EducationLevel>();
        CreateMap<EducationLevel, PatchEducationLevelDto>();

        CreateMap<EducationField, EducationFieldDto>();
        CreateMap<CreateEducationFieldDto, EducationField>();
        CreateMap<EducationField, PatchEducationFieldDto>();

        CreateMap<Job, JobDto>().ReverseMap();
        CreateMap<CreateJobDto, Job>();
        CreateMap<Job, PatchJobDto>().ReverseMap();

        CreateMap<JobCategory, JobCategoryDto>().ReverseMap();
        CreateMap<CreateJobCategoryDto, JobCategory>();
        CreateMap<JobCategory, PatchJobCategoryDto>().ReverseMap();

    }
}
