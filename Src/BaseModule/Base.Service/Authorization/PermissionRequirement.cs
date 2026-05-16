using Microsoft.AspNetCore.Authorization;

namespace NGErp.Base.Service.Authorization;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string EntityKey { get; }
    public string? CommandKey { get; }
    public ActionType Action { get; }

    public PermissionRequirement(string entityKey, string? commandKey = null, ActionType action = ActionType.Default)
    {
        EntityKey = entityKey;
        CommandKey = commandKey;
        Action = action;
    }
}
