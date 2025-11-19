using Accounting.Application.Interfaces.Repositories;
using Accounting.Infrastructure.DataAccess.Repositories;
using Common.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccountingInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountingRepositoryManager, AccountingRepositoryManager>();
            return services;
        }
    }
}
