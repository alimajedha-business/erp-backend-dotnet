using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOnDeleteBehaviourRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLot_Item_ItemId",
                schema: "Warehouse",
                table: "InventoryLot");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLotAttributeValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMovement_InventoryLot_LotId",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Category_CategoryId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttributeValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurement_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_Warehouse_WarehouseId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLot_Item_ItemId",
                schema: "Warehouse",
                table: "InventoryLot",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLotAttributeValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMovement_InventoryLot_LotId",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "LotId",
                principalSchema: "Warehouse",
                principalTable: "InventoryLot",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Category_CategoryId",
                schema: "Warehouse",
                table: "Item",
                column: "CategoryId",
                principalSchema: "Warehouse",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttributeValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
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
                name: "FK_WarehouseLocation_Warehouse_WarehouseId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "WarehouseId",
                principalSchema: "Warehouse",
                principalTable: "Warehouse",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLot_Item_ItemId",
                schema: "Warehouse",
                table: "InventoryLot");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLotAttributeValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMovement_InventoryLot_LotId",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Category_CategoryId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttributeValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurement_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_Warehouse_WarehouseId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLot_Item_ItemId",
                schema: "Warehouse",
                table: "InventoryLot",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLotAttributeValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMovement_InventoryLot_LotId",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "LotId",
                principalSchema: "Warehouse",
                principalTable: "InventoryLot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Category_CategoryId",
                schema: "Warehouse",
                table: "Item",
                column: "CategoryId",
                principalSchema: "Warehouse",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttributeValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
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
                name: "FK_WarehouseLocation_Warehouse_WarehouseId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "WarehouseId",
                principalSchema: "Warehouse",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
