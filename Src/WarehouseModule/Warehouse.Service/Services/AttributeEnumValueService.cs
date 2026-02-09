using AutoMapper;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.DTOs;
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
) : IAttributeEnumValueService
{
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IAttributeEnumValueRepository _attributeEnumValueRepository = attributeEnumValueRepository;
    private readonly ICompanyService _companyService = companyService;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;

    public async Task<AttributeEnumValueDto> CreateAttributeEnumValueAsync(
        Guid companyId,
        CreateAttributeEnumValueDto createAttributeEnumValueDto,
        CancellationToken ct
    )
    {
        await GetCompanyByIdOrThrowExceptionAsync(companyId, ct);

        var enumValue = _mapper.Map<AttributeEnumValue>(createAttributeEnumValueDto);
        enumValue.CompanyId = companyId;

        var createdEnumValue = _attributeEnumValueRepository.AddAsync(enumValue, ct);
        await _attributeEnumValueRepository.SaveChangesAsync(ct);

        return _mapper.Map<AttributeEnumValueDto>(createdEnumValue);
    }

    public async Task<ListResponseModel<AttributeEnumValueDto>> GetAttributeAllEnumValuesAsync(
        Guid companyId,
        Guid attributeId,
        AttributeEnumValueParameters attributeEnumValueParameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await GetCompanyByIdOrThrowExceptionAsync(companyId, ct);

        var advancedFilters = _filterBuilder
            .Build<AttributeEnumValue>(filterNodeDto);

        var listQueryResult = await _attributeEnumValueRepository.GetAllAsync(
            companyId,
            attributeId,
            attributeEnumValueParameters,
            ct,
            advancedFilters
        );

        return new ListResponseModel<AttributeEnumValueDto>(
            items: _mapper.Map<IReadOnlyList<AttributeEnumValueDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            attributeEnumValueParameters
        );
    }

    public async Task<AttributeEnumValueDto> GetAttributeEnumValueByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await GetCompanyByIdOrThrowExceptionAsync(companyId, ct);

        var attributeEnumValue = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        return _mapper.Map<AttributeEnumValueDto>(attributeEnumValue);
    }

    public Task<AttributeEnumValueDto> UpdateAttributeEnumValueAsync(
        Guid companyId,
        Guid id,
        UpdateAttributeEnumValueDto updateAttributeEnumValueDto,
        CancellationToken ct
    )
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAttributeEnumValueAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await GetCompanyByIdOrThrowExceptionAsync(companyId, ct);

        var attributeEnumValue = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        _attributeEnumValueRepository.Remove(attributeEnumValue);

        try
        {
            await _attributeEnumValueRepository.SaveChangesAsync(ct);
        }
        catch(DbUpdateException ex)
        when(ex.InnerException is SqlException { Number: 547 })
        {
            throw new ForeignKeyViolationException(_localizer["AttributeEnumValue"].Value);
        }

        return true;
    }

    private async Task<AttributeEnumValue> GetByIdOrThrowExceptionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        await GetCompanyByIdOrThrowExceptionAsync(companyId, ct);

        var attributeEnumValue = await _attributeEnumValueRepository.GetByIdAsync(
            companyId,
            id,
            ct,
            trackChanges
        );

        return attributeEnumValue ?? 
            throw new NotFoundException(_localizer["AttributeEnumValue"].Value);
    }

    private async Task<CompanyDto> GetCompanyByIdOrThrowExceptionAsync(
        Guid companyId,
        CancellationToken ct
    )
    {
        var company = await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        return company ?? throw new NotFoundException(_localizer["Company"].Value);
    }
}
