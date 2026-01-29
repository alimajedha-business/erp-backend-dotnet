using NGErp.Base.Service.RequestFeatures;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IItemService
{
    Task<ItemDto> CreateAsync(
        CreateItemDto item,
        CancellationToken ct
    );
    Task<IEnumerable<ItemDto>> GetListAsync(
        ItemParameters itemParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
    Task<ItemDto?> GetByIdAsync(Guid id);
    Task<ItemDto> UpdateAsync(
        Guid id,
        UpdateItemDto updateItemDto,
        CancellationToken ct
    );
    Task<bool> DeleteAsync(Guid id);
}
