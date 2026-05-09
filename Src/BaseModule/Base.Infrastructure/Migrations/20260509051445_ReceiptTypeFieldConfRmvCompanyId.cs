using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReceiptTypeFieldConfRmvCompanyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTypeFieldConfiguration_Companies_CompanyId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTypeFieldConfiguration_CompanyId_ReceiptTypeConfigurationId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeConfigurationId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                columns: new[] { "ReceiptTypeConfigurationId", "FieldDefinitionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeConfigurationId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_CompanyId_ReceiptTypeConfigurationId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                columns: new[] { "CompanyId", "ReceiptTypeConfigurationId", "FieldDefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "ReceiptTypeConfigurationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTypeFieldConfiguration_Companies_CompanyId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
