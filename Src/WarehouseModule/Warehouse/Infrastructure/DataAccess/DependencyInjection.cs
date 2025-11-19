using Common.Infrastructure.DataAccess;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWarehouseInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IWarehouseRepositoryManager, WarehouseRepositoryManager>();            
            return services;
        }
    }
}
