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

        item.ItemAttributes = [.. createItemDto
            .AttributeIds
            .Distinct()
            .Select(attributeId => new ItemAttribute
			{
				AttributeId = attributeId,
			})];

		item.ItemUnitOfMeasurements = [.. createItemDto
            .SecondaryUnitOfMeasurementIds
            .Distinct()
			.Select((uomId, index) => new ItemUnitOfMeasurement
			{
				UnitOfMeasurementId = uomId,
				UnitOrder = index + 2
			})];

		item.ItemWarehouses = [.. createItemDto
            .Warehouses
			.Select(warehouseDto => new ItemWarehouse
			{
				WarehouseId = warehouseDto.WarehouseId,
				ReorderPoint = warehouseDto.ReorderPoint,
				CriticalPoint = warehouseDto.CriticalPoint,
				ReorderQuantity = warehouseDto.ReorderQuantity,
				MaxStockLevel = warehouseDto.MaxStockLevel,

				Locations = [.. warehouseDto.LocationIds
					.Select(locationId => new ItemWarehouseLocation
					{
						LocationId = locationId
					})]
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
        var item = await GetByIdOrThrowAsync(
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
        var item = await GetByIdOrThrowAsync(
            trackChanges: true,
            predicate: p => 
                p.CompanyId == companyId &&
                p.CategoryId == categoryId &&
                p.Id == id,
            ct
        );

        return _mapper.Map<ItemDto>(item);
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
        var item = await GetByIdOrThrowAsync(
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

    private async Task<Item> GetByIdOrThrowAsync(
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
}
