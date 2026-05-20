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

public class EmployeeDependantService(
    IEmployeeDependantRepository employeeDependantRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEmployeeDependantService
{
    private readonly string _key = "EmployeeDependant";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEmployeeDependantRepository _employeeDependantRepository = employeeDependantRepository;

    public async Task<EmployeeDependantDto> CreateAsync(
        CreateEmployeeDependantDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<EmployeeDependant>(createDto);

        await _employeeDependantRepository.AddAsync(entity, ct);
        await _employeeDependantRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(
            entity.Id,
            trackChanges: false,
            ct
        );
    }

    public async Task<EmployeeDependantDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(id, trackChanges, ct);
        return _mapper.Map<EmployeeDependantDto>(entity);
    }

    public async Task<ListResponseModel<EmployeeDependantDto>> GetFilteredAsync(
        Guid employeeId,
        EmployeeDependantParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<EmployeeDependant>(filterNodeDto);
        var query = _employeeDependantRepository.GetFiltered(employeeId, advancedFilters);
        var res = await _employeeDependantRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EmployeeDependantDto>(
            results: _mapper.Map<IReadOnlyList<EmployeeDependantDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<EmployeeDependantDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchEmployeeDependantDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchEmployeeDependantDto>(entity);
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

        await _employeeDependantRepository.SaveChangesAsync(ct);
        return _mapper.Map<EmployeeDependantDto>(entity);
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

        _employeeDependantRepository.Remove(entity);
        await _employeeDependantRepository.SaveChangesAsync(ct);
    }

    private async Task<EmployeeDependant> GetByIdOrThrowAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _employeeDependantRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}

