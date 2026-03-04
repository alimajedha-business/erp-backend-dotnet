using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InventoryMovementTypeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Warehouse",
                table: "InventoryMovementType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "Warehouse",
                table: "InventoryMovementType",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "InventoryMovementType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IncreaseStockQuantity",
                schema: "Warehouse",
                table: "InventoryMovementType",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovementType_CompanyId",
                schema: "Warehouse",
                table: "InventoryMovementType",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMovementType_Companies_CompanyId",
                schema: "Warehouse",
                table: "InventoryMovementType",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMovementType_Companies_CompanyId",
                schema: "Warehouse",
                table: "InventoryMovementType");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMovementType_CompanyId",
                schema: "Warehouse",
                table: "InventoryMovementType");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Warehouse",
                table: "InventoryMovementType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "InventoryMovementType");

            migrationBuilder.DropColumn(
                name: "IncreaseStockQuantity",
                schema: "Warehouse",
                table: "InventoryMovementType");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Warehouse",
                table: "InventoryMovementType",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
