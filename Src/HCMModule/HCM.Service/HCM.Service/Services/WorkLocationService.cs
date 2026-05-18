using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.HCM.Service.Resources;

namespace NGErp.HCM.Service.Services;

public class WorkLocationService(
    IWorkLocationRepository workLocationRepository,
    IWorkLocationBusinessRuleValidator businessRuleValidator,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : IWorkLocationService
{
    private readonly string _key = "WorkLocation";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IWorkLocationRepository _workLocationRepository = workLocationRepository;
    private readonly IWorkLocationBusinessRuleValidator _businessRuleValidator = businessRuleValidator;


    public async Task<WorkLocationDto> CreateAsync(
        Guid companyId,
        CreateWorkLocationDto createDto,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);
        var entity = _mapper.Map<WorkLocation>(createDto);
        entity.CompanyId = companyId;

        await _workLocationRepository.AddAsync(entity, ct);
        await _workLocationRepository.SaveChangesAsync(ct);

        return  await GetByIdAsync(
                companyId,
                entity.Id,
                trackChanges: false,
                ct
            ) ; 
    }

    public async Task<WorkLocationDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<WorkLocationDto>(entity);
    }

    public async Task<ListResponseModel<WorkLocationDto>> GetFilteredAsync(
        Guid companyId,
        WorkLocationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<WorkLocation>(filterNodeDto);
        var query = _workLocationRepository.GetFiltered(companyId, advancedFilters);

        query = query.Where(e => e.ParentId == parameters.ParentId);

        var res = await _workLocationRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<WorkLocationDto>(
            results: _mapper.Map<IReadOnlyList<WorkLocationDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<WorkLocationDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchWorkLocationDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchWorkLocationDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        await _businessRuleValidator.ValidatePatchAsync(companyId, id, patchDto, ct);

        _mapper.Map(patchDto, entity);

        await _workLocationRepository.SaveChangesAsync(ct);
        return _mapper.Map<WorkLocationDto>(entity);
    }

    public async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, id, ct);

        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        _workLocationRepository.Remove(entity);
        await _workLocationRepository.SaveChangesAsync(ct);
    }

    private async Task<WorkLocation> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _workLocationRepository.GetByIdAsync(companyId ,id, trackChanges:false, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
