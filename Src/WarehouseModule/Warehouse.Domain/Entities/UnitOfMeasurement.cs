using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class UnitOfMeasurement :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<UnitOfMeasurement>
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required string Symbol { get; set; }
    public required Guid MeasurementDimensionId { get; set; }

    public required MeasurementDimension MeasurementDimension { get; set; }

    public void Map(EntityTypeBuilder<UnitOfMeasurement> builder)
    {
        builder
            .ToTable(nameof(UnitOfMeasurement), "Warehouse");

        builder
            .HasIndex(i => new { i.MeasurementDimensionId, i.Title })
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
            .HasOne(e => e.MeasurementDimension)
            .WithMany(e => e.UnitOfMeasurements)
            .HasForeignKey(e => e.MeasurementDimensionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
