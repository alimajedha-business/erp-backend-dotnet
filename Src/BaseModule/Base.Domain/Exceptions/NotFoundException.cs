namespace NGErp.Base.Domain.Exceptions;

public class NotFoundException(params object[] args) : Exception()
{
    public string LocalizationKey { get; } = "NotFound";
    public object[] Arguments { get; } = args;
}
