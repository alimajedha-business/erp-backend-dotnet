using Base.Service.Resources;
using Base.Domain.ErrorModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Base.API.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private static readonly string[] _bodyVerbs = new[] { "POST", "PUT", "PATCH" };
        private readonly IStringLocalizer _localizer;

        public ValidationFilterAttribute(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(typeof(BaseResource));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpMethod = context.HttpContext.Request.Method.ToUpperInvariant();

            if (!_bodyVerbs.Contains(httpMethod))
                return;

            var bodyParameter = context.ActionDescriptor.Parameters
                .FirstOrDefault(p => p.BindingInfo?.BindingSource == BindingSource.Body);

            if (bodyParameter == null)
                return;

            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value!.Errors.Count > 0 && x.Key != bodyParameter.Name)
                    .ToDictionary(
                        kvp => kvp.Key.Replace("$.", string.Empty).Replace("$", string.Empty),
                        kvp => kvp.Value?.Errors.Select(e => LocalizeSystemError(e.ErrorMessage)).ToArray()
                    );
                context.Result = new UnprocessableEntityObjectResult(new ErrorDetails 
                {
                    Title = _localizer["ValidationFailed"].Value,
                    Details = errors,
                    TraceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
                });
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private string LocalizeSystemError(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return message;

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
}
