using AutoMapper;
using Microsoft.Extensions.Localization;
using NGErp.Base.Service.Services;
using NGErp.Base.Domain.Exceptions;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.Resources;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using NGErp.Base.Service.ResponseModels;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.Base.Service.DTOs;

namespace NGErp.HCM.Service.Services;
public class EmployeeService(
    IEmployeeRepository employeeRepository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IEmployeeService
{
    private readonly string _key = "Employee";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<EmployeeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges,
        CancellationToken ct
        )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<EmployeeDto>(entity);

    }

    private async Task<Employee> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _employeeRepository.GetByIdAsync(companyId, id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }

    public virtual async Task<EmployeeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchEmployeeDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchEmployeeDto>(entity);
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

        await _employeeRepository.SaveChangesAsync(ct);
        return _mapper.Map<EmployeeDto>(entity);
    }

    public async Task<EmployeeDto> CreateAsync(
        Guid companyId,
        CreateEmployeeDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<Employee>(createDto);
        entity.CompanyId = companyId;

        await _employeeRepository.AddAsync(entity, ct);
        await _employeeRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(
            companyId,
            entity.Id,
            trackChanges: false,
            ct
        );
    }

    public async Task<ListResponseModel<EmployeeDto>> GetFilteredAsync(
        Guid companyId,
        EmployeeParamaters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<Employee>(filterNodeDto);
        var query = _employeeRepository.GetFiltered(companyId, advancedFilters);
        var res = await _employeeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<EmployeeDto>(
            results: _mapper.Map<IReadOnlyList<EmployeeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
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

        _employeeRepository.Remove(entity);
        await _employeeRepository.SaveChangesAsync(ct);
    }


}

