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

public class ItemService(
    IAdvancedFilterBuilder filterBuilder,
    IItemRepository itemRepository,
    IWarehouseLocationService locationService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IItemService
{
    private readonly string _key = "Item";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly IWarehouseLocationService _locationService = locationService;

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

        item.ItemAttributes = [.. createItemDto
            .AttributeIds
            .Distinct()
            .Select(attributeId => new ItemAttribute
            {
                AttributeId = attributeId,
            })];

        item.ItemUnitOfMeasurements = [.. createItemDto
            .SecondaryUnitOfMeasurementIds
            .Where(uomId => uomId != item.PrimaryUnitOfMeasurementId)
            .Distinct()
            .Select((uomId, index) => new ItemUnitOfMeasurement
            {
                UnitOfMeasurementId = uomId,
                UnitOrder = index + 2
            })];

        var duplicateWarehouseIds = createItemDto.ItemWarehouses
            .GroupBy(w => w.WarehouseId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateWarehouseIds.Count != 0)
            throw new DbUpdateBadRequestException(
                _localizer["Item.Warehouse.Duplicate"].Value
            );

        item.ItemWarehouses = [.. createItemDto
            .ItemWarehouses
            .Select(createItemWarehouseDto => new ItemWarehouse
            {
                WarehouseId = createItemWarehouseDto.WarehouseId,
                ReorderPoint = createItemWarehouseDto.ReorderPoint,
                CriticalPoint = createItemWarehouseDto.CriticalPoint,
                ReorderQuantity = createItemWarehouseDto.ReorderQuantity,
                MaxStockLevel = createItemWarehouseDto.MaxStockLevel,

                ItemWarehouseLocations = [.. createItemWarehouseDto.LocationIds
                    .Distinct()
                    .Select(locationId => new ItemWarehouseLocation
                    {
                        WarehouseLocationId = locationId
                    })]
            })];

        item.ItemUnitOfMeasurementConversions = [.. createItemDto
            .ItemUnitOfMeasurementConversions
            .Select(createItemUoMConversionDto => new ItemUnitOfMeasurementConversion
                {
                    UnitOfMeasurementId = createItemUoMConversionDto.UnitOfMeasurementId,
                    ConversionEquation = createItemUoMConversionDto.ConversionEquation,
                })];

        var createdItem = await _itemRepository.AddAsync(item, ct);
        await _itemRepository.SaveChangesAsync(ct);

        return _mapper.Map<ItemDto>(createdItem);
    }

    public async Task<ItemDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var item = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.CompanyId == companyId && p.Id == id,
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
        var item = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p =>
                p.CompanyId == companyId &&
                p.CategoryId == categoryId &&
                p.Id == id,
            ct
        );

        var locations = await _locationService.GetItemLocationsAsync(item, ct);
        var byId = locations.ToDictionary(x => x.Id);
        var cache = new Dictionary<Guid, string>();

        return new ItemDto(
            item.Id,
            item.Code,
            item.Title,
            item.TitleInEnglish,
            item.TechnicalNumber,
            item.Sku,
            item.Barcode,
            new UnitOfMeasurementSlimDto(
                item.PrimaryUnitOfMeasurementId,
                item.PrimaryUnitOfMeasurement.Code,
                item.PrimaryUnitOfMeasurement.Title
            ),
            new ItemTypeSlimDto(
                item.ItemTypeId,
                item.ItemType.Code,
                item.ItemType.Title
            ),
            new CategorySlimDto(
                item.CategoryId,
                item.Category.Code,
                item.Category.Title
            ),
            [.. item.ItemAttributes
                .Select(ia => new AttributeSlimDto(
                    ia.Attribute.Id,
                    ia.Attribute.Code,
                    ia.Attribute.Title
                ))],
            [.. item.ItemUnitOfMeasurements
                .Select(ium => new UnitOfMeasurementSlimDto(
                    ium.UnitOfMeasurement.Id,
                    ium.UnitOfMeasurement.Code,
                    ium.UnitOfMeasurement.Title
                ))],
            [.. item.ItemWarehouses
                .Select(iw => new ItemWarehouseDto(
                    new WarehouseSlimDto(
                        iw.Warehouse.Id,
                        iw.Warehouse.Code,
                        iw.Warehouse.Title
                    ),
                    iw.ReorderPoint,
                    iw.CriticalPoint,
                    iw.ReorderQuantity,
                    iw.MaxStockLevel,
                    [.. iw.ItemWarehouseLocations
                        .Select(iwl => new WarehouseLocationSlimDto(
                            iwl.WarehouseLocation.Id,
                            iwl.WarehouseLocation.Code,
                            BuildLocationPath(iwl.WarehouseLocation.Id, byId, cache)
                        ))]
                ))],
            [.. item.ItemUnitOfMeasurementConversions
                .Select(iumc => new ItemUnitOfMeasurementConversionDto(
                    iumc.UnitOfMeasurementId,
                    iumc.ConversionEquation
                ))],
            item.IsActive
        );
    }

    public async Task<ListResponseModel<ItemDto>> GetFilteredAsync(
        Guid companyId,
        ItemParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<Item>(filterNodeDto);
        var query = _itemRepository.GetFiltered(companyId, advancedFilters);
        var res = await _itemRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ItemDto>(
            results: _mapper.Map<IReadOnlyList<ItemDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<ItemListDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
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
        var item = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p =>
                p.CompanyId == companyId &&
                p.CategoryId == categoryId &&
                p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchItemDto>(item);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, item);

        await _itemRepository.SaveChangesAsync(ct);
        return _mapper.Map<ItemDto>(item);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        await _itemRepository.Remove(e =>
            e.CompanyId == companyId &&
            e.CategoryId == categoryId &&
            e.Id == id,
            ct
        );
    }

    private async Task<Item> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<Item, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _itemRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }

    private static string BuildLocationPath(
        Guid locationId,
        IReadOnlyDictionary<Guid, WarehouseLocationNode> byId,
        Dictionary<Guid, string> cache,
        HashSet<Guid>? visiting = null
    )
    {
        if (cache.TryGetValue(locationId, out var cached))
            return cached;

        visiting ??= [];
        if (!visiting.Add(locationId))
            throw new InvalidOperationException($"Cycle detected while building location path for '{locationId}'.");

        if (!byId.TryGetValue(locationId, out var node))
            throw new InvalidOperationException($"Location '{locationId}' not found while building location path.");

        var path = node.ParentLocationId is null
            ? node.Title
            : $"{BuildLocationPath(node.ParentLocationId.Value, byId, cache, visiting)}-{node.Title}";

        cache[locationId] = path;
        visiting.Remove(locationId);
        return path;
    }
}
