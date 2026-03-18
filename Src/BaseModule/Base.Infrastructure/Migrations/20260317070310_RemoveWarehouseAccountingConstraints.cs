using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWarehouseAccountingConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ExportSale_Account",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ReturnFromPurchase_Account",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ReturnFromSale_Account",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Sale_Account",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Warehouse_Account",
                schema: "Warehouse",
                table: "Warehouse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_ExportSale_Account",
                schema: "Warehouse",
                table: "Warehouse",
                sql: "(\r\n                    (\r\n                        ExportSaleSlaveAccountCompanyId IS NOT NULL\r\n                        AND ExportSaleAccountMasterValue IS NULL\r\n                        AND ExportSaleAccountSlaveValue IS NULL\r\n                        AND ExportSaleAccountDetailed1Value IS NULL\r\n                        AND ExportSaleAccountDetailed2Value IS NULL\r\n                    )\r\n                        OR\r\n                    (\r\n                        ExportSaleSlaveAccountCompanyId IS NULL\r\n                        AND ExportSaleAccountMasterValue IS NOT NULL\r\n                        AND ExportSaleAccountSlaveValue IS NOT NULL\r\n                        AND ExportSaleAccountDetailed1Value IS NOT NULL\r\n                        AND ExportSaleAccountDetailed2Value IS NOT NULL\r\n                    )\r\n                )");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ReturnFromPurchase_Account",
                schema: "Warehouse",
                table: "Warehouse",
                sql: "(\r\n                    (\r\n                        ReturnFromPurchaseSlaveAccountCompanyId IS NOT NULL \r\n                        AND ReturnFromPurchaseAccountMasterValue IS NULL \r\n                        AND ReturnFromPurchaseAccountSlaveValue IS NULL\r\n                        AND ReturnFromPurchaseAccountDetailed1Value IS NULL\r\n                        AND ReturnFromPurchaseAccountDetailed2Value IS NULL\r\n                    )\r\n                        OR\r\n                    (\r\n                        ReturnFromPurchaseSlaveAccountCompanyId IS NULL \r\n                        AND ReturnFromPurchaseAccountMasterValue IS NOT NULL \r\n                        AND ReturnFromPurchaseAccountSlaveValue IS NOT NULL\r\n                        AND ReturnFromPurchaseAccountDetailed1Value IS NOT NULL\r\n                        AND ReturnFromPurchaseAccountDetailed2Value IS NOT NULL\r\n                    )\r\n                )");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ReturnFromSale_Account",
                schema: "Warehouse",
                table: "Warehouse",
                sql: "(\r\n                    (\r\n                        ReturnFromSaleSlaveAccountCompanyId IS NOT NULL \r\n                        AND ReturnFromSaleAccountMasterValue IS NULL \r\n                        AND ReturnFromSaleAccountSlaveValue IS NULL\r\n                        AND ReturnFromSaleAccountDetailed1Value IS NULL\r\n                        AND ReturnFromSaleAccountDetailed2Value IS NULL\r\n                    )\r\n                        OR\r\n                    (\r\n                        ReturnFromSaleSlaveAccountCompanyId IS NULL \r\n                        AND ReturnFromSaleAccountMasterValue IS NOT NULL \r\n                        AND ReturnFromSaleAccountSlaveValue IS NOT NULL\r\n                        AND ReturnFromSaleAccountDetailed1Value IS NOT NULL\r\n                        AND ReturnFromSaleAccountDetailed2Value IS NOT NULL\r\n                    )\r\n                )");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Sale_Account",
                schema: "Warehouse",
                table: "Warehouse",
                sql: "(\r\n                    (\r\n                        SaleSlaveAccountCompanyId IS NOT NULL \r\n                        AND SaleAccountMasterValue IS NULL \r\n                        AND SaleAccountSlaveValue IS NULL\r\n                        AND SaleAccountDetailed1Value IS NULL\r\n                        AND SaleAccountDetailed2Value IS NULL\r\n                    )\r\n                        OR\r\n                    (\r\n                        SaleSlaveAccountCompanyId IS NULL \r\n                        AND SaleAccountMasterValue IS NOT NULL \r\n                        AND SaleAccountSlaveValue IS NOT NULL\r\n                        AND SaleAccountDetailed1Value IS NOT NULL\r\n                        AND SaleAccountDetailed2Value IS NOT NULL\r\n                    )\r\n                )");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Warehouse_Account",
                schema: "Warehouse",
                table: "Warehouse",
                sql: "(\r\n                    (\r\n                        WarehouseSlaveAccountCompanyId IS NOT NULL \r\n                        AND WarehouseAccountMasterValue IS NULL \r\n                        AND WarehouseAccountSlaveValue IS NULL\r\n                        AND WarehouseAccountDetailed1Value IS NULL\r\n                        AND WarehouseAccountDetailed2Value IS NULL\r\n                    )\r\n                        OR\r\n                    (\r\n                        WarehouseSlaveAccountCompanyId IS NULL \r\n                        AND WarehouseAccountMasterValue IS NOT NULL \r\n                        AND WarehouseAccountSlaveValue IS NOT NULL\r\n                        AND WarehouseAccountDetailed1Value IS NOT NULL\r\n                        AND WarehouseAccountDetailed2Value IS NOT NULL\r\n                    )\r\n                )");
        }
    }
}
