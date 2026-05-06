using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface IAttributeEnumValueService
{
    Task<AttributeEnumValueDto> CreateAsync(
        Guid companyId,
        Guid attributeId,
        CreateAttributeEnumValueDto createDto,
        CancellationToken ct
    );

    Task<ListResponseModel<AttributeEnumValueSlimDto>> FilterByQAsync(
        Guid companyId,
        Guid attributeId,
        AttributeEnumValueParameters parameters,
        CancellationToken ct = default
    );

    Task<ListResponseModel<AttributeEnumValueListDto>> GetFilteredAsync(
        Guid companyId,
        Guid attributeId,
        AttributeEnumValueParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<AttributeEnumValueDto> GetByIdAsync(
        Guid companyId,
        Guid attributeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<AttributeEnumValueDto> PatchAsync(
        Guid companyId,
        Guid attributeId,
        Guid id,
        JsonPatchDocument<PatchAttributeEnumValueDto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid attributeId,
        Guid id,
        CancellationToken ct
    );

    Task<int> GetNextCode(
        Guid companyId,
        Guid attributeId,
        CancellationToken ct
    );
}
