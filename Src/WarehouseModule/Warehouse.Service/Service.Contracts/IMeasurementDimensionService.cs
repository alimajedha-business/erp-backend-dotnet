using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IMeasurementDimensionService
{
    Task<MeasurementDimensionDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<MeasurementDimensionDto>> GetAllAsync(
        MeasurementDimensionParameters parameters,
        CancellationToken ct = default
    );
}
