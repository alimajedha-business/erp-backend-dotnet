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
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IOrganizationalStructure, OrganizationalStructureRepository>();
        return services;
    }
}
