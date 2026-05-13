using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

/* This table represents:
 * Rows in the grid is one item/good in the receipt.
 */

public class ReceiptLine :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ReceiptLine>
{
    public Guid ReceiptId { get; set; }
    public int RowNumber { get; set; }
    public Guid ItemId { get; set; }
    public Guid WarehouseLocationId { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }

    public Receipt Receipt { get; set; } = default!;
    public Item Item { get; set; } = default!;
    public WarehouseLocation WarehouseLocation { get; set; } = default!;

    public ICollection<ReceiptFieldValue> ReceiptFieldValues { get; set; } = [];
    public ICollection<ReceiptLineAttributeValue> ReceiptLineAttributeValues { get; set; } = [];
    public ICollection<ReceiptLineMeasurementValue> ReceiptLineMeasurementValues { get; set; } = [];

    public void Map(EntityTypeBuilder<ReceiptLine> builder)
    {
        builder
            .ToTable(nameof(ReceiptLine), "Warehouse");

        builder
            .HasIndex(i => new { i.ReceiptId, i.RowNumber })
            .IsUnique();

        builder
            .HasIndex(i => new
            { 
                i.CompanyId,
                i.ItemId,
                i.WarehouseLocationId
            });

        builder
            .Property(e => e.UnitPrice)
            .HasPrecision(22, 4);

        builder
            .Property(e => e.TotalPrice)
            .HasPrecision(22, 4);

        builder
            .HasOne(e => e.Receipt)
            .WithMany(e => e.ReceiptLines)
            .HasForeignKey(e => e.ReceiptId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.WarehouseLocation)
            .WithMany()
            .HasForeignKey(e => e.WarehouseLocationId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
