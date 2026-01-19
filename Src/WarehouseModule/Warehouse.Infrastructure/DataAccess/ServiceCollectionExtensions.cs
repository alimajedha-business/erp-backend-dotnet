using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NGErp.Warehouse.Infrastructure.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWarehouseInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}
