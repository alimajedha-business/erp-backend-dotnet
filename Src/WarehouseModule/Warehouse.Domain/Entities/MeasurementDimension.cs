using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class MeasurementDimension :
    BaseEntity,
    IBaseEntityTypeConfiguration<MeasurementDimension>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public bool IsDiscrete { get; private set; } = false;

    public virtual List<UnitOfMeasurement> Measurements { get; set; } = [];

    public void Map(EntityTypeBuilder<MeasurementDimension> builder)
    {
        builder
            .ToTable(nameof(MeasurementDimension), "Warehouse");

        builder
            .Property(e => e.Code)
            .HasMaxLength(64);

        builder
            .Property(e => e.Title)
            .HasMaxLength(200);

        builder
            .Property(e => e.IsDiscrete)
            .HasDefaultValue(false);
    }
}
