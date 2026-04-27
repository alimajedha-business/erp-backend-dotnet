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

public class EmployeeEducationService(
    IEmployeeEducationRepository employeeEducationRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEmployeeEducationService
{
    private readonly string _key = "EmployeeEducation";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEmployeeEducationRepository _employeeEducationRepository = employeeEducationRepository;

    public async Task<EmployeeEducationDto> CreateAsync(
        CreateEmployeeEducationDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<EmployeeEducation>(createDto);

        await _employeeEducationRepository.AddAsync(entity, ct);
        await _employeeEducationRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(
                entity.Id,
                trackChanges: false,
                ct
            );
}

    public async Task<EmployeeEducationDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<EmployeeEducationDto>(entity);
    }

    public async Task<ListResponseModel<EmployeeEducationDto>> GetFilteredAsync(
        EmployeeEducationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<EmployeeEducation>(filterNodeDto);
        var query = _employeeEducationRepository.GetFiltered(advancedFilters);
        var res = await _employeeEducationRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EmployeeEducationDto>(
            results: _mapper.Map<IReadOnlyList<EmployeeEducationDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<EmployeeEducationDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEmployeeEducationDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchEmployeeEducationDto>(entity);
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

        await _employeeEducationRepository.SaveChangesAsync(ct);
        return _mapper.Map<EmployeeEducationDto>(entity);
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

        _employeeEducationRepository.Remove(entity);
        await _employeeEducationRepository.SaveChangesAsync(ct);
    }

    private async Task<EmployeeEducation> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _employeeEducationRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
