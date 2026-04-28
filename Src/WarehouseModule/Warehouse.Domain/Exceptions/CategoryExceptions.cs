using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class CategoryNotFoundException()
    : NotFoundException("Category");

public sealed class CategoryCodeExceedsMaxLengthException(
    int maxLength,
    int actualLength,
    int levelNo
) : BadRequestException(maxLength)
{
    public int MaxLength { get; } = maxLength;
    public int ActualLength { get; } = actualLength;
    public int LevelNo { get; } = levelNo;

    public override string LocalizationKey => "Category.Code.ExceedsMaxLength";
}

public sealed class CategoryCodeAlreadyExistsException(string code)
    : BadRequestException()
{
    public string Code { get; } = code;

    public override string LocalizationKey => "Category.Code.Duplicate";
}

public sealed class CategoryLevelNoOutOfRangeException()
    : BadRequestException()
{
    public override string LocalizationKey => "Category.LevelNo.Range";
}

public sealed class CategoryParentRequiredException(int levelNo)
    : BadRequestException(levelNo)
{
    public int LevelNo { get; } = levelNo;

    public override string LocalizationKey => "Category.Parent.Required";
}

public sealed class CategoryRootCannotHaveParentException()
    : BadRequestException()
{
    public override string LocalizationKey => "Category.Parent.RootCannotHaveParent";
}

public sealed class CategoryParentLevelMismatchException(
    int expectedParentLevel,
    int actualParentLevel
) : BadRequestException(expectedParentLevel, actualParentLevel)
{
    public int ExpectedParentLevel { get; } = expectedParentLevel;
    public int ActualParentLevel { get; } = actualParentLevel;

    public override string LocalizationKey => "Category.Parent.LevelMismatch";
}

public sealed class CategoryParentCannotHaveChildrenException()
    : BadRequestException()
{
    public override string LocalizationKey => "Category.Parent.CannotHaveChildren";
}

public sealed class CategoryFirstLevelMustHaveChildrenException()
    : BadRequestException()
{
    public override string LocalizationKey => "Category.LevelNo.NotLastLevelIf1";
}

public sealed class CategoryLastLevelCannotHaveChildrenException()
    : BadRequestException()
{
    public override string LocalizationKey => "Category.LevelNo.LastLevelIf6";
}

public sealed class CategoryCannotDisableNextLevelWithChildrenException()
    : BadRequestException()
{
    public override string LocalizationKey => "Category.HasNextLevel.HasSubCategories";
}

public sealed class CategoryHasSubCategoriesException()
    : BadRequestException()
{
    public override string LocalizationKey => "Category.Delete.HasSubCategories";
}

public sealed class CategoryHasItemsException()
    : BadRequestException()
{
    public override string LocalizationKey => "Category.Delete.HasItems";
}

public sealed class CategoryInvalidOrderingException(string orderBy)
    : BadRequestException(orderBy)
{
    public string OrderBy { get; } = orderBy;

    public override string LocalizationKey => "Category.Ordering.Invalid";
}
