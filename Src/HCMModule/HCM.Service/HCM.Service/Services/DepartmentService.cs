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

public class DepartmentService(
    IDepartmentRepository departmentRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IDepartmentService
{
    private readonly string _key = "Department";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IDepartmentRepository _departmentRepository = departmentRepository;

    public async Task<DepartmentDto> CreateAsync(
        Guid companyId,
        CreateDepartmentDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<Department>(createDto);
        entity.CompanyId = companyId;

        var created = await _departmentRepository.AddAsync(entity, ct);

        await _departmentRepository.SaveChangesAsync(ct);
        return _mapper.Map<DepartmentDto>(created);
    }

    public async Task<DepartmentDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<DepartmentDto>(entity);
    }

    public async Task<ListResponseModel<DepartmentDto>> GetAllAsync(
        Guid companyId,
        DepartmentParameters parameters,
        CancellationToken ct = default
    )
    {
        // TODO: add specification if needed
        var listQueryResult = await _departmentRepository.GetAllAsync(
            companyId,
            parameters,
            spec: null,
            ct
        );

        return new ListResponseModel<DepartmentDto>(
            results: _mapper.Map<IReadOnlyList<DepartmentDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public async Task<ListResponseModel<DepartmentDto>> GetAllAsync(
        Guid companyId,
        DepartmentParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        // TODO: add specification if needed
        var advancedFilters = _filterBuilder.Build<Department>(filterNodeDto);
        var listQueryResult = await _departmentRepository.GetAllAsync(
            parameters,
            advancedFilters,
            spec: null,
            ct
        );

        return new ListResponseModel<DepartmentDto>(
            results: _mapper.Map<IReadOnlyList<DepartmentDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            parameters
        );
    }

    public virtual async Task<DepartmentDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchDepartmentDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchDepartmentDto>(entity);
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

        await _departmentRepository.SaveChangesAsync(ct);
        return _mapper.Map<DepartmentDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        _departmentRepository.Remove(entity);
        await _departmentRepository.SaveChangesAsync(ct);
    }

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        DepartmentChangeStatusDto changeStatusDto,
        CancellationToken ct)
    {
        // TODO: ensure company
        var department = await GetByIdOrThrowAsync(companyId, id, trackChanges: true, ct);
        department.ChangeStatus(changeStatusDto.Status,
            new DateTime((DateOnly)changeStatusDto.Date, TimeOnly.MinValue)
        );
        _departmentRepository.Update(department);
        await _departmentRepository.SaveChangesAsync(ct);
    }

    private async Task<Department> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        // TODO: add specification if needed
        var entity = await _departmentRepository.GetByIdAsync(companyId, id, trackChanges, spec: null, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}