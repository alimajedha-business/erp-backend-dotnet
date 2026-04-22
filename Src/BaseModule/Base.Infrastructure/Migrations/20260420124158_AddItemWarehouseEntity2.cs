using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddItemWarehouseEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouseLocation_WarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.DropIndex(
                name: "IX_ItemWarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.DropColumn(
                name: "WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_LocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "LocationId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemWarehouseLocation_WarehouseLocation_LocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.DropIndex(
                name: "IX_ItemWarehouseLocation_LocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "WarehouseLocationId");

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
    }
}
