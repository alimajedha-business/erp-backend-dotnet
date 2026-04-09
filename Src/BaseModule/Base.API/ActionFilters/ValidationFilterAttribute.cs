using System.Diagnostics;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.ErrorModels;
using NGErp.Base.Service.Resources;

namespace NGErp.Base.API.ActionFilters;

/// <summary>
/// Action filter that validates request body models before the action executes.
/// </summary>
/// <remarks>
/// This filter only runs validation for HTTP methods that typically carry a request body
/// (<c>POST</c>, <c>PUT</c>, and <c>PATCH</c>).
///
/// Validation is performed in two stages:
/// <list type="number">
/// <item>
/// Checks <see cref="ActionExecutingContext.ModelState"/> for model binding, JSON parsing,
/// and data annotation errors collected by ASP.NET Core.
/// </item>
/// <item>
/// Resolves and executes a matching FluentValidation validator for the body DTO, if one exists.
/// </item>
/// </list>
///
/// When validation fails, the filter returns a <see cref="UnprocessableEntityObjectResult"/>
/// containing a localized <see cref="ErrorDetails"/> payload.
/// </remarks>
/// <param name="factory">Factory used to create the string localizer for validation messages.</param>
/// <param name="serviceProvider">Service provider used to resolve FluentValidation validators.</param>
public class ValidationFilterAttribute(
    IStringLocalizerFactory factory,
    IServiceProvider serviceProvider
) : IAsyncActionFilter
{
    /// <summary>
    /// HTTP verbs for which request body validation should be applied.
    /// </summary>
    private static readonly string[] _bodyVerbs = ["POST", "PUT", "PATCH"];

    /// <summary>
    /// Localizer used to translate validation error titles and known system-generated messages.
    /// </summary>
    private readonly IStringLocalizer _localizer = factory.Create(typeof(BaseResource));

    /// <summary>
    /// Service provider used to resolve <see cref="IValidator{T}"/> instances dynamically.
    /// </summary>
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    /// <summary>
    /// Executes request body validation before the target action method runs.
    /// </summary>
    /// <param name="context">The current action execution context.</param>
    /// <param name="next">Delegate that executes the next action filter or the action itself.</param>
    /// <returns>A task that represents the asynchronous filter operation.</returns>
    /// <remarks>
    /// The filter skips validation when:
    /// <list type="bullet">
    /// <item>The request method does not support body validation.</item>
    /// <item>The endpoint is decorated with <c>SkipModelValidationAttribute</c>.</item>
    /// <item>No body-bound parameter is defined for the action.</item>
    /// <item>No FluentValidation validator is registered for the body DTO type.</item>
    /// </list>
    ///
    /// If validation fails, execution is short-circuited and a 422 response is returned.
    /// </remarks>
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next
    )
    {
        var httpMethod = context.HttpContext.Request.Method.ToUpperInvariant();

        // Skip validation for HTTP methods that do not typically contain a request body.
        if (!_bodyVerbs.Contains(httpMethod))
        {
            await next();
            return;
        }

        // Allow endpoints to explicitly opt out of model validation.
        var skipValidation = context.ActionDescriptor
            .EndpointMetadata
            .Any(x => x is SkipModelValidationAttribute);

        if (skipValidation)
        {
            await next();
            return;
        }

        // Identify the action parameter that is bound from the request body.
        var bodyParameter = context.ActionDescriptor.Parameters
            .FirstOrDefault(p => p.BindingInfo?.BindingSource == BindingSource.Body);

        if (bodyParameter is null)
        {
            await next();
            return;
        }

        // First stage:
        // Handle model binding, JSON parsing, and data annotation errors already collected by ASP.NET Core.
        if (!context.ModelState.IsValid)
        {
            context.Result = BuildValidationResult(context, context.ModelState, bodyParameter.Name);
            return;
        }

        // Second stage:
        // Run FluentValidation against the actual body DTO instance.
        if (!context.ActionArguments.TryGetValue(bodyParameter.Name!, out var body) || body is null)
        {
            await next();
            return;
        }

        // Resolve IValidator<T> dynamically for the runtime DTO type.
        var validatorType = typeof(IValidator<>).MakeGenericType(body.GetType());
        var validator = _serviceProvider.GetService(validatorType);

        if (validator is null)
        {
            await next();
            return;
        }

        var validationContextType = typeof(ValidationContext<>).MakeGenericType(body.GetType());
        var validationContext = (IValidationContext)Activator.CreateInstance(validationContextType, body)!;

        var validationResult = await ((IValidator)validator)
            .ValidateAsync(validationContext, context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            // Copy FluentValidation errors into ModelState so all validation errors
            // can be returned in a unified response format.
            foreach (var error in validationResult.Errors)
            {
                context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            context.Result = BuildValidationResult(context, context.ModelState, bodyParameter.Name);
            return;
        }

        await next();
    }

    /// <summary>
    /// Builds a standardized 422 Unprocessable Entity response from the current model state.
    /// </summary>
    /// <param name="context">The current action execution context.</param>
    /// <param name="modelState">The model state containing validation errors.</param>
    /// <param name="bodyParameterName">
    /// The name of the body parameter to exclude from the error dictionary root key.
    /// </param>
    /// <returns>
    /// An <see cref="UnprocessableEntityObjectResult"/> containing localized validation details.
    /// </returns>
    private UnprocessableEntityObjectResult BuildValidationResult(
        ActionExecutingContext context,
        ModelStateDictionary modelState,
        string? bodyParameterName
    )
    {
        // Build a normalized error dictionary keyed by property path.
        var errors = modelState
            .Where(x => x.Value is not null && x.Value.Errors.Count > 0 && x.Key != bodyParameterName)
            .ToDictionary(
                kvp => NormalizeKey(kvp.Key),
                kvp => kvp.Value!.Errors
                    .Select(e => LocalizeSystemError(e.ErrorMessage))
                    .Distinct()
                    .ToArray()
            );

        return new UnprocessableEntityObjectResult(new ErrorDetails
        {
            Title = _localizer["ValidationFailed"].Value,
            Details = errors,
            TraceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
        });
    }

    /// <summary>
    /// Normalizes model state keys by removing JSONPath root prefixes.
    /// </summary>
    /// <param name="key">The original model state key.</param>
    /// <returns>A simplified property path suitable for API error responses.</returns>
    private static string NormalizeKey(string key) =>
        key.Replace("$.", string.Empty).Replace("$", string.Empty);

    /// <summary>
    /// Converts known framework-generated validation messages into localized resource values.
    /// </summary>
    /// <param name="message">The original system-generated validation message.</param>
    /// <returns>A localized message when a known pattern is detected; otherwise the original message.</returns>
    private string LocalizeSystemError(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return message;

        // Translate common ASP.NET Core / System.Text.Json messages into resource-based messages.
        if (message.Contains("A non-empty request body is required", StringComparison.OrdinalIgnoreCase))
            return (string)_localizer["ObjectIsNull"];
        if (message.Contains("missing required properties", StringComparison.OrdinalIgnoreCase))
            return (string)_localizer["MissingRequiredProperties"];
        if (message.Contains("JSON deserialization", StringComparison.OrdinalIgnoreCase))
            return (string)_localizer["InvalidJsonFormat"];
        if (message.Contains("could not be converted", StringComparison.OrdinalIgnoreCase))
            return (string)_localizer["CouldNotConvert"];

        return message;
    }
}