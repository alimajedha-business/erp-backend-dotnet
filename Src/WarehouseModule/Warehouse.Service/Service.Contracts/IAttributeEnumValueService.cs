using Microsoft.AspNetCore.JsonPatch;

using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IAttributeEnumValueService
{
    Task<AttributeEnumValueDto> CreateAsync(
        Guid attributeId,
        CreateAttributeEnumValueDto createDto,
        CancellationToken ct
    );

    Task<AttributeEnumValueDto> GetByIdAsync(
        Guid attributeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );
    Task<AttributeEnumValueDto> PatchAsync(
        Guid attributeId,
        Guid id,
        JsonPatchDocument<PatchAttributeEnumValueDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid attributeId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(
        Guid attributeId,
        CancellationToken ct
    );
}
