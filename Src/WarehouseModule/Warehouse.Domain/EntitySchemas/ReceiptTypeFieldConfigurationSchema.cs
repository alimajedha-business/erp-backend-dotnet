using NGErp.Base.Domain.EntitySchemas;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class ReceiptTypeFieldConfigurationSchema :
    IFilterSchema<Entities.ReceiptTypeFieldConfiguration>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["receiptTypeId"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.ReceiptTypeFieldConfiguration.ReceiptTypeId),
            PropertyType: typeof(Guid),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["fieldDefinitionId"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.ReceiptTypeFieldConfiguration.FieldDefinitionId),
            PropertyType: typeof(Guid),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["exists"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.ReceiptTypeFieldConfiguration.Exists),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        filterSchema.Fields["isRequired"] = new FilterFieldInfo(
            PropertyName: nameof(Entities.ReceiptTypeFieldConfiguration.IsRequired),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
