namespace NGErp.Base.Domain.Exceptions;

public abstract class BadRequestException(
    params object[] args
) : Exception()
{
    public abstract string LocalizationKey { get; }
    public object[] Arguments { get; } = args;
}