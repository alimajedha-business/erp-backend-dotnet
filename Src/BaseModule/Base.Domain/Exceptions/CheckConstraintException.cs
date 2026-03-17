namespace NGErp.Base.Domain.Exceptions;

public class CheckConstraintException(params object[] args) : Exception()
{
    public string LocalizationKey { get; } = "CheckConstraint";
    public object[] Arguments { get; } = args;
}
