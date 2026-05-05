using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IWarehouseBusinessRuleValidator
{
    void ValidateParameters(WarehouseParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateWarehouseDto createDto,
        CancellationToken ct
    );

    Task ValidateWarehouseCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedWarehouseId,
        int code,
        CancellationToken ct
    );

    void ValidateAccountingRules(
        CreateWarehouseDto dto
    );

    void ValidateAccountingRules(
        PatchWarehouseDto dto
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
