using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Domain.Exceptions;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.HCM.Service.RequestValidators.DtoValidators;
using NGErp.HCM.Service.Service.Contracts;

namespace NGErp.HCM.Service.Services;

public class JobCategoryService(
    IAdvancedFilterBuilder filterBuilder,
    IJobCategoryRepository categoryRepository,
    IJobCategoryBusinessRulesValidator businessRuleValidator,
    IValidator<CreateJobCategoryDto> createValidator,
    IValidator<PatchJobCategoryDto> patchValidator,
    IMapper mapper
) : IJobCategoryService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IJobCategoryRepository _jobCategoryRepository = categoryRepository;
    private readonly IJobCategoryBusinessRulesValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateJobCategoryDto> _createValidator = createValidator;
    private readonly IValidator<PatchJobCategoryDto> _patchValidator = patchValidator;

    public async Task<JobCategoryDto> CreateAsync(
        CreateJobCategoryDto createDto,
        CancellationToken ct
    )
    {
        ThrowIfNull(createDto);
        await ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(createDto, ct);

        var entity = _mapper.Map<JobCategory>(createDto);

        var created = await _jobCategoryRepository.AddAsync(entity, ct);

        await _jobCategoryRepository.SaveChangesAsync(ct);
        return _mapper.Map<JobCategoryDto>(created);
    }

    public async Task<JobCategoryDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetSingleOrThrowAsync(
             trackChanges: trackChanges,
             predicate: p =>
                 p.Id == id,
             ct
         );

        return _mapper.Map<JobCategoryDto>(entity);
    }

    public async Task<ListResponseModel<JobCategoryDto>> FilterByQAsync(
        JobCategoryParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _jobCategoryRepository.FilterByQ(parameters);
        var res = await _jobCategoryRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<JobCategoryDto>(
            results: _mapper.Map<IReadOnlyList<JobCategoryDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<JobCategoryDto>> GetFilteredAsync(
        JobCategoryParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<JobCategory>(filterNodeDto);
        var query = _jobCategoryRepository.GetFiltered(advancedFilters);
        var res = await _jobCategoryRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<JobCategoryDto>(
            results: _mapper.Map<IReadOnlyList<JobCategoryDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<JobCategoryDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchJobCategoryDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchJobCategoryPolicy.Validate(patchDocument);

        var codePatched = HasProperty(
            patchDocument,
            nameof(PatchJobCategoryDto.Code)
        );

        var titlePatched = HasProperty(
            patchDocument,
            nameof(PatchJobCategoryDto.Title)
        );

        var entity = await GetSingleOrThrowAsync(
          trackChanges: true,
          predicate: p =>
              p.Id == id,
          ct
      );

        var patchDto = _mapper.Map<PatchJobCategoryDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        await ValidateAsync(_patchValidator, patchDto, ct);

        if (codePatched && patchDto.Code.HasValue)
        {
            await _businessRuleValidator.ValidateJobCategoryCodeUniquenessAsync(
                excludedJobCategoryId: id,
                patchDto.Code.Value,
                ct
            );
        }

        if (titlePatched && patchDto.Title is not null)
        {
            await _businessRuleValidator.ValidateTitleUniquenessAsync(
                excludedJobCategoryId: id,
                patchDto.Title,
                ct
            );
        }

        _mapper.Map(patchDto, entity);

        await _jobCategoryRepository.SaveChangesAsync(ct);
        return _mapper.Map<JobCategoryDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(id, ct);

        await _jobCategoryRepository.Remove(e =>
            e.Id == id,
            ct
        );
    }

    public async Task<JobCategory> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<JobCategory, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _jobCategoryRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new JobCategoryNotFoundException();
    }

    private static bool HasProperty(
        JsonPatchDocument<PatchJobCategoryDto> doc,
        string propertyName
    )
    {
        var path = "/" + propertyName.ToLowerInvariant();

        return doc.Operations.Any(op =>
            op.path is not null &&
            op.path.Equals(path, StringComparison.InvariantCultureIgnoreCase)
        );
    }

    private static void ThrowIfNull(CreateJobCategoryDto? createDto)
    {
        if (createDto is not null)
            return;

        throw new ValidationException([
            new ValidationFailure()
        ]);
    }

    private static async Task ValidateAsync<T>(
        IValidator<T> validator,
        T dto,
        CancellationToken ct
    )
    {
        var validationResult = await validator.ValidateAsync(dto, ct);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
