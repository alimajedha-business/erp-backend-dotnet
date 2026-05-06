using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Warehouse :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Warehouse>
{
    public required int Code { get; set; }
    public required string Title { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal MaxMonetaryValue { get; set; }
    public Guid WarehouseTypeId { get; set; }
    public Guid CompanyUnitId { get; set; }

    #region Warehouse
    public Guid? WarehouseSlaveAccountCompanyId { get; set; }
    public string? WarehouseAccountMasterValue { get; set; }
    public string? WarehouseAccountSlaveValue { get; set; }
    public string? WarehouseAccountDetailed1Value { get; set; }
    public string? WarehouseAccountDetailed2Value { get; set; }
    #endregion

    #region ReturnFromPurchase
    public Guid? ReturnFromPurchaseSlaveAccountCompanyId { get; set; }
    public string? ReturnFromPurchaseAccountMasterValue { get; set; }
    public string? ReturnFromPurchaseAccountSlaveValue { get; set; }
    public string? ReturnFromPurchaseAccountDetailed1Value { get; set; }
    public string? ReturnFromPurchaseAccountDetailed2Value { get; set; }
    #endregion

    public WarehouseType WarehouseType { get; set; } = default!;
    public CompanyUnit CompanyUnit { get; set; } = default!;

    public ICollection<ItemWarehouse> ItemWarehouses { get; set; } = [];

    public void Map(EntityTypeBuilder<Warehouse> builder)
    {
        builder
            .ToTable(nameof(Warehouse), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Warehouse_Company_Code");

        builder
            .Property(e => e.Title)
            .HasMaxLength(250);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder
            .Property(e => e.MaxMonetaryValue)
            .HasPrecision(22, 4);

        builder
            .Property(e => e.WarehouseAccountMasterValue)
            .HasMaxLength(20);

        builder
            .Property(e => e.WarehouseAccountSlaveValue)
            .HasMaxLength(20);

        builder
            .Property(e => e.WarehouseAccountDetailed1Value)
            .HasMaxLength(20);

        builder
            .Property(e => e.WarehouseAccountDetailed2Value)
            .HasMaxLength(20);

        builder
            .Property(e => e.ReturnFromPurchaseAccountMasterValue)
            .HasMaxLength(20);

        builder
            .Property(e => e.ReturnFromPurchaseAccountSlaveValue)
            .HasMaxLength(20);

        builder
            .Property(e => e.ReturnFromPurchaseAccountDetailed1Value)
            .HasMaxLength(20);

        builder
            .HasOne(e => e.WarehouseType)
            .WithMany()
            .HasForeignKey(e => e.WarehouseTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.CompanyUnit)
            .WithMany()
            .HasForeignKey(e => e.CompanyUnitId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
