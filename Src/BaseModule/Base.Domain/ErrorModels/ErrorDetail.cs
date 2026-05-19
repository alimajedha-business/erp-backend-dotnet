using System.Text.Json.Serialization;

namespace NGErp.Base.Domain.ErrorModels;

public sealed record ErrorDetail(
    [property: JsonPropertyName("code")]
    string Code,
    [property: JsonPropertyName("message")]
    string Message
);
