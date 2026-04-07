using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryLotAttributeValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryLotAttributeValue>
{
    public Guid LotId { get; set; }
    public Guid AttributeId { get; set; }
    public string? ValueText { get; set; }
    public int? ValueInt { get; set; }
    public decimal? ValueDecimal { get; set; }
    public DateTime? ValueDate { get; set; }
    public bool? ValueBoolean { get; set; }
    public Guid? EnumValueId { get; set; }

    public AttributeEnumValue? EnumValue { get; set; }
    public InventoryLot Lot { get; set; } = default!;
    public Attribute Attribute { get; set; } = default!;

    public void Map(EntityTypeBuilder<InventoryLotAttributeValue> builder)
    {
        builder
            .ToTable(nameof(InventoryLotAttributeValue), "Warehouse")
            .HasKey(k => new { k.LotId, k.AttributeId });

        builder
            .HasIndex(i => new { i.AttributeId, i.ValueText })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.ValueInt })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.ValueDecimal })
                   .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.ValueDate })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.ValueBoolean })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.EnumValueId })
            .IncludeProperties(e => e.LotId);

        builder
            .Property(e => e.ValueDecimal)
            .HasPrecision(18, 6);

        builder
            .HasOne(e => e.EnumValue)
            .WithMany()
            .HasForeignKey(e => e.EnumValueId);

        builder
            .HasOne(e => e.Lot)
            .WithMany()
            .HasForeignKey(e => e.LotId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
