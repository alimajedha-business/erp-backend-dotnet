using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixInventoryLotAttributeValueRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLotAttributeValue_InventoryLot_InventoryLotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.DropIndex(
                name: "IX_InventoryLotAttributeValue_InventoryLotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");

            migrationBuilder.DropColumn(
                name: "InventoryLotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InventoryLotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_InventoryLotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "InventoryLotId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLotAttributeValue_InventoryLot_InventoryLotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "InventoryLotId",
                principalSchema: "Warehouse",
                principalTable: "InventoryLot",
                principalColumn: "Id");
        }
    }
}
