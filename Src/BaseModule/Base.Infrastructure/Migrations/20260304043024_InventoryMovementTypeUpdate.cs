using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InventoryMovementTypeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncreaseStockQuantity",
                schema: "Warehouse",
                table: "InventoryMovementType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IncreaseStockQuantity",
                schema: "Warehouse",
                table: "InventoryMovementType",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }
    }
}
