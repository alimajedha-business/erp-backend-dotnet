using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.Base.Infrastructure.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("NGERPDATABASE")));            
            return services;
        }
    }
}
