using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReceiptRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptFieldValue_ReceiptFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptFieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptFieldValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptFieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_Item_ItemId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_WarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineMeasurementValue_ItemUnitOfMeasurement_ItemUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineMeasurementValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.DropColumn(
                name: "Length",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.DropColumn(
                name: "Volume",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.DropColumn(
                name: "Width",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLine_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "PreferredMassUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLine_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "PreferredVolumeUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptFieldValue_ReceiptFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                column: "FieldDefinitionId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptFieldDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptFieldValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                column: "ReceiptLineId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLine_Item_ItemId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ReceiptLine_WarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "WarehouseLocationId",
                principalSchema: "Warehouse",
                principalTable: "WarehouseLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ItemAttributeId",
                principalSchema: "Warehouse",
                principalTable: "ItemAttribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ReceiptLineId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptLine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineMeasurementValue_ItemUnitOfMeasurement_ItemUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                column: "ItemUnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "ItemUnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineMeasurementValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                column: "ReceiptLineId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptLine",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptFieldValue_ReceiptFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptFieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptFieldValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptFieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_Item_ItemId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_WarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineMeasurementValue_ItemUnitOfMeasurement_ItemUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineMeasurementValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptLine_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptLine_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropColumn(
                name: "BatchNumber",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropColumn(
                name: "PreferredMassUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropColumn(
                name: "PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropColumn(
                name: "Volume",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptFieldValue_ReceiptFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                column: "FieldDefinitionId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptFieldDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptFieldValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                column: "ReceiptLineId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptLine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLine_Item_ItemId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLine_WarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "WarehouseLocationId",
                principalSchema: "Warehouse",
                principalTable: "WarehouseLocation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ItemAttributeId",
                principalSchema: "Warehouse",
                principalTable: "ItemAttribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ReceiptLineId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineMeasurementValue_ItemUnitOfMeasurement_ItemUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                column: "ItemUnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "ItemUnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineMeasurementValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                column: "ReceiptLineId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
