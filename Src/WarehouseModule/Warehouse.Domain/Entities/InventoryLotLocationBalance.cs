using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryLotLocationBalance :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryLotLocationBalance>
{
    public Guid LotId { get; set; }
    public Guid WarehouseLocationId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Mass { get; set; }
    public decimal Volume { get; set; }

    public InventoryLot Lot { get; set; } = default!;
    public WarehouseLocation WarehouseLocation { get; set; } = default!;

    public void Map(EntityTypeBuilder<InventoryLotLocationBalance> builder)
    {
        builder
            .ToTable(nameof(InventoryLotLocationBalance), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.LotId, i.WarehouseLocationId })
            .IsUnique()
            .HasDatabaseName("UX_InventoryLotLocationBalance_Company_Lot_Location");

        builder
            .HasIndex(i => new { i.CompanyId, i.WarehouseLocationId })
            .IncludeProperties(e => new { e.Quantity, e.Mass, e.Volume })
            .HasDatabaseName("IX_InventoryLotLocationBalance_Location");

        builder
            .HasIndex(i => new { i.CompanyId, i.LotId })
            .IncludeProperties(e => new { e.Quantity, e.Mass, e.Volume })
            .HasDatabaseName("IX_InventoryLotLocationBalance_Lot");

        builder
            .Property(e => e.Quantity)
            .HasPrecision(23, 8);

        builder
            .Property(e => e.Mass)
            .HasPrecision(28, 14);

        builder
            .Property(e => e.Volume)
            .HasPrecision(28, 14);

        builder
            .HasOne(e => e.Lot)
            .WithMany()
            .HasForeignKey(e => e.LotId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.WarehouseLocation)
            .WithMany()
            .HasForeignKey(e => e.WarehouseLocationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
