using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IUnitOfMeasurementService
{
    Task<UnitOfMeasurementDto> CreateAsync(
        CreateUnitOfMeasurementDto createDto,
        CancellationToken ct
    );

    Task<UnitOfMeasurementDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<UnitOfMeasurementDto>> FilterByQAsync(
        UnitOfMeasurementParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<UnitOfMeasurementDto>> GetFilteredAsync(
        UnitOfMeasurementParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<UnitOfMeasurementDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchUnitOfMeasurementDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(CancellationToken ct);
}
