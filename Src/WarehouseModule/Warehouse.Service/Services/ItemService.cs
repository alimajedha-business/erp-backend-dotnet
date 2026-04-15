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

public class ItemService(
    IAdvancedFilterBuilder filterBuilder,
    IItemRepository itemRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IItemService
{
    private readonly string _key = "Item";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IItemRepository _itemRepository = itemRepository;

    public async Task<ItemDto> CreateAsync(
        Guid companyId,
        Guid categoryId,
        CreateItemDto createItemDto,
        CancellationToken ct
    )
    {
        var item = _mapper.Map<Item>(createItemDto);
        item.CompanyId = companyId;
        item.CategoryId = categoryId;

        var createdItem = await _itemRepository.AddAsync(item, ct);

        item.ItemAttributes = [.. createItemDto.AttributeIds
            .Select(attributeId => new ItemAttribute
            {
                AttributeId = attributeId,
                Item = item
            })];

        item.ItemUnitOfMeasurements = [.. createItemDto.SecondaryUnitOfMeasurementIds
            .Select((uomId, index) => new ItemUnitOfMeasurement
            {
                UnitOfMeasurementId = uomId,
                Item = item,
                UnitOrder = index + 2
            })];

        await _itemRepository.SaveChangesAsync(ct);
        return _mapper.Map<ItemDto>(createdItem);
    }

    public async Task<ItemDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var item = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<ItemDto> GetByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        var item = await GetByIdOrThrowAsync(
            companyId,
            categoryId,
            id,
            trackChanges: true,
            ct
        );

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<ListResponseModel<ItemDto>> GetAllAsync(
        Guid companyId,
        ItemParameters parameters,
        FilterNodeDto filterNodeDto,
        CancellationToken ct = default
    )
    {
        // TODO: add specification if needed
        var advancedFilters = _filterBuilder.Build<Item>(filterNodeDto);
        var listQueryResult = await _itemRepository.GetAllAsync(
            companyId,
            parameters,
            advancedFilters,
            spec: null,
            ct
        );

        return new ListResponseModel<ItemDto>(
            results: _mapper.Map<IReadOnlyList<ItemDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<ListResponseModel<ItemListDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters parameters,
        FilterNodeDto filterNodeDto,
        CancellationToken ct
    )
    {
        var advancedFilters = _filterBuilder.Build<Item>(filterNodeDto);
        var listQueryResult = await _itemRepository.GetCategoryAllAsync(
            companyId,
            categoryId,
            parameters,
            advancedFilters,
            ct
        );

        return new ListResponseModel<ItemListDto>(
            results: _mapper.Map<IReadOnlyList<ItemListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<ItemDto> PatchAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        JsonPatchDocument<PatchItemDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            categoryId,
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchItemDto>(entity);
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

        await _itemRepository.SaveChangesAsync(ct);
        return _mapper.Map<ItemDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            categoryId,
            id,
            trackChanges: true,
            ct
        );

        _itemRepository.Remove(entity);
        await _itemRepository.SaveChangesAsync(ct);
    }

    private async Task<Item> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _itemRepository.GetByIdAsync(companyId, id, trackChanges, ct);

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }

    private async Task<Item> GetByIdOrThrowAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _itemRepository.GetByIdAsync(
            companyId,
            categoryId,
            id,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
