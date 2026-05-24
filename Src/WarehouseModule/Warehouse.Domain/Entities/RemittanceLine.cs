using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceLine :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<RemittanceLine>
{
    public Guid RemittanceId { get; set; }
    public int RowNumber { get; set; }
    public Guid ItemId { get; set; }
    public Guid WarehouseLocationId { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Volume { get; set; }
    public Guid? PreferredMassUnitId { get; set; }
    public Guid? PreferredVolumeUnitId { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? BatchNumber { get; set; }
    public string? SerialNumber { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public string? Description { get; set; }

    public Remittance Remittance { get; set; } = default!;
    public Item Item { get; set; } = default!;
    public WarehouseLocation WarehouseLocation { get; set; } = default!;
    public SiUnit? PreferredMassUnit { get; set; }
    public SiUnit? PreferredVolumeUnit { get; set; }

    public ICollection<RemittanceFieldValue> RemittanceFieldValues { get; set; } = [];
    public ICollection<RemittanceLineAttributeValue> RemittanceLineAttributeValues { get; set; } = [];
    public ICollection<RemittanceLineMeasurementValue> RemittanceLineMeasurementValues { get; set; } = [];

    public void Map(EntityTypeBuilder<RemittanceLine> builder)
    {
        builder.ToTable(nameof(RemittanceLine), "Warehouse");

        builder
            .HasIndex(i => new { i.RemittanceId, i.RowNumber })
            .IsUnique();

        builder.HasIndex(i => new
        {
            i.CompanyId,
            i.ItemId,
            i.WarehouseLocationId
        });

        builder.Property(e => e.Weight).HasPrecision(28, 14);
        builder.Property(e => e.Volume).HasPrecision(28, 14);
        builder.Property(e => e.UnitPrice).HasPrecision(22, 4);
        builder.Property(e => e.TotalPrice).HasPrecision(22, 4);

        builder
            .HasOne(e => e.Remittance)
            .WithMany(e => e.RemittanceLines)
            .HasForeignKey(e => e.RemittanceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.WarehouseLocation)
            .WithMany()
            .HasForeignKey(e => e.WarehouseLocationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
