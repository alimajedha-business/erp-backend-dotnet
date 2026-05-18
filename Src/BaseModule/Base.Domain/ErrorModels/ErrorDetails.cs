using System.Text.Json;

namespace NGErp.Base.Domain.ErrorModels;

public  record ErrorDetails
{
    public string? Title { get; set; }
    public string? TraceId { get; set; }
    public object? Details { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
