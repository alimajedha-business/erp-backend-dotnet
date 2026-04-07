using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;


namespace NGErp.Warehouse.Domain.Entities;

public class UnitOfMeasurementConversion :
    BaseEntity,
    IBaseEntityTypeConfiguration<UnitOfMeasurementConversion>
{
    public required decimal Factor { get; set; }
    public Guid FromUnitOfMeasurementId { get; set; }
    public Guid ToUnitOfMeasurementId { get; set; }

    public UnitOfMeasurement FromUnitOfMeasurement { get; set; } = default!;
    public UnitOfMeasurement ToUnitOfMeasurement { get; set; } = default!;

    public void Map(EntityTypeBuilder<UnitOfMeasurementConversion> builder)
    {
        builder
            .ToTable(nameof(UnitOfMeasurementConversion), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_UomConv_Factor",
                "Factor > 0"
            ));

        builder
            .HasIndex(i => new { i.FromUnitOfMeasurementId, i.ToUnitOfMeasurementId })
            .IsUnique()
            .HasDatabaseName("UX_UomConv_Unique");

        builder
            .Property(e => e.Factor)
            .HasPrecision(18, 6);

        builder
            .HasOne(e => e.FromUnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.FromUnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.ToUnitOfMeasurement)
            .WithMany()
            .HasForeignKey(e => e.ToUnitOfMeasurementId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
