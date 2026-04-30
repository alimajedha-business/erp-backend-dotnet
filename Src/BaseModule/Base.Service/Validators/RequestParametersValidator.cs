using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Base.Service.Validators;

public static class RequestParametersValidator
{
    public static void ValidateOrdering(
        RequestParameters parameters,
        ISet<string> allowedOrderFields
    )
    {
        if (string.IsNullOrWhiteSpace(parameters.OrderBy))
            return;

        var orderBy = parameters.OrderBy.Trim();

        var hasDescendingPrefix = orderBy.StartsWith('-');
        var normalizedOrderBy = hasDescendingPrefix
            ? orderBy.TrimStart('-').Trim()
            : orderBy;

        var orderParts = normalizedOrderBy.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );

        var hasInvalidDirection =
            orderParts.Length > 2 ||
            (hasDescendingPrefix && orderParts.Length > 1) ||
            (
                orderParts is [_, var direction] &&
                !direction.Equals("asc", StringComparison.OrdinalIgnoreCase) &&
                !direction.Equals("desc", StringComparison.OrdinalIgnoreCase)
            );

        if (
            orderParts.Length == 0 ||
            hasInvalidDirection ||
            !allowedOrderFields.Contains(orderParts[0])
        )
        {
            throw new InvalidOrderingException(orderBy);
        }
    }
}
