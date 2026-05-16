using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.Services;
using NGErp.General.Domain.Entities;

namespace NGErp.Tools.EntityTypeSync;

internal static class UpdateEntities
{
    public static async Task Main(string[] args)
    {
        var options = ToolOptions.Parse(args);
        var connectionString = options.ConnectionString ?? ReadConnectionStringFromEnvironmentOrAppSettings();

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string not found.");
        }

        // Force load assemblies by referencing a type from each module
        _ = typeof(HCM.Service.EntityTypeRegistrations.HCMModuleProfile);
        _ = typeof(Warehouse.Service.EntityTypeRegistrations.WarehouseModuleProfile);
        _ = typeof(Payroll.Service.EntityTypeRegistrations.PayrollModuleProfile);

        var profiles = DiscoverProfiles();
        
        var dbOptions = new DbContextOptionsBuilder<MainDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        var result = new SyncResult();

        foreach (var profile in profiles)
        {
            Console.WriteLine($"Syncing module: {profile.Prefix} (ID: {profile.ModuleId})");
            
            await using var context = new MainDbContext(dbOptions, new ScriptCurrentUserService());
            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var module = await context.Modules.FirstOrDefaultAsync(m => m.Id == profile.ModuleId);
                if (module is null)
                {
                    result.MissingModules.Add($"{profile.Prefix} ({profile.ModuleId})");
                    continue;
                }

                if (module.Prefix != profile.Prefix)
                {
                    Console.WriteLine($"Updating module prefix from '{module.Prefix}' to '{profile.Prefix}'");
                    module.Prefix = profile.Prefix;
                }

                result.SyncedModules.Add($"{module.Prefix} ({module.Id})");

                var existingEntityTypes = await context.EntityTypes
                    .Include(et => et.Commands)
                    .Where(et => et.ModuleId == profile.ModuleId)
                    .ToListAsync();

                var definitions = profile.GetDefinitions().ToList();
                var profileKeys = definitions.Select(d => d.Key).ToHashSet(StringComparer.Ordinal);

                if (profile.DeleteStale)
                {
                    foreach (var stale in existingEntityTypes.Where(et => !profileKeys.Contains(et.Key)).ToList())
                    {
                        await DeleteReferencesAsync(context, stale);
                        context.EntityTypes.Remove(stale);
                        result.RemovedEntityTypes++;
                    }
                    await context.SaveChangesAsync();
                }

                foreach (var definition in definitions)
                {
                    var entityType = existingEntityTypes.FirstOrDefault(et => et.Key == definition.Key);

                    if (entityType is null)
                    {
                        // Check if an entity type with same NameFa exists for this module
                        var duplicate = await context.EntityTypes
                            .FirstOrDefaultAsync(et => et.ModuleId == profile.ModuleId && et.NameFa == definition.NameFa);
                        
                        if (duplicate != null)
                        {
                            Console.WriteLine($"[WRN] Conflict for '{definition.Key}': NameFa '{definition.NameFa}' is already used by '{duplicate.Key}'. Skipping.");
                            continue;
                        }

                        entityType = new EntityType
                        {
                            Id = Guid.NewGuid(),
                            ModuleId = profile.ModuleId,
                            Key = definition.Key,
                            NameFa = definition.NameFa,
                            NameEn = definition.NameEn,
                            Code = definition.Code,
                            Module = module
                        };
                        context.EntityTypes.Add(entityType);
                        result.AddedEntityTypes++;
                    }
                    
                    if (Apply(entityType, definition))
                        result.UpdatedEntityTypes++;

                    var modelName = definition.Key.Replace("_", "").ToLowerInvariant();
                    await SyncContentTypeAsync(context, entityType, profile.Prefix, modelName);
                    await SyncCommandsAsync(context, entityType, definition, result);
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var message = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine($"[ERR] Failed to sync module {profile.Prefix}: {message}");
            }
        }

        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
    }

    static List<IEntityTypeModuleProfile> DiscoverProfiles()
    {
        var interfaceType = typeof(IEntityTypeModuleProfile);
        var profiles = new List<IEntityTypeModuleProfile>();
        
        // Get all assemblies including the current one and all referenced ones
        var assemblies = new HashSet<System.Reflection.Assembly>
        {
            typeof(UpdateEntities).Assembly,
            typeof(HCM.Service.EntityTypeRegistrations.HCMModuleProfile).Assembly,
            typeof(Warehouse.Service.EntityTypeRegistrations.WarehouseModuleProfile).Assembly,
            typeof(Payroll.Service.EntityTypeRegistrations.PayrollModuleProfile).Assembly
        };

        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    profiles.Add((IEntityTypeModuleProfile)Activator.CreateInstance(type)!);
                }
            }
        }
        
        return profiles;
    }

    static bool Apply(EntityType entityType, EntityTypeDefinition definition)
    {
        bool changed = entityType.NameFa != definition.NameFa || 
                       entityType.NameEn != definition.NameEn ||
                       entityType.Code != definition.Code ||
                       entityType.Ordering != definition.Ordering ||
                       entityType.Readable != definition.Attributes.Readable ||
                       entityType.Creatable != definition.Attributes.Creatable ||
                       entityType.Editable != definition.Attributes.Editable ||
                       entityType.Deletable != definition.Attributes.Deletable ||
                       entityType.Loggable != definition.Attributes.Loggable ||
                       entityType.Printable != definition.Attributes.Printable ||
                       entityType.Importable != definition.Attributes.Importable ||
                       entityType.Exportable != definition.Attributes.Exportable ||
                       entityType.IfNotCreator != definition.Attributes.IfNotCreator ||
                       entityType.HasRestriction != definition.Attributes.HasRestriction ||
                       entityType.Permissible != definition.Attributes.Permissible;

        entityType.NameFa = definition.NameFa;
        entityType.NameEn = definition.NameEn;
        entityType.Code = definition.Code;
        entityType.Ordering = definition.Ordering;
        entityType.Readable = definition.Attributes.Readable;
        entityType.Creatable = definition.Attributes.Creatable;
        entityType.Editable = definition.Attributes.Editable;
        entityType.Deletable = definition.Attributes.Deletable;
        entityType.Loggable = definition.Attributes.Loggable;
        entityType.Printable = definition.Attributes.Printable;
        entityType.Importable = definition.Attributes.Importable;
        entityType.Exportable = definition.Attributes.Exportable;
        entityType.IfNotCreator = definition.Attributes.IfNotCreator;
        entityType.HasRestriction = definition.Attributes.HasRestriction;
        entityType.Permissible = definition.Attributes.Permissible;

        return changed;
    }

    static async Task SyncContentTypeAsync(MainDbContext context, EntityType entityType, string appLabel, string modelName)
    {
        var contentType = await context.ContentTypes.FirstOrDefaultAsync(ct => ct.AppLabel == appLabel && ct.Model == modelName);
        if (contentType is null)
        {
            contentType = new ContentType { AppLabel = appLabel, Model = modelName };
            context.ContentTypes.Add(contentType);
            await context.SaveChangesAsync();
        }
        entityType.ContentTypeId = contentType.Id;
    }

    static async Task SyncCommandsAsync(MainDbContext context, EntityType entityType, EntityTypeDefinition definition, SyncResult result)
    {
        var commandKeys = definition.Commands.Select(c => c.Key).ToHashSet();
        foreach (var cmdDef in definition.Commands)
        {
            var command = entityType.Commands.FirstOrDefault(c => c.Key == cmdDef.Key);
            if (command is null)
            {
                command = new EntityTypeCommand 
                { 
                    Id = Guid.NewGuid(), 
                    EntityType = entityType, 
                    Key = cmdDef.Key,
                    NameFa = cmdDef.NameFa,
                    NameEn = cmdDef.NameEn
                };
                context.EntityTypeCommands.Add(command);
                result.AddedCommands++;
            }
            else
            {
                if (command.NameFa != cmdDef.NameFa || command.NameEn != cmdDef.NameEn || command.Ordering != cmdDef.Ordering || command.Permissible != cmdDef.Permissible)
                {
                    result.UpdatedCommands++;
                }
            }

            command.NameFa = cmdDef.NameFa;
            command.NameEn = cmdDef.NameEn;
            command.Ordering = cmdDef.Ordering;
            command.Permissible = cmdDef.Permissible;
        }

        foreach (var stale in entityType.Commands.Where(c => !commandKeys.Contains(c.Key)).ToList())
        {
            await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM shared.role_permission_commands WHERE entity_type_command_id = {stale.Id}");
            context.EntityTypeCommands.Remove(stale);
            result.RemovedCommands++;
        }
    }

    static async Task DeleteReferencesAsync(MainDbContext context, EntityType entityType)
    {
        await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM general.menu_items WHERE entity_type_id = {entityType.Id}");
        await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM shared.role_permissions WHERE entity_type_id = {entityType.Id}");
        await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM shared.role_permission_commands WHERE entity_type_id = {entityType.Id}");
        await context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM general.entity_type_dependencies WHERE entity_type_id = {entityType.Id} OR required_entity_type_id = {entityType.Id}");
    }

    static string? ReadConnectionStringFromEnvironmentOrAppSettings()
    {
        var appSettingsPath = FindFileUpwards(Path.Combine("Src", "API", "API", "appsettings.Development.json"));
        if (appSettingsPath == null) return null;
        using var document = System.Text.Json.JsonDocument.Parse(File.ReadAllText(appSettingsPath));
        return document.RootElement.GetProperty("ConnectionStrings").GetProperty("NGERPDatabase").GetString();
    }

    static string? FindFileUpwards(string relativePath)
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (directory != null)
        {
            var candidate = Path.Combine(directory.FullName, relativePath);
            if (File.Exists(candidate)) return candidate;
            directory = directory.Parent;
        }
        return null;
    }
}

internal sealed record ToolOptions(string? ConnectionString)
{
    public static ToolOptions Parse(string[] args)
    {
        string? connectionString = null;
        for (var i = 0; i < args.Length; i++)
        {
            if (args[i] == "--connection" && i + 1 < args.Length) connectionString = args[++i];
        }
        return new ToolOptions(connectionString);
    }
}

internal sealed class SyncResult
{
    public List<string> SyncedModules { get; } = [];
    public List<string> MissingModules { get; } = [];
    public int AddedEntityTypes { get; set; }
    public int UpdatedEntityTypes { get; set; }
    public int RemovedEntityTypes { get; set; }
    public int AddedCommands { get; set; }
    public int UpdatedCommands { get; set; }
    public int RemovedCommands { get; set; }
}

internal sealed class ScriptCurrentUserService : ICurrentUserService
{
    public string? UserId => null;
    public string? Username => "entity-type-sync";
    public string? Email => null;
    public string? Token => null;
    public bool IsAuthenticated => false;
    public ClaimsPrincipal? User => null;
}
