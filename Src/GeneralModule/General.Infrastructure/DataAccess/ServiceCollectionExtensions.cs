using NGErp.General.Service.Interfaces.Repositories;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Infrastructure.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGeneralInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddScoped<IGeneralRepositoryManager, GeneralRepositoryManager>();            
            return services;
        }
    }
}
