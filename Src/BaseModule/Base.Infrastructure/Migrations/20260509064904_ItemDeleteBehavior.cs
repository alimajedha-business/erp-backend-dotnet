using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_Item_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouseLocation_ItemWarehouse_ItemWarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_Item_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "UnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemWarehouseLocation_ItemWarehouse_ItemWarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "ItemWarehouseId",
                principalSchema: "Warehouse",
                principalTable: "ItemWarehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_Item_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouseLocation_ItemWarehouse_ItemWarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_Item_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "UnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemWarehouseLocation_ItemWarehouse_ItemWarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "ItemWarehouseId",
                principalSchema: "Warehouse",
                principalTable: "ItemWarehouse",
                principalColumn: "Id");
        }
    }
}
