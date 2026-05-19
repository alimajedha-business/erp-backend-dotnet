using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class RemittanceBusinessRuleValidator(
    IRemittanceRepository remittanceRepository,
    IRemittanceTypeRepository remittanceTypeRepository
) : IRemittanceBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(StringComparer.OrdinalIgnoreCase)
    {
        "number",
        "remittanceDate"
    };

    private readonly IRemittanceRepository _remittanceRepository = remittanceRepository;
    private readonly IRemittanceTypeRepository _remittanceTypeRepository = remittanceTypeRepository;

    public void ValidateParameters(RemittanceParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(Guid companyId, CreateRemittanceDto createDto, CancellationToken ct)
    {
        await ValidateNumberUniquenessAsync(companyId, excludedId: null, createDto.Number, ct);
        await ValidateRemittanceTypeExistsAsync(companyId, createDto.RemittanceTypeId, ct);
    }

    public async Task ValidateUpdateAsync(Guid companyId, Guid id, CreateRemittanceDto updateDto, CancellationToken ct)
    {
        await ValidateNumberUniquenessAsync(companyId, id, updateDto.Number, ct);
        await ValidateRemittanceTypeExistsAsync(companyId, updateDto.RemittanceTypeId, ct);
    }

    public async Task ValidateDeleteAsync(Guid companyId, Guid id, CancellationToken ct)
    {
        var exists = await _remittanceRepository.AnyAsync(e => e.CompanyId == companyId && e.Id == id, ct);

        if (!exists)
            throw new RemittanceNotFoundException();
    }

    private async Task ValidateNumberUniquenessAsync(Guid companyId, Guid? excludedId, long number, CancellationToken ct)
    {
        var exists = excludedId is null
            ? await _remittanceRepository.AnyAsync(e => e.CompanyId == companyId && e.Number == number, ct)
            : await _remittanceRepository.AnyAsync(
                e => e.CompanyId == companyId && e.Id != excludedId.Value && e.Number == number,
                ct
            );

        if (exists)
            throw new RemittanceNumberAlreadyExistsException(number);
    }

    private async Task ValidateRemittanceTypeExistsAsync(Guid companyId, Guid remittanceTypeId, CancellationToken ct)
    {
        var exists = await _remittanceTypeRepository.AnyAsync(
            e => e.CompanyId == companyId && e.Id == remittanceTypeId,
            ct
        );

        if (!exists)
            throw new RemittanceTypeNotFoundException();
    }
}
