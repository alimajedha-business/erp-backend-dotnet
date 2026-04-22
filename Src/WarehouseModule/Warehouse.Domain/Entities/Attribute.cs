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
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required AttributeDataType DataType { get; set; }
    public required AttributeEntity AttributeEntity { get; set; }
    public bool IsRequired { get; set; } = false;
    public bool IsStockDimension {  get; set; } = false;
    public bool IsStatic { get; set; } = false;

    public ICollection<ItemAttribute> ItemAttributes { get; set; } = [];

    public void Map(EntityTypeBuilder<Attribute> builder)
    {
        builder
            .ToTable(nameof(Attribute), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Attribute_Company_Code");

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);

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
    Integer = 2,

    [Description("Decimal")]
    Decimal = 3,

    [Description("Date")]
    Date = 4,

    [Description("Boolean")]
    Boolean = 5,

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
            "Integer" => AttributeDataType.Integer,
            "Decimal" => AttributeDataType.Decimal,
            "Date" => AttributeDataType.Date,
            "Boolean" => AttributeDataType.Boolean,
            "Enum" => AttributeDataType.Enum,
            _ => throw new JsonException($"Unknown AttributeDataType: {value}")
        };
    }

    public override void Write(Utf8JsonWriter writer, AttributeDataType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(AttributeDataType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

[JsonConverter(typeof(AttributeEntityConverter))]
public enum AttributeEntity
{
    [Description("Item")]
    Item = 1,

    [Description("Location")]
    Location = 2,
}

public class AttributeEntityConverter : JsonConverter<AttributeEntity>
{
    public override AttributeEntity Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "Item" => AttributeEntity.Item,
            "Location" => AttributeEntity.Location,
            _ => throw new JsonException($"Unknown AttributeEntity: {value}")
        };
    }

    public override void Write(Utf8JsonWriter writer, AttributeEntity value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(GetEntityDescription(value));
    }

    private static string GetEntityDescription(AttributeEntity value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}