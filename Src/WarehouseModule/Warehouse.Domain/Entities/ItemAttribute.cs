using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemAttribute :
    BaseEntity,
    IBaseEntityTypeConfiguration<ItemAttribute>
{
    public required Guid ItemId { get; set; }
    public required Guid AttributeId { get; set; }

    public required Item Item { get; set; }
    public required Attribute Attribute { get; set; }

    public void Map(EntityTypeBuilder<ItemAttribute> builder)
    {
        builder
            .ToTable(nameof(ItemAttribute), "Warehouse")
            .HasKey(k => new { k.ItemId, k.AttributeId });

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId);

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
