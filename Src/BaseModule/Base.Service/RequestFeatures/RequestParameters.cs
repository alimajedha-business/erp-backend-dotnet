using System.ComponentModel;

using Microsoft.AspNetCore.Mvc;

namespace NGErp.Base.Service.RequestFeatures;

public abstract class RequestParameters
{
    const int maxPageSize = 50;

    private int _pageNumber = 1;

    [FromQuery(Name = "page")]
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    private int _pageSize = 10;

    [FromQuery(Name = "page_size")]
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

    [FromQuery(Name = "paginated")]
    public bool Paginated { get; set; } = true;

    [FromQuery(Name = "ordering")]
    public string? OrderBy { get; set; }

    [FromQuery(Name = "excludeIds")]
    [Description("This parameter is used in list APIs with GET method.")]
    public string? ExcludeIds { get; set; }

    [FromQuery(Name = "q")]
    [Description("This parameter is used in list APIs with GET method.")]
    public string? Q { get; set; }
}
