using Common.Infrastructure.DataAccess;
using HCM.Application.Interfaces.Repositories;
using HCM.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Infrastructure.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGeneralInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHCMRepositoryManager, HCMRepositoryManager>();
            services.AddSqlServerDbContext<HCMDbContext>(configuration);
            return services;
        }
    }
}
