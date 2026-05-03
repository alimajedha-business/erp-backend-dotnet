using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IRelativeTypeService
{
    Task<RelativeTypeDto> CreateAsync(
        CreateRelativeTypeDto createDto,
        CancellationToken ct
    );

    Task<RelativeTypeDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<RelativeTypeDto>> GetFilteredAsync(
        RelativeTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<RelativeTypeDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchRelativeTypeDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid id,
        CancellationToken ct
    );
}
