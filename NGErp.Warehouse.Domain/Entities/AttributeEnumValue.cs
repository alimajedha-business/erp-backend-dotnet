using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class AttributeEnumValue : BaseEntity, IBaseEntityTypeConfiguration<AttributeEnumValue>
{
    public string Code { get; private set; } = default!;
    public string Label { get; private set; } = default!;

    [ForeignKey(nameof(Attribute))]
    public Guid AttributeId { get; private set; }

    public void Map(EntityTypeBuilder<AttributeEnumValue> builder)
    {
        builder
            .ToTable(nameof(AttributeEnumValue), "Warehouse");
    }
}
