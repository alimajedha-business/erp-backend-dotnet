using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface ICategoryLevelConstraintBusinessRuleValidator
{
    Task ValidateCreateAsync(
        Guid companyId,
        CreateCategoryLevelConstraintDto createDto,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task ValidateCodeLengthChangeAsync(
        Guid companyId,
        int levelNo,
        int currentCodeLength,
        int newCodeLength,
        CancellationToken ct
    );
}
