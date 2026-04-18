using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IShippingCompanyService
{
    Task<ShippingCompanyDto> CreateAsync(
        CreateShippingCompanyDto createDto,
        CancellationToken ct
    );

    Task<ShippingCompanyDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ShippingCompanyDto>> FilterByQAsync(
        ShippingCompanyParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<ShippingCompanyDto>> GetFilteredAsync(
        ShippingCompanyParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<ShippingCompanyDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchShippingCompanyDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(CancellationToken ct);
}
