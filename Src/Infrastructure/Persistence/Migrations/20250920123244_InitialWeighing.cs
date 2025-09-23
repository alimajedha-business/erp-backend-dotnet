using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialWeighing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
               name: "weighing");

            migrationBuilder.CreateTable(
             name: "package_types",
             schema: "weighing",
             columns: table => new
             {
                 id = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                 code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                 name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                 description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                 company_id = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_package_types", x => x.id);
                 table.ForeignKey(
                     name: "FK_package_types_companies_company_id",
                     column: x => x.company_id,
                     principalSchema: "general",
                     principalTable: "companies",
                     principalColumn: "id",
                     onDelete: ReferentialAction.Restrict);
             });



            migrationBuilder.CreateTable(
                name: "discharge_stations",
                schema: "weighing",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discharge_stations", x => x.id);
                    table.ForeignKey(
                        name: "FK_discharge_stations_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
               name: "IX_package_types_company_id",
               schema: "weighing",
               table: "package_types",
               column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_discharge_stations_company_id",
                schema: "weighing",
                table: "discharge_stations",
                column: "company_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discharge_stations",
                schema: "weighing");

            migrationBuilder.DropTable(
              name: "package_types",
              schema: "weighing");
        }
    }
}
