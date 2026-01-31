using NGErp.Base.Service.RequestFeatures;

namespace NGErp.Base.Service.ResponseModels;

public sealed class ListResponseModel<T>
{
    public IReadOnlyList<T> Items { get; }
    public MetaData MetaData { get; }

    public ListResponseModel(
        IReadOnlyList<T> items,
        int totalCount,
        RequestParameters requestParameters
    )
    {
        var paginated = requestParameters.Paginated;
        var pageNumber = paginated ? requestParameters.PageNumber : 1;
        var pageSize = paginated ? requestParameters.PageSize : totalCount;

        ArgumentNullException.ThrowIfNull(items);
        ArgumentOutOfRangeException.ThrowIfNegative(totalCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

        Items = items;

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        MetaData = new MetaData
        {
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = totalPages,
        };
    }
}