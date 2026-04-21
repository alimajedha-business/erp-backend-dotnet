using System.Linq.Expressions;

using AutoMapper;

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
using NGErp.Warehouse.Service.Service.Contracts;

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
        var location = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.Id == id,
            ct
        );

        return _mapper.Map<WarehouseLocationDto>(location);
    }

    public async Task<ListResponseModel<WarehouseLocationSlimDto>> FilterByQAsync(
        WarehouseLocationParameters parameters,
        CancellationToken ct
    )
    {
        var res = _locationRepository.FilterByQ(parameters);

        return new ListResponseModel<WarehouseLocationSlimDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseLocationSlimDto>>(res.Result),
            totalCount: res.Result.Count,
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
        var location = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.Id == id,
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

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
