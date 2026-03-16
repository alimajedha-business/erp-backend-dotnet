using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class MeasurementDimension :
    BaseEntity,
    IBaseEntityTypeConfiguration<MeasurementDimension>
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public required bool IsDiscrete { get; set; } = false;

    public virtual List<UnitOfMeasurement> UnitOfMeasurements { get; set; } = [];

    public void Map(EntityTypeBuilder<MeasurementDimension> builder)
    {
        builder
            .ToTable(nameof(MeasurementDimension), "Warehouse");

        builder
            .Property(e => e.Title)
            .HasMaxLength(200);

        builder
            .Property(e => e.IsDiscrete)
            .HasDefaultValue(false);
    }
}
