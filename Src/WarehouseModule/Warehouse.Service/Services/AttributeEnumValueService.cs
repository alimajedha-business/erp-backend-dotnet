using AutoMapper;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class AttributeEnumValueService(
    IAdvancedFilterBuilder filterBuilder,
    IAttributeEnumValueRepository attributeEnumValueRepository,
    IMapper mapper,
    IValidator<AttributeEnumValue> validator,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        AttributeEnumValue,
        AttributeEnumValueDto,
        AttributeEnumValueListDto,
        AttributeEnumValueParameters,
        IAttributeEnumValueRepository,
        WarehouseResource
    >(
        filterBuilder,
        attributeEnumValueRepository,
        mapper,
        validator,
        localizer
    ),
    IAttributeEnumValueService
{
    protected override string LocalizerKey => "AttributeEnumValue";

    public Task<ListResponseModel<AttributeEnumValueListDto>> GetAllAsync(
        Guid attributeId,
        AttributeEnumValueParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        return GetByConditionAsync(
            parameters,
            e => e.AttributeId == attributeId,
            includeQuery,
            ct,
            filterNodeDto
        );
    }

    public override Task<AttributeEnumValueDto> GetByIdAsync(
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(id, includeQuery, ct);

    private static IQueryable<AttributeEnumValue> includeQuery(
        IQueryable<AttributeEnumValue> q
    ) => q.Include(c => c.Attribute);
}
