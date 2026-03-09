namespace NGErp.Base.Service.Services
{
    public interface IExceptionLocalizer
    {
        string Localize(Exception ex);
    }

    public interface IExceptionLocalizer<TEntityResource> : IExceptionLocalizer { }
}
