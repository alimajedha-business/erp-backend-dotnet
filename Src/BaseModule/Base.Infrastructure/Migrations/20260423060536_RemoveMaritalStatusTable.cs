using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaritalStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_MaritalStatus_MaritalStatusId",
                schema: "HCM",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaritalStatus",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatus_IsDeleted",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropIndex(
                name: "UX_MaritalStatus_Title",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.RenameTable(
                name: "MaritalStatus",
                schema: "HCM",
                newName: "marital_statuses",
                newSchema: "General");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "General",
                table: "marital_statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "General",
                table: "marital_statuses",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_marital_statuses",
                schema: "General",
                table: "marital_statuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_marital_statuses_MaritalStatusId",
                schema: "HCM",
                table: "Employee",
                column: "MaritalStatusId",
                principalSchema: "General",
                principalTable: "marital_statuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_marital_statuses_MaritalStatusId",
                schema: "HCM",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_marital_statuses",
                schema: "General",
                table: "marital_statuses");

            migrationBuilder.RenameTable(
                name: "marital_statuses",
                schema: "General",
                newName: "MaritalStatus",
                newSchema: "HCM");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "HCM",
                table: "MaritalStatus",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "HCM",
                table: "MaritalStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "HCM",
                table: "MaritalStatus",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "HCM",
                table: "MaritalStatus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "HCM",
                table: "MaritalStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                schema: "HCM",
                table: "MaritalStatus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                schema: "HCM",
                table: "MaritalStatus",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "HCM",
                table: "MaritalStatus",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaritalStatus",
                schema: "HCM",
                table: "MaritalStatus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatus_IsDeleted",
                schema: "HCM",
                table: "MaritalStatus",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_MaritalStatus_Title",
                schema: "HCM",
                table: "MaritalStatus",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_MaritalStatus_MaritalStatusId",
                schema: "HCM",
                table: "Employee",
                column: "MaritalStatusId",
                principalSchema: "HCM",
                principalTable: "MaritalStatus",
                principalColumn: "Id");
        }
    }
}
