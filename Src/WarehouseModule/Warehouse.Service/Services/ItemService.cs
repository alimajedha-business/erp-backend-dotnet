using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;
using FluentValidation.Results;

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

        List<Guid> unitOfMeasurementIds = [
            createItemDto.PrimaryUnitOfMeasurementId,
            .. createItemDto.SecondaryUnitOfMeasurementIds
                .Where(uomId => uomId != createItemDto.PrimaryUnitOfMeasurementId)
                .Distinct()
        ];

        item.ItemUnitOfMeasurements = [.. unitOfMeasurementIds
            .Skip(1)
            .Select((uomId, index) => new ItemUnitOfMeasurement
            {
                UnitOfMeasurementId = uomId,
                UnitOrder = index + 2
            })];

        var warehouseIds = new HashSet<Guid>();
        var duplicateWarehouseIds = new List<Guid>();

        foreach (var itemWarehouse in createItemDto.ItemWarehouses)
        {
            if (!warehouseIds.Add(itemWarehouse.WarehouseId))
                duplicateWarehouseIds.Add(itemWarehouse.WarehouseId);
        }

        if (duplicateWarehouseIds.Count != 0)
        {
            throw new ValidationException(duplicateWarehouseIds.Select(warehouseId =>
                new ValidationFailure(
                    nameof(CreateItemDto.ItemWarehouses),
                    $"Duplicate warehouseId '{warehouseId}' is not allowed."
                )
            ));
        }

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

        var unitConversions = createItemDto.ItemUnitOfMeasurementConversions
            .Where(conversion => !IsEmptyUnitConversion(conversion.Value))
            .ToList();

        ValidateUnitConversionCycles(unitConversions, unitOfMeasurementIds.Count);

        item.ItemUnitOfMeasurementConversions = [.. unitConversions
            .Select(conversion => CreateItemUnitOfMeasurementConversion(
                conversion,
                unitOfMeasurementIds
            ))
            .Where(conversion => conversion is not null)
            .Select(conversion => conversion!)];

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

        var patchedPaths = patchDocument.Operations
            .Select(x => x.path.Trim('/').Split('/')[0])
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        _mapper.Map(patchDto, item);

        if (patchedPaths.Contains(nameof(PatchItemDto.AttributeIds)))
        {
            SyncItemAttributes(item, patchDto.AttributeIds);
        }

        if (patchedPaths.Contains(nameof(PatchItemDto.SecondaryUnitOfMeasurementIds)))
        {
            SyncSecondaryUnits(item, patchDto.SecondaryUnitOfMeasurementIds);
        }

        if (patchedPaths.Contains(nameof(PatchItemDto.ItemWarehouses)))
        {
            SyncItemWarehouses(item, patchDto.ItemWarehouses);
        }

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

    private static ItemUnitOfMeasurementConversion? CreateItemUnitOfMeasurementConversion(
    KeyValuePair<string, UnitConversionEquationDto> conversion,
    List<Guid> unitOfMeasurementIds
)
    {
        if (!int.TryParse(conversion.Key, out var unitOrder) ||
            unitOrder < 1 ||
            unitOrder > unitOfMeasurementIds.Count)
        {
            throw new ValidationException([
                new ValidationFailure(
                nameof(CreateItemDto.ItemUnitOfMeasurementConversions),
                $"unitConversions key '{conversion.Key}' must be a unit order between 1 and {unitOfMeasurementIds.Count}."
            )
            ]);
        }

        try
        {
            var equation = UnitConversionEquationBuilder.Build(
                conversion.Value,
                maxUnitOrder: unitOfMeasurementIds.Count
            );

            if (equation is null)
                return null;

            return new ItemUnitOfMeasurementConversion
            {
                UnitOfMeasurementId = unitOfMeasurementIds[unitOrder - 1],
                ConversionEquation = equation,
            };
        }
        catch (ArgumentException ex)
        {
            throw new ValidationException([
                new ValidationFailure($"unitConversions[{conversion.Key}]", ex.Message)
            ]);
        }
    }

    private static void ValidateUnitConversionCycles(
        IReadOnlyList<KeyValuePair<string, UnitConversionEquationDto>> conversions,
        int unitCount
    )
    {
        var graph = new Dictionary<int, List<int>>();

        foreach (var conversion in conversions)
        {
            if (!int.TryParse(conversion.Key, out var unitOrder) ||
                unitOrder < 1 ||
                unitOrder > unitCount)
            {
                continue;
            }

            graph[unitOrder] = [.. GetReferencedUnitOrders(conversion.Value)
                .Where(reference => reference <= unitCount)];
        }

        var states = new Dictionary<int, VisitState>();
        var path = new Stack<int>();

        foreach (var unitOrder in graph.Keys)
        {
            VisitUnitConversion(unitOrder, graph, states, path);
        }
    }

    private static void VisitUnitConversion(
        int unitOrder,
        IReadOnlyDictionary<int, List<int>> graph,
        Dictionary<int, VisitState> states,
        Stack<int> path
    )
    {
        if (states.TryGetValue(unitOrder, out var state))
        {
            if (state == VisitState.Visiting)
                ThrowUnitConversionCycle(unitOrder, path);

            return;
        }

        states[unitOrder] = VisitState.Visiting;
        path.Push(unitOrder);

        if (graph.TryGetValue(unitOrder, out var references))
        {
            foreach (var reference in references)
            {
                if (graph.ContainsKey(reference))
                    VisitUnitConversion(reference, graph, states, path);
            }
        }

        path.Pop();
        states[unitOrder] = VisitState.Visited;
    }

    private static void ThrowUnitConversionCycle(int repeatedUnitOrder, Stack<int> path)
    {
        var cycle = path
            .Reverse()
            .SkipWhile(unitOrder => unitOrder != repeatedUnitOrder)
            .Append(repeatedUnitOrder)
            .Select(unitOrder => $"@u{unitOrder}");

        throw new ValidationException([
            new ValidationFailure(
                nameof(CreateItemDto.ItemUnitOfMeasurementConversions),
                $"Unit conversion cycle detected: {string.Join(" -> ", cycle)}."
            )
        ]);
    }

    private static IEnumerable<int> GetReferencedUnitOrders(UnitConversionEquationDto conversion)
    {
        return new[]
            {
                conversion.Operand1,
                conversion.Operand2,
                conversion.Operand3,
                conversion.Operand4
            }
            .Where(operand => !IsEmpty(operand))
            .Select(operand => operand!.ToString()!.Trim())
            .Where(operand => RegexHelpers.UnitOfMeasurementRefRegex().IsMatch(operand))
            .Select(operand => int.Parse(operand[2..]));
    }

    private static bool IsEmptyUnitConversion(UnitConversionEquationDto conversion)
    {
        return IsEmpty(conversion.Operand1) &&
            IsEmpty(conversion.Operand2) &&
            IsEmpty(conversion.Operand3) &&
            IsEmpty(conversion.Operand4) &&
            IsEmpty(conversion.Op1) &&
            IsEmpty(conversion.Op2) &&
            IsEmpty(conversion.Op3);
    }

    private static bool IsEmpty(object? value)
    {
        return value is null || string.IsNullOrWhiteSpace(value.ToString());
    }

    private enum VisitState
    {
        Visiting,
        Visited
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

    private static void SyncItemAttributes(
        Item item,
        IEnumerable<Guid>? requestedAttributeIds
    )
    {
        if (requestedAttributeIds is null)
            return;

        var requested = requestedAttributeIds
            .Distinct()
            .ToHashSet();

        var existing = item.ItemAttributes
            .Select(x => x.AttributeId)
            .ToHashSet();

        var toRemove = item.ItemAttributes
            .Where(x => !requested.Contains(x.AttributeId))
            .ToList();

        foreach (var relation in toRemove)
        {
            item.ItemAttributes.Remove(relation);
        }

        foreach (var attributeId in requested.Except(existing))
        {
            item.ItemAttributes.Add(new ItemAttribute
            {
                AttributeId = attributeId
            });
        }
    }

    private static void SyncSecondaryUnits(
        Item item,
        IEnumerable<Guid>? requestedUomIds
    )
    {
        if (requestedUomIds is null)
            return;

        var requested = requestedUomIds
            .Where(uomId => uomId != item.PrimaryUnitOfMeasurementId)
            .Distinct()
            .ToList();

        var requestedSet = requested.ToHashSet();

        var toRemove = item.ItemUnitOfMeasurements
            .Where(x => !requestedSet.Contains(x.UnitOfMeasurementId))
            .ToList();

        foreach (var relation in toRemove)
        {
            item.ItemUnitOfMeasurements.Remove(relation);
        }

        var existingByUomId = item.ItemUnitOfMeasurements
            .ToDictionary(x => x.UnitOfMeasurementId);

        for (var index = 0; index < requested.Count; index++)
        {
            var uomId = requested[index];
            var unitOrder = index + 2;

            if (existingByUomId.TryGetValue(uomId, out var existing))
            {
                if (existing.UnitOrder != unitOrder)
                {
                    existing.UnitOrder = unitOrder;
                }

                continue;
            }

            item.ItemUnitOfMeasurements.Add(new ItemUnitOfMeasurement
            {
                UnitOfMeasurementId = uomId,
                UnitOrder = unitOrder
            });
        }
    }

    private void SyncItemWarehouses(
        Item item,
        IEnumerable<CreateItemWarehouseDto>? requestedWarehouses
    )
    {
        if (requestedWarehouses is null)
            return;

        var requested = requestedWarehouses.ToList();

        var duplicateWarehouseIds = requested
            .GroupBy(x => x.WarehouseId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicateWarehouseIds.Count != 0)
        {
            // TODO: throw proper exception
            throw new Exception();
        }

        var requestedWarehouseIds = requested
            .Select(x => x.WarehouseId)
            .ToHashSet();

        var toRemove = item.ItemWarehouses
            .Where(x => !requestedWarehouseIds.Contains(x.WarehouseId))
            .ToList();

        foreach (var warehouse in toRemove)
        {
            item.ItemWarehouses.Remove(warehouse);
        }

        var existingByWarehouseId = item.ItemWarehouses
            .ToDictionary(x => x.WarehouseId);

        foreach (var requestedWarehouse in requested)
        {
            if (!existingByWarehouseId.TryGetValue(requestedWarehouse.WarehouseId, out var existingWarehouse))
            {
                item.ItemWarehouses.Add(new ItemWarehouse
                {
                    WarehouseId = requestedWarehouse.WarehouseId,
                    ReorderPoint = requestedWarehouse.ReorderPoint,
                    CriticalPoint = requestedWarehouse.CriticalPoint,
                    ReorderQuantity = requestedWarehouse.ReorderQuantity,
                    MaxStockLevel = requestedWarehouse.MaxStockLevel,
                    ItemWarehouseLocations = requestedWarehouse.LocationIds
                        .Distinct()
                        .Select(locationId => new ItemWarehouseLocation
                        {
                            WarehouseLocationId = locationId
                        })
                        .ToList()
                });

                continue;
            }

            UpdateWarehouseScalarValues(existingWarehouse, requestedWarehouse);
            SyncWarehouseLocations(existingWarehouse, requestedWarehouse.LocationIds);
        }
    }

    private static void UpdateWarehouseScalarValues(
        ItemWarehouse existing,
        CreateItemWarehouseDto requested
    )
    {
        if (existing.ReorderPoint != requested.ReorderPoint)
        {
            existing.ReorderPoint = requested.ReorderPoint;
        }

        if (existing.CriticalPoint != requested.CriticalPoint)
        {
            existing.CriticalPoint = requested.CriticalPoint;
        }

        if (existing.ReorderQuantity != requested.ReorderQuantity)
        {
            existing.ReorderQuantity = requested.ReorderQuantity;
        }

        if (existing.MaxStockLevel != requested.MaxStockLevel)
        {
            existing.MaxStockLevel = requested.MaxStockLevel;
        }
    }

    private static void SyncWarehouseLocations(
        ItemWarehouse warehouse,
        IEnumerable<Guid> requestedLocationIds
    )
    {
        var requested = requestedLocationIds
            .Distinct()
            .ToHashSet();

        var existing = warehouse.ItemWarehouseLocations
            .Select(x => x.WarehouseLocationId)
            .ToHashSet();

        var toRemove = warehouse.ItemWarehouseLocations
            .Where(x => !requested.Contains(x.WarehouseLocationId))
            .ToList();

        foreach (var relation in toRemove)
        {
            warehouse.ItemWarehouseLocations.Remove(relation);
        }

        foreach (var locationId in requested.Except(existing))
        {
            warehouse.ItemWarehouseLocations.Add(new ItemWarehouseLocation
            {
                WarehouseLocationId = locationId
            });
        }
    }
}
