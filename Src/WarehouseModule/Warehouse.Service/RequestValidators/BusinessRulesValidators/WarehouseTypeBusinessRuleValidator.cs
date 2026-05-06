using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class WarehouseTypeBusinessRuleValidator(
    IWarehouseTypeRepository warehouseTypeRepository,
    IWarehouseRepository warehouseRepository
) : IWarehouseTypeBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "title"
    };

    private readonly IWarehouseTypeRepository _warehouseTypeRepository = warehouseTypeRepository;
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;

    public void ValidateParameters(WarehouseTypeParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        CreateWarehouseTypeDto createDto,
        CancellationToken ct
    )
    {
        await ValidateWarehouseTypeCodeUniquenessAsync(
            excludedWarehouseTypeId: null,
            createDto.Code,
            ct
        );
    }

    public async Task ValidateWarehouseTypeCodeUniquenessAsync(
        Guid? excludedWarehouseTypeId,
        int code,
        CancellationToken ct
    )
    {
        var exists = excludedWarehouseTypeId is null
            ? await _warehouseTypeRepository.AnyAsync(
                e => e.Code == code,
                ct
            )
            : await _warehouseTypeRepository.AnyAsync(
                e =>
                    e.Id != excludedWarehouseTypeId.Value &&
                    e.Code == code,
                ct
            );

        if (exists)
            throw new WarehouseTypeCodeAlreadyExistsException(code);
    }

    public async Task ValidateDeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _warehouseTypeRepository.AnyAsync(
            e => e.Id == id,
            ct
        );

        if (!exists)
            throw new NotFoundException("WarehouseType");

        var hasWarehouses = await _warehouseRepository.AnyAsync(
            e => e.WarehouseTypeId == id,
            ct
        );

        if (hasWarehouses)
            throw new WarehouseTypeHasWarehousesException();
    }
}
