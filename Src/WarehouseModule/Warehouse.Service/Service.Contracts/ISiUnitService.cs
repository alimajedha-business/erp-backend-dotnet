using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface ISiUnitService
{
    Task<SiUnitDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<SiUnitSlimDto>> FilterByQAsync(
        SiUnitParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<SiUnitDto>> GetFilteredAsync(
        SiUnitParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );
}
