using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IMaritalStatusService
{
    Task<MaritalStatusDto> CreateAsync(
        CreateMaritalStatusDto createDto,
        CancellationToken ct
    );

    Task<MaritalStatusDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<MaritalStatusDto>> GetFilteredAsync(
        MaritalStatusParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<MaritalStatusDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchMaritalStatusDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
