using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using NGErp.HCM.Service.Mappings;
using NGErp.HCM.Service.RequestValidators;
using NGErp.HCM.Service.Services;

namespace NGErp.HCM.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHCMServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddValidatorsFromAssemblyContaining<DepartmentChangeStatusValidator>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IOrganizationalStructureService, OrganizationalStructureService>();
            services.AddScoped<IEmploymentGroupService, EmploymentGroupService>();
            services.AddScoped<IOrganizationNodeService, OrganizationNodeService>();
            return services;
        }
    }
}