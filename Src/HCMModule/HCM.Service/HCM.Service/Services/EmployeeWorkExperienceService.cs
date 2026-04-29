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

public class EmployeeWorkExperienceService(
    IEmployeeWorkExperienceRepository employeeWorkExperienceRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEmployeeWorkExperienceService
{
    private readonly string _key = "EmployeeWorkExperience";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEmployeeWorkExperienceRepository _employeeWorkExperienceRepository = employeeWorkExperienceRepository;

    public async Task<EmployeeWorkExperienceDto> CreateAsync(
        CreateEmployeeWorkExperienceDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<EmployeeWorkExperience>(createDto);

        await _employeeWorkExperienceRepository.AddAsync(entity, ct);
        await _employeeWorkExperienceRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(entity.Id, trackChanges: false, ct);
           
    }

    public async Task<EmployeeWorkExperienceDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<EmployeeWorkExperienceDto>(entity);
    }

    public async Task<ListResponseModel<EmployeeWorkExperienceDto>> GetFilteredAsync(
        EmployeeWorkExperienceParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<EmployeeWorkExperience>(filterNodeDto);
        var query = _employeeWorkExperienceRepository.GetFiltered(advancedFilters);
        var res = await _employeeWorkExperienceRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EmployeeWorkExperienceDto>(
            results: _mapper.Map<IReadOnlyList<EmployeeWorkExperienceDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<EmployeeWorkExperienceDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEmployeeWorkExperienceDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchEmployeeWorkExperienceDto>(entity);
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

        await _employeeWorkExperienceRepository.SaveChangesAsync(ct);
        return _mapper.Map<EmployeeWorkExperienceDto>(entity);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: true,
            ct
        );

        _employeeWorkExperienceRepository.Remove(entity);
        await _employeeWorkExperienceRepository.SaveChangesAsync(ct);
    }

    private async Task<EmployeeWorkExperience> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _employeeWorkExperienceRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
