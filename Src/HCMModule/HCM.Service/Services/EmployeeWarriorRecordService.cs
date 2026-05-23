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

public class EmployeeWarriorRecordService(
    IEmployeeWarriorRecordRepository employeeWarriorRecordRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEmployeeWarriorRecordService
{
    private readonly string _key = "EmployeeWarriorRecord";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEmployeeWarriorRecordRepository _employeeWarriorRecordRepository = employeeWarriorRecordRepository;

    public async Task<EmployeeWarriorRecordDto> CreateAsync(
        Guid employeeId,
        CreateEmployeeWarriorRecordDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<EmployeeWarriorRecord>(createDto);
        entity.EmployeeId = employeeId;

        await _employeeWarriorRecordRepository.AddAsync(entity, ct);
        await _employeeWarriorRecordRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(
         employeeId,
         entity.Id,
         trackChanges: false,
         ct
     );
    }

    public async Task<EmployeeWarriorRecordDto> GetByIdAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(employeeId, id, trackChanges, ct);
        return _mapper.Map<EmployeeWarriorRecordDto>(entity);
    }

    public async Task<ListResponseModel<EmployeeWarriorRecordDto>> GetFilteredAsync(
        Guid employeeId,
        EmployeeWarriorRecordParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<EmployeeWarriorRecord>(filterNodeDto);
        var query = _employeeWarriorRecordRepository.GetFiltered(employeeId, advancedFilters);
        var res = await _employeeWarriorRecordRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EmployeeWarriorRecordDto>(
            results: _mapper.Map<IReadOnlyList<EmployeeWarriorRecordDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<EmployeeWarriorRecordDto> PatchAsync(
        Guid employeeId,
        Guid id,
        JsonPatchDocument<PatchEmployeeWarriorRecordDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            employeeId,
            id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchEmployeeWarriorRecordDto>(entity);
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

        await _employeeWarriorRecordRepository.SaveChangesAsync(ct);
        return _mapper.Map<EmployeeWarriorRecordDto>(entity);
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

        _employeeWarriorRecordRepository.Remove(entity);
        await _employeeWarriorRecordRepository.SaveChangesAsync(ct);
    }

    private async Task<EmployeeWarriorRecord> GetByIdOrThrowAsync(
        Guid employeeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _employeeWarriorRecordRepository.GetByIdAsync(employeeId, id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}

