namespace NGErp.Base.Service.Services
{
    public interface IExceptionLocalizer
    {
        string Localize(Exception ex);
        string Localize(string key, params object[] args);
    }

    public interface IExceptionLocalizer<TEntityResource> : IExceptionLocalizer { }
}
