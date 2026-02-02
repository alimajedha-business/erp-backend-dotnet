using AutoMapper;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class ItemService(
    IItemRepository itemRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IItemService
{
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;

    public async Task<ItemDto> CreateItemAsync(
        Guid companyId,
        CreateItemDto createItemDto,
        CancellationToken ct
    )
    {
        var item = _mapper.Map<Item>(createItemDto);
        item.CompanyId = companyId;

        var createdItem = await _itemRepository.AddAsync(item, ct);
        await _itemRepository.SaveChangesAsync(ct);

        return _mapper.Map<ItemDto>(createdItem);
    }

    public async Task<ListResponseModel<ItemDto>> GetAllItemsAsync(
        Guid companyId,
        ItemParameters itemParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        var listQueryResult = await _itemRepository.GetAllAsync(
            companyId,
            itemParameters,
            ct,
            requestAdvancedFilters
        );

        return new ListResponseModel<ItemDto>(
            items: _mapper.Map<IReadOnlyList<ItemDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            itemParameters
        );
    }

    public async Task<ItemDto?> GetItemByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var item = await GetByIdOrThrowExceptionAsync(companyId,id,ct);
        return _mapper.Map<ItemDto>(item);
    }

    public async Task<ItemDto> UpdateItemAsync(
        Guid companyId,
        Guid id,
        UpdateItemDto updateItemDto,
        CancellationToken ct
    )
    {
        var item = await GetByIdOrThrowExceptionAsync(
            companyId,
            id,
            ct,
            trackChanges: true
        );

        _mapper.Map(updateItemDto, item);
        await _itemRepository.SaveChangesAsync(ct);

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<bool> DeleteItemAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var item = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        _itemRepository.Remove(item);

        try
        {
            await _itemRepository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new ForeignKeyViolationException(_localizer["Item"].Value);
        }

        return true;
    }

    private async Task<Item> GetByIdOrThrowExceptionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var item = await _itemRepository.GetByIdAsync(
            companyId,
            id,
            ct,
            trackChanges
        );

        return item ?? throw new NotFoundException(_localizer["Item"].Value);
    }
}
