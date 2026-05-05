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
    IWarehouseLocationBusinessRuleValidator businessRuleValidator,
    IValidator<CreateWarehouseLocationDto> createValidator,
    IValidator<PatchWarehouseLocationDto> patchValidator,
    IMapper mapper
) : IWarehouseLocationService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IWarehouseLocationRepository _locationRepository = locationRepository;
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

        var location = _mapper.Map<WarehouseLocation>(createLocationDto);
        location.WarehouseId = warehouseId;

        var createdLocation = await _locationRepository.AddAsync(location, ct);
        await _locationRepository.SaveChangesAsync(ct);

        return _mapper.Map<WarehouseLocationDto>(createdLocation);
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

        if (codePatched && patchDto.Code.HasValue)
        {
            await _businessRuleValidator.ValidateWarehouseLocationCodeUniquenessAsync(
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

        _mapper.Map(patchDto, location);

        await _locationRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseLocationDto>(location);
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
}
