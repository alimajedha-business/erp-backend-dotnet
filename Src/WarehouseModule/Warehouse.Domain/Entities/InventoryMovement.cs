using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryMovement :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryMovement>
{
    public Guid? SourceDocumentId { get; set; }
    public Guid? SourceDocumentLineId { get; set; }
    public required DateTime MovementDate { get; set; }
    public decimal Quantity { get; set; }
    public decimal Mass { get; set; }
    public decimal Volume { get; set; }
    public InventoryMovementDirection Direction { get; set; }
    public Guid MovementTypeId { get; set; }
    public Guid LotId { get; set; } = default!;
    public Guid? FromLocationId { get; set; }
    public Guid? ToLocationId { get; set; }

    public InventoryMovementType? MovementType { get; set; }
    public InventoryLot Lot { get; set; } = default!;
    public WarehouseLocation? FromLocation { get; set; }
    public WarehouseLocation? ToLocation { get; set; }

    public void Map(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder
            .ToTable(nameof(InventoryMovement), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_InventoryMovement_Qty",
                "Quantity <> 0"
            ));

        builder
            .HasIndex(i => new { i.CompanyId, i.MovementDate })
            .HasDatabaseName("IX_InventoryMovement_Company_Date");

        builder
            .HasIndex(i => new { i.SourceDocumentId, i.SourceDocumentLineId })
            .HasDatabaseName("IX_InventoryMovement_SourceDocument");

        builder
            .HasIndex(i => new { i.LotId })
            .HasDatabaseName("IX_InventoryMovement_Lot");

        builder
            .HasIndex(i => new { i.FromLocationId })
            .IncludeProperties(e => new { e.Quantity, e.Mass, e.Volume })
            .HasDatabaseName("IX_InventoryMovement_FromLocation");

        builder
            .HasIndex(i => new { i.ToLocationId })
            .IncludeProperties(e => new { e.Quantity, e.Mass, e.Volume })
            .HasDatabaseName("IX_InventoryMovement_ToLocation");

        builder
            .Property(e => e.MovementDate)
            .HasPrecision(3);

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
            .HasOne(e => e.MovementType)
            .WithMany()
            .HasForeignKey(e => e.MovementTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Lot)
            .WithMany()
            .HasForeignKey(e => e.LotId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.FromLocation)
            .WithMany()
            .HasForeignKey(e => e.FromLocationId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.ToLocation)
            .WithMany()
            .HasForeignKey(e => e.ToLocationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

public enum InventoryMovementDirection
{
    Inbound = 1,
    Outbound = 2,
    Transfer = 3
}
