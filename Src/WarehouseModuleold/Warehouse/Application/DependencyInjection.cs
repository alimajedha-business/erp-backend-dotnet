using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.Interfaces.Services;
using Warehouse.Application.Mappings;
using Warehouse.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWarehouseApplication(this IServiceCollection services)
        {
           // services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(WarehouseMappingProfile));
            return services;
        }
    }
}
