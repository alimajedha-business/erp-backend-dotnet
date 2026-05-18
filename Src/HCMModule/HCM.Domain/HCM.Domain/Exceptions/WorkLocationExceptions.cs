using NGErp.Base.Domain.Exceptions;

namespace NGErp.HCM.Domain.Exceptions;

public sealed class WorkLocationNotFoundException()
    : NotFoundException("WorkLocation");

public sealed class WorkLocationTitleAlreadyExistsException(string title)
    : DuplicateResourceException()
{
    public string Title { get; } = title;
    public override string LocalizationKey => "WorkLocation.Title.Duplicate";
}

public sealed class WorkLocationCircularDependencyException()
    : BadRequestException()
{
    public override string LocalizationKey => "WorkLocation.CircularDependency";
}
