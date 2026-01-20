using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseItemUomProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDefalulIssue",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                newName: "IsDefaultlIssue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDefaultlIssue",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                newName: "IsDefalulIssue");
        }
    }
}
