using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;
namespace NGErp.Warehouse.Domain.Entities;

public class ItemAttributeValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ItemAttributeValue>
{
    public required Guid ItemId { get; set; }
    public required Guid AttributeId { get; set; }
    public string? ValueText { get; set; }
    public int? ValueInt { get; set; }
    public decimal? ValueDecimal { get; set; }
    public DateTime? ValueDate { get; set; }
    public bool? ValueBoolean { get; set; }
    public Guid? EnumValueId { get; set; }

    public AttributeEnumValue? EnumValue { get; set; }
    public required Item Item { get; set; }
    public required Attribute Attribute { get; set; }

    public void Map(EntityTypeBuilder<ItemAttributeValue> builder)
    {
        builder
            .ToTable(nameof(ItemAttributeValue), "Warehouse")
            .HasKey(k => new { k.ItemId, k.AttributeId });

        builder
            .Property(e => e.ValueText)
            .HasMaxLength(50);

        builder
            .Property(e => e.ValueDecimal)
            .HasColumnType("decimal(23, 8)");

        builder
            .Property(e => e.ValueDate)
            .HasColumnType("datetime2(3)");

        builder
            .HasOne(e => e.EnumValue)
            .WithMany()
            .HasForeignKey(e => e.EnumValueId);

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
