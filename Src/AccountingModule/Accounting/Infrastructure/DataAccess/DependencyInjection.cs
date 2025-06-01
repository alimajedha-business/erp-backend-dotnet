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
        public static IServiceCollection AddAccountingDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServerDbContext<AccountingDbContext>(configuration);
            return services;
        }
    }
}
