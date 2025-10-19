// Ignore Spelling: Middleware

using Common.Application.Interfaces;
using Common.Domain.Exceptions;
using Common.ErrorModel;
using Common.Infrastructure.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;

namespace API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
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
                    var statusCode = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException or ValidationException => StatusCodes.Status400BadRequest,
                        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    context.Response.StatusCode = statusCode;

                    // Resolve localizer dynamically per module / exception
                    var isDevelopment = app.Environment.IsDevelopment();
                    var localizer = ExceptionLocalizerFactory.ResolveForException(context.RequestServices, ex);
                    var localizedMessage = localizer.Localize(ex);

                    // Structured logging with more context
                    logger.LogError(ex, "Exception occurred: {Message} | TraceId: {TraceId}",
                        localizedMessage, context.TraceIdentifier);

                    // Enhanced error response
                    var errorResponse = new ErrorDetails
                    {
                        StatusCode = statusCode,
                        Message = localizedMessage,//isDevelopment ? ex.Message : localizedMessage,
                        TraceId = context.TraceIdentifier,
                        Details = isDevelopment ? ex.StackTrace : null // Only in development
                    };


                    await context.Response.WriteAsJsonAsync(errorResponse);
                });
            });
        }
    }
}
