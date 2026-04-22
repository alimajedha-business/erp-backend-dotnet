using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IAttributeService
{
    Task<AttributeDto> CreateAsync(
        Guid companyId,
        CreateAttributeDto createDto,
        CancellationToken ct
    );

    Task<AttributeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<AttributeSlimDto>> FilterByQAsync(
        Guid companyId,
        AttributeParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<AttributeDto>> GetFilteredAsync(
        Guid companyId,
        AttributeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<AttributeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchAttributeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(Guid companyId, CancellationToken ct);
}
