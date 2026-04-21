using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.Repository.Contracts;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;
using NGErp.Warehouse.Service.Specifications;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseLocationService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseLocationRepository locationRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IWarehouseLocationService
{
    private readonly string _key = "WarehouseLocation";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IWarehouseLocationRepository _locationRepository = locationRepository;

    public async Task<WarehouseLocationDto> CreateAsync(
        Guid warehouseId,
        CreateWarehouseLocationDto createLocationDto,
        CancellationToken ct
    )
    {
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
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        return _mapper.Map<WarehouseLocationDto>(entity);
    }

    public async Task<ListResponseModel<WarehouseLocationListDto>> FilterByQAsync(
        Guid warehouseId,
        WarehouseLocationParameters parameters,
        CancellationToken ct
    )
    {
        var query = _locationRepository.FilterByQ(parameters);
        var res = await _locationRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<WarehouseLocationListDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseLocationListDto>>(res.items),
            totalCount: res.count,
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
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        var patchDto = _mapper.Map<PatchWarehouseLocationDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
            throw new InvalidPatchDocumentException(errors);

        _mapper.Map(patchDto, entity);

        await _locationRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseLocationDto>(entity);
    }

    public async Task DeleteAsync(
        Guid warehouseId,
        Guid id,
        CancellationToken ct
    )
    {
        // TODO: check permissions
        // TODO: check if the warehouse location belongs to this warehouse
        _locationRepository.Remove(id, ct);
        await _locationRepository.SaveChangesAsync(ct);
    }

    private async Task<WarehouseLocation> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _locationRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
