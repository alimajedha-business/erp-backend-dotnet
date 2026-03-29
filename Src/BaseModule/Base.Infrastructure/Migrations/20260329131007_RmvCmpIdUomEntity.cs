using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RmvCmpIdUomEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_Companies_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropIndex(
                name: "IX_ItemUnitOfMeasurementConversion_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurementConversion_Companies_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
