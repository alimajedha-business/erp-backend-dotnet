using Common.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.Domain.Exceptions;
using Common.Application.Services;
using HCM;
using Common.Resources;
using Accounting.Resources;

namespace API
{
    public static class ExceptionLocalizerFactory
    {
        //public static IExceptionLocalizer ResolveForException(IServiceProvider sp, Exception ex)
        //{
        //    // Add specific mappings if needed
        //    return ex switch
        //    {
        //        // Example: exceptions in the Accounting module
        //        LedgerNotFoundException => sp.GetRequiredService<ExceptionLocalizer<AccountingResource>>(),

        //        // Fallback to common
        //        _ => sp.GetRequiredService<ExceptionLocalizer<CommonResource>>()
        //    };
        //}

        public static IExceptionLocalizer ResolveForException(IServiceProvider sp, Exception ex)
        {
            var ns = ex.GetType().Namespace ?? string.Empty;

            if (ns.Contains("Accounting"))
                return sp.GetRequiredService<ExceptionLocalizer<AccountingResource>>();
            if (ns.Contains("HCM"))
                return sp.GetRequiredService<ExceptionLocalizer<HCMResource>>();

            return sp.GetRequiredService<ExceptionLocalizer<CommonResource>>();
        }
    }
}
