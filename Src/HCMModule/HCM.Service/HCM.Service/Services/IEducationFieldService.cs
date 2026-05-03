using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IEducationFieldService
{
    Task<EducationFieldDto> CreateAsync(
        CreateEducationFieldDto createDto,
        CancellationToken ct
    );

    Task<EducationFieldDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<EducationFieldDto>> GetFilteredAsync(
        EducationFieldParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<EducationFieldDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEducationFieldDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
