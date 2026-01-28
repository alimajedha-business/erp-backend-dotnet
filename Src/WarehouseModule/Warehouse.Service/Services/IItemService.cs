using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IItemService
{
    Task<ItemDto> CreateItemAsync(CreateItemDto item);
    Task<IEnumerable<ItemDto>> GetItemsAsync(
        ItemParameters itemParameters,
        string? search = null,
        object[]? searchParameters = null
    );
    Task<ItemDto?> GetItemByIdAsync(Guid id);
    Task<ItemDto> UpdateItemAsync(Guid id, UpdateItemDto item);
    Task<bool> DeleteItemAsync(Guid id);
}
