using System.Text.Json;

namespace NGErp.Base.Domain.ErrorModels;

public  record ErrorDetails
{
    public string? Title { get; set; }
    public string? TraceId { get; set; }
    public IDictionary<string, string[]>? Details { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}
