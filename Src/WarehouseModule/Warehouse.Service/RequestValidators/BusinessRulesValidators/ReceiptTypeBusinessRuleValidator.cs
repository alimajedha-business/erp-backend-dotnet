using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class ReceiptTypeBusinessRuleValidator(
    IReceiptTypeRepository receiptRepository,
    IReceiptTypeConfigurationRepository configurationRepository
) : IReceiptTypeBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "title"
    };

    private readonly IReceiptTypeRepository _receiptRepository = receiptRepository;
    private readonly IReceiptTypeConfigurationRepository _configurationRepository =
        configurationRepository;

    public void ValidateParameters(ReceiptTypeParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptTypeDto createDto,
        CancellationToken ct
    )
    {
        await ValidateReceiptTypeCodeUniquenessAsync(
            companyId,
            excludedReceiptId: null,
            createDto.Code,
            ct
        );
    }

    public async Task ValidateReceiptTypeCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedReceiptId,
        int code,
        CancellationToken ct
    )
    {
        var exists = excludedReceiptId is null
            ? await _receiptRepository.AnyAsync(
                e => e.CompanyId == companyId && e.Code == code,
                ct
            )
            : await _receiptRepository.AnyAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.Id != excludedReceiptId.Value &&
                    e.Code == code,
                ct
            );

        if (exists)
            throw new ReceiptTypeCodeAlreadyExistsException(code);
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _receiptRepository.AnyAsync(
            e =>
                e.CompanyId == companyId &&
                e.Id == id,
            ct
        );

        if (!exists)
            throw new ReceiptTypeNotFoundException();

        if (await HasRelatedEntitiesAsync(companyId, id, ct))
            throw new ReceiptTypeHasRelatedEntitiesException();
    }

    private async Task<bool> HasRelatedEntitiesAsync(
        Guid companyId,
        Guid receiptId,
        CancellationToken ct
    )
    {
        return await _configurationRepository.AnyAsync(
            e => e.CompanyId == companyId && e.ReceiptTypeId == receiptId,
            ct
        );
    }
}
