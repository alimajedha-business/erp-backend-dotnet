using NGErp.Base.Domain.EntitySchemas;
using NGErp.Base.Service.DTOs;

namespace NGErp.Base.Service.RequestFeatures;

public class AdvancedFilterBuilder(IFilterSchemaProvider schemaProvider) : IAdvancedFilterBuilder
{
    private readonly IFilterSchemaProvider _schemaProvider = schemaProvider;

    public RequestAdvancedFilters Build<T>(FilterNodeDto? filterNodeDto)
    {
        if (filterNodeDto is null)
        {
            return new RequestAdvancedFilters
            {
                Predicate = null,
                Args = []
            };
        }

        try
        {
            (string? search, object[] searchPrms) =
                DynamicLinqConditionBuilder.Build<T>(filterNodeDto, _schemaProvider);

            return new RequestAdvancedFilters
            {
                Predicate = search,
                Args = searchPrms
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}

