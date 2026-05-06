using NGErp.Base.Domain.Exceptions;

namespace NGErp.HCM.Domain.Exceptions;

public sealed class JobCategoryNotFoundException()
    : NotFoundException("JobCategory");

public sealed class JobCategoryCodeAlreadyExistsException(int code)
    : DuplicateResourceException()
{
    public int Code { get; } = code;
    public override string LocalizationKey => "JobCategory.Code.Duplicate";
}

public sealed class JobCategoryTitleAlreadyExistsException(string title)
    : DuplicateResourceException()
{
    public string Title { get; } = title;
    public override string LocalizationKey => "JobCategory.Title.Duplicate";
}