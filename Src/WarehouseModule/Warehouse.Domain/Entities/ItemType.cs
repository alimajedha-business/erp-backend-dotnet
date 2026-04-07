using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemType :
    BaseEntity,
    IBaseEntityTypeConfiguration<ItemType>
{
    public int Code { get; set; }
    public required string Title { get; set; }

    public virtual List<Item> Items { get; set; } = [];

    public void Map(EntityTypeBuilder<ItemType> builder)
    {
        builder.
            ToTable(nameof(ItemType), "Warehouse");

        builder
            .HasIndex(i => new { i.Code })
            .IsUnique()
            .HasDatabaseName("UX_ItemType_Code");

        builder
            .Property(e => e.Title)
            .HasMaxLength(200);
    }
}
