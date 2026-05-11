using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class WarehouseLocationSchema : IFilterSchema<WarehouseLocation>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["parentLocationId"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.ParentLocationId),
            PropertyType: typeof(Guid?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["code"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.Code),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["title"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.Title),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        filterSchema.Fields["canStoreItem"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.CanStoreItem),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["hasNextLevel"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.HasNextLevel),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["levelNo"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.LevelNo),
            PropertyType: typeof(int),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["length"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.Length),
            PropertyType: typeof(decimal?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["width"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.Width),
            PropertyType: typeof(decimal?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["height"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.Height),
            PropertyType: typeof(decimal?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["maxMass"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.MaxMass),
            PropertyType: typeof(decimal?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["maxVolume"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.MaxVolume),
            PropertyType: typeof(decimal?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["preferredMassUnitId"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.PreferredMassUnitId),
            PropertyType: typeof(Guid?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["preferredLengthUnitId"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.PreferredLengthUnitId),
            PropertyType: typeof(Guid?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["preferredVolumeUnitId"] = new FilterFieldInfo(
            PropertyName: nameof(WarehouseLocation.PreferredVolumeUnitId),
            PropertyType: typeof(Guid?),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
