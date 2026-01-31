using System.Linq.Dynamic.Core;

using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Base.Infrastructure.DataAccess;

public static class QueryableExtensions
{
    public static IQueryable<T> Sort<T>(
        this IQueryable<T> query,
        RequestParameters requestParameters
    )
    {
        if (!string.IsNullOrWhiteSpace(requestParameters.OrderBy))
        {
            var orderByClause = requestParameters.OrderBy.Trim();

            if (orderByClause.StartsWith('-'))
                orderByClause = orderByClause.TrimStart('-') + " DESC";

            query = query.OrderBy(orderByClause);
        }

        return query;
    }

    public static IQueryable<T> Paginate<T>(
            this IQueryable<T> query,
            RequestParameters requestParameters
        )
    {
        if (!requestParameters.Paginated)
        {
            return query;
        }

        return query
            .Skip(requestParameters.PageSize * (requestParameters.PageNumber - 1))
            .Take(requestParameters.PageSize);
    }
}
