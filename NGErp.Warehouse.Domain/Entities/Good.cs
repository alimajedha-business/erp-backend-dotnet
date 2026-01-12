using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class Good : BaseEntity, IBaseEntityTypeConfiguration<Good>
{
    public string Sku { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public bool IsActive { get; private set; }

    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; private set; }

    public void Map(EntityTypeBuilder<Good> builder)
    {
        builder.
            ToTable(nameof(Good), "Warehouse");

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
