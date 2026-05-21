using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Service.Contracts;

public interface IMilitaryServiceStatusService
{
    Task<MilitaryServiceStatusDto> CreateAsync(
        CreateMilitaryServiceStatusDto createDto,
        CancellationToken ct
    );

    Task<MilitaryServiceStatusDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<MilitaryServiceStatusDto>> GetFilteredAsync(
        MilitaryServiceStatusParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<MilitaryServiceStatusDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchMilitaryServiceStatusDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
