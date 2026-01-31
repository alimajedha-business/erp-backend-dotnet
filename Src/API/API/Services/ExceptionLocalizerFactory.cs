using NGErp.Base.Service.Resources;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Resources;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.API.Services
{
    public static class ExceptionLocalizerFactory
    {
        public static IExceptionLocalizer ResolveForException(IServiceProvider sp, Exception ex)
        {
            var ns = ex.GetType().Namespace ?? string.Empty;

            var mapping = new (string key, Type resource)[]
            {
                ("General", typeof(GeneralResource)),
                ("Warehouse", typeof(WarehouseResource)),
            };

            var match = mapping.FirstOrDefault(m => ns.Contains(m.key));

            var resourceType = match.resource ?? typeof(BaseResource);
            var serviceType = typeof(IExceptionLocalizer<>).MakeGenericType(resourceType);

            return (IExceptionLocalizer)sp.GetRequiredService(serviceType);
        }
    }
}
