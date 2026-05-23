using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

/* This table stores:
 * The actual values of configured fields.
 */

public class ReceiptFieldValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ReceiptFieldValue>
{
    public Guid ReceiptId { get; set; }
    
    // Null means header value.
    // Not null means detail/grid-row value.
    public Guid? ReceiptLineId { get; set; }
    public Guid FieldDefinitionId { get; set; }

    public string? StringValue { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }

    public Receipt Receipt { get; set; } = default!;
    public ReceiptLine? ReceiptLine { get; set; }
    public ReceiptFieldDefinition FieldDefinition { get; set; } = default!;

    public void Map(EntityTypeBuilder<ReceiptFieldValue> builder)
    {
        builder
            .ToTable(nameof(ReceiptFieldValue), "Warehouse");

        // One header value per field.
        builder.HasIndex(i => new
        {
            i.ReceiptId,
            i.FieldDefinitionId
        })
        .IsUnique()
        .HasFilter("[ReceiptLineId] IS NULL");

        // One detail value per row per field.
        builder.HasIndex(i => new
        {
            i.ReceiptLineId,
            i.FieldDefinitionId
        })
        .IsUnique()
        .HasFilter("[ReceiptLineId] IS NOT NULL");

        builder
            .Property(e => e.DecimalValue)
            .HasPrecision(22, 4);

        builder
            .HasOne(e => e.Receipt)
            .WithMany(e => e.ReceiptFieldValues)
            .HasForeignKey(e => e.ReceiptId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.ReceiptLine)
            .WithMany(e => e.ReceiptFieldValues)
            .HasForeignKey(e => e.ReceiptLineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.FieldDefinition)
            .WithMany()
            .HasForeignKey(e => e.FieldDefinitionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
