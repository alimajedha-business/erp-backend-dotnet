using System.ComponentModel.DataAnnotations.Schema;

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

    [ForeignKey(nameof(InventoryMovementType))]
    public Guid MovementTypeId { get; private set; }

    [ForeignKey(nameof(InventoryLot))]
    public Guid LotId { get; private set; }

    [ForeignKey(nameof(WarehouseLocation))]
    public Guid FromLocationId { get; private set; }

    [ForeignKey(nameof(WarehouseLocation))]
    public Guid ToLocationId { get; private set; }

    public static void Configure(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder
            .HasIndex(i => new { i.CompanyId, i.MovementDate })
            .HasDatabaseName("IX_InventoryMovement_Company_Time");
    }

    public void Map(EntityTypeBuilder<InventoryMovement> builder)
    {
        builder
            .ToTable(nameof(InventoryMovement), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_InventoryMovement_Qty",
                "QuantityBase <> 0"
            ));

        builder
            .Property(e => e.MovementDate)
            .HasColumnType("DATETIME2(3)");

        builder
            .Property(e => e.QuantityBase)
            .HasColumnType("DECIMAL(38, 12");
    }
}
