using FluentValidation;
using FluentValidation.Results;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class WarehouseLocationBusinessRuleValidator(
    IWarehouseLocationRepository locationRepository,
    ISiUnitRepository unitRepository
) : IWarehouseLocationBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "title"
    };

    private readonly IWarehouseLocationRepository _locationRepository = locationRepository;
    private readonly ISiUnitRepository _unitRepository = unitRepository;

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
            warehouseId,
            excludedLocationId: null,
            createDto.Code,
            ct
        );
    }

    public async Task ValidateWarehouseLocationCodeUniquenessAsync(
        Guid warehouseId,
        Guid? excludedLocationId,
        int code,
        CancellationToken ct
    )
    {
        var exists = excludedLocationId is null
            ? await _locationRepository.AnyAsync(
                e => e.WarehouseId == warehouseId && e.Code == code,
                ct
            )
            : await _locationRepository.AnyAsync(
                e => 
                    e.WarehouseId == warehouseId &&
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

        var exists = await _locationRepository.AnyAsync(
            e =>
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
        var exists = await _locationRepository.AnyAsync(
            e => e.WarehouseId == warehouseId && e.Id == id,
            ct
        );

        if (!exists)
            throw new WarehouseLocationNotFoundException();

        var hasChildren = await _locationRepository.AnyAsync(
            e =>
                e.WarehouseId == warehouseId &&
                e.ParentLocationId == id,
            ct
        );

        if (hasChildren)
            throw new WarehouseLocationHasChildrenException();
    }

    public async Task ValidatePreferredUnitsAsync(
        CreateWarehouseLocationDto dto,
        CancellationToken ct
    )
    {
        await ValidatePreferredUnitsAsync(
            dto.PreferredMassUnitId,
            dto.PreferredLengthUnitId,
            dto.PreferredVolumeUnitId,
            dto.MaxMass.HasValue,
            dto.Length.HasValue || dto.Width.HasValue || dto.Height.HasValue,
            dto.MaxVolume.HasValue,
            ct
        );
    }

    public async Task ValidatePreferredUnitsAsync(
        PatchWarehouseLocationDto dto,
        CancellationToken ct
    )
    {
        await ValidatePreferredUnitsAsync(
            dto.PreferredMassUnitId,
            dto.PreferredLengthUnitId,
            dto.PreferredVolumeUnitId,
            dto.MaxMass.HasValue,
            dto.Length.HasValue || dto.Width.HasValue || dto.Height.HasValue,
            dto.MaxVolume.HasValue,
            ct
        );
    }

    private async Task ValidatePreferredUnitsAsync(
        Guid? preferredMassUnitId,
        Guid? preferredLengthUnitId,
        Guid? preferredVolumeUnitId,
        bool requiresMassUnit,
        bool requiresLengthUnit,
        bool requiresVolumeUnit,
        CancellationToken ct
    )
    {
        var unitIds = new[]
            {
                preferredMassUnitId,
                preferredLengthUnitId,
                preferredVolumeUnitId
            }
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .Distinct()
            .ToArray();

        if (unitIds.Length == 0)
        {
            await ValidateBaseUnitsExistAsync(
                requiresMassUnit,
                requiresLengthUnit,
                requiresVolumeUnit,
                ct
            );
            return;
        }

        var unitsById = await _unitRepository.GetByIdsAsync(unitIds, ct);
        var missingIds = unitIds
            .Where(id => !unitsById.ContainsKey(id))
            .ToList();

        if (missingIds.Count != 0)
        {
            throw new ValidationException(missingIds.Select(id =>
                new ValidationFailure(
                    nameof(CreateWarehouseLocationDto),
                    $"SiUnit '{id}' was not found."
                )
            ));
        }

        ValidatePreferredUnitDimensions(
            preferredMassUnitId,
            preferredLengthUnitId,
            preferredVolumeUnitId,
            unitsById
        );
        await ValidateBaseUnitsExistAsync(
            requiresMassUnit,
            requiresLengthUnit,
            requiresVolumeUnit,
            ct
        );
    }

    private async Task ValidateBaseUnitsExistAsync(
        bool requiresMassUnit,
        bool requiresLengthUnit,
        bool requiresVolumeUnit,
        CancellationToken ct
    )
    {
        var requiredDimensions = new[]
            {
                requiresMassUnit ? UnitDimension.MASS : (UnitDimension?)null,
                requiresLengthUnit ? UnitDimension.LENGTH : (UnitDimension?)null,
                requiresVolumeUnit ? UnitDimension.VOLUME : (UnitDimension?)null
            }
            .Where(dimension => dimension.HasValue)
            .Select(dimension => dimension!.Value)
            .Distinct()
            .ToArray();

        if (requiredDimensions.Length == 0)
            return;

        var baseUnits = await _unitRepository.GetBaseUnitsByDimensionsAsync(
            requiredDimensions,
            ct
        );

        var missingDimensions = requiredDimensions
            .Where(dimension => !baseUnits.ContainsKey(dimension))
            .ToList();

        if (missingDimensions.Count != 0)
        {
            throw new ValidationException(missingDimensions.Select(dimension =>
                new ValidationFailure(
                    nameof(CreateWarehouseLocationDto),
                    $"Base unit for dimension '{dimension}' was not found."
                )
            ));
        }
    }

    private static void ValidatePreferredUnitDimensions(
        Guid? preferredMassUnitId,
        Guid? preferredLengthUnitId,
        Guid? preferredVolumeUnitId,
        IReadOnlyDictionary<Guid, SiUnit> unitsById
    )
    {
        var failures = new List<ValidationFailure>();

        ValidatePreferredUnitDimension(
            preferredMassUnitId,
            UnitDimension.MASS,
            nameof(CreateWarehouseLocationDto.PreferredMassUnitId),
            unitsById,
            failures
        );
        ValidatePreferredUnitDimension(
            preferredLengthUnitId,
            UnitDimension.LENGTH,
            nameof(CreateWarehouseLocationDto.PreferredLengthUnitId),
            unitsById,
            failures
        );
        ValidatePreferredUnitDimension(
            preferredVolumeUnitId,
            UnitDimension.VOLUME,
            nameof(CreateWarehouseLocationDto.PreferredVolumeUnitId),
            unitsById,
            failures
        );

        if (failures.Count != 0)
            throw new ValidationException(failures);
    }

    private static void ValidatePreferredUnitDimension(
        Guid? unitId,
        UnitDimension expectedDimension,
        string propertyName,
        IReadOnlyDictionary<Guid, SiUnit> unitsById,
        List<ValidationFailure> failures
    )
    {
        if (!unitId.HasValue || !unitsById.TryGetValue(unitId.Value, out var unit))
            return;

        if (unit.UnitDimension != expectedDimension)
        {
            failures.Add(new ValidationFailure(
                propertyName,
                $"SiUnit '{unitId}' must have '{expectedDimension}' dimension."
            ));
        }
    }
}
