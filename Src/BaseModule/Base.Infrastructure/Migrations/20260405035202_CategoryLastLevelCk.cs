using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryLastLevelCk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Category_LevelNo_HasNextLevel",
                schema: "Warehouse",
                table: "Category",
                sql: "(LevelNo = 1 AND HasNextLevel = 1) OR (LevelNo = 6 AND HasNextLevel = 0) OR (LevelNo > 1 AND LevelNo < 6 AND HasNextLevel IN (0, 1))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Category_LevelNo_HasNextLevel",
                schema: "Warehouse",
                table: "Category");
        }
    }
}
