using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceLineMeasurementValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<RemittanceLineMeasurementValue>
{
    public Guid RemittanceLineId { get; set; }
    public Guid ItemUnitOfMeasurementId { get; set; }
    public decimal Quantity { get; set; }

    public RemittanceLine RemittanceLine { get; set; } = null!;
    public ItemUnitOfMeasurement ItemUnitOfMeasurement { get; set; } = null!;

    public void Map(EntityTypeBuilder<RemittanceLineMeasurementValue> builder)
    {
        builder.ToTable(nameof(RemittanceLineMeasurementValue), "Warehouse");

        builder.HasIndex(i => new { i.RemittanceLineId, i.ItemUnitOfMeasurementId }).IsUnique();
        builder.Property(e => e.Quantity).HasPrecision(28, 14);

        builder.HasOne(e => e.RemittanceLine)
            .WithMany(e => e.RemittanceLineMeasurementValues)
            .HasForeignKey(e => e.RemittanceLineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ItemUnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.ItemUnitOfMeasurementId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
