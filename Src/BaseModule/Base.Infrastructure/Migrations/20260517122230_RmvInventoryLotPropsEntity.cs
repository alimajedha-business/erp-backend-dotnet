using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RmvInventoryLotPropsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_InvLot_Item_DimHash",
                schema: "Warehouse",
                table: "InventoryLot");

            migrationBuilder.DropColumn(
                name: "LotNumber",
                schema: "Warehouse",
                table: "InventoryLot");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                schema: "Warehouse",
                table: "InventoryLot");

            migrationBuilder.CreateIndex(
                name: "UX_InvLot_Item_DimHash",
                schema: "Warehouse",
                table: "InventoryLot",
                columns: new[] { "ItemId", "StockKeyHash" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_InvLot_Item_DimHash",
                schema: "Warehouse",
                table: "InventoryLot");

            migrationBuilder.AddColumn<string>(
                name: "LotNumber",
                schema: "Warehouse",
                table: "InventoryLot",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                schema: "Warehouse",
                table: "InventoryLot",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UX_InvLot_Item_DimHash",
                schema: "Warehouse",
                table: "InventoryLot",
                columns: new[] { "ItemId", "SerialNumber", "StockKeyHash" },
                unique: true,
                filter: "[SerialNumber] IS NOT NULL");
        }
    }
}
