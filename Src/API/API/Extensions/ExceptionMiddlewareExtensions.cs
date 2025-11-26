// Ignore Spelling: Middleware

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Domain.ErrorModel;
using NGErp.Base.Infrastructure.Logging;
using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using NGErp.API.Services;

namespace NGErp.API.Extensions
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

                    // Structured logging with more context
                    logger.LogError(ex, "Exception occurred: {Message} | TraceId: {TraceId}",
                        ex.Message, context.TraceIdentifier);

                    // Resolve localizer dynamically per module / exception
                    var isDevelopment = app.Environment.IsDevelopment();
                    var localizer = ExceptionLocalizerFactory.ResolveForException(context.RequestServices, ex);
                    var localizedMessage = localizer.Localize(ex);

                    var errorResponse = new ErrorDetails
                    {
                        Title = localizedMessage,
                        TraceId = context.TraceIdentifier,
                        Details = new Dictionary<string, string[]?>
                        {
                            { "exception", isDevelopment ? new[] {ex.InnerException != null?ex.InnerException.Message:"" } : null }
                        }
                    };

                    await context.Response.WriteAsJsonAsync(errorResponse);
                });
            });
        }
    }
}
