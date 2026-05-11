using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IUnitService
{
    Task<UnitDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<UnitSlimDto>> FilterByQAsync(
        UnitParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<UnitDto>> GetFilteredAsync(
        UnitParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );
}
