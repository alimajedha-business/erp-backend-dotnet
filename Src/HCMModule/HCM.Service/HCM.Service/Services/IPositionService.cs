using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;

namespace NGErp.HCM.Service.Services;

public interface IPositionService
{
    Task<PositionDto> CreateAsync(
        Guid companyId,
        CreatePositionDto createDto,
        CancellationToken ct
    );

    Task<PositionDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<PositionDto>> GetAllAsync(
        Guid companyId,
        PositionParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<PositionDto>> GetAllAsync(
        Guid companyId,
        PositionParameters parameters,
        FilterNodeDto filterNodeDto,
        CancellationToken ct = default
    );

    Task<PositionDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchPositionDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        PositionChangeStatusDto changeStatusDto,
        CancellationToken ct
    );
}