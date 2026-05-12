using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryBalance :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryBalance>
{
    public Guid ItemId { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid WarehouseLocationId { get; set; }
    public Guid InventoryLotId { get; set; }

    // Quantity in the item’s base stock UoM.
    public decimal OnHandQuantity { get; set; }
    public decimal ReservedQuantity { get; set; }
    public decimal BlockedQuantity { get; set; }

    // Snapshot footprint of this stock in this location.
    public decimal OccupiedMass { get; set; }
    public decimal OccupiedVolume { get; set; }

    // Optional optimistic concurrency token.
    public byte[] RowVersion { get; set; } = default!;

    public decimal AvailableQuantity => OnHandQuantity - ReservedQuantity - BlockedQuantity;

    public void Map(EntityTypeBuilder<InventoryBalance> builder)
    {
        builder
            .ToTable(nameof(InventoryBalance), "Warehouse");

        builder.HasIndex(e => new
        {
            e.CompanyId,
            e.ItemId,
            e.WarehouseId,
            e.WarehouseLocationId,
            e.InventoryLotId
        }).IsUnique();

        builder
            .Property(e => e.RowVersion)
            .IsRowVersion();
    }
}