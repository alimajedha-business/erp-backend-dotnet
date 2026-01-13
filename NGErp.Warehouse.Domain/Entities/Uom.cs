using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class Uom : BaseEntity, IBaseEntityTypeConfiguration<Uom>
{
    public string Dimension { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Symbol { get; private set; } = default!;
    public bool IsDiscrete { get; private set; }

    public static void Configure(EntityTypeBuilder<Uom> builder)
    {
        builder
            .HasIndex(i => new { i.Dimension, i.Title })
            .IsUnique()
            .HasDatabaseName("UX_Uom_Dimension_Title");
    }

    public void Map(EntityTypeBuilder<Uom> builder)
    {
        builder
            .ToTable(nameof(Uom), "Warehouse");

        builder
            .Property(e => e.Dimension)
            .HasMaxLength(50);

        builder
            .Property(e => e.Title)
            .HasMaxLength(100);

        builder
            .Property(e => e.Symbol)
            .HasMaxLength(20);
    }
}
