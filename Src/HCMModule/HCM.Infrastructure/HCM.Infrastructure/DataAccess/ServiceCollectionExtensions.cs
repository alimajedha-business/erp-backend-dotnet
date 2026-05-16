using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NGErp.HCM.Infrastructure.DataAccess.Repositories;
using NGErp.HCM.Service.Repository.Contracts;

namespace NGErp.HCM.Infrastructure.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHCMInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEducationFieldRepository, EducationFieldRepository>();
        services.AddScoped<IEducationLevelRepository, EducationLevelRepository>();
        services.AddScoped<IEducationalStatusRepository, EducationalStatusRepository>();
        services.AddScoped<IEmployeeEducationRepository, EmployeeEducationRepository>();
        services.AddScoped<IEmployeeRelativeRepository, EmployeeRelativeRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeWarriorRecordRepository, EmployeeWarriorRecordRepository>();
        services.AddScoped<IEmployeeWorkExperienceRepository, EmployeeWorkExperienceRepository>();
        services.AddScoped<IEmploymentGroupRepository, EmploymentGroupRepository>();
        services.AddScoped<IEmploymentGroupSpecificationRepository, EmploymentGroupSpecificationRepository>();
        services.AddScoped<IMaritalStatusRepository, MaritalStatusRepository>();
        services.AddScoped<IMilitaryServiceStatusRepository, MilitaryServiceStatusRepository>();
        services.AddScoped<IOrganizationNodeRepository, OrganizationNodeRepository>();
        services.AddScoped<IOrganizationalStructureRepository, OrganizationalStructureRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IRelativeTypeRepository, RelativeTypeRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
        services.AddScoped<IEmployeeDependantRepository, EmployeeDependantRepository>();
        services.AddScoped<IPositionJobRepository, PositionJobRepository>();

        return services;
    }
}
