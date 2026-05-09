using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

/* This table represents:
 * Fields here that are always header and not configurable. 
 */

public class Receipt :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Receipt>
{
    public long Number { get; set; }
    public DateOnly ReceiptDate { get; set; }
    public Guid ReceiptTypeId { get; set; }
    public string? Description { get; set; }

    public ReceiptType ReceiptType { get; set; } = null!;

    public ICollection<ReceiptLine> ReceiptLines { get; set; } = [];
    public ICollection<ReceiptFieldValue> ReceiptFieldValues { get; set; } = [];

    public void Map(EntityTypeBuilder<Receipt> builder)
    {
        builder
            .ToTable(nameof(Receipt), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Number })
            .IsUnique();

        builder.HasIndex(i => new
        {
            i.CompanyId,
            i.ReceiptTypeId,
            i.ReceiptDate
        });

        builder
            .HasOne(e => e.ReceiptType)
            .WithMany()
            .HasForeignKey(e => e.ReceiptTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
