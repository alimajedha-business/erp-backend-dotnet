namespace NGErp.Base.Domain.Exceptions;

public class ForeignKeyViolationException(params object[] args) : Exception()
{
    public string LocalizationKey { get; } = "ForeignKeyViolation";
    public object[] Arguments { get; } = args;
}
