using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ExportSaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportSaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportSaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportSaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExportSaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Warehouse",
                table: "Warehouse",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxRialValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "decimal(22,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromPurchaseAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromPurchaseAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromPurchaseAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromPurchaseAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReturnFromPurchaseSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromSaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromSaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromSaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromSaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReturnFromSaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "WarehouseAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarehouseAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyUnit",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUnit_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "CompanyUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_TypeId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "TypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUnit_CompanyId",
                schema: "General",
                table: "CompanyUnit",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUnit_IsDeleted",
                schema: "General",
                table: "CompanyUnit",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_CompanyUnit_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "CompanyUnitId",
                principalSchema: "General",
                principalTable: "CompanyUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_WarehouseType_TypeId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "TypeId",
                principalSchema: "Warehouse",
                principalTable: "WarehouseType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_CompanyUnit_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_WarehouseType_TypeId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropTable(
                name: "CompanyUnit",
                schema: "General");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_TypeId",
                schema: "Warehouse",
                table: "Warehouse");

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

            migrationBuilder.DropColumn(
                name: "CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "MaxRialValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromPurchaseAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromPurchaseAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromPurchaseAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromPurchaseAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromPurchaseSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "TypeId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "WarehouseAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "WarehouseAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "WarehouseAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "WarehouseAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "WarehouseSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
