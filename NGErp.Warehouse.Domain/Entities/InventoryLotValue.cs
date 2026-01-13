using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class InventoryLotValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<InventoryLotValue>
{
    public string ValueText { get; private set; } = default!;
    public int ValueInt { get; private set; }
    public decimal ValueDecimal { get; private set; }
    public DateTime ValueDate { get; private set; }
    public bool ValueBoolean { get; private set; }

    [ForeignKey(nameof(AttributeEnumValue))]
    public Guid EnumValueId { get; private set; }

    [ForeignKey(nameof(InventoryLot))]
    public Guid LotId { get; private set; }

    [ForeignKey(nameof(Attribute))]
    public Guid AttributeId { get; private set; }

    public void Map(EntityTypeBuilder<InventoryLotValue> builder)
    {
        builder
            .ToTable(nameof(InventoryLotValue), "Warehouse");
    }
}
