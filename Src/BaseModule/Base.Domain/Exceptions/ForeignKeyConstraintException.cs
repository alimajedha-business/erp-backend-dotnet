namespace NGErp.Base.Domain.Exceptions;

public class ForeignKeyConstraintException(params object[] args) : Exception()
{
    public string LocalizationKey { get; } = "ForeignKeyConstraint";
    public object[] Arguments { get; } = args;
}
