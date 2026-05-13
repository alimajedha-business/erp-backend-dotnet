using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ReceiptLineMeasurementValue :
    BaseEntity,
    IBaseEntityTypeConfiguration<ReceiptLineMeasurementValue>
{
    public Guid ReceiptLineId { get; set; }
    public Guid ItemUnitOfMeasurementId { get; set; }
    public decimal Quantity { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Volume { get; set; }

    public ReceiptLine ReceiptLine { get; set; } = default!;
    public ItemUnitOfMeasurement ItemUnitOfMeasurement { get; set; } = default!;

    public void Map(EntityTypeBuilder<ReceiptLineMeasurementValue> builder)
    {
        builder
            .ToTable(nameof(ReceiptLineMeasurementValue), "Warehouse");

        builder
            .HasIndex(i => new
            { 
                i.ReceiptLineId,
                i.ItemUnitOfMeasurementId
            }).IsUnique();

        builder
            .Property(e => e.Quantity)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.Weight)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.Length)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.Width)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.Height)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.Volume)
            .HasPrecision(28, 14);

        builder
            .HasOne(e => e.ReceiptLine)
            .WithMany(e => e.ReceiptLineMeasurementValues)
            .HasForeignKey(e => e.ReceiptLineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(e => e.ItemUnitOfMeasurement)
            .WithMany(e => e.ReceiptLineMeasurementValues)
            .HasForeignKey(e => e.ItemUnitOfMeasurementId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
