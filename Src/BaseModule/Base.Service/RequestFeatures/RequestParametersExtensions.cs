using System.Reflection;

using Microsoft.AspNetCore.Http;

namespace NGErp.Base.Service.RequestFeatures;

public static class RequestParametersExtensions
{
    public static Dictionary<string, string> GetEntitySpecificQueryParams<TParams>(
        this IQueryCollection query
    )
        where TParams : RequestParameters
    {
        var baseProps = typeof(RequestParameters)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return query
            .Where(kvp => !baseProps.Contains(kvp.Key))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString(), StringComparer.OrdinalIgnoreCase);
    }
}
