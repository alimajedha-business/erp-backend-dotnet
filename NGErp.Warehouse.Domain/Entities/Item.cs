using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class Item :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Item>
{
    public string Sku { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public bool IsActive { get; private set; }

    [ForeignKey(nameof(Category))]
    public required Category Category { get; set; }
    public Guid CategoryId { get; private set; }

    public void Map(EntityTypeBuilder<Item> builder)
    {
        builder.
            ToTable(nameof(Item), "Warehouse")
            .HasOne(o => o.Category);

        builder
            .HasOne(o => o.Category);

        builder
            .HasIndex(x => x.Sku);

        builder
            .Property(e => e.Sku)
            .HasMaxLength(80);

        builder
            .Property(e => e.Title)
            .HasMaxLength(255);
    }
}
