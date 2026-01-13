using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class ItemUom :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ItemUom>
{
    public bool IsBase { get; private set; }
    public bool IsDefaultPurchase { get; private set; }
    public bool IsDefalulIssue { get; private set; }

    public void Map(EntityTypeBuilder<ItemUom> builder)
    {
        builder
            .ToTable(nameof(ItemUom), "Warehouse");
    }
}
