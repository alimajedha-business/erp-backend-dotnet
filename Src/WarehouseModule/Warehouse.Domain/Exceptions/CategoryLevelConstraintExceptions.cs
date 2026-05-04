using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class CategoryLevelConstraintNotFoundException()
    : NotFoundException("CategoryLevelConstraint");

public sealed class CategoryLevelConstraintLevelAlreadyExistsException(int levelNo)
    : DuplicateResourceException(levelNo)
{
    public int LevelNo { get; } = levelNo;

    public override string LocalizationKey =>
        "CategoryLevelConstraint.LevelNo.Duplicate";
}

public sealed class CategoryLevelConstraintPreviousLevelRequiredException(
    int previousLevelNo,
    int levelNo
) : BusinessRuleViolationException(previousLevelNo, levelNo)
{
    public int PreviousLevelNo { get; } = previousLevelNo;
    public int LevelNo { get; } = levelNo;

    public override string LocalizationKey =>
        "CategoryLevelConstraint.LevelNo.PreviousRequired";
}

public sealed class CategoryLevelConstraintCodeLengthCannotDecreaseException(
    int levelNo,
    int currentCodeLength,
    int newCodeLength
) : BusinessRuleViolationException(levelNo, currentCodeLength, newCodeLength)
{
    public int LevelNo { get; } = levelNo;
    public int CurrentCodeLength { get; } = currentCodeLength;
    public int NewCodeLength { get; } = newCodeLength;

    public override string LocalizationKey =>
        "CategoryLevelConstraint.CodeLength.CannotDecrease";
}

public sealed class CategoryLevelConstraintHasCategoriesException(int levelNo)
    : BusinessRuleViolationException(levelNo)
{
    public int LevelNo { get; } = levelNo;

    public override string LocalizationKey =>
        "CategoryLevelConstraint.Delete.HasCategories";
}
