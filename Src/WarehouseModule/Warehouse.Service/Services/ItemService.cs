using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.Extensions.Logging;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public class ItemService(
    IItemRepository itemRepository,
    ILogger<ItemService> logger,
    IMapper mapper
) : IItemService
{
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly ILogger<ItemService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<ItemDto> CreateItemAsync(CreateItemDto item)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ItemDto>> GetItemsAsync(
        ItemParameters itemParameters,
        string? search,
        object[]? searchParameters
    )
    {
        var items = await _itemRepository.GetPaginatedAsync(itemParameters, search, searchParameters);
        return _mapper.Map<IEnumerable<ItemDto>>(items);
    }

    public async Task<ItemDto?> GetItemByIdAsync(Guid Id)
    {
        var item = await _itemRepository.GetByIdAsync(Id);
        return item != null ? _mapper.Map<ItemDto>(item) : null;
    }

    public async Task<ItemDto> UpdateItemAsync(Guid id, UpdateItemDto item)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteItemAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
