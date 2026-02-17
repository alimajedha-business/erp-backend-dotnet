using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IItemService
{
    Task<ItemDto> CreateItemAsync(
        Guid companyId,
        CreateItemDto createDto,
        CancellationToken ct
    );
    Task<ListResponseModel<ItemDto>> GetAllItemsAsync(
        Guid companyId,
        ItemParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );
    Task<ListResponseModel<ItemDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );
    Task<ItemDto> GetItemByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
    Task<ItemDto> PatchItemAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchItemDto> patchDocument,
        CancellationToken ct
    );
    Task DeleteItemAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
