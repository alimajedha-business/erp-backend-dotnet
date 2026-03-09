using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetDeleteBehaviorNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeEnumValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "AttributeEnumValue");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAttributeRule_Attribute_AttributeId",
                schema: "Warehouse",
                table: "CategoryAttributeRule");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLotAttributeValue_InventoryLot_LotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttributeValue_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttributeValue");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeEnumValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAttributeRule_Attribute_AttributeId",
                schema: "Warehouse",
                table: "CategoryAttributeRule",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLotAttributeValue_InventoryLot_LotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "LotId",
                principalSchema: "Warehouse",
                principalTable: "InventoryLot",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttributeValue_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeEnumValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "AttributeEnumValue");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryAttributeRule_Attribute_AttributeId",
                schema: "Warehouse",
                table: "CategoryAttributeRule");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLotAttributeValue_InventoryLot_LotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttributeValue_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttributeValue");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeEnumValue_Attribute_AttributeId",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryAttributeRule_Attribute_AttributeId",
                schema: "Warehouse",
                table: "CategoryAttributeRule",
                column: "AttributeId",
                principalSchema: "Warehouse",
                principalTable: "Attribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLotAttributeValue_InventoryLot_LotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "LotId",
                principalSchema: "Warehouse",
                principalTable: "InventoryLot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttributeValue_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
