using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompanyFromAttributeEnumValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeEnumValue_Companies_CompanyId",
                schema: "Warehouse",
                table: "AttributeEnumValue");

            migrationBuilder.DropIndex(
                name: "IX_AttributeEnumValue_CompanyId",
                schema: "Warehouse",
                table: "AttributeEnumValue");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "AttributeEnumValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AttributeEnumValue_CompanyId",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeEnumValue_Companies_CompanyId",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
