using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseLocationCapacityUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxMass",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxVolume",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredLengthUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredMassUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WarehouseLocationUsage",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    WarehouseLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccupiedMass = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: false),
                    OccupiedVolume = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseLocationUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseLocationUsage_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredLengthUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_PreferredMassUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredMassUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "PreferredVolumeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocationUsage_IsDeleted",
                schema: "Warehouse",
                table: "WarehouseLocationUsage",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocationUsage_WarehouseLocation",
                schema: "Warehouse",
                table: "WarehouseLocationUsage",
                column: "WarehouseLocationId",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropTable(
                name: "WarehouseLocationUsage",
                schema: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLocation_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLocation_PreferredMassUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLocation_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "Length",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "MaxMass",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "MaxVolume",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "PreferredLengthUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "PreferredMassUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "Width",
                schema: "Warehouse",
                table: "WarehouseLocation");
        }
    }
}
