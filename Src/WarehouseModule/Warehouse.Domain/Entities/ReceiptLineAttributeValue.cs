using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

/* This table stores:
 * Dynamic item attributes such as expiry date, batch number, color, etc.
 */

public class ReceiptLineAttributeValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ReceiptLineAttributeValue>
{
    public Guid ReceiptLineId { get; set; }
    public Guid ItemAttributeId { get; set; }

    public string? StringValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }

    public ReceiptLine ReceiptLine { get; set; } = null!;
    public ItemAttribute ItemAttribute { get; set; } = null!;

    public void Map(EntityTypeBuilder<ReceiptLineAttributeValue> builder)
    {
        builder
            .ToTable(nameof(ReceiptLineAttributeValue), "Warehouse");

        builder.HasIndex(i => new
        {
            i.ReceiptLineId,
            i.ItemAttributeId
        }).IsUnique();

        builder
            .Property(e => e.DecimalValue)
            .HasPrecision(22, 4);

        builder
            .HasOne(e => e.ReceiptLine)
            .WithMany(e => e.ReceiptLineAttributeValues)
            .HasForeignKey(e => e.ReceiptLineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.ItemAttribute)
            .WithMany(e => e.ReceiptLineAttributeValues)
            .HasForeignKey(e => e.ItemAttributeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
