using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Resources;
using NGErp.HCM.Service.Service.Contracts;

namespace NGErp.HCM.Service.Services;

public class RelativeTypeService(
    IRelativeTypeRepository relativeTypeRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IRelativeTypeService
{
    private readonly string _key = "RelativeType";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IRelativeTypeRepository _relativeTypeRepository = relativeTypeRepository;

    public async Task<RelativeTypeDto> CreateAsync(
        CreateRelativeTypeDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<RelativeType>(createDto);
        var created = await _relativeTypeRepository.AddAsync(entity, ct);

        await _relativeTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<RelativeTypeDto>(created);
    }

    public async Task<RelativeTypeDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<RelativeTypeDto>(entity);
    }

    public async Task<ListResponseModel<RelativeTypeDto>> GetFilteredAsync(
        RelativeTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<RelativeType>(filterNodeDto);
        var query = _relativeTypeRepository.GetFiltered(advancedFilters);
        var res = await _relativeTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<RelativeTypeDto>(
            results: _mapper.Map<IReadOnlyList<RelativeTypeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<RelativeTypeDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchRelativeTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        var patchDto = _mapper.Map<PatchRelativeTypeDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, entity);

        await _relativeTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<RelativeTypeDto>(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        _relativeTypeRepository.Remove(entity);
        await _relativeTypeRepository.SaveChangesAsync(ct);
    }

    private async Task<RelativeType> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _relativeTypeRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
