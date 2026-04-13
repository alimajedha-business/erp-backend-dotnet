using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseLocLevelNoProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.AddColumn<int>(
                name: "LevelNo",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.DropColumn(
                name: "LevelNo",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
