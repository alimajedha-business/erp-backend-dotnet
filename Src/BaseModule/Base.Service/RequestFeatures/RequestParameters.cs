using NGErp.Base.Service.DTOs;

namespace NGErp.Base.Service.RequestFeatures;

public abstract class RequestParameters
{
    const int maxPageSize = 50;

    private int _pageNumber = 1;
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value <= 0)
            {
                _pageSize = 1;
            }
            else if (value > maxPageSize)
            {
                _pageSize = maxPageSize;
            }
            else
            {
                _pageSize = value;
            }
        }
    }

    public bool Paginated { get; set; } = true;
    public string? OrderBy { get; set; }
}

public sealed record FilterRequest(FilterNodeDto? Filter);
