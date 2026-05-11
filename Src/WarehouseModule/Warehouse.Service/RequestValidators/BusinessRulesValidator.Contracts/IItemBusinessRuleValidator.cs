using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IItemBusinessRuleValidator
{
    void ValidateParameters(ItemParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateItemDto createDto,
        CancellationToken ct
    );

    Task ValidateItemCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedItemId,
        string code,
        CancellationToken ct
    );

    void ValidateItemUnitOfMeasurementCount(
        IEnumerable<CreateItemUnitOfMeasurementDto>? itemUnitOfMeasurements
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    );
}
