using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IAttributeEnumValueService
{
    Task<AttributeEnumValueDto> CreateAttributeEnumValueAsync(
        Guid companyId,
        CreateAttributeEnumValueDto createAttributeEnumValueDto,
        CancellationToken ct
    );
    Task<ListResponseModel<AttributeEnumValueDto>> GetAttributeAllEnumValuesAsync(
        Guid companyId,
        Guid attributeId,
        AttributeEnumValueParameters attributeEnumValueParameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );
    Task<AttributeEnumValueDto> GetAttributeEnumValueByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
    Task<AttributeEnumValueDto> PatchAttributeEnumValueAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchAttributeEnumValueDto> patchAttributeEnumValueDto,
        CancellationToken ct
    );
    Task DeleteAttributeEnumValueAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
