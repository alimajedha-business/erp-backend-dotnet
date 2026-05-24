using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.Services;

namespace NGErp.Base.Infrastructure.Authorization;

public class PermissionHandler(
    MainDbContext dbContext,
    ICurrentUserService currentUserService
) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement
    )
    {
        // 1. Basic Validation
        if (string.IsNullOrEmpty(currentUserService.UserId))
        {
            return;
        }

        // Handle both MVC and Minimal API/Other contexts
        var httpContext = context.Resource switch
        {
            HttpContext contextResource => contextResource,
            Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext mvcContext => mvcContext.HttpContext,
            _ => null
        };

        if (httpContext == null)
        {
            return;
        }

        // 2. Superuser bypass (Claim-based)
        // Check various common claim names for superuser status
        var isSuperUser = currentUserService.User?.Claims.Any(c => 
            (c.Type == "is_superuser" || c.Type == "superuser" || c.Type == "is_admin") && 
            (c.Value.Equals("true", StringComparison.OrdinalIgnoreCase) || c.Value == "1")
        ) ?? false;

        if (isSuperUser)
        {
            context.Succeed(requirement);
            return;
        }

        // 3. Superuser bypass (Database-based)
        if (Guid.TryParse(currentUserService.UserId, out var userGuid))
        {
            var user = await dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userGuid);
            
            if (user?.IsSuperuser == true)
            {
                context.Succeed(requirement);
                return;
            }
        }

        // 4. Role-based check
        // If we reach here, the user is not a superuser. We must check roles.
        List<Guid> roleMemberIds;
        if (httpContext.Items.TryGetValue("RoleMemberIds", out var cachedRoles))
        {
            roleMemberIds = (List<Guid>)cachedRoles!;
        }
        else
        {
            if (Guid.TryParse(currentUserService.UserId, out var guid))
            {
                roleMemberIds = await dbContext.RoleMembers
                    .Where(ur => ur.MemberId == guid)
                    .Select(ur => ur.RoleId)
                    .ToListAsync();
                
                httpContext.Items["RoleMemberIds"] = roleMemberIds;
            }
            else
            {
                roleMemberIds = new List<Guid>();
            }
        }

        if (roleMemberIds.Count == 0)
        {
            return;
        }

        // Check for InherentlyActionAttribute on the endpoint
        var endpoint = httpContext.GetEndpoint();
        var inherentAction = endpoint?.Metadata.GetMetadata<InherentlyActionAttribute>()?.ActionType;
        
        // Use ModuleId from requirement if set (explicitly requested in HasPermission)
        var moduleId = requirement.ModuleId;

        // Validation: If no ModuleId is provided, ensure the EntityKey is unique across modules
        if (!moduleId.HasValue)
        {
            var moduleIds = await dbContext.EntityTypes
                .AsNoTracking()
                .Where(et => et.Key == requirement.EntityKey)
                .Select(et => et.ModuleId)
                .Distinct()
                .ToListAsync();

            if (moduleIds.Count > 1)
            {
                throw new InvalidOperationException(
                    $"Entity type '{requirement.EntityKey}' is defined in multiple modules. " +
                    "You must specify the 'moduleId' explicitly in the [HasPermission] attribute on the controller or action.");
            }

            if (moduleIds.Count == 0)
            {
                // If not found at all, we can't proceed with permission check
                return;
            }

            moduleId = moduleIds[0];
        }

        // Auto-redirect to Inherent Module if defined for the local entity type
        var inherentModuleId = await dbContext.EntityTypes
            .AsNoTracking()
            .Where(et => et.ModuleId == moduleId.Value && et.Key == requirement.EntityKey)
            .Select(et => et.InherentlyModuleId)
            .FirstOrDefaultAsync();

        if (inherentModuleId.HasValue && inherentModuleId.Value > 0)
        {
            moduleId = inherentModuleId.Value;
        }

        // 5. Command-specific check
        if (!string.IsNullOrEmpty(requirement.CommandKey))
        {
            var commandQuery = dbContext.RolePermissionCommands
                .Where(rpc => roleMemberIds.Contains(rpc.RoleId) &&
                                 rpc.RolePermission.EntityType.Key == requirement.EntityKey &&
                                 rpc.EntityTypeCommand.Key == requirement.CommandKey &&
                                 rpc.Value);
            
            // Check logical ownership: InherentlyModuleId ?? ModuleId
            commandQuery = commandQuery.Where(rpc => 
                (rpc.RolePermission.EntityType.InherentlyModuleId ?? rpc.RolePermission.EntityType.ModuleId) == moduleId.Value);

            if (await commandQuery.AnyAsync())
            {
                context.Succeed(requirement);
            }
            return;
        }

        // 6. Standard CRUD check based on HTTP Method OR Explicit Action
        var query = dbContext.RolePermissions
            .Where(rp => roleMemberIds.Contains(rp.RoleId) && rp.EntityType.Key == requirement.EntityKey);

        // Check logical ownership: InherentlyModuleId ?? ModuleId
        query = query.Where(rp => 
            (rp.EntityType.InherentlyModuleId ?? rp.EntityType.ModuleId) == moduleId.Value);

        bool isAuthorized = false;

        if (requirement.Action != ActionType.Default || (inherentAction != null && inherentAction != ActionType.Default))
        {
            var targetAction = requirement.Action != ActionType.Default 
                ? requirement.Action 
                : inherentAction!.Value;

            isAuthorized = targetAction switch
            {
                ActionType.Read => await query.AnyAsync(rp => rp.Read),
                ActionType.Create => await query.AnyAsync(rp => rp.Create),
                ActionType.Edit => await query.AnyAsync(rp => rp.Edit),
                ActionType.Delete => await query.AnyAsync(rp => rp.Delete),
                ActionType.Log => await query.AnyAsync(rp => rp.Log),
                ActionType.Print => await query.AnyAsync(rp => rp.Print),
                ActionType.Import => await query.AnyAsync(rp => rp.Import),
                ActionType.Export => await query.AnyAsync(rp => rp.Export),
                _ => false
            };
        }
        else
        {
            var method = httpContext.Request.Method.ToUpperInvariant();
            var hasExport = httpContext.Request.Query.ContainsKey("export");
            var isLogRequest = httpContext.Request.RouteValues.ContainsKey("entity_type_key") || 
                               httpContext.Request.RouteValues.ContainsKey("entityTypeKey");

            isAuthorized = method switch
            {
                "GET" when hasExport => await query.AnyAsync(rp => rp.Export),
                "GET" when isLogRequest => await query.AnyAsync(rp => rp.Log),
                "GET" => await query.AnyAsync(rp => rp.Read),
                "POST" => await query.AnyAsync(rp => rp.Create),
                "PUT" or "PATCH" => await query.AnyAsync(rp => rp.Edit),
                "DELETE" => await query.AnyAsync(rp => rp.Delete),
                _ => false
            };
        }

        if (isAuthorized)
        {
            context.Succeed(requirement);
        }
    }
}
