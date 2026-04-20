using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddItemWarehouseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelNo",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemWarehouse",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReorderPoint = table.Column<decimal>(type: "decimal(22,4)", nullable: false),
                    CriticalPoint = table.Column<decimal>(type: "decimal(22,4)", nullable: false),
                    ReorderQuantity = table.Column<decimal>(type: "decimal(22,4)", nullable: false),
                    MaxStockLevel = table.Column<decimal>(type: "decimal(22,4)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemWarehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemWarehouse_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemWarehouse_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "Warehouse",
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemWarehouseLocation",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemWarehouseLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemWarehouseLocation_ItemWarehouse_ItemWarehouseId",
                        column: x => x.ItemWarehouseId,
                        principalSchema: "Warehouse",
                        principalTable: "ItemWarehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemWarehouseLocation_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_CategoryLevelConstraint_CodeLength",
                schema: "Warehouse",
                table: "CategoryLevelConstraint",
                sql: "CodeLength BETWEEN 1 AND 5");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouse_IsDeleted",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouse_ItemId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouse_WarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_IsDeleted",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_ItemWarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "ItemWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "WarehouseLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemWarehouseLocation",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemWarehouse",
                schema: "Warehouse");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CategoryLevelConstraint_CodeLength",
                schema: "Warehouse",
                table: "CategoryLevelConstraint");

            migrationBuilder.DropColumn(
                name: "LevelNo",
                schema: "Warehouse",
                table: "WarehouseLocation");
        }
    }
}
