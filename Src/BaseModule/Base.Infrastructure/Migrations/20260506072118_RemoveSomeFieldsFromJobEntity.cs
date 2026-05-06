using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSomeFieldsFromJobEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Job_CompanyId",
                schema: "HCM",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "UX_Job_Code",
                schema: "HCM",
                table: "Job");

            migrationBuilder.RenameIndex(
                name: "IX_Job_ParentId",
                schema: "HCM",
                table: "Job",
                newName: "IX_Job_Parent");

            migrationBuilder.RenameIndex(
                name: "IX_Job_NextJobId",
                schema: "HCM",
                table: "Job",
                newName: "IX_Job_NextJob");

            migrationBuilder.RenameIndex(
                name: "IX_Job_JobCategoryId",
                schema: "HCM",
                table: "Job",
                newName: "IX_Job_JobCategory");

            migrationBuilder.AlterColumn<bool>(
                name: "Seniority",
                schema: "HCM",
                table: "Job",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                schema: "HCM",
                table: "Job",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "NextJobId",
                schema: "HCM",
                table: "Job",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "HCM",
                table: "Job",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "HCM",
                table: "Job",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "UX_Job_CompanyId_Code",
                schema: "HCM",
                table: "Job",
                columns: new[] { "CompanyId", "Code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_Job_CompanyId_Code",
                schema: "HCM",
                table: "Job");

            migrationBuilder.RenameIndex(
                name: "IX_Job_Parent",
                schema: "HCM",
                table: "Job",
                newName: "IX_Job_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Job_NextJob",
                schema: "HCM",
                table: "Job",
                newName: "IX_Job_NextJobId");

            migrationBuilder.RenameIndex(
                name: "IX_Job_JobCategory",
                schema: "HCM",
                table: "Job",
                newName: "IX_Job_JobCategoryId");

            migrationBuilder.AlterColumn<bool>(
                name: "Seniority",
                schema: "HCM",
                table: "Job",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                schema: "HCM",
                table: "Job",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "NextJobId",
                schema: "HCM",
                table: "Job",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "HCM",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "HCM",
                table: "Job",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Job_CompanyId",
                schema: "HCM",
                table: "Job",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "UX_Job_Code",
                schema: "HCM",
                table: "Job",
                column: "Code",
                unique: true);
        }
    }
}
