using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public interface IItemService : IBaseServiceWithCompany<
    Item,
    ItemDto,
    ItemListDto,
    ItemParameters,
    IItemRepository,
    WarehouseResource
>
{
    Task<ItemDto> CreateAsync(
        Guid companyId,
        Guid categoryId,
        CreateItemDto createItemDto,
        CancellationToken ct
    );

    Task<ListResponseModel<ItemListDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );

    Task<ItemDto> GetByIdAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    );
}
