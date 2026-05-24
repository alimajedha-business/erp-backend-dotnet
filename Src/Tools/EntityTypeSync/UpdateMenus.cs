using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using NGErp.Base.Domain.EntityTypeRegistration;
using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Service.Services;
using NGErp.General.Domain.Entities;

namespace NGErp.Tools.EntityTypeSync;

internal static class UpdateMenus
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
        _ = typeof(HCM.Service.MenuRegistrations.HCMMenuProfile);
        _ = typeof(Warehouse.Service.MenuRegistrations.WarehouseMenuProfile);
        _ = typeof(Shared.Service.MenuRegistrations.SharedMenuProfile);
        _ = typeof(General.Service.MenuRegistrations.GeneralMenuProfile);

        var profiles = DiscoverProfiles();
        
        var dbOptions = new DbContextOptionsBuilder<MainDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        var result = new SyncMenuResult();

        foreach (var profile in profiles)
        {
            Console.WriteLine($"Syncing menus for module: {profile.ModuleId}");
            
            await using var context = new MainDbContext(dbOptions, new ScriptCurrentUserService());
            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var module = await context.Modules.FirstOrDefaultAsync(m => m.Id == profile.ModuleId);
                if (module is null)
                {
                    result.MissingModules.Add(profile.ModuleId.ToString());
                    continue;
                }

                result.SyncedModules.Add($"{module.Prefix} ({module.Id})");

                var definitions = profile.GetMenus().ToList();

                if (profile.DeleteStale)
                {
                    var existingMenus = await context.MenuItems
                        .Where(m => m.ModuleId == profile.ModuleId)
                        .ToListAsync();
                    
                    var flatDefinitions = FlattenMenus(definitions);
                    var staleMenus = existingMenus
                        .Where(m => !flatDefinitions.Any(d => d.NameFa == m.NameFa))
                        .ToList();

                    foreach (var stale in staleMenus)
                    {
                        context.MenuItems.Remove(stale);
                        result.RemovedMenus++;
                    }
                    await context.SaveChangesAsync();
                }

                await SyncMenusRecursiveAsync(context, module, null, definitions, result);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var message = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine($"[ERR] Failed to sync menus for module {profile.ModuleId}: {message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
    }

    static List<MenuDefinition> FlattenMenus(IEnumerable<MenuDefinition> definitions)
    {
        var flatList = new List<MenuDefinition>();
        foreach (var def in definitions)
        {
            flatList.Add(def);
            flatList.AddRange(FlattenMenus(def.Children));
        }
        return flatList;
    }

    static async Task SyncMenusRecursiveAsync(MainDbContext context, Module module, MenuItem? parent, IEnumerable<MenuDefinition> definitions, SyncMenuResult result)
    {
        foreach (var def in definitions)
        {
            var menuItem = await context.MenuItems
                .FirstOrDefaultAsync(m => m.ModuleId == module.Id && m.NameFa == def.NameFa && m.ParentId == (parent != null ? parent.Id : (Guid?)null));

            // Fallback for initial migration: find by name and parent if module_id is not set
            if (menuItem is null)
            {
                menuItem = await context.MenuItems
                    .FirstOrDefaultAsync(m => m.ModuleId == 0 && m.NameFa == def.NameFa && m.ParentId == (parent != null ? parent.Id : (Guid?)null));
            }

            if (menuItem is null)
            {
                menuItem = new MenuItem
                {
                    Id = Guid.NewGuid(),
                    ModuleId = module.Id,
                    NameFa = def.NameFa,
                    NameEn = def.NameEn,
                    ParentId = parent?.Id
                };
                context.MenuItems.Add(menuItem);
                result.AddedMenus++;
            }
            else
            {
                result.UpdatedMenus++;
            }

            menuItem.ModuleId = module.Id; // Ensure ModuleId is set
            menuItem.NameEn = def.NameEn;
            menuItem.Order = def.Order;
            menuItem.Link = def.Link;
            menuItem.ShortKey = def.ShortKey;
            menuItem.NewTab = def.NewTab;
            menuItem.StandardPage = def.StandardPage;
            menuItem.Meta = def.Meta;

            if (!string.IsNullOrEmpty(def.EntityTypeKey))
            {
                var entityType = await context.EntityTypes.FirstOrDefaultAsync(et => et.Key == def.EntityTypeKey);
                if (entityType != null)
                {
                    menuItem.EntityTypeId = entityType.Id;
                    
                    if (!string.IsNullOrEmpty(def.EntityTypeCommandKey))
                    {
                        var command = await context.EntityTypeCommands.FirstOrDefaultAsync(c => c.EntityTypeId == entityType.Id && c.Key == def.EntityTypeCommandKey);
                        menuItem.EntityTypeCommandId = command?.Id;
                    }
                }
            }

            // Save changes to ensure ID is generated for new menu items so they can be used as parents
            if (context.Entry(menuItem).State == EntityState.Added)
            {
                await context.SaveChangesAsync();
            }

            if (def.Children.Any())
            {
                await SyncMenusRecursiveAsync(context, module, menuItem, def.Children, result);
            }
        }
    }

    static List<IMenuModuleProfile> DiscoverProfiles()
    {
        var interfaceType = typeof(IMenuModuleProfile);
        var profiles = new List<IMenuModuleProfile>();

        // Get all assemblies including the current one and all referenced ones
        var assemblies = new HashSet<System.Reflection.Assembly>
        {
            typeof(UpdateMenus).Assembly,
            typeof(HCM.Service.MenuRegistrations.HCMMenuProfile).Assembly,
            typeof(Warehouse.Service.MenuRegistrations.WarehouseMenuProfile).Assembly,
            typeof(Shared.Service.MenuRegistrations.SharedMenuProfile).Assembly,
            typeof(General.Service.MenuRegistrations.GeneralMenuProfile).Assembly
        };

        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    profiles.Add((IMenuModuleProfile)Activator.CreateInstance(type)!);
                }
            }
        }

        return profiles;
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

internal sealed class SyncMenuResult
{
    public List<string> SyncedModules { get; } = [];
    public List<string> MissingModules { get; } = [];
    public int AddedMenus { get; set; }
    public int UpdatedMenus { get; set; }
    public int RemovedMenus { get; set; }
}
