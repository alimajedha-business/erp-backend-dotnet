using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompanyIdFromGeneralEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitOfMeasurement_Companies_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseType_Companies_CompanyId",
                schema: "Warehouse",
                table: "WarehouseType");

            migrationBuilder.DropIndex(
                name: "UX_WarehouseType_Company_Code",
                schema: "Warehouse",
                table: "WarehouseType");

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasurement_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.DropIndex(
                name: "IX_Category_CompanyId",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "UX_Attribute_Company_Code",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "WarehouseType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "CategoryPath",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.AlterColumn<bool>(
                name: "IsLastLevel",
                schema: "Warehouse",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "UX_WarehouseType_Code",
                schema: "Warehouse",
                table: "WarehouseType",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_UnitOfMeasurement_Code",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Category_Company_Code",
                schema: "Warehouse",
                table: "Category",
                columns: new[] { "CompanyId", "Code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_WarehouseType_Code",
                schema: "Warehouse",
                table: "WarehouseType");

            migrationBuilder.DropIndex(
                name: "UX_UnitOfMeasurement_Code",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.DropIndex(
                name: "UX_Category_Company_Code",
                schema: "Warehouse",
                table: "Category");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "WarehouseType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsLastLevel",
                schema: "Warehouse",
                table: "Category",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CategoryPath",
                schema: "Warehouse",
                table: "Category",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UX_WarehouseType_Company_Code",
                schema: "Warehouse",
                table: "WarehouseType",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurement_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_CompanyId",
                schema: "Warehouse",
                table: "Category",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "UX_Attribute_Company_Code",
                schema: "Warehouse",
                table: "Category",
                columns: new[] { "Code", "CompanyId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOfMeasurement_Companies_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseType_Companies_CompanyId",
                schema: "Warehouse",
                table: "WarehouseType",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
