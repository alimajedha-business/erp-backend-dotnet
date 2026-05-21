using NGErp.Base.Domain.Exceptions;

namespace NGErp.HCM.Domain.Exceptions;

public sealed class JobNotFoundException()
    : NotFoundException("Job");

public sealed class JobCodeAlreadyExistsException(string code)
    : DuplicateResourceException()
{
    public string Code { get; } = code;
    public override string LocalizationKey => "Job.Code.Duplicate";
}

public sealed class JobTitleAlreadyExistsException(string title)
    : DuplicateResourceException()
{
    public string Title { get; } = title;
    public override string LocalizationKey => "Job.Title.Duplicate";
}
