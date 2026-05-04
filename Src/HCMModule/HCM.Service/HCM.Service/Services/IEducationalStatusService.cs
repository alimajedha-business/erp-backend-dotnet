using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IEducationalStatusService
{
    Task<EducationalStatusDto> CreateAsync(
        CreateEducationalStatusDto createDto,
        CancellationToken ct
    );

    Task<EducationalStatusDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EducationalStatusDto>> GetFilteredAsync(
        EducationalStatusParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EducationalStatusDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEducationalStatusDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
