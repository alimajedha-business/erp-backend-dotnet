using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateDischatfeStationCreatePackageTypeCreatePersonDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Weighing");

            migrationBuilder.CreateTable(
                name: "DischargeStation",
                schema: "Weighing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DischargeStation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DischargeStation_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PackageType",
                schema: "Weighing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageType_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonDriver",
                schema: "Weighing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    VehicleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InitialWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DriverType = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDriver_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonDriver_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "general",
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DischargeStation_CompanyId",
                schema: "Weighing",
                table: "DischargeStation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageType_CompanyId",
                schema: "Weighing",
                table: "PackageType",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDriver_CompanyId",
                schema: "Weighing",
                table: "PersonDriver",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDriver_PersonId",
                schema: "Weighing",
                table: "PersonDriver",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DischargeStation",
                schema: "Weighing");

            migrationBuilder.DropTable(
                name: "PackageType",
                schema: "Weighing");

            migrationBuilder.DropTable(
                name: "PersonDriver",
                schema: "Weighing");
        }
    }
}
