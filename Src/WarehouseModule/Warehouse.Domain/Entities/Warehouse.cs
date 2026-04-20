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

    #region Sale
    public Guid? SaleSlaveAccountCompanyId { get; set; }
    public string? SaleAccountMasterValue { get; set; }
    public string? SaleAccountSlaveValue { get; set; }
    public string? SaleAccountDetailed1Value { get; set; }
    public string? SaleAccountDetailed2Value { get; set; }
    #endregion

    #region ExportSale
    public Guid? ExportSaleSlaveAccountCompanyId { get; set; }
    public string? ExportSaleAccountMasterValue { get; set; }
    public string? ExportSaleAccountSlaveValue { get; set; }
    public string? ExportSaleAccountDetailed1Value { get; set; }
    public string? ExportSaleAccountDetailed2Value { get; set; }
    #endregion

    #region ReturnFromSale
    public Guid? ReturnFromSaleSlaveAccountCompanyId { get; set; }
    public string? ReturnFromSaleAccountMasterValue { get; set; }
    public string? ReturnFromSaleAccountSlaveValue { get; set; }
    public string? ReturnFromSaleAccountDetailed1Value { get; set; }
    public string? ReturnFromSaleAccountDetailed2Value { get; set; }
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

    public ICollection<ItemWarehouse> Items { get; set; } = [];

    public virtual List<WarehouseLocation> Locations { get; set; } = [];

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
            .HasColumnType("decimal(22, 4)");

        builder
            .Property(e => e.WarehouseAccountMasterValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.WarehouseAccountSlaveValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.WarehouseAccountDetailed1Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.WarehouseAccountDetailed2Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.SaleAccountMasterValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.SaleAccountSlaveValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.SaleAccountDetailed1Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.SaleAccountDetailed2Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ExportSaleAccountMasterValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ExportSaleAccountSlaveValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ExportSaleAccountDetailed1Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ExportSaleAccountDetailed2Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ReturnFromSaleAccountMasterValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ReturnFromSaleAccountSlaveValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ReturnFromSaleAccountDetailed1Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ReturnFromSaleAccountDetailed2Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ReturnFromPurchaseAccountMasterValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ReturnFromPurchaseAccountSlaveValue)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ReturnFromPurchaseAccountDetailed1Value)
            .HasColumnType("nvarchar(20)");

        builder
            .Property(e => e.ReturnFromSaleAccountDetailed2Value)
            .HasColumnType("nvarchar(20)");

        builder
            .HasOne(e => e.WarehouseType)
            .WithMany(e => e.Warehouses)
            .HasForeignKey(e => e.WarehouseTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.CompanyUnit)
            .WithMany()
            .HasForeignKey(e => e.CompanyUnitId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
