using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RnmUnitEntityToSiUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Unit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_Unit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.RenameTable(
                name: "Unit",
                schema: "Warehouse",
                newName: "SiUnit",
                newSchema: "Warehouse");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_IsDeleted",
                schema: "Warehouse",
                table: "SiUnit",
                newName: "IX_SiUnit_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "UX_Unit_Code",
                schema: "Warehouse",
                table: "SiUnit",
                newName: "UX_SiUnit_Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_SiUnit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "Item",
                column: "PreferredLengthUnitId",
                principalSchema: "Warehouse",
                principalTable: "SiUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_SiUnit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "Item",
                column: "PreferredMassUnitId",
                principalSchema: "Warehouse",
                principalTable: "SiUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_SiUnit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "Item",
                column: "PreferredVolumeUnitId",
                principalSchema: "Warehouse",
                principalTable: "SiUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLine_SiUnit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "PreferredMassUnitId",
                principalSchema: "Warehouse",
                principalTable: "SiUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLine_SiUnit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "PreferredVolumeUnitId",
                principalSchema: "Warehouse",
                principalTable: "SiUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocation_SiUnit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredLengthUnitId",
                principalSchema: "Warehouse",
                principalTable: "SiUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocation_SiUnit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredMassUnitId",
                principalSchema: "Warehouse",
                principalTable: "SiUnit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocation_SiUnit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredVolumeUnitId",
                principalSchema: "Warehouse",
                principalTable: "SiUnit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_SiUnit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_SiUnit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_SiUnit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_SiUnit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_SiUnit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_SiUnit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_SiUnit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_SiUnit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.RenameTable(
                name: "SiUnit",
                schema: "Warehouse",
                newName: "Unit",
                newSchema: "Warehouse");

            migrationBuilder.RenameIndex(
                name: "IX_SiUnit_IsDeleted",
                schema: "Warehouse",
                table: "Unit",
                newName: "IX_Unit_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "UX_SiUnit_Code",
                schema: "Warehouse",
                table: "Unit",
                newName: "UX_Unit_Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Unit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "Item",
                column: "PreferredLengthUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "Item",
                column: "PreferredMassUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "Item",
                column: "PreferredVolumeUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLine_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "PreferredMassUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLine_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "PreferredVolumeUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocation_Unit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredLengthUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocation_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredMassUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocation_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredVolumeUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");
        }
    }
}
