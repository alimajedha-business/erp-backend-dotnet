using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Domain.EntitySchemas;
using NGErp.HCM.Service.Mappings;
using NGErp.HCM.Service.RequestValidators;
using NGErp.HCM.Service.Services;

namespace NGErp.HCM.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHCMServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddValidatorsFromAssemblyContaining<DepartmentChangeStatusValidator>();

        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEducationFieldService, EducationFieldService>();
        services.AddScoped<IEducationLevelService, EducationLevelService>();
        services.AddScoped<IEducationalStatusService, EducationalStatusService>();
        services.AddScoped<IEmployeeEducationService, EmployeeEducationService>();
        services.AddScoped<IEmployeeRelativeService, EmployeeRelativeService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IEmployeeWarriorRecordService, EmployeeWarriorRecordService>();
        services.AddScoped<IEmployeeWorkExperienceService, EmployeeWorkExperienceService>();
        services.AddScoped<IEmploymentGroupService, EmploymentGroupService>();
        services.AddScoped<IMaritalStatusService, MaritalStatusService>();
        services.AddScoped<IMilitaryServiceStatusService, MilitaryServiceStatusService>();
        services.AddScoped<IOrganizationNodeService, OrganizationNodeService>();
        services.AddScoped<IOrganizationalStructureService, OrganizationalStructureService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IRelativeTypeService, RelativeTypeService>();

        services.AddSingleton<IFilterSchema<Department>, DepartmentSchema>();
        services.AddSingleton<IFilterSchema<EducationField>, EducationFieldSchema>();
        services.AddSingleton<IFilterSchema<EducationLevel>, EducationLevelSchema>();
        services.AddSingleton<IFilterSchema<EducationalStatus>, EducationalStatusSchema>();
        services.AddSingleton<IFilterSchema<MaritalStatus>, MaritalStatusSchema>();
        services.AddSingleton<IFilterSchema<MilitaryServiceStatus>, MilitaryServiceStatusSchema>();
        services.AddSingleton<IFilterSchema<Position>, PositionSchema>();
        services.AddSingleton<IFilterSchema<RelativeType>, RelativeTypeSchema>();

        return services;
    }
}
