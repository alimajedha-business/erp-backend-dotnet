using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IItemService
{
    Task<ItemDto> CreateAsync(
        Guid companyId,
        Guid categoryId,
        CreateItemDto createItemDto,
        CancellationToken ct
    );

    Task<ItemDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<ItemDto> GetByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    );

    Task<ListResponseModel<ItemDto>> GetAllAsync(
        Guid companyId,
        ItemParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ItemListDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<ItemDto> PatchAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        JsonPatchDocument<PatchItemDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    );
}
