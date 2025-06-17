using Common.Infrastructure.DataAccess;
using General.Application.Interfaces.Repositories;
using General.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Infrastructure.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGeneralDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGeneralRepositoryManager, GeneralRepositoryManager>();
            services.AddSqlServerDbContext<GeneralDbContext>(configuration);
            return services;
        }
    }
}
