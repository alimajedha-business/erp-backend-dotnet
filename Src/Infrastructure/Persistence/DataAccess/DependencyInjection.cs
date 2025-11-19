using Common.Application.Interfaces;
using Common.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMainDbContext, MainDbContext>();
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("NGERPDATABASE")));            
            return services;
        }
    }
}
