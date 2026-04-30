using NGErp.Base.Domain.Exceptions;

namespace NGErp.HCM.Domain.Exceptions;

public sealed class JobCategoryNotFoundException()
    : NotFoundException("JobCategory");

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
    : DuplicateResourceException()
{
    public string Code { get; } = code;
    public override string LocalizationKey => "Category.Code.Duplicate";
}