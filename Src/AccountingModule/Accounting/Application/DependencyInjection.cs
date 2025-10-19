using Accounting.Application.Interfaces.Repositories;
using Accounting.Application.Interfaces.Services;
using Accounting.Application.Mappings;
using Accounting.Application.Services;
using Accounting.Infrastructure.DataAccess.Repositories;
using Accounting.Resources;
using Common.Application.Interfaces;
using Common.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccountingApplication(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(AccountingMappingProfile));
            services.AddScoped<IExceptionLocalizer, ExceptionLocalizer<AccountingResource>>();
            return services;
        }
    }
}
