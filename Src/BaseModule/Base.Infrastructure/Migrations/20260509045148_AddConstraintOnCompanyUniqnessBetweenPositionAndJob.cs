using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintOnCompanyUniqnessBetweenPositionAndJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionJob_Job_JobId",
                schema: "HCM",
                table: "PositionJob");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionJob_Position_PositionId",
                schema: "HCM",
                table: "PositionJob");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "HCM",
                table: "PositionJob",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Position_CompanyId_Id",
                schema: "HCM",
                table: "Position",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Job_CompanyId_Id",
                schema: "HCM",
                table: "Job",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionJob_CompanyId_JobId",
                schema: "HCM",
                table: "PositionJob",
                columns: new[] { "CompanyId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionJob_CompanyId_PositionId",
                schema: "HCM",
                table: "PositionJob",
                columns: new[] { "CompanyId", "PositionId" });

            migrationBuilder.CreateIndex(
                name: "UX_Position_CompanyId_Id",
                schema: "HCM",
                table: "Position",
                columns: new[] { "CompanyId", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Job_CompanyId_Id",
                schema: "HCM",
                table: "Job",
                columns: new[] { "CompanyId", "Id" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PositionJob_Companies_CompanyId",
                schema: "HCM",
                table: "PositionJob",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionJob_Job_CompanyId_JobId",
                schema: "HCM",
                table: "PositionJob",
                columns: new[] { "CompanyId", "JobId" },
                principalSchema: "HCM",
                principalTable: "Job",
                principalColumns: new[] { "CompanyId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_PositionJob_Position_CompanyId_PositionId",
                schema: "HCM",
                table: "PositionJob",
                columns: new[] { "CompanyId", "PositionId" },
                principalSchema: "HCM",
                principalTable: "Position",
                principalColumns: new[] { "CompanyId", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PositionJob_Companies_CompanyId",
                schema: "HCM",
                table: "PositionJob");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionJob_Job_CompanyId_JobId",
                schema: "HCM",
                table: "PositionJob");

            migrationBuilder.DropForeignKey(
                name: "FK_PositionJob_Position_CompanyId_PositionId",
                schema: "HCM",
                table: "PositionJob");

            migrationBuilder.DropIndex(
                name: "IX_PositionJob_CompanyId_JobId",
                schema: "HCM",
                table: "PositionJob");

            migrationBuilder.DropIndex(
                name: "IX_PositionJob_CompanyId_PositionId",
                schema: "HCM",
                table: "PositionJob");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Position_CompanyId_Id",
                schema: "HCM",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "UX_Position_CompanyId_Id",
                schema: "HCM",
                table: "Position");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Job_CompanyId_Id",
                schema: "HCM",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "UX_Job_CompanyId_Id",
                schema: "HCM",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "HCM",
                table: "PositionJob");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionJob_Job_JobId",
                schema: "HCM",
                table: "PositionJob",
                column: "JobId",
                principalSchema: "HCM",
                principalTable: "Job",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PositionJob_Position_PositionId",
                schema: "HCM",
                table: "PositionJob",
                column: "PositionId",
                principalSchema: "HCM",
                principalTable: "Position",
                principalColumn: "Id");
        }
    }
}
