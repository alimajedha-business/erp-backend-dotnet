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

namespace NGErp.HCM.Service.Services;

public class EducationalStatusService(
    IEducationalStatusRepository educationalStatusRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEducationalStatusService
{
    private readonly string _key = "EducationalStatus";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEducationalStatusRepository _educationalStatusRepository = educationalStatusRepository;

    public async Task<EducationalStatusDto> CreateAsync(
        CreateEducationalStatusDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<EducationalStatus>(createDto);
        var created = await _educationalStatusRepository.AddAsync(entity, ct);

        await _educationalStatusRepository.SaveChangesAsync(ct);
        return _mapper.Map<EducationalStatusDto>(created);
    }

    public async Task<EducationalStatusDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<EducationalStatusDto>(entity);
    }

    public async Task<ListResponseModel<EducationalStatusDto>> GetFilteredAsync(
        EducationalStatusParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<EducationalStatus>(filterNodeDto);
        var query = _educationalStatusRepository.GetFiltered(advancedFilters);
        var res = await _educationalStatusRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EducationalStatusDto>(
            results: _mapper.Map<IReadOnlyList<EducationalStatusDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<EducationalStatusDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEducationalStatusDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        var patchDto = _mapper.Map<PatchEducationalStatusDto>(entity);
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

        await _educationalStatusRepository.SaveChangesAsync(ct);
        return _mapper.Map<EducationalStatusDto>(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        _educationalStatusRepository.Remove(entity);
        await _educationalStatusRepository.SaveChangesAsync(ct);
    }

    private async Task<EducationalStatus> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _educationalStatusRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
