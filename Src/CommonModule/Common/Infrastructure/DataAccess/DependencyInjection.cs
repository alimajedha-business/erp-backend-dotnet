using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSqlServerDbContext<TContext>(
        this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
        {
            services.AddDbContext<TContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("NGERPDatabase")));
            return services;
        }
    }
}
