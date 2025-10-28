using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private static readonly string[] _bodyVerbs = new[] { "POST", "PUT", "PATCH" };
        public ValidationFilterAttribute()
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpMethod = context.HttpContext.Request.Method.ToUpperInvariant();

            if (!_bodyVerbs.Contains(httpMethod))
                return;

            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            if (context.ActionArguments.Values.Any(v => v == null))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    //message = $"Object is null. Controller: {controller}, action: {action}"
                    message = "ObjectIsNull"
                });
                return;
            }

            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
