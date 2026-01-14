using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class AttributeEnumValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<AttributeEnumValue>
{
    public string Code { get; private set; } = default!;
    public string Label { get; private set; } = default!;

    [ForeignKey(nameof(Attribute))]
    public Guid AttributeId { get; private set; }

    public void Map(EntityTypeBuilder<AttributeEnumValue> builder)
    {
        builder
            .ToTable(nameof(AttributeEnumValue), "Warehouse");

        builder
            .HasIndex(i => new { i.AttributeId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_AttributeEnum_Attribute_Code");

        builder
            .Property(e => e.Code)
            .HasMaxLength(80);

        builder
            .Property(e => e.Label)
            .HasMaxLength(200);
    }
}
