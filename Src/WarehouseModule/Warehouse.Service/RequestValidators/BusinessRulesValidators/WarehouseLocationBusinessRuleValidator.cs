using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class WarehouseLocationBusinessRuleValidator(
    IWarehouseLocationRepository locationRepository
) : IWarehouseLocationBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "title"
    };

    private readonly IWarehouseLocationRepository _locationRepository = locationRepository;

    public void ValidateParameters(WarehouseLocationParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid warehouseId,
        CreateWarehouseLocationDto createDto,
        CancellationToken ct
    )
    {
        ValidateHasNextLevel(createDto.LevelNo, createDto.HasNextLevel);

        await ValidateParentAsync(
            warehouseId,
            createDto.ParentLocationId,
            ct
        );

        await ValidateWarehouseLocationCodeUniquenessAsync(
            excludedLocationId: null,
            createDto.Code,
            ct
        );
    }

    public async Task ValidateWarehouseLocationCodeUniquenessAsync(
        Guid? excludedLocationId,
        int code,
        CancellationToken ct
    )
    {
        var exists = excludedLocationId is null
            ? await _locationRepository.AnyAsync(e =>
                e.Code == code,
                ct
            )
            : await _locationRepository.AnyAsync(e =>
                e.Id != excludedLocationId.Value &&
                e.Code == code,
                ct
            );

        if (exists)
            throw new WarehouseLocationCodeAlreadyExistsException(code);
    }

    public async Task ValidateParentAsync(
        Guid warehouseId,
        Guid? parentLocationId,
        CancellationToken ct
    )
    {
        if (parentLocationId is null)
            return;

        var exists = await _locationRepository.AnyAsync(e =>
            e.WarehouseId == warehouseId &&
            e.Id == parentLocationId.Value,
            ct
        );

        if (!exists)
            throw new WarehouseLocationParentNotInWarehouseException();
    }

    public void ValidateHasNextLevel(
        int levelNo,
        bool hasNextLevel
    )
    {
        if (levelNo == 6 && hasNextLevel)
            throw new WarehouseLocationLastLevelCannotHaveChildrenException();
    }

    public async Task ValidateDeleteAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _locationRepository.AnyAsync(e =>
            e.WarehouseId == warehouseId &&
            e.Id == id,
            ct
        );

        if (!exists)
            throw new WarehouseLocationNotFoundException();

        var hasChildren = await _locationRepository.AnyAsync(e =>
            e.WarehouseId == warehouseId &&
            e.ParentLocationId == id,
            ct
        );

        if (hasChildren)
            throw new WarehouseLocationHasChildrenException();
    }
}
