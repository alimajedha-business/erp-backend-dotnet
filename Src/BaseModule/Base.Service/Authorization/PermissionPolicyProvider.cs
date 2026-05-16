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
            var parts = policyName.Split(':');
            if (parts.Length >= 2)
            {
                var entityKey = parts[1];
                string? commandKey = null;
                ActionType action = ActionType.Default;

                if (parts.Length > 2)
                {
                    // If the 3rd part is a number, it's an ActionType enum value
                    if (int.TryParse(parts[2], out var actionValue))
                    {
                        action = (ActionType)actionValue;
                    }
                    else
                    {
                        commandKey = parts[2];
                    }
                }

                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(entityKey, commandKey, action));
                return Task.FromResult<AuthorizationPolicy?>(policy.Build());
            }
        }

        return FallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}
