namespace NGErp.Base.Domain.Exceptions;

public abstract class ListValidationException(
    IReadOnlyDictionary<int, IReadOnlyList<Exception>> errors
) : BusinessRuleViolationException()
{
    public IReadOnlyDictionary<int, IReadOnlyList<Exception>> Errors { get; } = errors;
}
