using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.General.Domain;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class WarehouseLocation : BaseEntity, IBaseEntityTypeConfiguration<WarehouseLocation>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;

    [ForeignKey(nameof(WarehouseLocation))]
    public Guid ParentLocationId { get; private set; }

    [ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; private set; }

    public void Map(EntityTypeBuilder<WarehouseLocation> builder)
    {
        builder
            .ToTable(nameof(WarehouseLocation), "Warehouse");
    }
}
