using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Domain.EntitySchemas;
using NGErp.HCM.Service.Mappings;
using NGErp.HCM.Service.RequestValidators.DtoValidators;
using NGErp.HCM.Service.Services;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidators;
using NGErp.HCM.Service.Resources;
using NGErp.Base.Service.Services;
using NGErp.HCM.Service.Service.Contracts;

namespace NGErp.HCM.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHCMServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); });

            services.AddScoped<IExceptionLocalizer<HCMResource>, ExceptionLocalizer<HCMResource>>();

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
        services.AddScoped<IEmployeeDependantService, EmployeeDependantService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IJobCategoryService, JobCategoryService>();
        services.AddScoped<IJobCategoryBusinessRulesValidator, JobCategoryBusinessRuleValidator>();
        services.AddScoped<IJobBusinessRuleValidator, JobBusinessRuleValidator>();
        services.AddScoped<IPositionJobBusinessRuleValidator, PositionJobBusinessRuleValidator>();
        services.AddScoped<IWorkLocationBusinessRuleValidator, WorkLocationBusinessRuleValidator>();
        services.AddScoped<IPositionJobService, PositionJobService>();

        services.AddSingleton<IFilterSchema<Department>, DepartmentSchema>();
        services.AddSingleton<IFilterSchema<EducationField>, EducationFieldSchema>();
        services.AddSingleton<IFilterSchema<EducationLevel>, EducationLevelSchema>();
        services.AddSingleton<IFilterSchema<EducationalStatus>, EducationalStatusSchema>();
        services.AddSingleton<IFilterSchema<MaritalStatus>, MaritalStatusSchema>();
        services.AddSingleton<IFilterSchema<MilitaryServiceStatus>, MilitaryServiceStatusSchema>();
        services.AddSingleton<IFilterSchema<Position>, PositionSchema>();
        services.AddSingleton<IFilterSchema<RelativeType>, RelativeTypeSchema>();
        services.AddSingleton<IFilterSchema<JobCategory>, JobCategorySchema>();
        services.AddSingleton<IFilterSchema<WorkLocation>, WorkLocationSchema>();
        services.AddScoped<IWorkLocationService, WorkLocationService>();

        return services;
    }
}
