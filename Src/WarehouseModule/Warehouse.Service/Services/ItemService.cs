using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.RequestFeatures;
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

    public async Task<ItemDto> CreateAsync(
        CreateItemDto item,
        CancellationToken ct
    )
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ItemDto>> GetListAsync(
        ItemParameters itemParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        var items = await _itemRepository.GetListAsync(
            itemParameters,
            requestAdvancedFilters
        );

        return _mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<ItemDto?> GetByIdAsync(Guid id)
    {
        return _mapper.Map<ItemDto>(await GetItemByIdAsync(id));
    }

    public async Task<ItemDto> UpdateAsync(
        Guid id,
        UpdateItemDto updateItemDto,
        CancellationToken ct
    )
    {
        var item = await GetItemByIdAsync(id);

        _mapper.Map(updateItemDto, item);
        await _itemRepository.SaveChangesAsync(ct);

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _itemRepository.Remove(await GetItemByIdAsync(id));
        return true;
    }

    private async Task<Item> GetItemByIdAsync(Guid id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        return item ?? throw new NotFoundException(_localizer["Item"].Value);
    }
}
