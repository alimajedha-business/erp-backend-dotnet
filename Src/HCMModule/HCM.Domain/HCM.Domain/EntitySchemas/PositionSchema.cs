using NGErp.Base.Domain.EntitySchemas;
using NGErp.HCM.Domain.Entities;

namespace NGErp.HCM.Domain.EntitySchemas;

public sealed class PositionSchema:IFilterSchema<Position>
{
    public FilterSchema Build()
    {
        var filterSchema = new FilterSchema();

        filterSchema.Fields["status"] = new FilterFieldInfo(
            PropertyName: nameof(Position.Status),
            PropertyType: typeof(bool),
            AllowedOps: new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { "eq", "ne" }
        );
        return filterSchema;

        }
}
