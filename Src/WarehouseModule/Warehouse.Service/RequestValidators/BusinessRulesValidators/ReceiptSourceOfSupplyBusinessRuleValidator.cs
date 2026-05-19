using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class ReceiptSourceOfSupplyBusinessRuleValidator(
    IReceiptSourceOfSupplyRepository receiptSourceOfSupplyRepository
) : IReceiptSourceOfSupplyBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "title"
    };

    private readonly IReceiptSourceOfSupplyRepository _receiptSourceOfSupplyRepository =
        receiptSourceOfSupplyRepository;

    public void ValidateParameters(ReceiptSourceOfSupplyParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptSourceOfSupplyDto createDto,
        CancellationToken ct
    )
    {
        await ValidateReceiptSourceOfSupplyCodeUniquenessAsync(
            companyId,
            excludedReceiptSourceOfSupplyId: null,
            createDto.Code!.Value,
            ct
        );
    }

    public async Task ValidateReceiptSourceOfSupplyCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedReceiptSourceOfSupplyId,
        int code,
        CancellationToken ct
    )
    {
        var exists = excludedReceiptSourceOfSupplyId is null
            ? await _receiptSourceOfSupplyRepository.AnyAsync(
                e => e.CompanyId == companyId && e.Code == code,
                ct
            )
            : await _receiptSourceOfSupplyRepository.AnyAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.Id != excludedReceiptSourceOfSupplyId.Value &&
                    e.Code == code,
                ct
            );

        if (exists)
            throw new ReceiptSourceOfSupplyCodeAlreadyExistsException(code);
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _receiptSourceOfSupplyRepository.AnyAsync(
            e =>
                e.CompanyId == companyId &&
                e.Id == id,
            ct
        );

        if (!exists)
            throw new ReceiptSourceOfSupplyNotFoundException();

        if (await _receiptSourceOfSupplyRepository.HasReceiptFieldValueReferencesAsync(
            companyId,
            id,
            ct
        ))
            throw new ReceiptSourceOfSupplyHasRelatedEntitiesException();
    }
}
