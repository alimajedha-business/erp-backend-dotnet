using NGErp.Base.Domain.EntitySchemas;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Base.Service.Services;

public class AdvancedFilterBuilder(IFilterSchemaProvider schemaProvider) : IAdvancedFilterBuilder
{
    private readonly IFilterSchemaProvider _schemaProvider = schemaProvider;

    public RequestAdvancedFilters Build<T>(FilterNodeDto? filterNodeDto)
    {
        if (filterNodeDto is null || IsEmpty(filterNodeDto))
        {
            return new RequestAdvancedFilters
            {
                Predicate = null,
                Args = []
            };
        }

        var (predicate, args) = DynamicLinqConditionBuilder.Build<T>(filterNodeDto, _schemaProvider);

        return new RequestAdvancedFilters
        {
            Predicate = string.IsNullOrWhiteSpace(predicate) ? null : predicate,
            Args = args
        };
    }

    private static bool IsEmpty(FilterNodeDto node)
    {
        return node is not null &&
               node.Field == null &&
               node.Operator == null &&
               node.Value == null &&
               (node.Children == null || node.Children.Count == 0);
    }
}