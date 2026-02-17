using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class AttributeEnumValueService(
    IAdvancedFilterBuilder filterBuilder,
    IAttributeEnumValueRepository attributeEnumValueRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        AttributeEnumValue,
        AttributeEnumValueDto,
        AttributeEnumValueParameters,
        IAttributeEnumValueRepository,
        WarehouseResource
    >(
        filterBuilder,
        attributeEnumValueRepository,
        companyService,
        mapper,
        localizer
    ),
    IAttributeEnumValueService
{
    protected override string LocalizerKey => "AttributeEnumValue";

    public Task<AttributeEnumValueDto> CreateAttributeEnumValueAsync(
        Guid companyId,
        CreateAttributeEnumValueDto createAttributeEnumValueDto,
        CancellationToken ct
    )
    {
        return CreateAsync(companyId, createAttributeEnumValueDto, ct);
    }

    public Task<ListResponseModel<AttributeEnumValueDto>> GetAttributeAllEnumValuesAsync(
        Guid companyId,
        Guid attributeId,
        AttributeEnumValueParameters attributeEnumValueParameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        return GetAllAsync(
            companyId,
            attributeEnumValueParameters,
            ct,
            filterNodeDto
        );
    }

    public Task<AttributeEnumValueDto> GetAttributeEnumValueByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        return GetByIdAsync(companyId, id, ct);
    }

    public Task<AttributeEnumValueDto> PatchAttributeEnumValueAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchAttributeEnumValueDto> patchDoc,
        CancellationToken ct
    )
    {
        return PatchAsync(companyId, id, patchDoc, ct);
    }

    public Task DeleteAttributeEnumValueAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
       return DeleteAsync(companyId, id, ct);
    }
}
