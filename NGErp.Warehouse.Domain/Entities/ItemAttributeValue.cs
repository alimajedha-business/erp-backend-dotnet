using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class ItemAttributeValue : BaseEntity, IBaseEntityTypeConfiguration<ItemAttributeValue>
{
    public string ValueText { get; private set; } = default!;
    public int ValueInt { get; private set; }
    public decimal ValueDecimal { get; private set; }
    public DateTime ValueDate { get; private set; }
    public bool ValueBoolean { get; private set; }

    [ForeignKey(nameof(AttributeEnumValue))]
    public Guid EnumValueId { get; private set; }

    [ForeignKey(nameof(Item))]
    public Guid ItemId { get; private set; }

    [ForeignKey(nameof(Attribute))]
    public Guid AttributeId { get; private set; }

    public void Map(EntityTypeBuilder<ItemAttributeValue> builder)
    {
        builder
            .ToTable(nameof(ItemAttributeValue), "Warehouse");
    }
}
