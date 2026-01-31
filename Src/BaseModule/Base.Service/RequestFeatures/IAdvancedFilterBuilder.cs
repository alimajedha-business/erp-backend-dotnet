using NGErp.Base.Service.DTOs;

namespace NGErp.Base.Service.RequestFeatures;

public interface IAdvancedFilterBuilder
{
    RequestAdvancedFilters Build<T>(FilterNodeDto? filterNodeDto);
}

