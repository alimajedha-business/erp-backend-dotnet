using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryLotAttributeValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryLotAttributeValue>
{
    public Guid LotId { get; private set; }
    public Guid AttributeId { get; private set; }
    public string? ValueText { get; private set; } = default!;
    public int? ValueInt { get; private set; }
    public decimal? ValueDecimal { get; private set; }
    public DateTime? ValueDate { get; private set; }
    public bool? ValueBoolean { get; private set; }
    public Guid? EnumValueId { get; private set; }

    public required AttributeEnumValue? EnumValue { get; set; }
    public required InventoryLot Lot { get; set; }
    public required Attribute Attribute { get; set; }

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
