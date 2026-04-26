using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemRelationOnDeleteAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Attribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurement_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouse_Item_ItemId",
                schema: "Warehouse",
                table: "ItemWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouse_Warehouse_WarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouse");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttribute_Attribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "UnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemWarehouse_Item_ItemId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemWarehouse_Warehouse_WarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "WarehouseId",
                principalSchema: "Warehouse",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Attribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurement_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouse_Item_ItemId",
                schema: "Warehouse",
                table: "ItemWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouse_Warehouse_WarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouse");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttribute_Attribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "UnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemWarehouse_Item_ItemId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemWarehouse_Warehouse_WarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "WarehouseId",
                principalSchema: "Warehouse",
                principalTable: "Warehouse",
                principalColumn: "Id");
        }
    }
}
