using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

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
    private readonly IItemRepository _itemRepository = itemRepository;

    public Task<ItemDto> CreateItemAsync(
        Guid companyId,
        CreateItemDto createDto,
        CancellationToken ct
    ) => CreateAsync(companyId, createDto, ct);

    public Task<ListResponseModel<ItemDto>> GetAllItemsAsync(
        Guid companyId,
        ItemParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    ) => GetAllAsync(companyId, parameters, ct, filterNodeDto);

    public async Task<ListResponseModel<ItemDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        var advancedFilters = _filterBuilder.Build<Item>(filterNodeDto);
        var listQueryResult = await _itemRepository.GetCategoryAllAsync(
            companyId,
            categoryId,
            parameters,
            ct,
            advancedFilters
        );

        return new ListResponseModel<ItemDto>(
            items: _mapper.Map<IReadOnlyList<ItemDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public Task<ItemDto> GetItemByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(companyId, id, ct);

    public Task<ItemDto> PatchItemAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchItemDto> patchDoc,
        CancellationToken ct
    ) => PatchAsync(companyId, id, patchDoc, ct);

    public Task DeleteItemAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => DeleteAsync(companyId, id, ct);
}
