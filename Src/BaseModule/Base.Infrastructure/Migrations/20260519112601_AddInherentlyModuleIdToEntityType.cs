using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    public partial class AddInherentlyModuleIdToEntityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "inherently_module_id",
                schema: "general",
                table: "entity_types",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inherently_module_id",
                schema: "general",
                table: "entity_types");
        }
    }
}
