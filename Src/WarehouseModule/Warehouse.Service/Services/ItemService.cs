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

    public Task<ItemDto> CreateAsync(
        CreateItemDto item,
        CancellationToken ct
    )
    {
        throw new NotImplementedException();
    }

    public Task<ItemDto> CreateItemAsync(
        Guid companyId,
        CreateItemDto item,
        CancellationToken ct
    )
    {
        throw new NotImplementedException();
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
        return _mapper.Map<ItemDto>(await GetByIdAsync(companyId, id, ct));
    }

    public async Task<ItemDto> UpdateItemAsync(
        Guid companyId,
        Guid id,
        UpdateItemDto updateItemDto,
        CancellationToken ct
    )
    {
        var item = await GetByIdAsync(companyId, id, ct);

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
        _itemRepository.Remove(await GetByIdAsync(companyId, id, ct));

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

    private async Task<Item> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var item = await _itemRepository.GetByIdAsync(companyId, id, ct);
        return item ?? throw new NotFoundException(_localizer["Item"].Value);
    }
}
