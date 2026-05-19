using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Remittance :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Remittance>
{
    public long Number { get; set; }
    public DateOnly RemittanceDate { get; set; }
    public Guid RemittanceTypeId { get; set; }
    public RemittanceStatus Status { get; set; } = RemittanceStatus.Draft;
    public string? Description { get; set; }

    public RemittanceType RemittanceType { get; set; } = null!;

    public ICollection<RemittanceLine> RemittanceLines { get; set; } = [];
    public ICollection<RemittanceFieldValue> RemittanceFieldValues { get; set; } = [];

    public void Map(EntityTypeBuilder<Remittance> builder)
    {
        builder.ToTable(nameof(Remittance), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Number })
            .IsUnique();

        builder.HasIndex(i => new
        {
            i.CompanyId,
            i.RemittanceTypeId,
            i.RemittanceDate
        });

        builder
            .HasOne(e => e.RemittanceType)
            .WithMany()
            .HasForeignKey(e => e.RemittanceTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

[JsonConverter(typeof(RemittanceStatusConverter))]
public enum RemittanceStatus
{
    [Description("Draft")]
    Draft = 1,

    [Description("Posted")]
    Posted = 2,

    [Description("Cancelled")]
    Cancelled = 3
}

public class RemittanceStatusConverter : JsonConverter<RemittanceStatus>
{
    public override RemittanceStatus Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "Draft" => RemittanceStatus.Draft,
            "Posted" => RemittanceStatus.Posted,
            "Cancelled" => RemittanceStatus.Cancelled,
            _ => throw new JsonException($"Unknown RemittanceStatus: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        RemittanceStatus value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(RemittanceStatus value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
