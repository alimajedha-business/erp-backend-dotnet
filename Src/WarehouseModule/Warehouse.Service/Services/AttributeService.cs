using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class AttributeService(
    IAdvancedFilterBuilder filterBuilder,
    IAttributeRepository attributeRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IAttributeService
{
    private readonly string _key = "Attribute";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IAttributeRepository _attributeRepository = attributeRepository;

    public async Task<AttributeDto> CreateAsync(
        Guid companyId,
        CreateAttributeDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<Domain.Entities.Attribute>(createDto);
        entity.CompanyId = companyId;

        var created = await _attributeRepository.AddAsync(entity, ct);

        await _attributeRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeDto>(created);
    }

    public async Task<AttributeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var attribute = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        return _mapper.Map<AttributeDto>(attribute);
    }

    public async Task<ListResponseModel<AttributeSlimDto>> FilterByQAsync(
        Guid companyId,
        AttributeParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _attributeRepository.FilterByQ(companyId, parameters);
        var res = await _attributeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<AttributeSlimDto>(
            results: _mapper.Map<IReadOnlyList<AttributeSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<AttributeDto>> GetFilteredAsync(
        Guid companyId,
        AttributeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<Domain.Entities.Attribute>(filterNodeDto);
        var query = _attributeRepository.GetFiltered(companyId, advancedFilters);
        var res = await _attributeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<AttributeDto>(
            results: _mapper.Map<IReadOnlyList<AttributeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<AttributeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchAttributeDto> patchDocument,
        CancellationToken ct
    )
    {
        var attribute = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchAttributeDto>(attribute);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, attribute);

        await _attributeRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeDto>(attribute);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var attribute = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        if (attribute.IsStatic)
            throw new DbUpdateBadRequestException(
                "Attribute",
                "IsStatic.Delete"
            );

        _attributeRepository.Remove(attribute);
        await _attributeRepository.SaveChangesAsync(ct);
    }

    public Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    )
    {
        return _attributeRepository.GetNextCodeAsync(companyId, ct);
    }

    private async Task<Domain.Entities.Attribute> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<Domain.Entities.Attribute, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _attributeRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}


