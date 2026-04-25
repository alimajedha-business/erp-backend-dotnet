using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemWarehouseLocationPrp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_Item_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_FromUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_ToUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouseLocation_WarehouseLocation_LocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.DropIndex(
                name: "IX_ItemUnitOfMeasurementConversion_FromUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropIndex(
                name: "UX_ItemUomConv_Unique",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ItemUomConv_Factor",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropColumn(
                name: "Factor",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropColumn(
                name: "FromUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                newName: "WarehouseLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemWarehouseLocation_LocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                newName: "IX_ItemWarehouseLocation_WarehouseLocationId");

            migrationBuilder.RenameColumn(
                name: "ToUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                newName: "UnitOfMeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemUnitOfMeasurementConversion_ToUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                newName: "IX_ItemUnitOfMeasurementConversion_UnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "ItemId");

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
                name: "FK_ItemWarehouseLocation_WarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "WarehouseLocationId",
                principalSchema: "Warehouse",
                principalTable: "WarehouseLocation",
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
                name: "FK_ItemWarehouseLocation_WarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.DropIndex(
                name: "IX_ItemUnitOfMeasurementConversion_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.RenameColumn(
                name: "WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemWarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                newName: "IX_ItemWarehouseLocation_LocationId");

            migrationBuilder.RenameColumn(
                name: "UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                newName: "ToUnitOfMeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemUnitOfMeasurementConversion_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                newName: "IX_ItemUnitOfMeasurementConversion_ToUnitOfMeasurementId");

            migrationBuilder.AddColumn<decimal>(
                name: "Factor",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                type: "decimal(18,6)",
                precision: 18,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "FromUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_FromUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "FromUnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "UX_ItemUomConv_Unique",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                columns: new[] { "ItemId", "FromUnitOfMeasurementId", "ToUnitOfMeasurementId" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_ItemUomConv_Factor",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                sql: "Factor > 0");

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
                name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_FromUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "FromUnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_ToUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "ToUnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemWarehouseLocation_WarehouseLocation_LocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "LocationId",
                principalSchema: "Warehouse",
                principalTable: "WarehouseLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
