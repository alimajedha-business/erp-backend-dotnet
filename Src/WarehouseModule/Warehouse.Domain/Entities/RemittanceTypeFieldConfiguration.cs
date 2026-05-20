using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceTypeFieldConfiguration :
    BaseEntity,
    IBaseEntityTypeConfiguration<RemittanceTypeFieldConfiguration>
{
    public Guid RemittanceTypeConfigurationId { get; set; }
    public Guid FieldDefinitionId { get; set; }
    public bool Exists { get; set; }
    public bool IsRequired { get; set; }
    public RemittanceConfiguredPlacement Placement { get; set; }

    public RemittanceTypeConfiguration RemittanceTypeConfiguration { get; set; } = null!;
    public RemittanceFieldDefinition FieldDefinition { get; set; } = null!;

    public void Map(EntityTypeBuilder<RemittanceTypeFieldConfiguration> builder)
    {
        builder
            .ToTable(nameof(RemittanceTypeFieldConfiguration), "Warehouse");

        builder.HasIndex(i => new
        {
            i.RemittanceTypeConfigurationId,
            i.FieldDefinitionId
        }).IsUnique();

        builder.HasOne(e => e.RemittanceTypeConfiguration)
            .WithMany(e => e.FieldConfigurations)
            .HasForeignKey(e => e.RemittanceTypeConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.FieldDefinition)
            .WithMany()
            .HasForeignKey(e => e.FieldDefinitionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

[JsonConverter(typeof(RemittanceConfiguredPlacementConverter))]
public enum RemittanceConfiguredPlacement
{
    [Description("Header")]
    Header = 1,

    [Description("Detail")]
    Detail = 2
}

public class RemittanceConfiguredPlacementConverter :
    JsonConverter<RemittanceConfiguredPlacement>
{
    public override RemittanceConfiguredPlacement Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "Header" => RemittanceConfiguredPlacement.Header,
            "Detail" => RemittanceConfiguredPlacement.Detail,
            _ => throw new JsonException($"Unknown RemittanceFieldPlacement: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        RemittanceConfiguredPlacement value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(RemittanceConfiguredPlacement value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
