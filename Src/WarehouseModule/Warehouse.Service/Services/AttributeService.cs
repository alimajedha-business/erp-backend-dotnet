using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class AttributeService(
    IAdvancedFilterBuilder filterBuilder,
    IAttributeRepository attributeRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        Domain.Entities.Attribute,
        AttributeDto,
        AttributeParameters,
        IAttributeRepository,
        WarehouseResource
    >(
        filterBuilder,
        attributeRepository,
        companyService,
        mapper,
        localizer
    ),
    IAttributeService
{
    protected override string LocalizerKey => "Attribute";

    public Task<AttributeDto> CreateAttributeAsync(
        Guid companyId,
        CreateAttributeDto createAttributeDto,
        CancellationToken ct
    ) => CreateAsync(companyId, createAttributeDto, ct);

    public Task<ListResponseModel<AttributeDto>> GetAllAttributesAsync(
        Guid companyId,
        AttributeParameters attributeParameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    ) => GetAllAsync(companyId, attributeParameters, ct, filterNodeDto);

    public Task<AttributeDto> GetAttributeByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(companyId, id, ct);

    public Task<AttributeDto> PatchAttributeAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchAttributeDto> patchAttributeDto,
        CancellationToken ct
    ) => PatchAsync(companyId, id, patchAttributeDto, ct);

    public Task DeleteAttributeAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => DeleteAsync(companyId, id, ct);
}
