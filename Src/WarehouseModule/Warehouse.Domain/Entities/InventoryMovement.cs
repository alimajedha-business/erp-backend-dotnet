using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryMovement :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryMovement>
{
    public required DateTime MovementDate { get; set; }
    public Guid ReferenceDocId { get; set; } = default!;
    public decimal QuantityBase { get; set; }
    public Guid MovementTypeId { get; set; } = default!;
    public Guid LotId { get; set; } = default!;
    public Guid FromLocationId { get; set; } = default!;
    public Guid ToLocationId { get; set; } = default!;

    public InventoryMovementType MovementType { get; set; } = default!;
    public InventoryLot Lot { get; set; } = default!;
    public WarehouseLocation FromLocation { get; set; } = default!;
    public WarehouseLocation ToLocation { get; set; } = default!;

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
            .HasPrecision(3);

        builder
            .Property(e => e.QuantityBase)
            .HasPrecision(23, 8);

        builder
            .HasOne(e => e.MovementType)
            .WithMany()
            .HasForeignKey(e => e.MovementTypeId);

        builder
            .HasOne(e => e.Lot)
            .WithMany()
            .HasForeignKey(e => e.LotId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.FromLocation)
            .WithMany(e => e.SrcLocations)
            .HasForeignKey(e => e.FromLocationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.ToLocation)
            .WithMany(e => e.DstLocations)
            .HasForeignKey(e => e.ToLocationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
