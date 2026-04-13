using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ItemService(
    IAdvancedFilterBuilder filterBuilder,
    ICompanyService companyService,
    IItemRepository itemRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        Item,
        ItemDto,
        ItemListDto,
        ItemParameters,
        IItemRepository,
        WarehouseResource
    >(
        filterBuilder,
        itemRepository,
        companyService,
        mapper,
        localizer
    ),
    IItemService
{
    protected override string LocalizerKey => "Item";

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

        var createdItem = await _repo.AddAsync(item, ct);

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

        await _repo.SaveChangesAsync(ct);
        return _mapper.Map<ItemDto>(createdItem);
    }

    public async Task<ListResponseModel<ItemListDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await _companyService.GetByIdAsync(
            companyId,
            ct
        );

        var advancedFilters = _filterBuilder.Build<Item>(filterNodeDto);
        var listQueryResult = await _repo.GetCategoryAllAsync(
            companyId,
            categoryId,
            parameters,
            ct,
            advancedFilters
        );

        return new ListResponseModel<ItemListDto>(
            results: _mapper.Map<IReadOnlyList<ItemListDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<ItemDto> GetByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        await _companyService.GetByIdAsync(
            companyId,
            ct
        );

        var item = await GetByIdOrThrowAsync(
            companyId,
            categoryId,
            id,
            ct,
            trackChanges: true
        );

        return _mapper.Map<ItemDto>(item);
    }

    private async Task<Item> GetByIdOrThrowAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var entity = await _repo.GetByIdAsync(companyId, categoryId, id, ct, trackChanges);

        return entity ?? throw new NotFoundException(_localizer[LocalizerKey].Value);
    }
}
