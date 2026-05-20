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

    public string? StringValue { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public bool? BooleanValue { get; set; }
    public Guid? EnumReferenceId { get; set; }

    public InventoryLot InventoryLot { get; set; } = default!;
    public Attribute Attribute { get; set; } = default!;
    public AttributeEnumValue? EnumValue { get; set; }

    public void Map(EntityTypeBuilder<InventoryLotAttributeValue> builder)
    {
        builder
            .ToTable(nameof(InventoryLotAttributeValue), "Warehouse");

        builder
            .HasIndex(i => new { i.LotId, i.AttributeId })
            .IsUnique()
            .HasDatabaseName("UX_InventoryLotAttributeValue_Lot_Attribute");

        builder
            .HasIndex(i => new { i.AttributeId, i.StringValue })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.IntegerValue })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.DecimalValue })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.DateValue })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.BooleanValue })
            .IncludeProperties(e => e.LotId);

        builder
            .HasIndex(i => new { i.AttributeId, i.EnumReferenceId })
            .IncludeProperties(e => e.LotId);

        builder
            .Property(e => e.DecimalValue)
            .HasPrecision(18, 6);

        builder
            .HasOne(e => e.EnumValue)
            .WithMany()
            .HasForeignKey(e => e.EnumReferenceId);

        builder
            .HasOne(e => e.InventoryLot)
            .WithMany(e => e.AttributeValues)
            .HasForeignKey(e => e.LotId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
