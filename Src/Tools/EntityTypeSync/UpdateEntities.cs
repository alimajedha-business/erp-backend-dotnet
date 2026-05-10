using System.Security.Claims;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.Services;
using NGErp.General.Domain.Entities;

namespace NGErp.Tools.EntityTypeSync;

internal static class UpdateEntities
{
    public static async Task Main(string[] args)
    {
        var options = ToolOptions.Parse(args);
        var profilesPath = Path.GetFullPath(options.ProfilesPath ?? Path.Combine(AppContext.BaseDirectory, "profiles"));
        var connectionString = options.ConnectionString ?? ReadConnectionStringFromEnvironmentOrAppSettings();

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string not found. Pass --connection, set NGERP_CONNECTION_STRING, or keep Src/API/API/appsettings.Development.json available."
            );
        }

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        var profiles = await ReadProfilesAsync(profilesPath, jsonOptions);
        Validate(profiles);

        var dbOptions = new DbContextOptionsBuilder<MainDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        await using var context = new MainDbContext(dbOptions, new ScriptCurrentUserService());
        await using var transaction = await context.Database.BeginTransactionAsync();

        var result = new SyncResult();

        foreach (var profile in profiles)
        {
            var module = await context.Modules
                .FirstOrDefaultAsync(m => m.Id == profile.ModuleId);

            if (module is null)
            {
                result.MissingModules.Add($"{profile.Prefix} ({profile.ModuleId})");
                continue;
            }

            result.SyncedModules.Add($"{module.Prefix} ({module.Id})");

            var existingEntityTypes = await context.EntityTypes
                .Include(entityType => entityType.Commands)
                .Where(entityType => entityType.ModuleId == profile.ModuleId)
                .ToListAsync();

            var profileKeys = profile.EntityTypes
                .Select(entityType => entityType.Key)
                .ToHashSet(StringComparer.Ordinal);

            foreach (var entityTypeDefinition in profile.EntityTypes)
            {
                var entityType = existingEntityTypes
                    .FirstOrDefault(entityType => entityType.Key == entityTypeDefinition.Key);

                if (entityType is null)
                {
                    entityType = new EntityType
                    {
                        Id = Guid.NewGuid(),
                        ModuleId = profile.ModuleId,
                        Key = entityTypeDefinition.Key,
                        NameFa = entityTypeDefinition.NameFa,
                        NameEn = entityTypeDefinition.NameEn,
                        Code = entityTypeDefinition.Code,
                        Module = module
                    };

                    context.EntityTypes.Add(entityType);
                    existingEntityTypes.Add(entityType);
                    result.AddedEntityTypes++;
                    Apply(entityType, entityTypeDefinition);
                }
                else
                {
                    if (Apply(entityType, entityTypeDefinition))
                        result.UpdatedEntityTypes++;
                }

                var modelName = entityTypeDefinition.Key.Replace("_", "").ToLowerInvariant();
                if (await SyncContentTypeAsync(context, entityType, profile.Prefix, modelName))
                    result.UpdatedEntityTypes++;

                await SyncCommandsAsync(context, entityType, entityTypeDefinition, result);
            }

            if (!profile.DeleteStale)
                continue;

            foreach (var staleEntityType in existingEntityTypes.Where(entityType => !profileKeys.Contains(entityType.Key)).ToList())
            {
                await DeleteRolePermissionReferencesAsync(context, staleEntityType);
                context.EntityTypes.Remove(staleEntityType);
                result.RemovedEntityTypes++;
            }
        }

        await context.SaveChangesAsync();
        await transaction.CommitAsync();

        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
    }

    static async Task<IReadOnlyList<EntityTypeProfile>> ReadProfilesAsync(
        string profilesPath,
        JsonSerializerOptions jsonOptions
    )
    {
        var files = File.Exists(profilesPath)
            ? [profilesPath]
            : Directory.Exists(profilesPath)
                ? Directory.GetFiles(profilesPath, "*.json").OrderBy(path => path).ToArray()
                : throw new FileNotFoundException("Entity type profile path was not found.", profilesPath);

        if (files.Length == 0)
            throw new InvalidOperationException($"No entity type profile JSON files found in {profilesPath}.");

        var profiles = new List<EntityTypeProfile>();

        foreach (var file in files)
        {
            var profile = JsonSerializer.Deserialize<EntityTypeProfile>(
                await File.ReadAllTextAsync(file),
                jsonOptions
            ) ?? throw new InvalidOperationException($"Entity type profile JSON is empty: {file}");

            profile.SourceFile = file;
            profiles.Add(profile);
        }

        return profiles;
    }

    static bool Apply(EntityType entityType, EntityTypeDefinition definition)
    {
        var changed =
            entityType.NameFa != definition.NameFa ||
            entityType.NameEn != definition.NameEn ||
            entityType.Code != definition.Code ||
            entityType.Ordering != definition.Ordering ||
            entityType.Readable != definition.Attrs.Readable ||
            entityType.Creatable != definition.Attrs.Creatable ||
            entityType.Editable != definition.Attrs.Editable ||
            entityType.Deletable != definition.Attrs.Deletable ||
            entityType.Loggable != definition.Attrs.Loggable ||
            entityType.Printable != definition.Attrs.Printable ||
            entityType.Importable != definition.Attrs.Importable ||
            entityType.Exportable != definition.Attrs.Exportable ||
            entityType.IfNotCreator != definition.Attrs.IfNotCreator ||
            entityType.HasRestriction != definition.Attrs.HasRestriction ||
            entityType.Permissible != definition.Attrs.Permissible ||
            entityType.HasConstraint != definition.Attrs.HasConstraint;

        entityType.NameFa = definition.NameFa;
        entityType.NameEn = definition.NameEn;
        entityType.Code = definition.Code;
        entityType.Ordering = definition.Ordering;
        entityType.Readable = definition.Attrs.Readable;
        entityType.Creatable = definition.Attrs.Creatable;
        entityType.Editable = definition.Attrs.Editable;
        entityType.Deletable = definition.Attrs.Deletable;
        entityType.Loggable = definition.Attrs.Loggable;
        entityType.Printable = definition.Attrs.Printable;
        entityType.Importable = definition.Attrs.Importable;
        entityType.Exportable = definition.Attrs.Exportable;
        entityType.IfNotCreator = definition.Attrs.IfNotCreator;
        entityType.HasRestriction = definition.Attrs.HasRestriction;
        entityType.Permissible = definition.Attrs.Permissible;
        entityType.HasConstraint = definition.Attrs.HasConstraint;

        return changed;
    }

    static async Task<bool> SyncContentTypeAsync(
        MainDbContext context,
        EntityType entityType,
        string appLabel,
        string modelName
    )
    {
        var contentType = await context.ContentTypes
            .FirstOrDefaultAsync(ct => ct.AppLabel == appLabel && ct.Model == modelName);

        if (contentType is null)
        {
            contentType = new ContentType
            {
                AppLabel = appLabel,
                Model = modelName
            };
            context.ContentTypes.Add(contentType);
            // Save changes immediately to get the identity ID
            await context.SaveChangesAsync();
        }

        if (entityType.ContentTypeId != contentType.Id)
        {
            entityType.ContentTypeId = contentType.Id;
            return true;
        }

        return false;
    }

    static async Task SyncCommandsAsync(
        MainDbContext context,
        EntityType entityType,
        EntityTypeDefinition definition,
        SyncResult result
    )
    {
        var commandKeys = definition.Commands
            .Select(command => command.Key)
            .ToHashSet(StringComparer.Ordinal);

        foreach (var commandDefinition in definition.Commands)
        {
            var command = entityType.Commands
                .FirstOrDefault(entityTypeCommand => entityTypeCommand.Key == commandDefinition.Key);

            if (command is null)
            {
                command = new EntityTypeCommand
                {
                    Id = Guid.NewGuid(),
                    EntityType = entityType,
                    Key = commandDefinition.Key,
                    NameFa = commandDefinition.NameFa,
                    NameEn = commandDefinition.NameEn
                };

                entityType.Commands.Add(command);
                context.EntityTypeCommands.Add(command);
                result.AddedCommands++;
            }
            else
            {
                if (
                    command.NameFa != commandDefinition.NameFa ||
                    command.NameEn != commandDefinition.NameEn ||
                    command.Ordering != commandDefinition.Ordering ||
                    command.Permissible != commandDefinition.Permissible
                )
                {
                    result.UpdatedCommands++;
                }
            }

            command.NameFa = commandDefinition.NameFa;
            command.NameEn = commandDefinition.NameEn;
            command.Ordering = commandDefinition.Ordering;
            command.Permissible = commandDefinition.Permissible;
        }

        foreach (var staleCommand in entityType.Commands.Where(command => !commandKeys.Contains(command.Key)).ToList())
        {
            await DeleteRolePermissionReferencesAsync(context, staleCommand);
            context.EntityTypeCommands.Remove(staleCommand);
            result.RemovedCommands++;
        }
    }

    static async Task DeleteRolePermissionReferencesAsync(MainDbContext context, EntityType entityType)
    {
        await context.Database.ExecuteSqlInterpolatedAsync($@"
DELETE FROM general.entity_type_dependencies
WHERE entity_type_id = {entityType.Id}
   OR required_entity_type_id = {entityType.Id};
");

        await context.Database.ExecuteSqlInterpolatedAsync($@"
DELETE rpc
FROM shared.role_permission_commands AS rpc
INNER JOIN general.entity_type_commands AS etc
    ON etc.id = rpc.entity_type_command_id
WHERE etc.entity_type_id = {entityType.Id};
");

        await context.Database.ExecuteSqlInterpolatedAsync($@"
DELETE FROM shared.role_permission_commands
WHERE entity_type_id = {entityType.Id};
");

        await context.Database.ExecuteSqlInterpolatedAsync($@"
DELETE FROM shared.role_permissions
WHERE entity_type_id = {entityType.Id};
");
    }

    static async Task DeleteRolePermissionReferencesAsync(MainDbContext context, EntityTypeCommand command)
    {
        await context.Database.ExecuteSqlInterpolatedAsync($@"
DELETE FROM shared.role_permission_commands
WHERE entity_type_command_id = {command.Id};
");
    }

    static void Validate(IReadOnlyList<EntityTypeProfile> profiles)
    {
        var duplicateProfiles = profiles
            .GroupBy(profile => profile.ModuleId)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key.ToString())
            .ToList();

        if (duplicateProfiles.Count > 0)
            throw new InvalidOperationException($"Duplicate profile module IDs: {string.Join(", ", duplicateProfiles)}");

        foreach (var profile in profiles)
        {
            if (string.IsNullOrWhiteSpace(profile.Prefix))
                throw new InvalidOperationException($"Profile {profile.SourceFile} must have a prefix.");

            if (!DjangoModuleIds.All.Contains(profile.ModuleId))
                throw new InvalidOperationException(
                    $"Profile {profile.SourceFile} has invalid moduleId {profile.ModuleId}. Valid Django module IDs are: {string.Join(", ", DjangoModuleIds.All)}."
                );

            var duplicateEntityTypeKeys = profile.EntityTypes
                .GroupBy(entityType => entityType.Key, StringComparer.Ordinal)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();

            if (duplicateEntityTypeKeys.Count > 0)
            {
                throw new InvalidOperationException(
                    $"Profile {profile.Prefix} has duplicate entity type keys: {string.Join(", ", duplicateEntityTypeKeys)}"
                );
            }

            foreach (var entityType in profile.EntityTypes)
            {
                ValidateUppercaseKey(entityType.Key, $"entity type in profile {profile.Prefix}");

                if (!entity_types.DeclaredKeys.Contains(entityType.Key))
                    throw new InvalidOperationException($"Entity type key {entityType.Key} is not declared in entity_types.cs.");

                if (string.IsNullOrWhiteSpace(entityType.Code) || entityType.Code.Length > 4)
                    throw new InvalidOperationException($"Entity type {entityType.Key} must have a non-empty code with at most 4 characters.");

                var duplicateCommandKeys = entityType.Commands
                    .GroupBy(command => command.Key, StringComparer.Ordinal)
                    .Where(group => group.Count() > 1)
                    .Select(group => group.Key)
                    .ToList();

                if (duplicateCommandKeys.Count > 0)
                {
                    throw new InvalidOperationException(
                        $"Entity type {entityType.Key} has duplicate command keys: {string.Join(", ", duplicateCommandKeys)}"
                    );
                }

                foreach (var command in entityType.Commands)
                {
                    ValidateUppercaseKey(command.Key, $"command in entity type {entityType.Key}");
                }
            }
        }
    }

    static void ValidateUppercaseKey(string key, string context)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException($"Missing key for {context}.");

        if (key != key.ToUpperInvariant())
            throw new InvalidOperationException($"Key {key} for {context} must be uppercase.");
    }

    static string? ReadConnectionStringFromEnvironmentOrAppSettings()
    {
        var fromEnvironment = Environment.GetEnvironmentVariable("NGERP_CONNECTION_STRING")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__NGERPDatabase");

        if (!string.IsNullOrWhiteSpace(fromEnvironment))
            return fromEnvironment;

        var appSettingsPath = FindFileUpwards(Path.Combine("Src", "API", "API", "appsettings.Development.json"));
        if (appSettingsPath is null)
            return null;

        using var document = JsonDocument.Parse(File.ReadAllText(appSettingsPath));
        return document.RootElement
            .GetProperty("ConnectionStrings")
            .GetProperty("NGERPDatabase")
            .GetString();
    }

    static string? FindFileUpwards(string relativePath)
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, relativePath);
            if (File.Exists(candidate))
                return candidate;

            directory = directory.Parent;
        }

        return null;
    }
}

internal sealed record ToolOptions(string? ProfilesPath, string? ConnectionString)
{
    public static ToolOptions Parse(string[] args)
    {
        string? profilesPath = null;
        string? connectionString = null;

        for (var i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--profiles" when i + 1 < args.Length:
                case "--profile" when i + 1 < args.Length:
                    profilesPath = args[++i];
                    break;
                case "--connection" when i + 1 < args.Length:
                    connectionString = args[++i];
                    break;
                default:
                    throw new InvalidOperationException($"Unknown or incomplete argument: {args[i]}");
            }
        }

        return new ToolOptions(profilesPath, connectionString);
    }
}

internal sealed class EntityTypeProfile
{
    public long ModuleId { get; init; }
    public string Prefix { get; init; } = string.Empty;
    public bool DeleteStale { get; init; } = true;
    public IReadOnlyList<EntityTypeDefinition> EntityTypes { get; init; } = [];
    public string? SourceFile { get; set; }
}

internal sealed class EntityTypeDefinition
{
    public string Key { get; init; } = string.Empty;
    public string NameFa { get; init; } = string.Empty;
    public string NameEn { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public short? Ordering { get; init; }
    public EntityTypeAttrs Attrs { get; init; } = new();
    public IReadOnlyList<EntityTypeCommandDefinition> Commands { get; init; } = [];
}

internal sealed class EntityTypeAttrs
{
    public bool Readable { get; init; }
    public bool Creatable { get; init; }
    public bool Editable { get; init; }
    public bool Deletable { get; init; }
    public bool Loggable { get; init; }
    public bool Printable { get; init; }
    public bool Importable { get; init; }
    public bool Exportable { get; init; }
    public bool IfNotCreator { get; init; }
    public bool HasRestriction { get; init; }
    public bool HasConstraint { get; init; }
    public bool Permissible { get; init; } = true;
}

internal sealed class EntityTypeCommandDefinition
{
    public string Key { get; init; } = string.Empty;
    public string NameFa { get; init; } = string.Empty;
    public string NameEn { get; init; } = string.Empty;
    public short? Ordering { get; init; }
    public bool Permissible { get; init; } = true;
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

internal static class DjangoModuleIds
{
    public const long General = 1;
    public const long Shared = 2;
    public const long Accounting = 3;
    public const long Store = 4;
    public const long ProjectManagement = 5;
    public const long Payroll = 6;
    public const long Treasury = 7;
    public const long File = 8;

    public static IReadOnlySet<long> All { get; } =
        new HashSet<long> { General, Shared, Accounting, Store, ProjectManagement, Payroll, Treasury, File };
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

