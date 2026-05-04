using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using NGErp.HCM.Service.Mappings;
using NGErp.HCM.Service.RequestValidators;
using NGErp.HCM.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Domain.EntitySchemas;
using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidators;
using NGErp.HCM.Service.Resources;
using NGErp.Base.Service.Services;

namespace NGErp.HCM.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHCMServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IExceptionLocalizer<HCMResource>, ExceptionLocalizer<HCMResource>>();

            services.AddValidatorsFromAssemblyContaining<DepartmentChangeStatusValidator>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IOrganizationalStructureService, OrganizationalStructureService>();
            services.AddScoped<IEmploymentGroupService, EmploymentGroupService>();
            services.AddScoped<IOrganizationNodeService, OrganizationNodeService>();

            services.AddSingleton<IFilterSchema<Position>, PositionSchema>();
            services.AddSingleton<IFilterSchema<Department>, DepartmentSchema>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IEmployeeEducationService, EmployeeEducationService>();

            services.AddScoped<IEmployeeWorkExperienceService, EmployeeWorkExperienceService>();
            services.AddScoped<IJobCategoryService, JobCategoryService>();
            services.AddScoped<IJobCategoryBusinessRulesValidator, JobCategoryBusinessRuleValidator>();

            return services;
        }
    }
}