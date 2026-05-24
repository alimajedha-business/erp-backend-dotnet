using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Service.Contracts;

public interface IPositionJobService
{
    Task<PositionJobDto> CreateAsync(
        Guid companyId,
        CreatePositionJobDto createDto,
        CancellationToken ct
    );

    Task<PositionJobDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<PositionJobDto>> GetFilteredAsync(
        Guid companyId,
        PositionJobParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<PositionJobDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchPositionJobDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
