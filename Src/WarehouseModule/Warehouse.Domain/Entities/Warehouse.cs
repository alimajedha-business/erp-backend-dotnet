using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Warehouse :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Warehouse>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public decimal MaxRialValue { get; private set; }
    public Guid TypeId { get; private set; }
    public Guid CompanyUnitId { get; private set; }

    #region Warehouse
    public Guid? WarehouseSlaveAccountCompanyId { get; private set; }
    public string? WarehouseAccountMasterValue { get; private set; }
    public string? WarehouseAccountSlaveValue { get; private set; }
    public string? WarehouseAccountDetailed1Value { get; private set; }
    public string? WarehouseAccountDetailed2Value { get; private set; }
    #endregion

    #region Sale
    public Guid? SaleSlaveAccountCompanyId { get; private set; }
    public string? SaleAccountMasterValue { get; private set; }
    public string? SaleAccountSlaveValue { get; private set; }
    public string? SaleAccountDetailed1Value { get; private set; }
    public string? SaleAccountDetailed2Value { get; private set; }
    #endregion

    #region ExportSale
    public Guid? ExportSaleSlaveAccountCompanyId { get; private set; }
    public string? ExportSaleAccountMasterValue { get; private set; }
    public string? ExportSaleAccountSlaveValue { get; private set; }
    public string? ExportSaleAccountDetailed1Value { get; private set; }
    public string? ExportSaleAccountDetailed2Value { get; private set; }
    #endregion

    #region ReturnFromSale
    public Guid? ReturnFromSaleSlaveAccountCompanyId { get; private set; }
    public string? ReturnFromSaleAccountMasterValue { get; private set; }
    public string? ReturnFromSaleAccountSlaveValue { get; private set; }
    public string? ReturnFromSaleAccountDetailed1Value { get; private set; }
    public string? ReturnFromSaleAccountDetailed2Value { get; private set; }
    #endregion

    #region ReturnFromPurchase
    public Guid? ReturnFromPurchaseSlaveAccountCompanyId { get; private set; }
    public string? ReturnFromPurchaseAccountMasterValue { get; private set; }
    public string? ReturnFromPurchaseAccountSlaveValue { get; private set; }
    public string? ReturnFromPurchaseAccountDetailed1Value { get; private set; }
    public string? ReturnFromPurchaseAccountDetailed2Value { get; private set; }
    #endregion

    public required WarehouseType Type { get; set; }
    public required CompanyUnit CompanyUnit { get; set; }

    public virtual List<WarehouseLocation> Locations { get; set; } = [];

    public void Map(EntityTypeBuilder<Warehouse> builder)
    {
        builder
            .ToTable(nameof(Warehouse), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_Warehouse_Account",
                @"(
                    (
                        WarehouseSlaveAccountCompanyId IS NOT NULL 
                        AND WarehouseAccountMasterValue IS NULL 
                        AND WarehouseAccountSlaveValue IS NULL
                        AND WarehouseAccountDetailed1Value IS NULL
                        AND WarehouseAccountDetailed2Value IS NULL
                    )
                        OR
                    (
                        WarehouseSlaveAccountCompanyId IS NULL 
                        AND WarehouseAccountMasterValue IS NOT NULL 
                        AND WarehouseAccountSlaveValue IS NOT NULL
                        AND WarehouseAccountDetailed1Value IS NOT NULL
                        AND WarehouseAccountDetailed2Value IS NOT NULL
                    )
                )"
            ))
            .ToTable(t => t.HasCheckConstraint(
                "CK_Sale_Account",
                @"(
                    (
                        SaleSlaveAccountCompanyId IS NOT NULL 
                        AND SaleAccountMasterValue IS NULL 
                        AND SaleAccountSlaveValue IS NULL
                        AND SaleAccountDetailed1Value IS NULL
                        AND SaleAccountDetailed2Value IS NULL
                    )
                        OR
                    (
                        SaleSlaveAccountCompanyId IS NULL 
                        AND SaleAccountMasterValue IS NOT NULL 
                        AND SaleAccountSlaveValue IS NOT NULL
                        AND SaleAccountDetailed1Value IS NOT NULL
                        AND SaleAccountDetailed2Value IS NOT NULL
                    )
                )"
            ))
            .ToTable(t => t.HasCheckConstraint(
                "CK_ExportSale_Account",
                @"(
                    (
                        ExportSaleSlaveAccountCompanyId IS NOT NULL
                        AND ExportSaleAccountMasterValue IS NULL
                        AND ExportSaleAccountSlaveValue IS NULL
                        AND ExportSaleAccountDetailed1Value IS NULL
                        AND ExportSaleAccountDetailed2Value IS NULL
                    )
                        OR
                    (
                        ExportSaleSlaveAccountCompanyId IS NULL
                        AND ExportSaleAccountMasterValue IS NOT NULL
                        AND ExportSaleAccountSlaveValue IS NOT NULL
                        AND ExportSaleAccountDetailed1Value IS NOT NULL
                        AND ExportSaleAccountDetailed2Value IS NOT NULL
                    )
                )"
            ))
            .ToTable(t => t.HasCheckConstraint(
                "CK_ReturnFromSale_Account",
                @"(
                    (
                        ReturnFromSaleSlaveAccountCompanyId IS NOT NULL 
                        AND ReturnFromSaleAccountMasterValue IS NULL 
                        AND ReturnFromSaleAccountSlaveValue IS NULL
                        AND ReturnFromSaleAccountDetailed1Value IS NULL
                        AND ReturnFromSaleAccountDetailed2Value IS NULL
                    )
                        OR
                    (
                        ReturnFromSaleSlaveAccountCompanyId IS NULL 
                        AND ReturnFromSaleAccountMasterValue IS NOT NULL 
                        AND ReturnFromSaleAccountSlaveValue IS NOT NULL
                        AND ReturnFromSaleAccountDetailed1Value IS NOT NULL
                        AND ReturnFromSaleAccountDetailed2Value IS NOT NULL
                    )
                )"
            ))
            .ToTable(t => t.HasCheckConstraint(
                "CK_ReturnFromPurchase_Account",
                @"(
                    (
                        ReturnFromPurchaseSlaveAccountCompanyId IS NOT NULL 
                        AND ReturnFromPurchaseAccountMasterValue IS NULL 
                        AND ReturnFromPurchaseAccountSlaveValue IS NULL
                        AND ReturnFromPurchaseAccountDetailed1Value IS NULL
                        AND ReturnFromPurchaseAccountDetailed2Value IS NULL
                    )
                        OR
                    (
                        ReturnFromPurchaseSlaveAccountCompanyId IS NULL 
                        AND ReturnFromPurchaseAccountMasterValue IS NOT NULL 
                        AND ReturnFromPurchaseAccountSlaveValue IS NOT NULL
                        AND ReturnFromPurchaseAccountDetailed1Value IS NOT NULL
                        AND ReturnFromPurchaseAccountDetailed2Value IS NOT NULL
                    )
                )"
            ));

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Warehouse_Company_Code");

        builder
            .Property(e => e.Code)
            .HasMaxLength(50);

        builder
            .Property(e => e.Title)
            .HasMaxLength(250);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder
            .Property(e => e.MaxRialValue)
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
            .HasOne(e => e.Type)
            .WithMany(e => e.Warehouses)
            .HasForeignKey(e => e.TypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.CompanyUnit)
            .WithMany()
            .HasForeignKey(e => e.CompanyUnitId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
