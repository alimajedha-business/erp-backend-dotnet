using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class CategoryBusinessRuleValidator(
    ICategoryRepository categoryRepository,
    ICategoryLevelConstraintService constraintService,
    IItemRepository itemRepository
) : ICategoryBusinessRuleValidator
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

    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ICategoryLevelConstraintService _constraintService = constraintService;
    private readonly IItemRepository _itemRepository = itemRepository;

    public void ValidateParameters(CategoryParameters parameters)
    {
        if (string.IsNullOrWhiteSpace(parameters.OrderBy))
            return;

        var orderBy = parameters.OrderBy.Trim();
        var hasDescendingPrefix = orderBy.StartsWith('-');
        var normalizedOrderBy = hasDescendingPrefix
            ? orderBy.TrimStart('-').Trim()
            : orderBy;

        var orderParts = normalizedOrderBy.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );

        var hasInvalidDirection = orderParts.Length > 2 ||
            (hasDescendingPrefix && orderParts.Length > 1) ||
            (
                orderParts is [_, var direction] &&
                !direction.Equals("asc", StringComparison.OrdinalIgnoreCase) &&
                !direction.Equals("desc", StringComparison.OrdinalIgnoreCase)
            );

        if (
            orderParts.Length == 0 ||
            hasInvalidDirection ||
            !_allowedOrderFields.Contains(orderParts[0])
        )
            throw new CategoryInvalidOrderingException(orderBy);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    )
    {
        var levelNo = createDto.LevelNo;
        var hasNextLevel = createDto.HasNextLevel;
        var parentCategoryId = createDto.ParentCategoryId;
        var code = createDto.Code;

        ValidateHasNextLevel(levelNo, hasNextLevel);

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

        await ValidateCategoryCodeUniquenessAsync(
            companyId,
            excludedCategoryId: null,
            code,
            ct
        );
    }

    public async Task CheckDeletePermissionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _categoryRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.Id == id,
            ct
        );

        if (!exists)
            throw new CategoryNotFoundException();

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

    public async Task ValidateParentAsync(
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

        var parent = await _categoryRepository.SingleOrDefaultAsync(
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == parentCategoryId.Value,
            trackChanges: false,
            ct
        ) ?? throw new CategoryNotFoundException();

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

    public void ValidateHasNextLevel(
        int levelNo,
        bool hasNextLevel
    )
    {
        if (levelNo == 1 && !hasNextLevel)
            throw new CategoryFirstLevelMustHaveChildrenException();

        if (levelNo == 6 && hasNextLevel)
            throw new CategoryLastLevelCannotHaveChildrenException();
    }

    public async Task ValidateCategoryCodeLengthAsync(
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

    public async Task ValidateCategoryCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedCategoryId,
        string code,
        CancellationToken ct
    )
    {
        var exists = excludedCategoryId is null
            ? await _categoryRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Code == code,
                ct
            )
            : await _categoryRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Id != excludedCategoryId.Value &&
                e.Code == code,
                ct
            );

        if (exists)
            throw new CategoryCodeAlreadyExistsException(code);
    }
}
