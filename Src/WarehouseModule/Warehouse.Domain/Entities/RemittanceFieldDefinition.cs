using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceFieldDefinition :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<RemittanceFieldDefinition>
{
    public required string Key { get; set; }
    public required string Title { get; set; }
    public required RemittanceFieldDataType DataType { get; set; }
    public required RemittanceFieldPlacement AllowedPlacement { get; set; }
    public bool IsActive { get; set; } = true;

    public void Map(EntityTypeBuilder<RemittanceFieldDefinition> builder)
    {
        builder.ToTable(nameof(RemittanceFieldDefinition), "Warehouse");

        builder.HasIndex(i => new { i.CompanyId, i.Key }).IsUnique();

        builder.Property(e => e.Key).HasMaxLength(100).IsRequired();
        builder.Property(e => e.Title).HasMaxLength(100).IsRequired();
        builder.Property(e => e.IsActive).HasDefaultValue(true);
    }
}

[JsonConverter(typeof(RemittanceFieldDataTypeConverter))]
public enum RemittanceFieldDataType
{
    [Description("Text")]
    Text = 1,

    [Description("Integer")]
    Integer = 2,

    [Description("Decimal")]
    Decimal = 3,

    [Description("Date")]
    Date = 4,

    [Description("Boolean")]
    Boolean = 5,

    [Description("Guid")]
    Guid = 6
}

public class RemittanceFieldDataTypeConverter : JsonConverter<RemittanceFieldDataType>
{
    public override RemittanceFieldDataType Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "Text" => RemittanceFieldDataType.Text,
            "Integer" => RemittanceFieldDataType.Integer,
            "Decimal" => RemittanceFieldDataType.Decimal,
            "Date" => RemittanceFieldDataType.Date,
            "Boolean" => RemittanceFieldDataType.Boolean,
            "Guid" => RemittanceFieldDataType.Guid,
            _ => throw new JsonException($"Unknown RemittanceFieldDataType: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        RemittanceFieldDataType value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDescription(value));
    }

    private static string GetDescription(RemittanceFieldDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

[JsonConverter(typeof(RemittanceFieldPlacementConverter))]
[Flags]
public enum RemittanceFieldPlacement
{
    [Description("Header")]
    Header = 1,

    [Description("Detail")]
    Detail = 2,

    [Description("HeaderOrDetail")]
    HeaderOrDetail = Header | Detail
}

public class RemittanceFieldPlacementConverter : JsonConverter<RemittanceFieldPlacement>
{
    public override RemittanceFieldPlacement Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "Header" => RemittanceFieldPlacement.Header,
            "Detail" => RemittanceFieldPlacement.Detail,
            "HeaderOrDetail" => RemittanceFieldPlacement.HeaderOrDetail,
            _ => throw new JsonException($"Unknown RemittanceFieldPlacement: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        RemittanceFieldPlacement value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDescription(value));
    }

    private static string GetDescription(RemittanceFieldPlacement value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
