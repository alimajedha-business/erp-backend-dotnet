using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IUnitOfMeasurementConversionService
{
    Task<UnitOfMeasurementConversionDto> CreateUnitOfMeasurementConversionAsync(
        Guid companyId,
        CreateUnitOfMeasurementConversionDto createUnitOfMeasurementConversionDto,
        CancellationToken ct
    );
    Task<ListResponseModel<UnitOfMeasurementConversionListDto>> GetAllUnitOfMeasurementConversionsAsync(
        Guid companyId,
        UnitOfMeasurementConversionParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );
    Task<UnitOfMeasurementConversionDto> GetUnitOfMeasurementConversionByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
    Task<UnitOfMeasurementConversionDto> PatchUnitOfMeasurementConversionAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchUnitOfMeasurementConversionDto> patchDoc,
        CancellationToken ct
    );
    Task DeleteUnitOfMeasurementConversionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
