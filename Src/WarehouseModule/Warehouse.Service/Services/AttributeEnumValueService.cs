using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class AttributeEnumValueService(
    IAdvancedFilterBuilder filterBuilder,
    IAttributeEnumValueRepository enumValueRepository,
    IAttributeService attributeService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IAttributeEnumValueService
{
    private readonly string _key = "AttributeEnumValue";

    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAttributeEnumValueRepository _enumValueRepository = enumValueRepository;
    private readonly IAttributeService _attributeService = attributeService;

    public async Task<AttributeEnumValueDto> CreateAsync(
        Guid companyId,
        Guid attributeId,
        CreateAttributeEnumValueDto createDto,
        CancellationToken ct
    )
    {
        await CheckRouteRelationsOrThrowAsync(companyId, attributeId, ct);

        var entity = _mapper.Map<AttributeEnumValue>(createDto);
        entity.AttributeId = attributeId;

        var created = await _enumValueRepository.AddAsync(entity, ct);

        await _enumValueRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeEnumValueDto>(created);
    }

    public async Task<AttributeEnumValueDto> GetByIdAsync(
        Guid companyId,
        Guid attributeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        await CheckRouteRelationsOrThrowAsync(companyId, attributeId, ct);

        var enumValue = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p =>
                p.AttributeId == attributeId &&
                p.Id == id,
            ct
        );

        return _mapper.Map<AttributeEnumValueDto>(enumValue);
    }

    public async Task<ListResponseModel<AttributeEnumValueSlimDto>> FilterByQAsync(
        Guid companyId,
        Guid attributeId,
        AttributeEnumValueParameters parameters,
        CancellationToken ct = default
    )
    {
        await CheckRouteRelationsOrThrowAsync(companyId, attributeId, ct);

        var query = _enumValueRepository
            .FilterByQ(parameters)
            .Where(e => e.AttributeId == attributeId);

        var res = await _enumValueRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<AttributeEnumValueSlimDto>(
            results: _mapper.Map<IReadOnlyList<AttributeEnumValueSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<AttributeEnumValueListDto>> GetFilteredAsync(
        Guid companyId,
        Guid attributeId,
        AttributeEnumValueParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        await CheckRouteRelationsOrThrowAsync(companyId, attributeId, ct);

        var advancedFilters = _filterBuilder.Build<AttributeEnumValue>(filterNodeDto);
        var query = _enumValueRepository.GetFiltered(attributeId, advancedFilters);
        var res = await _enumValueRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<AttributeEnumValueListDto>(
            results: _mapper.Map<IReadOnlyList<AttributeEnumValueListDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<AttributeEnumValueDto> PatchAsync(
        Guid companyId,
        Guid attributeId,
        Guid id,
        JsonPatchDocument<PatchAttributeEnumValueDto> patchDocument,
        CancellationToken ct
    )
    {
        await CheckRouteRelationsOrThrowAsync(companyId, attributeId, ct);

        var enumValue = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.AttributeId == attributeId && p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchAttributeEnumValueDto>(enumValue);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, enumValue);

        await _enumValueRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeEnumValueDto>(enumValue);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid attributeId,
        Guid id,
        CancellationToken ct
    )
    {
        await CheckRouteRelationsOrThrowAsync(companyId, attributeId, ct);
        await _enumValueRepository.Remove(e =>
            e.AttributeId == attributeId &&
            e.Id == id,
            ct
        );
    }

    public async Task<int> GetNextCode(
        Guid companyId,
        Guid attributeId,
        CancellationToken ct
    )
    {
        await CheckRouteRelationsOrThrowAsync(companyId, attributeId, ct);
        return await _enumValueRepository.GetNextCodeAsync(attributeId, ct);
    }

    private async Task<AttributeEnumValue> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<AttributeEnumValue, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _enumValueRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }

    private async Task CheckRouteRelationsOrThrowAsync(
        Guid companyId,
        Guid attributeId,
        CancellationToken ct
    )
    {
        await _attributeService.GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p =>
                 p.CompanyId == companyId &&
                 p.Id == attributeId,
             ct
        );
    }
}


