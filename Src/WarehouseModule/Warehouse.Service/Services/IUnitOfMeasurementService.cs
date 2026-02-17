using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IUnitOfMeasurementService
{
    Task<UnitOfMeasurementDto> CreateUnitOfMeasurementAsync(
        Guid companyId,
        CreateUnitOfMeasurementDto createDto,
        CancellationToken ct
    );
    Task<ListResponseModel<UnitOfMeasurementDto>> GetAllUnitOfMeasurementsAsync(
        Guid companyId,
        UnitOfMeasurementParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );
    Task<UnitOfMeasurementDto> GetUnitOfMeasurementByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
    Task<UnitOfMeasurementDto> PatchUnitOfMeasurementAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchUnitOfMeasurementDto> patchDoc,
        CancellationToken ct
    );
    Task DeleteUnitOfMeasurementAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
