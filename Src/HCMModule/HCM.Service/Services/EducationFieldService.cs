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

public class EducationFieldService(
    IEducationFieldRepository educationFieldRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEducationFieldService
{
    private readonly string _key = "EducationField";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEducationFieldRepository _educationFieldRepository = educationFieldRepository;

    public async Task<EducationFieldDto> CreateAsync(
        CreateEducationFieldDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<EducationField>(createDto);
        var created = await _educationFieldRepository.AddAsync(entity, ct);

        await _educationFieldRepository.SaveChangesAsync(ct);
        return _mapper.Map<EducationFieldDto>(created);
    }

    public async Task<EducationFieldDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<EducationFieldDto>(entity);
    }

    public async Task<ListResponseModel<EducationFieldDto>> GetFilteredAsync(
        EducationFieldParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<EducationField>(filterNodeDto);
        var query = _educationFieldRepository.GetFiltered(advancedFilters);
        var res = await _educationFieldRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EducationFieldDto>(
            results: _mapper.Map<IReadOnlyList<EducationFieldDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<EducationFieldDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEducationFieldDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        var patchDto = _mapper.Map<PatchEducationFieldDto>(entity);
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

        await _educationFieldRepository.SaveChangesAsync(ct);
        return _mapper.Map<EducationFieldDto>(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges: true, ct);
        _educationFieldRepository.Remove(entity);
        await _educationFieldRepository.SaveChangesAsync(ct);
    }

    private async Task<EducationField> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _educationFieldRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
