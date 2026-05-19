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

public class EmployeeRelativeService(
    IEmployeeRelativeRepository employeeRelativeRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEmployeeRelativeService
{
    private readonly string _key = "EmployeeRelative";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEmployeeRelativeRepository _employeeRelativeRepository = employeeRelativeRepository;

    public async Task<EmployeeRelativeDto> CreateAsync(
        Guid employeeId,
        CreateEmployeeRelativeDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<EmployeeRelative>(createDto);
        entity.EmployeeId = employeeId;

        await _employeeRelativeRepository.AddAsync(entity, ct);
        await _employeeRelativeRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(
            employeeId,
            entity.Id,
            trackChanges: false,
            ct
        ); ;
    }

    public async Task<EmployeeRelativeDto> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(employeeId, id, trackChanges, ct);
        return _mapper.Map<EmployeeRelativeDto>(entity);
    }

    public async Task<ListResponseModel<EmployeeRelativeDto>> GetFilteredAsync(
        Guid employeeId,
        EmployeeRelativeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<EmployeeRelative>(filterNodeDto);
        var query = _employeeRelativeRepository.GetFiltered(employeeId, advancedFilters);
        var res = await _employeeRelativeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EmployeeRelativeDto>(
            results: _mapper.Map<IReadOnlyList<EmployeeRelativeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<EmployeeRelativeDto> PatchAsync(
        Guid employeeId,
        Guid id,
        JsonPatchDocument<PatchEmployeeRelativeDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            employeeId,
            id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchEmployeeRelativeDto>(entity);
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

        await _employeeRelativeRepository.SaveChangesAsync(ct);
        return _mapper.Map<EmployeeRelativeDto>(entity);
    }

    public async Task DeleteAsync(
        Guid employeeId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            employeeId,
            id,
            trackChanges: true,
            ct
        );

        _employeeRelativeRepository.Remove(entity);
        await _employeeRelativeRepository.SaveChangesAsync(ct);
    }

    private async Task<EmployeeRelative> GetByIdOrThrowAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _employeeRelativeRepository.GetByIdAsync(employeeId, id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
