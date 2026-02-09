using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Base.Service.Services;

public interface IAdvancedFilterBuilder
{
    RequestAdvancedFilters Build<T>(FilterNodeDto? filterNodeDto);
}

