using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IWarehouseTypeBusinessRuleValidator
{
    void ValidateParameters(WarehouseTypeParameters parameters);

    Task ValidateCreateAsync(
        CreateWarehouseTypeDto createDto,
        CancellationToken ct
    );

    Task ValidateWarehouseTypeCodeUniquenessAsync(
        Guid? excludedWarehouseTypeId,
        int code,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
