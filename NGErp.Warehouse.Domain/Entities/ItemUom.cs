using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemUom :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ItemUom>
{
    public bool IsBase { get; private set; }
    public bool IsDefaultPurchase { get; private set; }
    public bool IsDefalulIssue { get; private set; }
    public Guid ItemId { get; private set; }
    public Guid UomId { get; private set; }

    public required Item Item { get; set; }
    public required UnitOfMeasurement Uom { get; set; }

    public void Map(EntityTypeBuilder<ItemUom> builder)
    {
        builder
            .ToTable(nameof(ItemUom), "Warehouse")
            .HasKey(k => new { k.ItemId, k.UomId });

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId);

        builder
            .HasOne(e => e.Uom)
            .WithMany()
            .HasForeignKey(e => e.UomId);
    }
}
