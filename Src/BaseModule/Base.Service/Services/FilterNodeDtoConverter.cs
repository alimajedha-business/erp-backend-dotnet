using NGErp.Base.Service.DTOs;

namespace NGErp.Base.Service.Services;

using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class FilterNodeDtoConverter : JsonConverter<FilterNodeDto?>
{
    public override FilterNodeDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);

        if (doc.RootElement.ValueKind == JsonValueKind.Object &&
            !doc.RootElement.EnumerateObject().Any())
            return null;

        if (!doc.RootElement.TryGetProperty("type", out var typeProp))
            throw new JsonException("Missing discriminator property 'type'.");

        var type = typeProp.GetString();
        var json = doc.RootElement.GetRawText();

        return type switch
        {
            "group" => JsonSerializer.Deserialize<FilterGroupDto>(json, options),
            "condition" => JsonSerializer.Deserialize<FilterConditionDto>(json, options),
            _ => throw new JsonException($"Unknown filter node type '{type}'.")
        };
    }

    public override void Write(Utf8JsonWriter writer, FilterNodeDto? value, JsonSerializerOptions options)
    {
        if (value is null)
        { writer.WriteNullValue(); return; }

        using var payload = JsonDocument.Parse(JsonSerializer.Serialize(value, value.GetType(), options));

        writer.WriteStartObject();
        writer.WriteString("type", value switch
        {
            FilterGroupDto => "group",
            FilterConditionDto => "condition",
            _ => throw new JsonException($"Unknown node runtime type: {value.GetType().Name}")
        });

        foreach (var p in payload.RootElement.EnumerateObject())
            p.WriteTo(writer);

        writer.WriteEndObject();
    }
}

