using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseLocationEntityParent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_Companies_CompanyId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseLocation_CompanyId",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "WarehouseLocation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_CompanyId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocation_Companies_CompanyId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
