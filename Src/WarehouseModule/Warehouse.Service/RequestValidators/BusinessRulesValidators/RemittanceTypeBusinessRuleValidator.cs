using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class RemittanceTypeBusinessRuleValidator(
    IRemittanceTypeRepository remittanceTypeRepository
) : IRemittanceTypeBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "title"
    };

    private readonly IRemittanceTypeRepository _remittanceTypeRepository = remittanceTypeRepository;

    public void ValidateParameters(RemittanceTypeParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateRemittanceTypeDto createDto,
        CancellationToken ct
    )
    {
        await ValidateRemittanceTypeCodeUniquenessAsync(
            companyId,
            excludedRemittanceTypeId: null,
            createDto.Code,
            ct
        );
    }

    public async Task ValidateRemittanceTypeCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedRemittanceTypeId,
        int code,
        CancellationToken ct
    )
    {
        var exists = excludedRemittanceTypeId is null
            ? await _remittanceTypeRepository.AnyAsync(
                e => e.CompanyId == companyId && e.Code == code,
                ct
            )
            : await _remittanceTypeRepository.AnyAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.Id != excludedRemittanceTypeId.Value &&
                    e.Code == code,
                ct
            );

        if (exists)
            throw new RemittanceTypeCodeAlreadyExistsException(code);
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _remittanceTypeRepository.AnyAsync(
            e =>
                e.CompanyId == companyId &&
                e.Id == id,
            ct
        );

        if (!exists)
            throw new RemittanceTypeNotFoundException();

        if (await HasRelatedEntitiesAsync(id, ct))
            throw new RemittanceTypeHasRelatedEntitiesException();
    }

    private static Task<bool> HasRelatedEntitiesAsync(
        Guid remittanceTypeId,
        CancellationToken ct
    )
    {
        return Task.FromResult(false);
    }
}
