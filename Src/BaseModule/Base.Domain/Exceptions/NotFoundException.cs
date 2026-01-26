namespace NGErp.Base.Domain.Exceptions;

public abstract class NotFoundException : Exception
{
    public string LocalizationKey { get; }
    public object[] Arguments { get; }

    protected NotFoundException(string localizationKey, params object[] args)
        : base(localizationKey) // base message = key, for backward compatibility
    {
        LocalizationKey = localizationKey;
        Arguments = args;
    }
}
