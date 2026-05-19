using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IRemittanceService
{
    Task<RemittanceDto> CreateAsync(
        Guid companyId,
        CreateRemittanceDto createDto,
        CancellationToken ct
    );

    Task<RemittanceDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<RemittanceListDto>> GetFilteredAsync(
        Guid companyId,
        RemittanceParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<RemittanceDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchRemittanceDto> patchDocument,
        CancellationToken ct
    );

    Task<RemittanceDto> PostAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextNumber(Guid companyId, CancellationToken ct);
}
