using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Service.Contracts;

public interface IWorkLocationService
{
    Task<WorkLocationDto> CreateAsync(
        Guid companyId,
        CreateWorkLocationDto createDto,
        CancellationToken ct
    );

    Task<WorkLocationDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<WorkLocationDto>> GetFilteredAsync(
        Guid companyId,
        WorkLocationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<WorkLocationDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchWorkLocationDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
