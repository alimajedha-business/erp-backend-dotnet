using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class SiUnit :
    BaseEntity,
    IBaseEntityTypeConfiguration<SiUnit>
{
    public int Code { get; set; }
    public required string Title { get; set; }
    public string Symbol { get; set; } = default!;
    public decimal FactorToBase { get; set; }
    public bool IsBaseUnit { get; set; }
    public UnitDimension UnitDimension { get; set; }

    public void Map(EntityTypeBuilder<SiUnit> builder)
    {
        builder
            .ToTable(nameof(SiUnit), "Warehouse");

        builder
            .Property(e => e.FactorToBase)
            .HasPrecision(28, 14);

        builder
            .HasIndex(e => new { e.Code })
            .IsUnique()
            .HasDatabaseName("UX_SiUnit_Code");
    }
}

[JsonConverter(typeof(UnitDimensionConverter))]
public enum UnitDimension
{
    [Description("MASS")]
    MASS = 1,

    [Description("LENGTH")]
    LENGTH = 2,

    [Description("VOLUME")]
    VOLUME = 3,

    [Description("AREA")]
    AREA = 4,

    [Description("QUANTITY")]
    QUANTITY = 5
}

public class UnitDimensionConverter :
    JsonConverter<UnitDimension>
{
    public override UnitDimension Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "MASS" => UnitDimension.MASS,
            "LENGTH" => UnitDimension.LENGTH,
            "VOLUME" => UnitDimension.VOLUME,
            "AREA" => UnitDimension.AREA,
            "QUANTITY" => UnitDimension.QUANTITY,
            _ => throw new JsonException($"Unknown UnitDimension: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        UnitDimension value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(UnitDimension value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
