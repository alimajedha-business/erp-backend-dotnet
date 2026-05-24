using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Service.Contracts;

public interface IJobService
{
    Task<JobDto> CreateAsync(
        Guid companyId,
        CreateJobDto createDto,
        CancellationToken ct
    );

    Task<JobDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<JobDto>> GetFilteredAsync(
        Guid companyId,
        JobParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<ListResponseModel<JobDto>> FilterByQAsync(
        Guid companyId,
        JobParameters parameters,
        CancellationToken ct = default
    );

    Task<JobDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchJobDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
