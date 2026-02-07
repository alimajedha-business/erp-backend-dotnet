using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IItemService
{
    Task<ItemDto> CreateItemAsync(
        Guid companyId,
        CreateItemDto item,
        CancellationToken ct
    );
    Task<ListResponseModel<ItemDto>> GetAllItemsAsync(
        Guid companyId,
        ItemParameters itemParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
    Task<ListResponseModel<ItemDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters itemParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
    Task<ItemDto?> GetItemByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
    Task<ItemDto> UpdateItemAsync(
        Guid companyId,
        Guid id,
        UpdateItemDto updateItemDto,
        CancellationToken ct
    );
    Task<bool> DeleteItemAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
