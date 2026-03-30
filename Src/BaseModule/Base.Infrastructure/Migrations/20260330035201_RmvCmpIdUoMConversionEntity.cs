using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RmvCmpIdUoMConversionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitOfMeasurementConversion_Companies_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion");

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasurementConversion_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurementConversion_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOfMeasurementConversion_Companies_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
