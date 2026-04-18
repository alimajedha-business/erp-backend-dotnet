using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using AutoMapper.Execution;

using DocumentFormat.OpenXml.Office2010.Excel;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Infrastructure.DataAccess;

public static class QueryableExtensions
{
    public static IQueryable<T> Filter<T>(
        this IQueryable<T> query,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        if (requestAdvancedFilters is null)
            return query;

        var (search, args) = requestAdvancedFilters;

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = (args is { Length: > 0 })
                ? query.Where(search, args)
                : query.Where(search);
        }

        return query;
    }

    public static IQueryable<T> Filter<T>(
        this IQueryable<T> query,
        RequestParameters? requestParameters = null
    )
    {
        if (requestParameters is null)
            return query;

        var excludeIds = requestParameters.ExcludeIds;

        if (!string.IsNullOrWhiteSpace(excludeIds))
        {
            var guidList = excludeIds.Split(',')
                .Select(id => Guid.Parse(id))
                .ToList();

            query = query.Where(e => e != null && !guidList.Contains(
                EF.Property<Guid>(e, "Id")
            ));
        }

        var q = requestParameters.Q;

        if (string.IsNullOrWhiteSpace(q))
            return query;

        var searchTerms = q.Split('+', StringSplitOptions.RemoveEmptyEntries)
                          .Select(term => term.Trim())
                          .Where(term => !string.IsNullOrEmpty(term))
                          .ToList();

        if (searchTerms.Count == 0)
            return query;

        var conditions = new List<Expression<Func<T, bool>>>();

        var codeField = typeof(T).GetProperty("Code");
        if (codeField is not null)
        {
            if (codeField.PropertyType == typeof(int))
            {
                conditions.Add(e => e != null && searchTerms.All(term =>
                    EF.Property<int>(e, "Code").ToString().StartsWith(term))
                );
            }

            if (codeField.PropertyType == typeof(string))
            {
                conditions.Add(e => e != null && searchTerms.All(term =>
                    EF.Property<string>(e, "Code").Contains(term))
                );
            }
        }

        var titleField = typeof(T).GetProperty("Title");
        if (titleField is not null)
        {
            conditions.Add(e => e != null && searchTerms.All(term =>
                EF.Property<string>(e, "Title").Contains(term))
            );
        }

        var nameField = typeof(T).GetProperty("Name");
        if (nameField is not null)
        {
            conditions.Add(e => e != null && searchTerms.All(term =>
                EF.Property<string>(e, "Name").Contains(term))
            );
        }

        if (conditions.Count > 0)
        {
            Expression<Func<T, bool>> combinedExpression = conditions[0];
            for (int i = 1; i < conditions.Count; i++)
            {
                combinedExpression = CombineExpressions(
                    combinedExpression,
                    conditions[i],
                    ExpressionType.OrElse
                );
            }

            query = query.Where(combinedExpression);
        }

        return query;
    }

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
            return query;

        return query
            .Skip(requestParameters.PageSize * (requestParameters.PageNumber - 1))
            .Take(requestParameters.PageSize);
    }

    public static async Task<ListQueryResult<T>> ToResponseListAsync<T>(
        this IQueryable<T> query,
        RequestParameters requestParameters,
        CancellationToken ct
    )
    {
        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Sort(requestParameters)
            .Paginate(requestParameters)
            .ToListAsync(ct);

        return new ListQueryResult<T>(items, totalCount);
    }

    private static Expression<Func<T, bool>> CombineExpressions<T>(
        Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2,
        ExpressionType expressionType
    )
    {
        var parameter = Expression.Parameter(typeof(T));

        var left = expr1.Body.Replace(expr1.Parameters[0], parameter);
        var right = expr2.Body.Replace(expr2.Parameters[0], parameter);

        var body = Expression.MakeBinary(expressionType, left, right);

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}
