using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixInventoryDynamicDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_DateTimeValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.DropColumn(
                name: "DateTimeValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.RenameColumn(
                name: "IntValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                newName: "IntegerValue");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_IntValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                newName: "IX_InventoryLotAttributeValue_AttributeId_IntegerValue");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpiryDate",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                type: "date",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_DateValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "DateValue" })
                .Annotation("SqlServer:Include", new[] { "LotId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_DateValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.DropColumn(
                name: "DateValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.RenameColumn(
                name: "IntegerValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                newName: "IntValue");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_IntegerValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                newName: "IX_InventoryLotAttributeValue_AttributeId_IntValue");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_DateTimeValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "DateTimeValue" })
                .Annotation("SqlServer:Include", new[] { "LotId" });
        }
    }
}
