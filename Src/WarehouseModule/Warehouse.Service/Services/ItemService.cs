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
    ISiUnitRepository unitRepository,
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
    private readonly ISiUnitRepository _unitRepository = unitRepository;
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
            ItemTypeId = createItemDto.ItemTypeId
        };

        await SetItemMeasurementValuesAsync(item, createItemDto, ct);
        AddItemAttributes(item, createItemDto.AttributeIds);
        AddItemUnitOfMeasurements(item, createItemDto.ItemUnitOfMeasurements);
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
        _businessRuleValidator.ValidateItemUnitOfMeasurementCount(
            updateDto.ItemUnitOfMeasurements
        );

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

        item.Code = updateDto.Code;
        item.Title = updateDto.Title;
        item.TitleInEnglish = updateDto.TitleInEnglish;
        item.TechnicalNumber = updateDto.TechnicalNumber;
        item.Barcode = updateDto.Barcode;
        item.IsActive = updateDto.IsActive;
        item.ItemTypeId = updateDto.ItemTypeId;
        await SetItemMeasurementValuesAsync(item, updateDto, ct);

        if (
            patchedPaths.Contains(nameof(PatchItemDto.AttributeIds)) ||
            patchedPaths.Contains("itemAttributes")
        )
        {
            ReplaceItemAttributes(item, updateDto.AttributeIds);
        }

        if (patchedPaths.Contains(nameof(PatchItemDto.ItemUnitOfMeasurements)))
        {
            ReplaceItemUnitOfMeasurements(
                item,
                updateDto.ItemUnitOfMeasurements
            );

            RemoveUnitConversionsForMissingUnits(item);
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

    private static void AddItemUnitOfMeasurements(
        Item item,
        IReadOnlyCollection<CreateItemUnitOfMeasurementDto> itemUnitOfMeasurements
    )
    {
        foreach (var dto in itemUnitOfMeasurements.OrderBy(e => e.UnitOrder))
        {
            item.ItemUnitOfMeasurements.Add(MapItemUnitOfMeasurement(dto));
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

    private static ItemUnitOfMeasurement MapItemUnitOfMeasurement(
        CreateItemUnitOfMeasurementDto dto
    )
    {
        return new ItemUnitOfMeasurement
        {
            UnitOfMeasurementId = dto.UnitOfMeasurementId,
            UnitOrder = dto.UnitOrder
        };
    }

    private async Task<IReadOnlyDictionary<Guid, SiUnit>> LoadPreferredUnitsAsync(
        CreateItemDto itemDto,
        CancellationToken ct
    )
    {
        var unitIds = new[]
            {
                itemDto.PreferredMassUnitId,
                itemDto.PreferredLengthUnitId,
                itemDto.PreferredVolumeUnitId
            }
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .Distinct()
            .ToArray();

        if (unitIds.Length == 0)
            return new Dictionary<Guid, SiUnit>();

        var unitsById = await _unitRepository.GetByIdsAsync(unitIds, ct);
        var missingIds = unitIds
            .Where(id => !unitsById.ContainsKey(id))
            .ToList();

        if (missingIds.Count != 0)
        {
            throw new ValidationException(missingIds.Select(id =>
                new ValidationFailure(
                    nameof(CreateItemDto),
                    $"SiUnit '{id}' was not found."
                )
            ));
        }

        ValidatePreferredUnitDimensions(itemDto, unitsById);
        return unitsById;
    }

    private async Task ValidateBaseUnitsExistAsync(
        CreateItemDto itemDto,
        CancellationToken ct
    )
    {
        var requiredDimensions = new[]
            {
                itemDto.Weight.HasValue ? UnitDimension.MASS : (UnitDimension?)null,
                HasAnyLength(itemDto) ? UnitDimension.LENGTH : (UnitDimension?)null,
                itemDto.Volume.HasValue ? UnitDimension.VOLUME : (UnitDimension?)null
            }
            .Where(dimension => dimension.HasValue)
            .Select(dimension => dimension!.Value)
            .Distinct()
            .ToArray();

        if (requiredDimensions.Length == 0)
            return;

        var baseUnits = await _unitRepository.GetBaseUnitsByDimensionsAsync(
            requiredDimensions,
            ct
        );

        var missingDimensions = requiredDimensions
            .Where(dimension => !baseUnits.ContainsKey(dimension))
            .ToList();

        if (missingDimensions.Count != 0)
        {
            throw new ValidationException(missingDimensions.Select(dimension =>
                new ValidationFailure(
                    nameof(CreateItemDto),
                    $"Base unit for dimension '{dimension}' was not found."
                )
            ));
        }
    }

    private static void ValidatePreferredUnitDimensions(
        CreateItemDto itemDto,
        IReadOnlyDictionary<Guid, SiUnit> unitsById
    )
    {
        var failures = new List<ValidationFailure>();

        ValidatePreferredUnitDimension(
            itemDto.PreferredMassUnitId,
            UnitDimension.MASS,
            nameof(CreateItemDto.PreferredMassUnitId),
            unitsById,
            failures
        );
        ValidatePreferredUnitDimension(
            itemDto.PreferredLengthUnitId,
            UnitDimension.LENGTH,
            nameof(CreateItemDto.PreferredLengthUnitId),
            unitsById,
            failures
        );
        ValidatePreferredUnitDimension(
            itemDto.PreferredVolumeUnitId,
            UnitDimension.VOLUME,
            nameof(CreateItemDto.PreferredVolumeUnitId),
            unitsById,
            failures
        );

        if (failures.Count != 0)
            throw new ValidationException(failures);
    }

    private static void ValidatePreferredUnitDimension(
        Guid? unitId,
        UnitDimension expectedDimension,
        string propertyName,
        IReadOnlyDictionary<Guid, SiUnit> unitsById,
        List<ValidationFailure> failures
    )
    {
        if (!unitId.HasValue || !unitsById.TryGetValue(unitId.Value, out var unit))
            return;

        if (unit.UnitDimension != expectedDimension)
        {
            failures.Add(new ValidationFailure(
                propertyName,
                $"SiUnit '{unitId}' must have '{expectedDimension}' dimension."
            ));
        }
    }

    private static bool HasAnyLength(CreateItemDto dto)
    {
        return dto.Length.HasValue || dto.Width.HasValue || dto.Height.HasValue;
    }

    private async Task SetItemMeasurementValuesAsync(
        Item item,
        CreateItemDto itemDto,
        CancellationToken ct
    )
    {
        var unitsById = await LoadPreferredUnitsAsync(itemDto, ct);
        await ValidateBaseUnitsExistAsync(itemDto, ct);

        item.Weight = ConvertToBase(
            itemDto.Weight,
            itemDto.PreferredMassUnitId,
            unitsById
        );
        item.Length = ConvertToBase(
            itemDto.Length,
            itemDto.PreferredLengthUnitId,
            unitsById
        );
        item.Width = ConvertToBase(
            itemDto.Width,
            itemDto.PreferredLengthUnitId,
            unitsById
        );
        item.Height = ConvertToBase(
            itemDto.Height,
            itemDto.PreferredLengthUnitId,
            unitsById
        );
        item.Volume = ConvertToBase(
            itemDto.Volume,
            itemDto.PreferredVolumeUnitId,
            unitsById
        );
        item.PreferredMassUnitId = itemDto.PreferredMassUnitId;
        item.PreferredLengthUnitId = itemDto.PreferredLengthUnitId;
        item.PreferredVolumeUnitId = itemDto.PreferredVolumeUnitId;
    }

    private static decimal? ConvertToBase(
        decimal? value,
        Guid? unitId,
        IReadOnlyDictionary<Guid, SiUnit> unitsById
    )
    {
        if (!value.HasValue)
            return null;

        if (!unitId.HasValue || !unitsById.TryGetValue(unitId.Value, out var unit))
            return value;

        return value.Value * unit.FactorToBase;
    }

    private static decimal? ConvertFromBase(decimal? value, SiUnit? preferredUnit)
    {
        if (!value.HasValue || preferredUnit is null)
            return value;

        if (preferredUnit.FactorToBase == 0)
            return value;

        return value.Value / preferredUnit.FactorToBase;
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
            throw new ValidationException("SiUnit Error");
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
                $"SiUnit conversion cycle detected: {string.Join(" -> ", cycle)}."
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
            ConvertFromBase(
                item.Weight,
                item.PreferredMassUnit
            ),
            ConvertFromBase(
                item.Length,
                item.PreferredLengthUnit
            ),
            ConvertFromBase(
                item.Width,
                item.PreferredLengthUnit
            ),
            ConvertFromBase(
                item.Height,
                item.PreferredLengthUnit
            ),
            ConvertFromBase(
                item.Volume,
                item.PreferredVolumeUnit
            ),
            MapSiUnitSlimDto(item.PreferredMassUnit),
            MapSiUnitSlimDto(item.PreferredLengthUnit),
            MapSiUnitSlimDto(item.PreferredVolumeUnit),
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
                .OrderBy(ium => ium.UnitOrder)
                .Select(MapItemUnitOfMeasurementDto)],
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

    private static ItemUnitOfMeasurementDto MapItemUnitOfMeasurementDto(
        ItemUnitOfMeasurement itemUnitOfMeasurement
    )
    {
        return new ItemUnitOfMeasurementDto(
            new UnitOfMeasurementSlimDto(
                itemUnitOfMeasurement.UnitOfMeasurement.Id,
                itemUnitOfMeasurement.UnitOfMeasurement.Code,
                itemUnitOfMeasurement.UnitOfMeasurement.Title
            ),
            itemUnitOfMeasurement.UnitOrder
        );
    }

    private static SiUnitAsReferenceDto? MapSiUnitSlimDto(SiUnit? unit)
    {
        return unit is null
            ? null
            : new SiUnitAsReferenceDto(
                unit.Id,
                unit.Code,
                unit.Title,
                unit.Symbol,
                unit.UnitDimension
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
            ItemTypeId = item.ItemTypeId,
            Weight = ConvertFromBase(
                item.Weight,
                item.PreferredMassUnit
            ),
            Length = ConvertFromBase(
                item.Length,
                item.PreferredLengthUnit
            ),
            Width = ConvertFromBase(
                item.Width,
                item.PreferredLengthUnit
            ),
            Height = ConvertFromBase(
                item.Height,
                item.PreferredLengthUnit
            ),
            Volume = ConvertFromBase(
                item.Volume,
                item.PreferredVolumeUnit
            ),
            PreferredMassUnitId = item.PreferredMassUnitId,
            PreferredLengthUnitId = item.PreferredLengthUnitId,
            PreferredVolumeUnitId = item.PreferredVolumeUnitId,
            AttributeIds = [.. item.ItemAttributes
                .Select(e => e.AttributeId)],
            ItemUnitOfMeasurements = [.. item.ItemUnitOfMeasurements
                .OrderBy(e => e.UnitOrder)
                .Select(MapCreateItemUnitOfMeasurementDto)],
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
            ItemTypeId = patchDto.ItemTypeId!.Value,
            CategoryId = item.CategoryId,
            Weight = patchDto.Weight,
            Length = patchDto.Length,
            Width = patchDto.Width,
            Height = patchDto.Height,
            Volume = patchDto.Volume,
            PreferredMassUnitId = patchDto.PreferredMassUnitId,
            PreferredLengthUnitId = patchDto.PreferredLengthUnitId,
            PreferredVolumeUnitId = patchDto.PreferredVolumeUnitId,
            ItemUnitOfMeasurements = patchDto.ItemUnitOfMeasurements ?? [],
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

    private static CreateItemUnitOfMeasurementDto MapCreateItemUnitOfMeasurementDto(
        ItemUnitOfMeasurement itemUnitOfMeasurement
    )
    {
        return new CreateItemUnitOfMeasurementDto
        {
            UnitOfMeasurementId = itemUnitOfMeasurement.UnitOfMeasurementId,
            UnitOrder = itemUnitOfMeasurement.UnitOrder
        };
    }

    private static Dictionary<string, ItemUnitConversionEquationDto> BuildUnitConversionsByOrder(Item item)
    {
        var unitOrderById = item.ItemUnitOfMeasurements
            .ToDictionary(
                itemUnitOfMeasurement => itemUnitOfMeasurement.UnitOfMeasurementId,
                itemUnitOfMeasurement => itemUnitOfMeasurement.UnitOrder
            );

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
        return [.. item.ItemUnitOfMeasurements
            .OrderBy(x => x.UnitOrder)
            .Select(x => x.UnitOfMeasurementId)
            .Distinct()];
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

    private static void ReplaceItemUnitOfMeasurements(
        Item item,
        IReadOnlyCollection<CreateItemUnitOfMeasurementDto> requestedItemUnitOfMeasurements
    )
    {
        var requested = requestedItemUnitOfMeasurements
            .OrderBy(e => e.UnitOrder)
            .ToList();

        var requestedSet = requested
            .Select(e => e.UnitOfMeasurementId)
            .ToHashSet();

        var toRemove = item.ItemUnitOfMeasurements
            .Where(x => !requestedSet.Contains(x.UnitOfMeasurementId))
            .ToList();

        foreach (var relation in toRemove)
        {
            item.ItemUnitOfMeasurements.Remove(relation);
        }

        var existingByUomId = item.ItemUnitOfMeasurements
            .ToDictionary(x => x.UnitOfMeasurementId);

        foreach (var dto in requested)
        {
            if (existingByUomId.TryGetValue(dto.UnitOfMeasurementId, out var existing))
            {
                UpdateItemUnitOfMeasurement(existing, dto);

                continue;
            }

            item.ItemUnitOfMeasurements.Add(MapItemUnitOfMeasurement(dto));
        }
    }

    private static void UpdateItemUnitOfMeasurement(
        ItemUnitOfMeasurement existing,
        CreateItemUnitOfMeasurementDto requested
    )
    {
        existing.UnitOrder = requested.UnitOrder;
    }

    private static void RemoveUnitConversionsForMissingUnits(Item item)
    {
        var currentUnitOfMeasurementIds = item.ItemUnitOfMeasurements
            .Select(e => e.UnitOfMeasurementId)
            .ToHashSet();

        var toRemove = item.ItemUnitOfMeasurementConversions
            .Where(e => !currentUnitOfMeasurementIds.Contains(e.UnitOfMeasurementId))
            .ToList();

        foreach (var conversion in toRemove)
        {
            item.ItemUnitOfMeasurementConversions.Remove(conversion);
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
