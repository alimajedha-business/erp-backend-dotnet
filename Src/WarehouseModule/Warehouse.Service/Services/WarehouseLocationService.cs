using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseLocationService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseLocationRepository locationRepository,
    ISiUnitRepository unitRepository,
    IWarehouseLocationBusinessRuleValidator businessRuleValidator,
    IValidator<CreateWarehouseLocationDto> createValidator,
    IValidator<PatchWarehouseLocationDto> patchValidator,
    IMapper mapper
) : IWarehouseLocationService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IWarehouseLocationRepository _locationRepository = locationRepository;
    private readonly ISiUnitRepository _unitRepository = unitRepository;
    private readonly IWarehouseLocationBusinessRuleValidator _businessRuleValidator =
        businessRuleValidator;
    private readonly IValidator<CreateWarehouseLocationDto> _createValidator = createValidator;
    private readonly IValidator<PatchWarehouseLocationDto> _patchValidator = patchValidator;

    public async Task<WarehouseLocationDto> CreateAsync(
        Guid warehouseId,
        CreateWarehouseLocationDto createLocationDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createLocationDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createLocationDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(warehouseId, createLocationDto, ct);
        await _businessRuleValidator.ValidatePreferredUnitsAsync(createLocationDto, ct);

        var location = _mapper.Map<WarehouseLocation>(createLocationDto);
        location.WarehouseId = warehouseId;
        await SetMeasurementValuesAsync(location, createLocationDto, ct);

        var createdLocation = await _locationRepository.AddAsync(location, ct);
        await _locationRepository.SaveChangesAsync(ct);

        var savedLocation = await _locationRepository.GetByIdAsync(
            createdLocation.Id,
            trackChanges: false,
            ct
        );

        return _mapper.Map<WarehouseLocationDto>(savedLocation ?? createdLocation);
    }

    public async Task<WarehouseLocationDto> GetByIdAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    )
    {
        var location = await GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p => p.WarehouseId == warehouseId && p.Id == id,
            ct
        );

        return _mapper.Map<WarehouseLocationDto>(location);
    }

    public async Task<List<WarehouseLocationNode>> GetItemLocationsAsync(
        Item item,
        CancellationToken ct
    )
    {
        return await _locationRepository.GetItemLocationsAsync(item, ct);
    }

    public async Task<ListResponseModel<WarehouseLocationSlimDto>> FilterByQAsync(
        Guid warehouseId,
        WarehouseLocationParameters parameters,
        CancellationToken ct
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var res = await _locationRepository.FilterByQ(warehouseId, parameters, ct);

        return new ListResponseModel<WarehouseLocationSlimDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseLocationSlimDto>>(res),
            totalCount: res.Count,
            parameters
        );
    }

    public async Task<ListResponseModel<WarehouseLocationListDto>> GetFilteredAsync(
        Guid warehouseId,
        WarehouseLocationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<WarehouseLocation>(filterNodeDto);
        var query = _locationRepository.GetFiltered(warehouseId, advancedFilters);
        var res = await _locationRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<WarehouseLocationListDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseLocationListDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<int> GetNextCodeAsync(
        Guid warehouseId,
        CancellationToken ct
    )
    {
        return await _locationRepository.GetNextCodeAsync(warehouseId, ct);
    }

    public async Task<WarehouseLocationDto> PatchAsync(
        Guid warehouseId,
        Guid id,
        JsonPatchDocument<PatchWarehouseLocationDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchWarehouseLocationPolicy.Validate(patchDocument);

        var codePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchWarehouseLocationDto.Code)
        );

        var levelNoPatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchWarehouseLocationDto.LevelNo)
        );

        var hasNextLevelPatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchWarehouseLocationDto.HasNextLevel)
        );
        var patchedPaths = patchDocument.Operations
            .Select(e => e.path.Trim('/').Split('/')[0])
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var location = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.WarehouseId == warehouseId && p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchWarehouseLocationDto>(location);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
            throw new InvalidPatchDocumentException(errors);

        await RequestBodyValidator.ValidateAsync(_patchValidator, patchDto, ct);
        await _businessRuleValidator.ValidatePreferredUnitsAsync(patchDto, ct);

        if (codePatched && patchDto.Code.HasValue)
        {
            await _businessRuleValidator.ValidateWarehouseLocationCodeUniquenessAsync(
                warehouseId,
                excludedLocationId: id,
                patchDto.Code.Value,
                ct
            );
        }

        if (
            (levelNoPatched || hasNextLevelPatched) &&
            patchDto.LevelNo.HasValue &&
            patchDto.HasNextLevel.HasValue
        )
        {
            _businessRuleValidator.ValidateHasNextLevel(
                patchDto.LevelNo.Value,
                patchDto.HasNextLevel.Value
            );
        }

        await UpdateLocationAsync(location, patchDto, patchedPaths, ct);

        await _locationRepository.SaveChangesAsync(ct);

        var updatedLocation = await _locationRepository.GetByIdAsync(
            location.Id,
            trackChanges: false,
            ct
        );

        return _mapper.Map<WarehouseLocationDto>(updatedLocation ?? location);
    }

    public async Task DeleteAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(warehouseId, id, ct);

        await _locationRepository.Remove(e =>
            e.WarehouseId == warehouseId &&
            e.Id == id,
            ct
        );
    }

    private async Task<WarehouseLocation> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<WarehouseLocation, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _locationRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new WarehouseLocationNotFoundException();
    }

    private async Task UpdateLocationAsync(
        WarehouseLocation location,
        PatchWarehouseLocationDto patchDto,
        ISet<string> patchedPaths,
        CancellationToken ct
    )
    {
        location.Code = patchDto.Code!.Value;
        location.Title = patchDto.Title!;
        location.CanStoreItem = patchDto.CanStoreItem!.Value;
        location.HasNextLevel = patchDto.HasNextLevel!.Value;
        location.LevelNo = patchDto.LevelNo!.Value;
        await PreserveUnpatchedMeasurementValuesAsync(patchDto, location, patchedPaths, ct);
        await SetMeasurementValuesAsync(location, patchDto, ct);
    }

    private async Task PreserveUnpatchedMeasurementValuesAsync(
        PatchWarehouseLocationDto patchDto,
        WarehouseLocation location,
        ISet<string> patchedPaths,
        CancellationToken ct
    )
    {
        var unitsById = await LoadPreferredUnitsAsync(
            patchDto.PreferredMassUnitId,
            patchDto.PreferredLengthUnitId,
            patchDto.PreferredVolumeUnitId,
            ct
        );

        if (!patchedPaths.Contains(nameof(PatchWarehouseLocationDto.Length)))
        {
            patchDto.Length = MeasurementUnitConverter.ConvertFromBase(
                location.Length,
                GetUnit(patchDto.PreferredLengthUnitId, unitsById)
            );
        }

        if (!patchedPaths.Contains(nameof(PatchWarehouseLocationDto.Width)))
        {
            patchDto.Width = MeasurementUnitConverter.ConvertFromBase(
                location.Width,
                GetUnit(patchDto.PreferredLengthUnitId, unitsById)
            );
        }

        if (!patchedPaths.Contains(nameof(PatchWarehouseLocationDto.Height)))
        {
            patchDto.Height = MeasurementUnitConverter.ConvertFromBase(
                location.Height,
                GetUnit(patchDto.PreferredLengthUnitId, unitsById)
            );
        }

        if (!patchedPaths.Contains(nameof(PatchWarehouseLocationDto.MaxMass)))
        {
            patchDto.MaxMass = MeasurementUnitConverter.ConvertFromBase(
                location.MaxMass,
                GetUnit(patchDto.PreferredMassUnitId, unitsById)
            );
        }

        if (!patchedPaths.Contains(nameof(PatchWarehouseLocationDto.MaxVolume)))
        {
            patchDto.MaxVolume = MeasurementUnitConverter.ConvertFromBase(
                location.MaxVolume,
                GetUnit(patchDto.PreferredVolumeUnitId, unitsById)
            );
        }
    }

    private static SiUnit? GetUnit(
        Guid? unitId,
        IReadOnlyDictionary<Guid, SiUnit> unitsById
    )
    {
        return unitId.HasValue && unitsById.TryGetValue(unitId.Value, out var unit)
            ? unit
            : null;
    }

    private async Task SetMeasurementValuesAsync(
        WarehouseLocation location,
        CreateWarehouseLocationDto dto,
        CancellationToken ct
    )
    {
        var unitsById = await LoadPreferredUnitsAsync(
            dto.PreferredMassUnitId,
            dto.PreferredLengthUnitId,
            dto.PreferredVolumeUnitId,
            ct
        );

        location.Length = MeasurementUnitConverter.ConvertToBase(
            dto.Length,
            dto.PreferredLengthUnitId,
            unitsById
        );
        location.Width = MeasurementUnitConverter.ConvertToBase(
            dto.Width,
            dto.PreferredLengthUnitId,
            unitsById
        );
        location.Height = MeasurementUnitConverter.ConvertToBase(
            dto.Height,
            dto.PreferredLengthUnitId,
            unitsById
        );
        location.MaxMass = MeasurementUnitConverter.ConvertToBase(
            dto.MaxMass,
            dto.PreferredMassUnitId,
            unitsById
        );
        location.MaxVolume = MeasurementUnitConverter.ConvertToBase(
            dto.MaxVolume,
            dto.PreferredVolumeUnitId,
            unitsById
        );
        location.PreferredMassUnitId = dto.PreferredMassUnitId;
        location.PreferredLengthUnitId = dto.PreferredLengthUnitId;
        location.PreferredVolumeUnitId = dto.PreferredVolumeUnitId;
    }

    private async Task SetMeasurementValuesAsync(
        WarehouseLocation location,
        PatchWarehouseLocationDto dto,
        CancellationToken ct
    )
    {
        var unitsById = await LoadPreferredUnitsAsync(
            dto.PreferredMassUnitId,
            dto.PreferredLengthUnitId,
            dto.PreferredVolumeUnitId,
            ct
        );

        location.Length = MeasurementUnitConverter.ConvertToBase(
            dto.Length,
            dto.PreferredLengthUnitId,
            unitsById
        );
        location.Width = MeasurementUnitConverter.ConvertToBase(
            dto.Width,
            dto.PreferredLengthUnitId,
            unitsById
        );
        location.Height = MeasurementUnitConverter.ConvertToBase(
            dto.Height,
            dto.PreferredLengthUnitId,
            unitsById
        );
        location.MaxMass = MeasurementUnitConverter.ConvertToBase(
            dto.MaxMass,
            dto.PreferredMassUnitId,
            unitsById
        );
        location.MaxVolume = MeasurementUnitConverter.ConvertToBase(
            dto.MaxVolume,
            dto.PreferredVolumeUnitId,
            unitsById
        );
        location.PreferredMassUnitId = dto.PreferredMassUnitId;
        location.PreferredLengthUnitId = dto.PreferredLengthUnitId;
        location.PreferredVolumeUnitId = dto.PreferredVolumeUnitId;
    }

    private async Task<IReadOnlyDictionary<Guid, SiUnit>> LoadPreferredUnitsAsync(
        Guid? preferredMassUnitId,
        Guid? preferredLengthUnitId,
        Guid? preferredVolumeUnitId,
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

        return unitIds.Length == 0
            ? new Dictionary<Guid, SiUnit>()
            : await _unitRepository.GetByIdsAsync(unitIds, ct);
    }
}
