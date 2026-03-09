using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Base.Service.ResponseModels;

public sealed class ListResponseModel<T>
{
    public IReadOnlyList<T> Results { get; }
    public int Count { get; }

    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int PageSize { get; }

    public bool? HasPrevious => CurrentPage > 1;
    public bool? HasNext => CurrentPage < TotalPages;

    public ListResponseModel(
        IReadOnlyList<T> results,
        int totalCount,
        RequestParameters requestParameters
    )
    {
        var paginated = requestParameters.Paginated;
        var pageNumber = paginated ? requestParameters.PageNumber : 1;
        var pageSize = paginated ? requestParameters.PageSize : totalCount;

        ArgumentNullException.ThrowIfNull(results);
        ArgumentOutOfRangeException.ThrowIfNegative(totalCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);
        ArgumentOutOfRangeException.ThrowIfNegative(pageSize);

        Results = results;

        var ps = (double)pageSize;
        var totalPages = ps > 0 ? (int)Math.Ceiling(totalCount / ps) : 0;

        Count = totalCount;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = totalPages;
    }
}