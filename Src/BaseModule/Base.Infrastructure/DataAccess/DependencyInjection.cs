using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("NGERPDATABASE")));            
            return services;
        }
    }
}
