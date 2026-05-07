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
    public Guid UnitOfMeasurementId { get; set; }
    public decimal Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }

    public Receipt Receipt { get; set; } = null!;
    public Item Item { get; set; } = null!;
    public UnitOfMeasurement UnitOfMeasurement { get; set; } = null!;

    public void Map(EntityTypeBuilder<ReceiptLine> builder)
    {
        builder
            .ToTable(nameof(ReceiptLine), "Warehouse");

        builder
            .HasIndex(i => new { i.ReceiptId, i.RowNumber })
            .IsUnique();

        builder
            .HasIndex(i => new { i.CompanyId, i.ItemId});
    }
}
