namespace NGErp.Base.Domain.Exceptions;

public class DuplicateInsertException(params object[] args) : Exception()
{
    public string LocalizationKey { get; } = "DuplicateInsert";
    public object[] Arguments { get; } = args;
}
