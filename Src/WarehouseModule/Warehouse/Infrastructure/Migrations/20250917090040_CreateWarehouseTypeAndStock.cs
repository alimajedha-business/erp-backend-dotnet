using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateWarehouseTypeAndStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseTypes",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeCode = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseStocks",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseCode = table.Column<int>(type: "int", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MaxAssetValue = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    WarehouseTypeId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    WarehouseMasterAccount = table.Column<int>(type: "int", nullable: true),
                    WarehouseSlaveAccount = table.Column<int>(type: "int", nullable: true),
                    WarehouseDetail1Account = table.Column<int>(type: "int", nullable: true),
                    WarehouseDetail2Account = table.Column<int>(type: "int", nullable: true),
                    WarehouseAccountFixed = table.Column<bool>(type: "bit", nullable: false),
                    SalesMasterAccount = table.Column<int>(type: "int", nullable: true),
                    SalesSlaveAccount = table.Column<int>(type: "int", nullable: true),
                    SalesDetail1Account = table.Column<int>(type: "int", nullable: true),
                    SalesDetail2Account = table.Column<int>(type: "int", nullable: true),
                    SalesAccountIsStatic = table.Column<bool>(type: "bit", nullable: false),
                    ExportMasterAccount = table.Column<int>(type: "int", nullable: true),
                    ExportSlaveAccount = table.Column<int>(type: "int", nullable: true),
                    ExportDetail1Account = table.Column<int>(type: "int", nullable: true),
                    ExportDetail2Account = table.Column<int>(type: "int", nullable: true),
                    ExportAccountIsStatic = table.Column<bool>(type: "bit", nullable: false),
                    ReturnMasterAccount = table.Column<int>(type: "int", nullable: true),
                    ReturnSlaveAccount = table.Column<int>(type: "int", nullable: true),
                    ReturnDetail1Account = table.Column<int>(type: "int", nullable: true),
                    ReturnDetail2Account = table.Column<int>(type: "int", nullable: true),
                    ReturnAccountIsStatic = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseReturnMasterAccount = table.Column<int>(type: "int", nullable: true),
                    PurchaseReturnSlaveAccount = table.Column<int>(type: "int", nullable: true),
                    PurchaseReturnDetail1Account = table.Column<int>(type: "int", nullable: true),
                    PurchaseReturnDetail2Account = table.Column<int>(type: "int", nullable: true),
                    PurchaseReturnAccountIsStatic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseStocks_WarehouseTypes_WarehouseTypeId",
                        column: x => x.WarehouseTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "WarehouseStocks_BranchId",
                schema: "Warehouse",
                table: "WarehouseStocks",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "WarehouseStocks_CompanyId",
                schema: "Warehouse",
                table: "WarehouseStocks",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "WarehouseStocks_WarehouseTypeId",
                schema: "Warehouse",
                table: "WarehouseStocks",
                column: "WarehouseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseStocks",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "WarehouseTypes",
                schema: "Warehouse");
        }
    }
}
