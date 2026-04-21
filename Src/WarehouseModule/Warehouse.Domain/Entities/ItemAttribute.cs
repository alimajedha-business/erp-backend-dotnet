using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemAttribute :
    BaseEntity,
    IBaseEntityTypeConfiguration<ItemAttribute>
{
    public Guid ItemId { get; set; }
    public Guid AttributeId { get; set; }

    public Item Item { get; set; } = default!;
    public Attribute Attribute { get; set; } = default!;

    public void Map(EntityTypeBuilder<ItemAttribute> builder)
    {
        builder
            .ToTable(nameof(ItemAttribute), "Warehouse")
            .HasKey(k => new { k.ItemId, k.AttributeId });

        builder
            .HasOne(e => e.Item)
            .WithMany(e => e.ItemAttributes)
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Attribute)
            .WithMany(e => e.ItemAttributes)
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
