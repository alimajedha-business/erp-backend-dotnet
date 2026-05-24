using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

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
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<FeatureSettingsDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchFeatureSettingsDto> patchDocument,
        CancellationToken ct
    );
}
