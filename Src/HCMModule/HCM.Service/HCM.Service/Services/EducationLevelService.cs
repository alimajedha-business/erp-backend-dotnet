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

public class EducationLevelService(
    IEducationLevelRepository educationLevelRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEducationLevelService
{
    private readonly string _key = "EducationLevel";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEducationLevelRepository _educationLevelRepository = educationLevelRepository;

    public async Task<EducationLevelDto> CreateAsync(
        CreateEducationLevelDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<EducationLevel>(createDto);
        var created = await _educationLevelRepository.AddAsync(entity, ct);

        await _educationLevelRepository.SaveChangesAsync(ct);
        return _mapper.Map<EducationLevelDto>(created);
    }

    public async Task<EducationLevelDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<EducationLevelDto>(entity);
    }

    public async Task<ListResponseModel<EducationLevelDto>> GetFilteredAsync(
        EducationLevelParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<EducationLevel>(filterNodeDto);
        var query = _educationLevelRepository.GetFiltered(advancedFilters);
        var res = await _educationLevelRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EducationLevelDto>(
            results: _mapper.Map<IReadOnlyList<EducationLevelDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<EducationLevelDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEducationLevelDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        var patchDto = _mapper.Map<PatchEducationLevelDto>(entity);
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

        await _educationLevelRepository.SaveChangesAsync(ct);
        return _mapper.Map<EducationLevelDto>(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        _educationLevelRepository.Remove(entity);
        await _educationLevelRepository.SaveChangesAsync(ct);
    }

    private async Task<EducationLevel> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _educationLevelRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
