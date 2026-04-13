using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseLocationService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseLocationRepository locationRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        WarehouseLocation,
        WarehouseLocationDto,
        WarehouseLocationListDto,
        WarehouseLocationParameters,
        IWarehouseLocationRepository,
        WarehouseResource
    >(
        filterBuilder,
        locationRepository,
        mapper,
        localizer
    ),
    IWarehouseLocationService
{
    protected override string LocalizerKey => "WarehouseLocation";

    public async Task<WarehouseLocationDto> CreateAsync(
        Guid warehouseId,
        CreateWarehouseLocationDto createLocationDto,
        CancellationToken ct
    )
    {
        var location = _mapper.Map<WarehouseLocation>(createLocationDto);
        location.WarehouseId = warehouseId;

        var createdLocation = await _repo.AddAsync(location, ct);
        await _repo.SaveChangesAsync(ct);

        return _mapper.Map<WarehouseLocationDto>(createdLocation);
    }

    public async Task<ListResponseModel<WarehouseLocationListDto>> GetAllAsync(
        Guid warehouseId,
        WarehouseLocationParameters parameters,
        CancellationToken ct
    )
    {
        var listQueryResult = await _repo.GetWarehouseAllAsync(
            warehouseId,
            parameters,
            ct
        );

        return new ListResponseModel<WarehouseLocationListDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseLocationListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<ListResponseModel<WarehouseLocationListDto>> GetAllAsync(
        Guid warehouseId,
        WarehouseLocationParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        var advancedFilters = _filterBuilder.Build<WarehouseLocation>(filterNodeDto);
        var listQueryResult = await _repo.GetWarehouseAllAsync(
            warehouseId,
            parameters,
            ct,
            advancedFilters
        );

        return new ListResponseModel<WarehouseLocationListDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseLocationListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<WarehouseLocationDto> GetByIdAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(warehouseId, id, ct);
        return _mapper.Map<WarehouseLocationDto>(entity);
    }

    public async Task<int> GetNextCodeAsync(
        Guid warehouseId,
        CancellationToken ct
    )
    {
        return await _repo.GetNextCodeAsync(warehouseId, ct);
    }

    public async Task<WarehouseLocationDto> PatchAsync(
        Guid warehouseId,
        Guid id,
        JsonPatchDocument<PatchWarehouseLocationDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            warehouseId,
            id,
            ct,
            trackChanges: true
        );

        var patchDto = _mapper.Map<PatchWarehouseLocationDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
            throw new InvalidPatchDocumentException(errors);

        _mapper.Map(patchDto, entity);

        await _repo.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseLocationDto>(entity);
    }

    public async Task DeleteAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(warehouseId, id, ct);
        _repo.Remove(entity);

        await _repo.SaveChangesAsync(ct);
    }

    private async Task<WarehouseLocation> GetByIdOrThrowAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var entity = await _repo.GetByIdAsync(warehouseId, id, ct, trackChanges);
        return entity ?? throw new NotFoundException(_localizer[LocalizerKey].Value);
    }
}
