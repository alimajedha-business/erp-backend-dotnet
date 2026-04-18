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

public class ItemTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IItemTypeRepository itemTypeRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IItemTypeService
{
    private readonly string _key = "ItemType";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IItemTypeRepository _itemTypeRepository = itemTypeRepository;

    public async Task<ItemTypeDto> CreateAsync(
        CreateItemTypeDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<ItemType>(createDto);
        var created = await _itemTypeRepository.AddAsync(entity, ct);

        await _itemTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<ItemTypeDto>(created);
    }

    public async Task<ItemTypeDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<ItemTypeDto>(entity);
    }

    public async Task<ListResponseModel<ItemTypeDto>> FilterByQAsync(
        ItemTypeParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _itemTypeRepository.FilterByQ(parameters);
        var res = await _itemTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ItemTypeDto>(
            results: _mapper.Map<IReadOnlyList<ItemTypeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<ItemTypeDto>> GetFilteredAsync(
        ItemTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<ItemType>(filterNodeDto);
        var query = _itemTypeRepository.GetFiltered(advancedFilters);
        var res = await _itemTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ItemTypeDto>(
            results: _mapper.Map<IReadOnlyList<ItemTypeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<ItemTypeDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchItemTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchItemTypeDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, entity);

        await _itemTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<ItemTypeDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: true,
            ct
        );

        _itemTypeRepository.Remove(entity);
        await _itemTypeRepository.SaveChangesAsync(ct);
    }

    public Task<int> GetNextCode(CancellationToken ct)
    {
        return _itemTypeRepository.GetNextCodeAsync(ct);
    }

    private async Task<ItemType> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _itemTypeRepository.GetByIdAsync(
            id,
            trackChanges,
            spec: null,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
