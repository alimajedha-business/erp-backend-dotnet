using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsDeletedDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "WarehouseLocation",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "Warehouse",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "HCM",
                table: "Position",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "HCM",
                table: "OrganizationalStructure",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "Item",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "InventoryMovementType",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "InventoryMovement",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "InventoryLot",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "HCM",
                table: "Department",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "CategoryAttributeRule",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "Category",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "Warehouse",
                table: "Attribute",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusChangeDate",
                schema: "HCM",
                table: "Department",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "WarehouseLocation",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "Warehouse",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "HCM",
                table: "Position",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "HCM",
                table: "OrganizationalStructure",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "Item",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "InventoryMovementType",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "InventoryMovement",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "InventoryLot",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "HCM",
                table: "Department",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "CategoryAttributeRule",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "Category",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Warehouse",
                table: "Attribute",
                newName: "CreateAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StatusChangeDate",
                schema: "HCM",
                table: "Department",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
