using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Domain.Exceptions;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.HCM.Service.Resources;
using NGErp.HCM.Service.Service.Contracts;

namespace NGErp.HCM.Service.Services;

public class JobService(
    IJobRepository jobRepository,
    IJobBusinessRuleValidator businessRuleValidator,
    IMapper mapper,
    IAdvancedFilterBuilder filterBuilder
) : IJobService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IJobRepository _jobRepository = jobRepository;
    private readonly IJobBusinessRuleValidator _businessRuleValidator = businessRuleValidator;


    public async Task<JobDto> CreateAsync(
        Guid companyId,
        CreateJobDto createDto,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<Job>(createDto);
        entity.CompanyId = companyId;

        await _jobRepository.AddAsync(entity, ct);
        await _jobRepository.SaveChangesAsync(ct);

        return await GetByIdAsync(
                companyId,
                entity.Id,
                trackChanges: false,
                ct
            );
    }

    public async Task<JobDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<JobDto>(entity);
    }

    public async Task<ListResponseModel<JobDto>> GetFilteredAsync(
        Guid companyId,
        JobParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);
        var advancedFilters = _filterBuilder.Build<Job>(filterNodeDto);
        var query = _jobRepository.GetFiltered(companyId, advancedFilters);
        var res = await _jobRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<JobDto>(
            results: _mapper.Map<IReadOnlyList<JobDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<JobDto>> FilterByQAsync(
        Guid companyId,
        JobParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _jobRepository.FilterByQ(companyId, parameters);
        var res = await _jobRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<JobDto>(
            results: _mapper.Map<IReadOnlyList<JobDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<JobDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchJobDto> patchDocument,
        CancellationToken ct
    )
    {
        var codePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchJobDto.Code)
        );

        var titlePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchJobDto.Title)
        );

        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchJobDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        if (codePatched && patchDto.Code is not null)
        {
            await _businessRuleValidator.ValidateJobCodeUniquenessAsync(
                companyId,
                excludedJobId: id,
                patchDto.Code!,
                ct
            );
        }

        if (titlePatched && patchDto.Title is not null)
        {
            await _businessRuleValidator.ValidateTitleUniquenessAsync(
                companyId,
                excludedJobId: id,
                patchDto.Title!,
                ct
            );
        }

        _mapper.Map(patchDto, entity);

        await _jobRepository.SaveChangesAsync(ct);
        return _mapper.Map<JobDto>(entity);
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

        _jobRepository.Remove(entity);
        await _jobRepository.SaveChangesAsync(ct);
    }

    private async Task<Job> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _jobRepository.GetByIdAsync(companyId, id, trackChanges, ct);
        return entity ?? throw new JobNotFoundException();
    }
}
