using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace NGErp.Base.Service.Authorization;

public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : IAuthorizationPolicyProvider
{
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; } = new(options);

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(HasPermissionAttribute.PolicyPrefix, StringComparison.OrdinalIgnoreCase))
        {
            var parts = policyName.Split(':').ToList();
            if (parts.Count >= 2)
            {
                var entityKey = parts[1];
                long? moduleId = null;
                string? commandKey = null;
                ActionType action = ActionType.Default;

                // Remove prefix and entityKey
                parts.RemoveAt(0); 
                parts.RemoveAt(0);

                // Check if next part is moduleId (starts with 'm')
                if (parts.Count > 0 && parts[0].StartsWith('m') && long.TryParse(parts[0].AsSpan(1), out var mid))
                {
                    moduleId = mid;
                    parts.RemoveAt(0);
                }

                if (parts.Count > 0)
                {
                    // If the next part is a number, it's an ActionType enum value
                    if (int.TryParse(parts[0], out var actionValue))
                    {
                        action = (ActionType)actionValue;
                    }
                    else
                    {
                        commandKey = parts[0];
                    }
                }

                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(entityKey, commandKey, action, moduleId));
                return Task.FromResult<AuthorizationPolicy?>(policy.Build());
            }
        }

        return FallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}
