using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Service.Contracts;

public interface IEducationLevelService
{
    Task<EducationLevelDto> CreateAsync(
        CreateEducationLevelDto createDto,
        CancellationToken ct
    );

    Task<EducationLevelDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EducationLevelDto>> GetFilteredAsync(
        EducationLevelParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EducationLevelDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEducationLevelDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
