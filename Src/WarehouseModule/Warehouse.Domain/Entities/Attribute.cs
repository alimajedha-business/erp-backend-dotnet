using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Attribute :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Attribute>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public AttributeDataType DataType { get; private set; }
    public bool IsItemAttribute { get; private set; } = false;
    public bool IsRequired { get; private set; } = false;
    public bool IsStockDimension {  get; private set; } = false;

    public void Map(EntityTypeBuilder<Attribute> builder)
    {
        builder
            .ToTable(nameof(Attribute), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Attribute_Company_Code");

        builder
            .Property(e => e.Code)
            .HasMaxLength(20);

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);

        builder
            .Property(e => e.IsItemAttribute)
            .HasDefaultValue(false);

        builder
            .Property(e => e.IsRequired)
            .HasDefaultValue(false);

        builder
            .Property(e => e.IsStockDimension)
            .HasDefaultValue(false);
    }
}

[JsonConverter(typeof(AttributeDataTypeConverter))]
public enum AttributeDataType
{
    [Description("Text")]
    Text = 1,

    [Description("Integer")]
    Int = 2,

    [Description("Decimal")]
    Decimal = 3,

    [Description("Date")]
    Date = 4,

    [Description("Boolean")]
    Bool = 5,

    [Description("Enum")]
    Enum = 6
}

public class AttributeDataTypeConverter : JsonConverter<AttributeDataType>
{
    public override AttributeDataType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "Text" => AttributeDataType.Text,
            "Integer" => AttributeDataType.Int,
            "Decimal" => AttributeDataType.Decimal,
            "Date" => AttributeDataType.Date,
            "Boolean" => AttributeDataType.Bool,
            "Enum" => AttributeDataType.Enum,
            _ => throw new JsonException($"Unknown AttributeDataType: {value}")
        };
    }

    public override void Write(Utf8JsonWriter writer, AttributeDataType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(GetDescription(value));
    }

    private static string GetDescription(AttributeDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}