using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class WeighingPersonDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "person_driver",
                schema: "weighing",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    person_id = table.Column<int>(type: "int", nullable: false),
                    vehicle_type_id = table.Column<int>(type: "int", nullable: false),
                    vehicle_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    plate_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    initial_weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    driver_type = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_driver", x => x.id);
                    table.ForeignKey(
                        name: "FK_person_driver_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_person_driver_persons_person_id",
                        column: x => x.person_id,
                        principalSchema: "general",
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_person_driver_vehicle_types_vehicle_type_id",
                        column: x => x.vehicle_type_id,
                        principalSchema: "shared",
                        principalTable: "vehicle_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_person_driver_company_id",
                schema: "weighing",
                table: "person_driver",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_driver_person_id",
                schema: "weighing",
                table: "person_driver",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_person_driver_vehicle_type_id",
                schema: "weighing",
                table: "person_driver",
                column: "vehicle_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "person_driver",
                schema: "weighing");
        }
    }
}
