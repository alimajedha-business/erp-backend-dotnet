using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateWarehouseBaseInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "warehouse");

            migrationBuilder.CreateTable(
                name: "ProductCode",
                schema: "warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstLevelCode = table.Column<int>(type: "int", nullable: false),
                    FirstLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondLevelCode = table.Column<int>(type: "int", nullable: false),
                    SecondLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThirdNextLevel = table.Column<bool>(type: "bit", nullable: false),
                    ThirdLevelCode = table.Column<int>(type: "int", nullable: true),
                    ThirdLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FourthNextLevel = table.Column<bool>(type: "bit", nullable: false),
                    FourthLevelCode = table.Column<int>(type: "int", nullable: true),
                    FourthLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FifthNextLevel = table.Column<bool>(type: "bit", nullable: false),
                    FifthLevelCode = table.Column<int>(type: "int", nullable: true),
                    FifthLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SixthNextLevel = table.Column<bool>(type: "bit", nullable: false),
                    SixthLevelCode = table.Column<int>(type: "int", nullable: true),
                    SixthLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SeventhNextLevel = table.Column<bool>(type: "bit", nullable: false),
                    SeventhLevelCode = table.Column<int>(type: "int", nullable: true),
                    SeventhLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCode_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductHierarchy",
                schema: "warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstLevelSize = table.Column<byte>(type: "tinyint", nullable: false),
                    FirstLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SecondLevelSize = table.Column<byte>(type: "tinyint", nullable: false),
                    SecondLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ThirdLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    ThirdLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FourthLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    FourthLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FifthLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    FifthLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SixthLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    SixthLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SeventhLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    SeventhLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHierarchy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHierarchy_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseType",
                schema: "warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseStock",
                schema: "warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MaxAssetValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CompanyUnitId = table.Column<int>(type: "int", nullable: true),
                    WarehouseTypeId = table.Column<int>(type: "int", nullable: true),
                    WarehouseMasterAccount = table.Column<int>(type: "int", nullable: true),
                    WarehouseSlaveAccount = table.Column<int>(type: "int", nullable: true),
                    WarehouseDetail1Account = table.Column<int>(type: "int", nullable: true),
                    WarehouseDetail2Account = table.Column<int>(type: "int", nullable: true),
                    WarehouseAccountIsStatic = table.Column<bool>(type: "bit", nullable: false),
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
                    PurchaseReturnAccountIsStatic = table.Column<bool>(type: "bit", nullable: false),
                    ContractorMasterAccount = table.Column<int>(type: "int", nullable: true),
                    ContractorSlaveAccount = table.Column<int>(type: "int", nullable: true),
                    ContractorDetail1Account = table.Column<int>(type: "int", nullable: true),
                    ContractorDetail2Account = table.Column<int>(type: "int", nullable: true),
                    ContractorAccountIsStatic = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseStock_WarehouseType_WarehouseTypeId",
                        column: x => x.WarehouseTypeId,
                        principalSchema: "warehouse",
                        principalTable: "WarehouseType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseStock_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseStock_company_units_CompanyUnitId",
                        column: x => x.CompanyUnitId,
                        principalSchema: "shared",
                        principalTable: "company_units",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ProductCode_CompanyId",
                schema: "warehouse",
                table: "ProductCode",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "ProductHierarchy_CompanyId",
                schema: "warehouse",
                table: "ProductHierarchy",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "WarehouseStock_CompanyId",
                schema: "warehouse",
                table: "WarehouseStock",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "WarehouseStock_CompanyUnitId",
                schema: "warehouse",
                table: "WarehouseStock",
                column: "CompanyUnitId");

            migrationBuilder.CreateIndex(
                name: "WarehouseStock_WarehouseTypeId",
                schema: "warehouse",
                table: "WarehouseStock",
                column: "WarehouseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCode",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "ProductHierarchy",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "WarehouseStock",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "WarehouseType",
                schema: "warehouse");
        }
    }
}
