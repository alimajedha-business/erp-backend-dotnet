using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public interface IAttributeService
{
    Task<AttributeDto> CreateAttributeAsync(
        Guid companyId,
        CreateAttributeDto createAttributeDto,
        CancellationToken ct
    );
    Task<ListResponseModel<AttributeDto>> GetAllAttributesAsync(
        Guid companyId,
        AttributeParameters attributeParameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    );
    Task<AttributeDto> GetAttributeByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
    Task<AttributeDto> PatchAttributeAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchAttributeDto> patchAttributeDto,
        CancellationToken ct
    );
    Task<bool> DeleteAttributeAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );
}
