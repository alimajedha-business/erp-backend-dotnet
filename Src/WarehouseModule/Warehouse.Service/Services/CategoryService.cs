using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class CategoryService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryRepository categoryRepository,
    ICategoryBusinessRuleValidator businessRuleValidator,
    IValidator<CreateCategoryDto> createValidator,
    IValidator<PatchCategoryDto> patchValidator,
    IMapper mapper
) : ICategoryService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICategoryBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateCategoryDto> _createValidator = createValidator;
    private readonly IValidator<PatchCategoryDto> _patchValidator = patchValidator;

    public async Task<CategoryDto> CreateAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<Category>(createDto);
        entity.CompanyId = companyId;

        var created = await _categoryRepository.AddAsync(entity, ct);

        await _categoryRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryDto>(created);
    }

    public async Task<CategoryDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var category = await GetSingleOrThrowAsync(
             trackChanges: trackChanges,
             predicate: p =>
                 p.CompanyId == companyId &&
                 p.Id == id,
             ct
         );

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<ListResponseModel<CategorySlimDto>> FilterByQAsync(
        Guid companyId,
        CategoryParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _categoryRepository.FilterByQ(companyId, parameters);
        var res = await _categoryRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<CategorySlimDto>(
            results: _mapper.Map<IReadOnlyList<CategorySlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<CategoryDto>> GetFilteredAsync(
        Guid companyId,
        CategoryParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<Category>(filterNodeDto);
        var query = _categoryRepository.GetFiltered(companyId, advancedFilters);
        var res = await _categoryRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<CategoryDto>(
            results: _mapper.Map<IReadOnlyList<CategoryDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<CategoryDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchCategoryDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchCategoryPolicy.Validate(patchDocument);

        var codePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchCategoryDto.Code)
        );

        var hasNextLevelPatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchCategoryDto.HasNextLevel)
        );

        var category = await GetSingleOrThrowAsync(
             trackChanges: true,
             predicate: p =>
                 p.CompanyId == companyId &&
                 p.Id == id,
             ct
         );

        var patchDto = _mapper.Map<PatchCategoryDto>(category);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        await RequestBodyValidator.ValidateAsync(_patchValidator, patchDto, ct);

        if (hasNextLevelPatched && patchDto.HasNextLevel.HasValue)
        {
            var levelNo = category.LevelNo;
            var hasNextLevel = patchDto.HasNextLevel.Value;

            _businessRuleValidator.ValidateHasNextLevel(levelNo, hasNextLevel);

            if (!hasNextLevel)
            {
                var hasSubCategories = await _categoryRepository.AnyAsync(e =>
                     e.CompanyId == companyId &&
                     e.ParentCategoryId == id,
                     ct
                 );

                if (hasSubCategories)
                    throw new CategoryCannotDisableNextLevelWithChildrenException();
            }
        }

        if (codePatched)
        {
            var levelNo = category.LevelNo;
            var code = patchDto.Code!;

            await _businessRuleValidator.ValidateCategoryCodeLengthAsync(
                companyId,
                levelNo,
                code,
                ct
            );

            await _businessRuleValidator.ValidateCategoryCodeUniquenessAsync(
                companyId,
                excludedCategoryId: id,
                code,
                ct
            );
        }

        _mapper.Map(patchDto, category);

        await _categoryRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryDto>(category);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, id, ct);
        await _categoryRepository.Remove(e =>
            e.CompanyId == companyId &&
            e.Id == id,
            ct
        );
    }

    public async Task<CategoryCodeDto?> GetCategoryCodeAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        return await _categoryRepository.GetCategoryCodeAsync(companyId, id, ct);
    }

    public async Task<Category> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<Category, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _categoryRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new CategoryNotFoundException();
    }
}
