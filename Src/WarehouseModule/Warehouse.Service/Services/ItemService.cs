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
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ItemService(
    IAdvancedFilterBuilder filterBuilder,
    IItemRepository itemRepository,
    IItemBusinessRuleValidator businessRuleValidator,
    IValidator<CreateItemDto> createValidator,
    IValidator<PatchItemDto> patchValidator,
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
    private readonly IItemBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateItemDto> _createValidator = createValidator;
    private readonly IValidator<PatchItemDto> _patchValidator = patchValidator;
    private readonly IWarehouseLocationService _locationService = locationService;

    public async Task<ItemDto> CreateAsync(
        Guid companyId,
        Guid categoryId,
        CreateItemDto createItemDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createItemDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createItemDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createItemDto, ct);

        var item = new Item
        {
            CompanyId = companyId,
            CategoryId = categoryId,
            Code = createItemDto.Code,
            Title = createItemDto.Title,
            TitleInEnglish = createItemDto.TitleInEnglish,
            TechnicalNumber = createItemDto.TechnicalNumber,
            Sku = createItemDto.Sku,
            Barcode = createItemDto.Barcode,
            IsActive = createItemDto.IsActive,
            PrimaryUnitOfMeasurementId = createItemDto.PrimaryUnitOfMeasurementId,
            ItemTypeId = createItemDto.ItemTypeId
        };

        AddItemAttributes(item, createItemDto.AttributeIds);
        AddSecondaryUnits(item, createItemDto.SecondaryUnitOfMeasurementIds);
        AddItemWarehouses(item, createItemDto.ItemWarehouses);
        AddItemUnitOfMeasurementConversions(
            item,
            createItemDto.ItemUnitOfMeasurementConversions
        );

        var createdItem = await _itemRepository.AddAsync(item, ct);
        await _itemRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(companyId, categoryId, createdItem.Id, ct);
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

        return await BuildItemDtoAsync(item, ct);
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

        return await BuildItemDtoAsync(item, ct);
    }

    public async Task<ListResponseModel<ItemListDto>> GetFilteredAsync(
        Guid companyId,
        ItemParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<Item>(filterNodeDto);
        var query = _itemRepository.GetFiltered(companyId, advancedFilters);
        var res = await _itemRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ItemListDto>(
            results: _mapper.Map<IReadOnlyList<ItemListDto>>(res.items),
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
        _businessRuleValidator.ValidateParameters(parameters);

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
        PatchItemPolicy.Validate(patchDocument);

        var item = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p =>
                p.CompanyId == companyId &&
                p.CategoryId == categoryId &&
                p.Id == id,
            ct
        );

        var patchDto = BuildPatchDto(item);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        await RequestBodyValidator.ValidateAsync(_patchValidator, patchDto, ct);

        var updateDto = BuildCreateDto(patchDto, item);
        await RequestBodyValidator.ValidateAsync(_createValidator, updateDto, ct);

        var patchedPaths = patchDocument.Operations
            .Select(e => e.path.Trim('/').Split('/')[0])
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        if (patchedPaths.Contains(nameof(PatchItemDto.Code)))
        {
            await _businessRuleValidator.ValidateItemCodeUniquenessAsync(
                companyId,
                excludedItemId: id,
                updateDto.Code,
                ct
            );
        }

        if (
            patchedPaths.Contains(nameof(PatchItemDto.SecondaryUnitOfMeasurementIds)) ||
            patchedPaths.Contains("itemUnitOfMeasurements")
        )
        {
            _businessRuleValidator.ValidateItemUnitOfMeasurementCount(
                updateDto.PrimaryUnitOfMeasurementId,
                updateDto.SecondaryUnitOfMeasurementIds
            );
        }

        item.Code = updateDto.Code;
        item.Title = updateDto.Title;
        item.TitleInEnglish = updateDto.TitleInEnglish;
        item.TechnicalNumber = updateDto.TechnicalNumber;
        item.Barcode = updateDto.Barcode;
        item.IsActive = updateDto.IsActive;
        item.PrimaryUnitOfMeasurementId = updateDto.PrimaryUnitOfMeasurementId;
        item.ItemTypeId = updateDto.ItemTypeId;

        if (
            patchedPaths.Contains(nameof(PatchItemDto.AttributeIds)) ||
            patchedPaths.Contains("itemAttributes")
        )
        {
            ReplaceItemAttributes(item, updateDto.AttributeIds);
        }

        if (
            patchedPaths.Contains(nameof(PatchItemDto.SecondaryUnitOfMeasurementIds)) ||
            patchedPaths.Contains("itemUnitOfMeasurements")
        )
        {
            ReplaceSecondaryUnits(item, updateDto.SecondaryUnitOfMeasurementIds);
        }

        if (patchedPaths.Contains(nameof(PatchItemDto.ItemWarehouses)))
        {
            ReplaceItemWarehouses(item, updateDto.ItemWarehouses);
        }

        if (
            patchedPaths.Contains(nameof(PatchItemDto.ItemUnitOfMeasurementConversions)) ||
            patchedPaths.Contains(nameof(PatchItemDto.ItemUnitOfMeasurementConversionsAlias)) ||
            patchedPaths.Contains("unitConversions")
        )
        {
            ReplaceItemUnitOfMeasurementConversions(
                item,
                updateDto.ItemUnitOfMeasurementConversions
            );
        }

        await _itemRepository.SaveChangesAsync(ct);

        var updatedItem = await GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p =>
                p.CompanyId == companyId &&
                p.CategoryId == categoryId &&
                p.Id == id,
            ct
        );

        return await BuildItemDtoAsync(updatedItem, ct);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, categoryId, id, ct);

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

    private static void AddItemAttributes(
        Item item,
        IEnumerable<Guid> attributeIds
    )
    {
        foreach (var attributeId in attributeIds.Distinct())
        {
            item.ItemAttributes.Add(new ItemAttribute
            {
                AttributeId = attributeId
            });
        }
    }

    private static void AddSecondaryUnits(
        Item item,
        IEnumerable<Guid> secondaryUnitOfMeasurementIds
    )
    {
        var unitOfMeasurementIds = secondaryUnitOfMeasurementIds
            .Where(uomId => uomId != item.PrimaryUnitOfMeasurementId)
            .Distinct()
            .ToList();

        for (var index = 0; index < unitOfMeasurementIds.Count; index++)
        {
            item.ItemUnitOfMeasurements.Add(new ItemUnitOfMeasurement
            {
                UnitOfMeasurementId = unitOfMeasurementIds[index],
                UnitOrder = index + 2
            });
        }
    }

    private static void AddItemWarehouses(
        Item item,
        IReadOnlyCollection<CreateItemWarehouseDto> itemWarehouses
    )
    {
        ValidateDuplicateWarehouses(
            itemWarehouses,
            nameof(CreateItemDto.ItemWarehouses)
        );

        foreach (var itemWarehouseDto in itemWarehouses)
        {
            item.ItemWarehouses.Add(MapItemWarehouse(itemWarehouseDto));
        }
    }

    private static ItemWarehouse MapItemWarehouse(CreateItemWarehouseDto dto)
    {
        return new ItemWarehouse
        {
            WarehouseId = dto.WarehouseId,
            ReorderPoint = dto.ReorderPoint,
            CriticalPoint = dto.CriticalPoint,
            ReorderQuantity = dto.ReorderQuantity,
            MaxStockLevel = dto.MaxStockLevel,
            ItemWarehouseLocations = [.. dto.LocationIds
                .Distinct()
                .Select(locationId => new ItemWarehouseLocation
                {
                    WarehouseLocationId = locationId
                })]
        };
    }

    private static void ValidateDuplicateWarehouses(
        IReadOnlyCollection<CreateItemWarehouseDto> itemWarehouses,
        string propertyName
    )
    {
        var duplicateWarehouseIds = itemWarehouses
            .GroupBy(e => e.WarehouseId)
            .Where(e => e.Count() > 1)
            .Select(e => e.Key)
            .ToList();

        if (duplicateWarehouseIds.Count != 0)
        {
            throw new ValidationException(duplicateWarehouseIds.Select(warehouseId =>
                new ValidationFailure(
                    propertyName,
                    $"Duplicate warehouseId '{warehouseId}' is not allowed."
                )
            ));
        }
    }

    private static void AddItemUnitOfMeasurementConversions(
        Item item,
        IReadOnlyDictionary<string, ItemUnitConversionEquationDto> conversions
    )
    {
        var unitOfMeasurementIds = BuildUnitOfMeasurementIdsByOrder(item);
        var requestedConversions = conversions
            .Where(conversion => !IsEmptyUnitConversion(conversion.Value))
            .ToList();

        ValidateUnitConversionCycles(requestedConversions, unitOfMeasurementIds.Count);

        foreach (var conversion in requestedConversions)
        {
            var itemUnitOfMeasurementConversion = CreateItemUnitOfMeasurementConversion(
                conversion,
                unitOfMeasurementIds
            );

            if (itemUnitOfMeasurementConversion is not null)
            {
                item.ItemUnitOfMeasurementConversions.Add(itemUnitOfMeasurementConversion);
            }
        }
    }

    private static ItemUnitOfMeasurementConversion? CreateItemUnitOfMeasurementConversion(
        KeyValuePair<string, ItemUnitConversionEquationDto> conversion,
        List<Guid> unitOfMeasurementIds
    )
    {
        var unitOrder = GetConversionKey(conversion);
        if (unitOrder < 1 || unitOrder > unitOfMeasurementIds.Count)
        {
            throw new ValidationException([
                new ValidationFailure(
                    nameof(CreateItemDto.ItemUnitOfMeasurementConversions),
                    $"unitConversions key '{unitOrder}' must be a unit order between 1 and {unitOfMeasurementIds.Count}."
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
                new ValidationFailure($"unitConversions[{unitOrder}]", ex.Message)
            ]);
        }
    }

    private static void ValidateUnitConversionCycles(
        IReadOnlyList<KeyValuePair<string, ItemUnitConversionEquationDto>> conversions,
        int unitCount
    )
    {
        var graph = new Dictionary<int, List<int>>();

        foreach (var conversion in conversions)
        {
            var unitOrder = GetConversionKey(conversion);

            if (unitOrder < 1 || unitOrder > unitCount)
                continue;

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

    private static int GetConversionKey(
        KeyValuePair<string, ItemUnitConversionEquationDto> conversion
    )
    {
        var conversionKey = conversion.Key;
        var match = RegexHelpers
            .UnitOfMeasurementConversionRegex()
            .Match(conversionKey);

        if (!match.Success)
        {
            throw new ValidationException("Unit Error");
        }

        return int.Parse(match.Groups[1].Value);
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

    private static IEnumerable<int> GetReferencedUnitOrders(ItemUnitConversionEquationDto conversion)
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

    private static bool IsEmptyUnitConversion(ItemUnitConversionEquationDto conversion)
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

    private async Task<ItemDto> BuildItemDtoAsync(
        Item item,
        CancellationToken ct
    )
    {
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
            BuildUnitConversionsByOrder(item),
            item.IsActive
        );
    }

    private static PatchItemDto BuildPatchDto(Item item)
    {
        return new PatchItemDto
        {
            Code = item.Code,
            Title = item.Title,
            TitleInEnglish = item.TitleInEnglish,
            TechnicalNumber = item.TechnicalNumber,
            Barcode = item.Barcode,
            IsActive = item.IsActive,
            PrimaryUnitOfMeasurementId = item.PrimaryUnitOfMeasurementId,
            ItemTypeId = item.ItemTypeId,
            AttributeIds = [.. item.ItemAttributes
                .Select(e => e.AttributeId)],
            SecondaryUnitOfMeasurementIds = [.. item.ItemUnitOfMeasurements
                .OrderBy(e => e.UnitOrder)
                .Select(e => e.UnitOfMeasurementId)],
            ItemWarehouses = [.. item.ItemWarehouses
                .Select(MapCreateItemWarehouseDto)],
            ItemUnitOfMeasurementConversions = BuildUnitConversionsByOrder(item)
        };
    }

    private static CreateItemDto BuildCreateDto(
        PatchItemDto patchDto,
        Item item
    )
    {
        return new CreateItemDto
        {
            Code = patchDto.Code!,
            Title = patchDto.Title!,
            TitleInEnglish = patchDto.TitleInEnglish!,
            TechnicalNumber = patchDto.TechnicalNumber!,
            Sku = item.Sku,
            Barcode = patchDto.Barcode!,
            IsActive = patchDto.IsActive!.Value,
            PrimaryUnitOfMeasurementId = patchDto.PrimaryUnitOfMeasurementId!.Value,
            ItemTypeId = patchDto.ItemTypeId!.Value,
            CategoryId = item.CategoryId,
            SecondaryUnitOfMeasurementIds = patchDto.SecondaryUnitOfMeasurementIds ?? [],
            AttributeIds = patchDto.AttributeIds ?? [],
            ItemWarehouses = patchDto.ItemWarehouses ?? [],
            ItemUnitOfMeasurementConversions = patchDto.ItemUnitOfMeasurementConversions ?? []
        };
    }

    private static CreateItemWarehouseDto MapCreateItemWarehouseDto(ItemWarehouse itemWarehouse)
    {
        return new CreateItemWarehouseDto
        {
            WarehouseId = itemWarehouse.WarehouseId,
            ReorderPoint = itemWarehouse.ReorderPoint,
            CriticalPoint = itemWarehouse.CriticalPoint,
            ReorderQuantity = itemWarehouse.ReorderQuantity,
            MaxStockLevel = itemWarehouse.MaxStockLevel,
            LocationIds = [.. itemWarehouse.ItemWarehouseLocations
                .Select(e => e.WarehouseLocationId)]
        };
    }

    private static Dictionary<string, ItemUnitConversionEquationDto> BuildUnitConversionsByOrder(Item item)
    {
        var unitOrderById = new Dictionary<Guid, int>
        {
            [item.PrimaryUnitOfMeasurementId] = 1
        };

        foreach (var itemUnitOfMeasurement in item.ItemUnitOfMeasurements)
        {
            unitOrderById[itemUnitOfMeasurement.UnitOfMeasurementId] =
                itemUnitOfMeasurement.UnitOrder;
        }

        return item.ItemUnitOfMeasurementConversions
            .Where(conversion => unitOrderById.ContainsKey(conversion.UnitOfMeasurementId))
            .OrderBy(conversion => unitOrderById[conversion.UnitOfMeasurementId])
            .ToDictionary(
                conversion => $"unit{unitOrderById[conversion.UnitOfMeasurementId]}",
                conversion => UnitConversionEquationBuilder.Parse(conversion.ConversionEquation)
            );
    }

    private static List<Guid> BuildUnitOfMeasurementIdsByOrder(Item item)
    {
        return [
            item.PrimaryUnitOfMeasurementId,
            .. item.ItemUnitOfMeasurements
                .OrderBy(x => x.UnitOrder)
                .Select(x => x.UnitOfMeasurementId)
                .Where(uomId => uomId != item.PrimaryUnitOfMeasurementId)
                .Distinct()
        ];
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

    private static void ReplaceItemAttributes(
        Item item,
        IEnumerable<Guid> requestedAttributeIds
    )
    {
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

    private static void ReplaceSecondaryUnits(
        Item item,
        IEnumerable<Guid> requestedUomIds
    )
    {
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

    private static void ReplaceItemWarehouses(
        Item item,
        IReadOnlyCollection<CreateItemWarehouseDto> requestedWarehouses
    )
    {
        var requested = requestedWarehouses.ToList();

        ValidateDuplicateWarehouses(
            requested,
            nameof(PatchItemDto.ItemWarehouses)
        );

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
                item.ItemWarehouses.Add(MapItemWarehouse(requestedWarehouse));

                continue;
            }

            UpdateWarehouseScalarValues(existingWarehouse, requestedWarehouse);
            SyncWarehouseLocations(existingWarehouse, requestedWarehouse.LocationIds);
        }
    }

    private static void ReplaceItemUnitOfMeasurementConversions(
        Item item,
        IReadOnlyDictionary<string, ItemUnitConversionEquationDto> requestedConversions
    )
    {
        var unitOfMeasurementIds = BuildUnitOfMeasurementIdsByOrder(item);
        var requested = requestedConversions
            .Where(conversion => !IsEmptyUnitConversion(conversion.Value))
            .ToList();

        ValidateUnitConversionCycles(requested, unitOfMeasurementIds.Count);

        var requestedEntities = requested
            .Select(conversion => CreateItemUnitOfMeasurementConversion(
                conversion,
                unitOfMeasurementIds
            ))
            .Where(conversion => conversion is not null)
            .Select(conversion => conversion!)
            .ToList();

        var requestedUomIds = requestedEntities
            .Select(x => x.UnitOfMeasurementId)
            .ToHashSet();

        var toRemove = item.ItemUnitOfMeasurementConversions
            .Where(x => !requestedUomIds.Contains(x.UnitOfMeasurementId))
            .ToList();

        foreach (var conversion in toRemove)
        {
            item.ItemUnitOfMeasurementConversions.Remove(conversion);
        }

        var existingByUomId = item.ItemUnitOfMeasurementConversions
            .ToDictionary(x => x.UnitOfMeasurementId);

        foreach (var requestedEntity in requestedEntities)
        {
            if (existingByUomId.TryGetValue(requestedEntity.UnitOfMeasurementId, out var existing))
            {
                if (existing.ConversionEquation != requestedEntity.ConversionEquation)
                {
                    existing.ConversionEquation = requestedEntity.ConversionEquation;
                }

                continue;
            }

            item.ItemUnitOfMeasurementConversions.Add(requestedEntity);
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
