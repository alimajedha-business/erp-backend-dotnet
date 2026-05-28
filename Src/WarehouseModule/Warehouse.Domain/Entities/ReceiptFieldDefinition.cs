using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ReceiptFieldDefinition :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ReceiptFieldDefinition>
{
    public required string Key { get; set; }
    public required string Title { get; set; }
    public required ReceiptFieldDataType DataType { get; set; }
    public required ReceiptFieldPlacement AllowedPlacement { get; set; }
    public ReceiptReferenceEntityType? ReferenceEntityType { get; set; }
    public bool IsActive { get; set; } = true;

    public void Map(EntityTypeBuilder<ReceiptFieldDefinition> builder)
    {
        builder
            .ToTable(nameof(ReceiptFieldDefinition), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Key })
            .IsUnique();

        builder
            .Property(e => e.Key)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);
    }
}

[JsonConverter(typeof(ReceiptFieldDataTypeConverter))]
public enum ReceiptFieldDataType
{
    [Description("String")]
    String = 1,

    [Description("Integer")]
    Integer = 2,

    [Description("Decimal")]
    Decimal = 3,

    [Description("Date")]
    Date = 4,

    [Description("Boolean")]
    Boolean = 5,

    [Description("Reference")]
    Reference = 6
}

public class ReceiptFieldDataTypeConverter :
    JsonConverter<ReceiptFieldDataType>
{
    public override ReceiptFieldDataType Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "String" => ReceiptFieldDataType.String,
            "Integer" => ReceiptFieldDataType.Integer,
            "Decimal" => ReceiptFieldDataType.Decimal,
            "Date" => ReceiptFieldDataType.Date,
            "Boolean" => ReceiptFieldDataType.Boolean,
            "Reference" => ReceiptFieldDataType.Reference,
            _ => throw new JsonException($"Unknown ReceiptFieldDataType: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        ReceiptFieldDataType value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(ReceiptFieldDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

[JsonConverter(typeof(ReceiptFieldPlacementConverter))]
[Flags]
public enum ReceiptFieldPlacement
{
    [Description("Header")]
    Header = 1,

    [Description("Detail")]
    Detail = 2,

    [Description("HeaderOrDetail")]
    HeaderOrDetail = Header | Detail
}

public class ReceiptFieldPlacementConverter :
    JsonConverter<ReceiptFieldPlacement>
{
    public override ReceiptFieldPlacement Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "Header" => ReceiptFieldPlacement.Header,
            "Detail" => ReceiptFieldPlacement.Detail,
            "HeaderOrDetail" => ReceiptFieldPlacement.HeaderOrDetail,
            _ => throw new JsonException($"Unknown ReceiptFieldPlacement: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        ReceiptFieldPlacement value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(ReceiptFieldPlacement value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public enum ReceiptReferenceEntityType
{
    Warehouse = 1,
    SourceOfSupply = 2,
}