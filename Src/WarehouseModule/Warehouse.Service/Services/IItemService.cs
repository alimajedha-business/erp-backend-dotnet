using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IItemService
{
    Task<ItemDto> CreateAsync(
        Guid companyId,
        CreateItemDto item,
        CancellationToken ct
    );
    Task<ListResponseModel<ItemDto>> GetListAsync(
        Guid companyId,
        ItemParameters itemParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    );
    Task<ItemDto?> GetByIdAsync(Guid companyId, Guid id);
    Task<ItemDto> UpdateAsync(
        Guid companyId,
        Guid id,
        UpdateItemDto updateItemDto,
        CancellationToken ct
    );
    Task<bool> DeleteAsync(Guid companyId, Guid id);
}
