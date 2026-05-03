using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IMaritalStatusService
{
    Task<MaritalStatusDto> CreateAsync(
        Guid companyId,
        CreateMaritalStatusDto createDto,
        CancellationToken ct
    );

    Task<MaritalStatusDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<MaritalStatusDto>> GetFilteredAsync(
        Guid companyId,
        MaritalStatusParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<MaritalStatusDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchMaritalStatusDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
