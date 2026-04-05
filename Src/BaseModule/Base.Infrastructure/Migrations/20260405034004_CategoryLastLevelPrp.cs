using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryLastLevelPrp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Category_LevelNo",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Category_LevelNo_LastLevel",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsLastLevel",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.AddColumn<bool>(
                name: "HasNextLevel",
                schema: "Warehouse",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Category_LevelNo",
                schema: "Warehouse",
                table: "Category",
                sql: "LevelNo BETWEEN 1 AND 6");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Category_LevelNo",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "HasNextLevel",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.AddColumn<bool>(
                name: "IsLastLevel",
                schema: "Warehouse",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Category_LevelNo",
                schema: "Warehouse",
                table: "Category",
                sql: "LevelNo BETWEEN 1 AND 7");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Category_LevelNo_LastLevel",
                schema: "Warehouse",
                table: "Category",
                sql: "(LevelNo = 1 AND IsLastLevel = 0) OR LevelNo <> 1");
        }
    }
}
