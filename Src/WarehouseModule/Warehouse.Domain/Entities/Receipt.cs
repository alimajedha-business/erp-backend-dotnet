using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

/* This table represents:
 * Fields here that are always header and not configurable. 
 */

public class Receipt :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Receipt>
{
    public long Number { get; set; }
    public DateOnly ReceiptDate { get; set; }
    public Guid ReceiptTypeId { get; set; }
    public ReceiptStatus Status { get; set; } = ReceiptStatus.Draft;
    public string? Description { get; set; }

    public ReceiptType ReceiptType { get; set; } = null!;

    public ICollection<ReceiptLine> ReceiptLines { get; set; } = [];
    public ICollection<ReceiptFieldValue> ReceiptFieldValues { get; set; } = [];

    public void Map(EntityTypeBuilder<Receipt> builder)
    {
        builder
            .ToTable(nameof(Receipt), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Number })
            .IsUnique();

        builder.HasIndex(i => new
        {
            i.CompanyId,
            i.ReceiptTypeId,
            i.ReceiptDate
        });

        builder
            .HasOne(e => e.ReceiptType)
            .WithMany()
            .HasForeignKey(e => e.ReceiptTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

[JsonConverter(typeof(ReceiptStatusConverter))]
public enum ReceiptStatus
{
    [Description("Draft")]
    Draft = 1,

    [Description("Posted")]
    Posted = 2,

    [Description("Cancelled")]
    Cancelled = 3
}

public class ReceiptStatusConverter :
    JsonConverter<ReceiptStatus>
{
    public override ReceiptStatus Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "Draft" => ReceiptStatus.Draft,
            "Posted" => ReceiptStatus.Posted,
            "Cancelled" => ReceiptStatus.Cancelled,
            _ => throw new JsonException($"Unknown ReceiptStatus: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        ReceiptStatus value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(ReceiptStatus value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}