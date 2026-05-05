using FluentValidation;
using FluentValidation.Results;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class WarehouseBusinessRuleValidator(
    IWarehouseRepository warehouseRepository,
    IWarehouseLocationRepository warehouseLocationRepository
) : IWarehouseBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "title",
        "maxMonetaryValue"
    };

    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IWarehouseLocationRepository _warehouseLocationRepository =
        warehouseLocationRepository;

    public void ValidateParameters(WarehouseParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateWarehouseDto createDto,
        CancellationToken ct
    )
    {
        ValidateAccountingRules(createDto);

        await ValidateWarehouseCodeUniquenessAsync(
            companyId,
            excludedWarehouseId: null,
            createDto.Code,
            ct
        );
    }

    public async Task ValidateWarehouseCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedWarehouseId,
        int code,
        CancellationToken ct
    )
    {
        var exists = excludedWarehouseId is null
            ? await _warehouseRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Code == code,
                ct
            )
            : await _warehouseRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Id != excludedWarehouseId.Value &&
                e.Code == code,
                ct
            );

        if (exists)
            throw new WarehouseCodeAlreadyExistsException(code);
    }

    public void ValidateAccountingRules(
        CreateWarehouseDto dto
    )
    {
        ValidateWarehouseAccountingRules(
            dto.WarehouseSlaveAccountCompanyId,
            dto.WarehouseAccountMasterValue,
            dto.WarehouseAccountSlaveValue,
            dto.WarehouseAccountDetailed1Value,
            dto.WarehouseAccountDetailed2Value
        );

        ValidateReturnFromPurchaseAccountingRules(
            dto.ReturnFromPurchaseSlaveAccountCompanyId,
            dto.ReturnFromPurchaseAccountMasterValue,
            dto.ReturnFromPurchaseAccountSlaveValue,
            dto.ReturnFromPurchaseAccountDetailed1Value,
            dto.ReturnFromPurchaseAccountDetailed2Value
        );
    }

    public void ValidateAccountingRules(
        PatchWarehouseDto dto
    )
    {
        ValidateWarehouseAccountingRules(
            dto.WarehouseSlaveAccountCompanyId,
            dto.WarehouseAccountMasterValue,
            dto.WarehouseAccountSlaveValue,
            dto.WarehouseAccountDetailed1Value,
            dto.WarehouseAccountDetailed2Value
        );

        ValidateReturnFromPurchaseAccountingRules(
            dto.ReturnFromPurchaseSlaveAccountCompanyId,
            dto.ReturnFromPurchaseAccountMasterValue,
            dto.ReturnFromPurchaseAccountSlaveValue,
            dto.ReturnFromPurchaseAccountDetailed1Value,
            dto.ReturnFromPurchaseAccountDetailed2Value
        );
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _warehouseRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.Id == id,
            ct
        );

        if (!exists)
            throw new WarehouseNotFoundException();

        var hasLocations = await _warehouseLocationRepository.AnyAsync(e =>
            e.WarehouseId == id,
            ct
        );

        if (hasLocations)
            throw new WarehouseHasLocationsException();
    }

    private static void ValidateWarehouseAccountingRules(
        Guid? slaveAccountCompanyId,
        string? masterValue,
        string? slaveValue,
        string? detailed1Value,
        string? detailed2Value
    )
    {
        ThrowIfInvalid(ValidateAccountingGroup(
            nameof(CreateWarehouseDto.WarehouseSlaveAccountCompanyId),
            slaveAccountCompanyId,
            nameof(CreateWarehouseDto.WarehouseAccountMasterValue),
            masterValue,
            nameof(CreateWarehouseDto.WarehouseAccountSlaveValue),
            slaveValue,
            nameof(CreateWarehouseDto.WarehouseAccountDetailed1Value),
            detailed1Value,
            nameof(CreateWarehouseDto.WarehouseAccountDetailed2Value),
            detailed2Value
        ));
    }

    private static void ValidateReturnFromPurchaseAccountingRules(
        Guid? slaveAccountCompanyId,
        string? masterValue,
        string? slaveValue,
        string? detailed1Value,
        string? detailed2Value
    )
    {
        ThrowIfInvalid(ValidateAccountingGroup(
            nameof(CreateWarehouseDto.ReturnFromPurchaseSlaveAccountCompanyId),
            slaveAccountCompanyId,
            nameof(CreateWarehouseDto.ReturnFromPurchaseAccountMasterValue),
            masterValue,
            nameof(CreateWarehouseDto.ReturnFromPurchaseAccountSlaveValue),
            slaveValue,
            nameof(CreateWarehouseDto.ReturnFromPurchaseAccountDetailed1Value),
            detailed1Value,
            nameof(CreateWarehouseDto.ReturnFromPurchaseAccountDetailed2Value),
            detailed2Value
        ));
    }

    private static List<ValidationFailure> ValidateAccountingGroup(
        string slaveAccountCompanyIdName,
        Guid? slaveAccountCompanyId,
        string masterName,
        string? masterValue,
        string slaveName,
        string? slaveValue,
        string detailed1Name,
        string? detailed1Value,
        string detailed2Name,
        string? detailed2Value
    )
    {
        var failures = new List<ValidationFailure>();

        if (
            slaveAccountCompanyId.HasValue &&
            (
                HasValue(masterValue) ||
                HasValue(slaveValue) ||
                HasValue(detailed1Value) ||
                HasValue(detailed2Value)
            )
        )
        {
            failures.Add(new ValidationFailure(
                slaveAccountCompanyIdName,
                $"{masterName}, {slaveName}, {detailed1Name}, and {detailed2Name} must be null when {slaveAccountCompanyIdName} has value."
            ));
        }

        if (HasValue(detailed2Value) && !HasValue(detailed1Value))
        {
            failures.Add(new ValidationFailure(
                detailed2Name,
                $"{detailed2Name} cannot have value when {detailed1Name} is null."
            ));
        }

        if (HasValue(detailed1Value) && !HasValue(slaveValue))
        {
            failures.Add(new ValidationFailure(
                detailed1Name,
                $"{detailed1Name} cannot have value when {slaveName} is null."
            ));
        }

        if (HasValue(slaveValue) && !HasValue(masterValue))
        {
            failures.Add(new ValidationFailure(
                slaveName,
                $"{slaveName} cannot have value when {masterName} is null."
            ));
        }

        return failures;
    }

    private static bool HasValue(string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    private static void ThrowIfInvalid(List<ValidationFailure> failures)
    {
        if (failures.Count != 0)
            throw new ValidationException(failures);
    }
}
