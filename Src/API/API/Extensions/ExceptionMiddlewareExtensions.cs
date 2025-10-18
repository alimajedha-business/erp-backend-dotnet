// Ignore Spelling: Middleware

using Common.Application.Interfaces;
using Common.Domain.Exceptions;
using Common.ErrorModel;
using Common.Infrastructure.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Localization;
using System.Net;
using System.Reflection;

namespace API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(static appError =>
            {
                appError.Run(async context =>
                {
                     var logger = context.RequestServices.GetRequiredService<ILoggerService>();
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is null)
                        return;

                    var ex = contextFeature.Error;

                    // Select proper status code
                    context.Response.StatusCode = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    // Get correct ExceptionLocalizer based on module/exception type
                    var localizer = ExceptionLocalizerFactory.ResolveForException(context.RequestServices, ex);

                    // Localize the message
                    var localizedMessage = localizer.Localize(ex);

                    // Log the original error
                    logger.LogError(null, $"Something went wrong: {ex}");

                    // Return response
                    await context.Response.WriteAsJsonAsync(new 
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = localizedMessage
                    });
                });
            });
        }
    }
}
