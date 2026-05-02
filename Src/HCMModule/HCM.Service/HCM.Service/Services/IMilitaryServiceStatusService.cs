using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IMilitaryServiceStatusService
{
    Task<MilitaryServiceStatusDto> CreateAsync(
        Guid companyId,
        CreateMilitaryServiceStatusDto createDto,
        CancellationToken ct
    );

    Task<MilitaryServiceStatusDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<MilitaryServiceStatusDto>> GetFilteredAsync(
        Guid companyId,
        MilitaryServiceStatusParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<MilitaryServiceStatusDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchMilitaryServiceStatusDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
