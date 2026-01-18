using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryMovement :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryMovement>
{
    public DateTime MovementDate { get; private set; }
    public Guid ReferenceDocId { get; private set; }
    public decimal QuantityBase { get; private set; }
    public Guid MovementTypeId { get; private set; }
    public Guid LotId { get; private set; }
    public Guid FromLocationId { get; private set; }
    public Guid ToLocationId { get; private set; }

    public required InventoryMovementType MovementType { get; set; }
    public required InventoryLot Lot { get; set; }
    public required WarehouseLocation FromLocation { get; set; }
    public required WarehouseLocation ToLocation { get; set; }

    public void Map(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder
            .ToTable(nameof(InventoryMovement), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_InventoryMovement_Qty",
                "QuantityBase <> 0"
            ));

        builder
            .HasIndex(i => new { i.CompanyId, i.MovementDate })
            .HasDatabaseName("IX_InventoryMovement_Company_Date");

        builder
            .HasIndex(i => new { i.LotId })
            .HasDatabaseName("IX_InventoryMovement_Lot");

        builder
            .HasIndex(i => new { i.FromLocationId })
            .IncludeProperties(e => e.QuantityBase)
            .HasDatabaseName("IX_InventoryMovement_FromLocation");

        builder
            .HasIndex(i => new { i.ToLocationId })
            .IncludeProperties(e => e.QuantityBase)
            .HasDatabaseName("IX_InventoryMovement_ToLocation");

        builder
            .Property(e => e.MovementDate)
            .HasColumnType("datetime2(3)");

        builder
            .Property(e => e.QuantityBase)
            .HasColumnType("decimal(23, 8");

        builder
            .HasOne(e => e.MovementType)
            .WithMany()
            .HasForeignKey(e => e.MovementTypeId);

        builder
            .HasOne(e => e.Lot)
            .WithMany()
            .HasForeignKey(e => e.LotId);

        builder
            .HasOne(e => e.FromLocation)
            .WithMany(e => e.SrcLocations)
            .HasForeignKey(e => e.FromLocationId);

        builder
            .HasOne(e => e.ToLocation)
            .WithMany(e => e.DstLocations)
            .HasForeignKey(e => e.ToLocationId);
    }
}
