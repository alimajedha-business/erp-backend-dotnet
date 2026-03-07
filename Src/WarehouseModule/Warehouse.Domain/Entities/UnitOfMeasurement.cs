using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class UnitOfMeasurement :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<UnitOfMeasurement>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Symbol { get; private set; } = default!;
    public Guid DimensionId { get; private set; }

    public required MeasurementDimension Dimension { get; set; }

    public void Map(EntityTypeBuilder<UnitOfMeasurement> builder)
    {
        builder
            .ToTable(nameof(UnitOfMeasurement), "Warehouse");

        builder
            .HasIndex(i => new { i.Dimension, i.Title })
            .IsUnique()
            .HasDatabaseName("UX_Uom_Dimension_Title");

        builder
            .Property(e => e.Code)
            .HasMaxLength(50);

        builder
            .Property(e => e.Title)
            .HasMaxLength(100);

        builder
            .Property(e => e.Symbol)
            .HasMaxLength(20);

        builder
            .HasOne(e => e.Dimension)
            .WithMany(e => e.Measurements)
            .HasForeignKey(e => e.Dimension)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
