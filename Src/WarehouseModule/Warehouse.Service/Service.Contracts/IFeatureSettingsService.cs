using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IFeatureSettingsService
{
    Task<FeatureSettingsDto> CreateAsync(
        Guid companyId,
        CreateFeatureSettingsDto createDto,
        CancellationToken ct
    );

    Task<FeatureSettingsDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<FeatureSettingsDto>> GetFilteredAsync(
        Guid companyId,
        FeatureSettingsParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<FeatureSettingsDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchFeatureSettingsDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
