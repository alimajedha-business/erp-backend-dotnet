using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IWarehouseLocationBusinessRuleValidator
{
    void ValidateParameters(WarehouseLocationParameters parameters);

    void ValidateHasNextLevel(
        int levelNo,
        bool hasNextLevel
    );

    Task ValidateParentAsync(
        Guid warehouseId,
        Guid? parentLocationId,
        CancellationToken ct
    );

    Task ValidateWarehouseLocationCodeUniquenessAsync(
        Guid warehouseId,
        Guid? excludedLocationId,
        int code,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    );

    Task ValidateCreateAsync(
        Guid warehouseId,
        CreateWarehouseLocationDto createDto,
        CancellationToken ct
    );
}
