using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyIdToWorkLocationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "HCM",
                table: "WorkLocation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "UX_WorkLocation_CompanyId_Title",
                schema: "HCM",
                table: "WorkLocation",
                columns: new[] { "CompanyId", "Title" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkLocation_Companies_CompanyId",
                schema: "HCM",
                table: "WorkLocation",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkLocation_Companies_CompanyId",
                schema: "HCM",
                table: "WorkLocation");

            migrationBuilder.DropIndex(
                name: "UX_WorkLocation_CompanyId_Title",
                schema: "HCM",
                table: "WorkLocation");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "HCM",
                table: "WorkLocation");
        }
    }
}
