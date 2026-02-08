using AutoMapper;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class AttributeService(
    IAttributeRepository attributeRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IAttributeService
{
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;

    public async Task<AttributeDto> CreateAttributeAsync(
        Guid companyId,
        CreateAttributeDto createAttributeDto,
        CancellationToken ct
    )
    {
        var category = _mapper.Map<Domain.Entities.Attribute>(createAttributeDto);
        category.CompanyId = companyId;

        var createdCategory = await _attributeRepository.AddAsync(category, ct);
        await _attributeRepository.SaveChangesAsync(ct);

        return _mapper.Map<AttributeDto>(createdCategory);
    }

    public async Task<ListResponseModel<AttributeDto>> GetAllAttributesAsync(
        Guid companyId,
        AttributeParameters attributeParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        var listQueryResult = await _attributeRepository.GetAllAsync(
            companyId,
            attributeParameters,
            ct,
            requestAdvancedFilters
        );

        return new ListResponseModel<AttributeDto>(
            items: _mapper.Map<IReadOnlyList<AttributeDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            attributeParameters
        );
    }

    public async Task<AttributeDto> GetAttributeByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var attribute = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        return _mapper.Map<AttributeDto>(attribute);
    }

    public async Task<AttributeDto> UpdateAttributeAsync(
        Guid companyId,
        Guid id,
        UpdateAttributeDto updateAttributeDto,
        CancellationToken ct
    )
    {
        var attribute = await GetByIdOrThrowExceptionAsync(
            companyId,
            id,
            ct,
            trackChanges: true
        );

        _mapper.Map(updateAttributeDto, attribute);
        await _attributeRepository.SaveChangesAsync(ct);

        return _mapper.Map<AttributeDto>(attribute);
    }

    public async Task<bool> DeleteAttributeAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var attribute = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        _attributeRepository.Remove(attribute);

        try
        {
            await _attributeRepository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new ForeignKeyViolationException(_localizer["Attribute"].Value);
        }

        return true;
    }

    private async Task<Domain.Entities.Attribute> GetByIdOrThrowExceptionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var attribute = await _attributeRepository.GetByIdAsync(
            companyId,
            id,
            ct,
            trackChanges
        );

        return attribute ?? throw new NotFoundException(_localizer["Attribute"].Value);
    }
}
