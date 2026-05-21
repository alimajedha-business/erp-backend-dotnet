using Microsoft.AspNetCore.Authorization;

namespace NGErp.Base.Service.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class HasPermissionAttribute : AuthorizeAttribute
{
    public const string PolicyPrefix = "Permission";

    public HasPermissionAttribute(string entityKey, string? commandKey = null, long moduleId = 0)
    {
        var mid = moduleId > 0 ? $":m{moduleId}" : "";
        Policy = commandKey == null 
            ? $"{PolicyPrefix}:{entityKey}{mid}" 
            : $"{PolicyPrefix}:{entityKey}{mid}:{commandKey}";
    }

    public HasPermissionAttribute(string entityKey, ActionType action, long moduleId = 0)
    {
        var mid = moduleId > 0 ? $":m{moduleId}" : "";
        Policy = $"{PolicyPrefix}:{entityKey}{mid}:{(int)action}";
    }
}
