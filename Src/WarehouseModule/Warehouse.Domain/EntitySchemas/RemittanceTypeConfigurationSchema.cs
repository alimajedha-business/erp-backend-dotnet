using NGErp.Base.Domain.EntitySchemas;
using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Domain.EntitySchemas;

public sealed class RemittanceTypeConfigurationSchema : IFilterSchema<RemittanceTypeConfiguration>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["remittanceTypeId"] = new FilterFieldInfo(
            PropertyName: nameof(RemittanceTypeConfiguration.RemittanceTypeId),
            PropertyType: typeof(Guid),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );

        return filterSchema;
    }
}
