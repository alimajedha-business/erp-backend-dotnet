using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorMaritalStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaritalStatus_Companies_CompanyId",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropIndex(
                name: "UX_MaritalStatus_CompanyId_Type",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "HCM",
                table: "MaritalStatus",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UX_MaritalStatus_Title",
                schema: "HCM",
                table: "MaritalStatus",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_MaritalStatus_Title",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "HCM",
                table: "MaritalStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "UX_MaritalStatus_CompanyId_Type",
                schema: "HCM",
                table: "MaritalStatus",
                columns: new[] { "CompanyId", "Type" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaritalStatus_Companies_CompanyId",
                schema: "HCM",
                table: "MaritalStatus",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
