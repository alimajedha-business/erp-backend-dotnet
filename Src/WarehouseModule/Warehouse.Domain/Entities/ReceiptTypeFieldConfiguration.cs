using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ReceiptTypeFieldConfiguration :
    BaseEntity,
    IBaseEntityTypeConfiguration<ReceiptTypeFieldConfiguration>
{
    public Guid ReceiptTypeConfigurationId { get; set; }
    public Guid FieldDefinitionId { get; set; }
    public bool Exists { get; set; }
    public bool IsRequired { get; set; }
    public ReceiptConfiguredPlacement Placement { get; set; }

    public ReceiptTypeConfiguration ReceiptTypeConfiguration { get; set; } = null!;
    public ReceiptFieldDefinition FieldDefinition { get; set; } = null!;
    
    public void Map(EntityTypeBuilder<ReceiptTypeFieldConfiguration> builder)
    {
        builder
            .ToTable(nameof(ReceiptTypeFieldConfiguration), "Warehouse");

        builder.HasIndex(i => new
        {
            i.ReceiptTypeConfigurationId,
            i.FieldDefinitionId
        }).IsUnique();

        builder.HasOne(e => e.ReceiptTypeConfiguration)
            .WithMany(e => e.FieldConfigurations)
            .HasForeignKey(e => e.ReceiptTypeConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.FieldDefinition)
            .WithMany()
            .HasForeignKey(e => e.FieldDefinitionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

[JsonConverter(typeof(ReceiptConfiguredPlacementConverter))]
public enum ReceiptConfiguredPlacement
{
    [Description("Header")]
    Header = 1,

    [Description("Detail")]
    Detail = 2
}

public class ReceiptConfiguredPlacementConverter :
    JsonConverter<ReceiptConfiguredPlacement>
{
    public override ReceiptConfiguredPlacement Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "Header" => ReceiptConfiguredPlacement.Header,
            "Detail" => ReceiptConfiguredPlacement.Detail,
            _ => throw new JsonException($"Unknown ReceiptFieldPlacement: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        ReceiptConfiguredPlacement value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(ReceiptConfiguredPlacement value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
