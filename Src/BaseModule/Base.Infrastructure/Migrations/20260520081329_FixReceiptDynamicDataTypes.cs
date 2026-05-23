using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixReceiptDynamicDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeValue",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.DropColumn(
                name: "DateTimeValue",
                schema: "Warehouse",
                table: "ReceiptFieldValue");

            migrationBuilder.RenameColumn(
                name: "IntValue",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                newName: "IntegerValue");

            migrationBuilder.AddColumn<int>(
                name: "IntegerValue",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntegerValue",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.RenameColumn(
                name: "IntegerValue",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                newName: "IntValue");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeValue",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeValue",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
