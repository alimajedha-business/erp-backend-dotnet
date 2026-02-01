using AutoMapper;

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

    public async Task<ItemDto> CreateAsync(
        Guid companyId,
        CreateItemDto item,
        CancellationToken ct
    )
    {
        throw new NotImplementedException();
    }

    public async Task<ListResponseModel<ItemDto>> GetListAsync(
        Guid companyId,
        ItemParameters itemParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        var listQueryResult = await _itemRepository.GetListAsync(
            companyId,
            itemParameters,
            requestAdvancedFilters
        );

        return new ListResponseModel<ItemDto>(
            items: _mapper.Map<IReadOnlyList<ItemDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            itemParameters
        );
    }

    public async Task<ItemDto?> GetByIdAsync(Guid companyId, Guid id)
    {
        return _mapper.Map<ItemDto>(await GetItemByIdAsync(companyId, id));
    }

    public async Task<ItemDto> UpdateAsync(
        Guid companyId,
        Guid id,
        UpdateItemDto updateItemDto,
        CancellationToken ct
    )
    {
        var item = await GetItemByIdAsync(companyId, id);

        _mapper.Map(updateItemDto, item);
        await _itemRepository.SaveChangesAsync(ct);

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<bool> DeleteAsync(Guid companyId, Guid id)
    {
        _itemRepository.Remove(await GetItemByIdAsync(companyId, id));
        return true;
    }

    private async Task<Item> GetItemByIdAsync(Guid companyId, Guid id)
    {
        var item = await _itemRepository.GetByIdAsync(companyId, id);
        return item ?? throw new NotFoundException(_localizer["Item"].Value);
    }
}
