using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class RemittanceSchema : IFilterSchema<Remittance>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["number"] = new FilterFieldInfo(
            PropertyName: nameof(Remittance.Number),
            PropertyType: typeof(long),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["remittanceDate"] = new FilterFieldInfo(
            PropertyName: nameof(Remittance.RemittanceDate),
            PropertyType: typeof(DateOnly),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "gt", "ge", "lt", "le" }
        );

        filterSchema.Fields["remittanceTypeId"] = new FilterFieldInfo(
            PropertyName: nameof(Remittance.RemittanceTypeId),
            PropertyType: typeof(Guid),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["status"] = new FilterFieldInfo(
            PropertyName: nameof(Remittance.Status),
            PropertyType: typeof(RemittanceStatus),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["description"] = new FilterFieldInfo(
            PropertyName: nameof(Remittance.Description),
            PropertyType: typeof(string),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne", "startsWith", "contains", "endsWith" }
        );

        return filterSchema;
    }
}
