using Common.Infrastructure.DataAccess;
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
            services.AddSqlServerDbContext<GeneralDbContext>(configuration);
            return services;
        }
    }
}
