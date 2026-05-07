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
    public required long Number { get; set; }
    public DateOnly ReceiptDate { get; set; }
    public Guid ReceiptTypeId { get; set; }
    public string? Description { get; set; }

    public ReceiptType ReceiptType { get; set; } = null!;

    public ICollection<ReceiptLine> Lines { get; set; } = [];
    public ICollection<ReceiptFieldValue> FieldValues { get; set; } = [];

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
    }
}
