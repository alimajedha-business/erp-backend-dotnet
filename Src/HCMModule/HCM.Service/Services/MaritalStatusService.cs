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

public class MaritalStatusService(
    IMaritalStatusRepository maritalStatusRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IMaritalStatusService
{
    private readonly string _key = "MaritalStatus";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IMaritalStatusRepository _maritalStatusRepository = maritalStatusRepository;

    public async Task<MaritalStatusDto> CreateAsync(
        CreateMaritalStatusDto createDto,
        CancellationToken ct
    )
    {

        var entity = _mapper.Map<MaritalStatus>(createDto);
        var created = await _maritalStatusRepository.AddAsync(entity, ct);

        await _maritalStatusRepository.SaveChangesAsync(ct);
        return _mapper.Map<MaritalStatusDto>(created);
    }

    public async Task<MaritalStatusDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {

        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<MaritalStatusDto>(entity);
    }

    public async Task<ListResponseModel<MaritalStatusDto>> GetFilteredAsync(
        MaritalStatusParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<MaritalStatus>(filterNodeDto);
        var query = _maritalStatusRepository.GetFiltered(advancedFilters);
        var res = await _maritalStatusRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<MaritalStatusDto>(
            results: _mapper.Map<IReadOnlyList<MaritalStatusDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<MaritalStatusDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchMaritalStatusDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        var patchDto = _mapper.Map<PatchMaritalStatusDto>(entity);
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

        await _maritalStatusRepository.SaveChangesAsync(ct);
        return _mapper.Map<MaritalStatusDto>(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        _maritalStatusRepository.Remove(entity);
        await _maritalStatusRepository.SaveChangesAsync(ct);
    }

    private async Task<MaritalStatus> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _maritalStatusRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
