using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class CategoryService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryRepository categoryRepository,
    ICategoryLevelConstraintService constraintService,
    IItemRepository itemRepository,
    IMapper mapper
) : ICategoryService
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "code",
        "title",
        "levelNo",
        "createdAt"
    };

    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICategoryLevelConstraintService _constraintService = constraintService;
    private readonly IItemRepository _itemRepository = itemRepository;

    public async Task<CategoryDto> CreateAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    )
    {
        await ValidateCategoryInvariantsAsync(
            companyId,
            createDto.ParentCategoryId,
            createDto.LevelNo,
            createDto.HasNextLevel,
            createDto.Code,
            ct
        );

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
        ValidateParameters(parameters);

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
        ValidateParameters(parameters);

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
        if (patchDocument is null)
            throw new InvalidPatchDocumentException("Patch document is required.");

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

        ValidateLevelHasNextLevel(category.LevelNo, patchDto.HasNextLevel!.Value);

        await ValidateCategoryCodeLengthAsync(
            companyId,
            category.LevelNo,
            patchDto.Code!,
            ct
        );

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
        var category = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == id,
            ct
        );

        await ValidateCanDeleteAsync(companyId, id, ct);

        _categoryRepository.Remove(category);
        await _categoryRepository.SaveChangesAsync(ct);
    }

    private static void ValidateParameters(CategoryParameters parameters)
    {
        if (string.IsNullOrWhiteSpace(parameters.OrderBy))
            return;

        var orderBy = parameters.OrderBy.Trim();
        if (!_allowedOrderFields.Contains(orderBy))
            throw new CategoryInvalidOrderingException(orderBy);
    }

    private async Task ValidateCategoryInvariantsAsync(
        Guid companyId,
        Guid? parentCategoryId,
        int levelNo,
        bool hasNextLevel,
        string code,
        CancellationToken ct
    )
    {
        ValidateLevelHasNextLevel(levelNo, hasNextLevel);

        await ValidateParentAsync(
            companyId,
            parentCategoryId,
            levelNo,
            ct
        );

        await ValidateCategoryCodeLengthAsync(
            companyId,
            levelNo,
            code,
            ct
        );
    }

    private async Task ValidateCanDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var hasSubCategories = await _categoryRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.ParentCategoryId == id,
            ct
        );

        if (hasSubCategories)
            throw new CategoryHasSubCategoriesException();

        var hasItems = await _itemRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.CategoryId == id,
            ct
        );

        if (hasItems)
            throw new CategoryHasItemsException();
    }

    private async Task<Category> GetSingleOrThrowAsync(
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

    private async Task ValidateCategoryCodeLengthAsync(
        Guid companyId,
        int levelNo,
        string code,
        CancellationToken ct
    )
    {
        var categoryLevel = await _constraintService.GetByLevelNoAsync(
            companyId,
            levelNo,
            ct
        );

        if (categoryLevel == null || categoryLevel.CodeLength <= 0)
            return;

        if (code.Length > categoryLevel.CodeLength)
        {
            throw new CategoryCodeExceedsMaxLengthException(
                categoryLevel.CodeLength,
                code.Length,
                levelNo
            );
        }
    }

    private async Task ValidateParentAsync(
        Guid companyId,
        Guid? parentCategoryId,
        int levelNo,
        CancellationToken ct
    )
    {
        if (levelNo == 1)
        {
            if (parentCategoryId is not null)
                throw new CategoryRootCannotHaveParentException();

            return;
        }

        if (parentCategoryId is null)
            throw new CategoryParentRequiredException(levelNo);

        var parent = await GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == parentCategoryId.Value,
            ct
        );

        var expectedParentLevel = levelNo - 1;
        if (parent.LevelNo != expectedParentLevel)
        {
            throw new CategoryParentLevelMismatchException(
                expectedParentLevel,
                parent.LevelNo
            );
        }

        if (!parent.HasNextLevel)
            throw new CategoryParentCannotHaveChildrenException();
    }

    private static void ValidateLevelHasNextLevel(
        int levelNo,
        bool hasNextLevel
    )
    {
        if (levelNo is < 1 or > 6)
            throw new CategoryLevelOutOfRangeException();

        if (levelNo == 1 && !hasNextLevel)
            throw new CategoryFirstLevelMustHaveChildrenException();

        if (levelNo == 6 && hasNextLevel)
            throw new CategoryLastLevelCannotHaveChildrenException();
    }
}


