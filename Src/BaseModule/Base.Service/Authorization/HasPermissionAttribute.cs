using Microsoft.AspNetCore.Authorization;

namespace NGErp.Base.Service.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class HasPermissionAttribute : AuthorizeAttribute
{
    public const string PolicyPrefix = "Permission";

    public HasPermissionAttribute(string entityKey, string? commandKey = null)
    {
        Policy = commandKey == null 
            ? $"{PolicyPrefix}:{entityKey}" 
            : $"{PolicyPrefix}:{entityKey}:{commandKey}";
    }

    public HasPermissionAttribute(string entityKey, ActionType action)
    {
        Policy = $"{PolicyPrefix}:{entityKey}:{(int)action}";
    }
}
