using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitEntityModifyItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_UnitOfMeasurement_PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.AddColumn<decimal>(
                name: "CubeVolume",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weigh",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Unit",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FactorToBase = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    IsBaseUnit = table.Column<bool>(type: "bit", nullable: false),
                    UnitDimension = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredLengthUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredMassUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredVolumeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_IsDeleted",
                schema: "Warehouse",
                table: "Unit",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_Unit_Code",
                schema: "Warehouse",
                table: "Unit",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredLengthUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredMassUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredVolumeUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropTable(
                name: "Unit",
                schema: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "CubeVolume",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "Length",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "Weigh",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "Width",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Item_PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item",
                column: "PrimaryUnitOfMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_UnitOfMeasurement_PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item",
                column: "PrimaryUnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id");
        }
    }
}
