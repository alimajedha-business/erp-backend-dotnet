using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IReceiptBusinessRuleValidator
{
    Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptDto createDto,
        CancellationToken ct
    );
}
