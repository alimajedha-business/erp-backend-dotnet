using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class AttributeEnumValue :
    BaseEntity,
    IBaseEntityTypeConfiguration<AttributeEnumValue>
{
    public required int Code { get; set; }
    public required string Label { get; set; }
    public required Guid AttributeId { get; set; }

    public required Attribute Attribute { get; set; }

    public void Map(EntityTypeBuilder<AttributeEnumValue> builder)
    {
        builder
            .ToTable(nameof(AttributeEnumValue), "Warehouse");

        builder
            .HasIndex(i => new { i.AttributeId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_AttributeEnum_Attribute_Code");

        builder
            .Property(e => e.Label)
            .HasMaxLength(200);

        builder
            .HasOne(e => e.Attribute)
            .WithMany()
            .HasForeignKey(e => e.AttributeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
