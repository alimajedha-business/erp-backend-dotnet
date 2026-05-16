using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class WarehouseLocationUsage :
    BaseEntity,
    IBaseEntityTypeConfiguration<WarehouseLocationUsage>
{
    public Guid WarehouseLocationId { get; set; }
    public decimal OccupiedMass { get; set; }
    public decimal OccupiedVolume { get; set; }

    // Optional optimistic concurrency token.
    public byte[] RowVersion { get; set; } = default!;

    public WarehouseLocation WarehouseLocation { get; set; } = default!;

    public void Map(EntityTypeBuilder<WarehouseLocationUsage> builder)
    {
        builder
            .ToTable(nameof(WarehouseLocationUsage), "Warehouse");

        builder
            .HasIndex(i => new { i.WarehouseLocationId })
            .IsUnique()
            .HasDatabaseName("IX_WarehouseLocationUsage_WarehouseLocation");

        builder
            .Property(e => e.RowVersion)
            .IsRowVersion();

        builder
            .Property(e => e.OccupiedMass)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.OccupiedVolume)
            .HasPrecision(28, 14);

        builder
            .HasOne(e => e.WarehouseLocation)
            .WithOne()
            .HasForeignKey<WarehouseLocationUsage>(x => x.WarehouseLocationId);
    }
}
