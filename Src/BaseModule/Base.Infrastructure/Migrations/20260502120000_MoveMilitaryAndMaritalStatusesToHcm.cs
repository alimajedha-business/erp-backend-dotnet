using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    public partial class MoveMilitaryAndMaritalStatusesToHcm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_marital_statuses_MaritalStatusId",
                schema: "HCM",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_military_service_statuses_MilitaryServiceStatusId",
                schema: "HCM",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "marital_statuses",
                schema: "General",
                newName: "marital_statuses",
                newSchema: "HCM");

            migrationBuilder.RenameTable(
                name: "military_service_statuses",
                schema: "General",
                newName: "military_service_statuses",
                newSchema: "HCM");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "HCM",
                table: "marital_statuses",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "HCM",
                table: "marital_statuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "HCM",
                table: "marital_statuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                schema: "HCM",
                table: "marital_statuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                schema: "HCM",
                table: "marital_statuses",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "HCM",
                table: "marital_statuses",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "HCM",
                table: "military_service_statuses",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "HCM",
                table: "military_service_statuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "HCM",
                table: "military_service_statuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                schema: "HCM",
                table: "military_service_statuses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                schema: "HCM",
                table: "military_service_statuses",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "HCM",
                table: "military_service_statuses",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "HCM",
                table: "marital_statuses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "HCM",
                table: "military_service_statuses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_marital_statuses_IsDeleted",
                schema: "HCM",
                table: "marital_statuses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_military_service_statuses_IsDeleted",
                schema: "HCM",
                table: "military_service_statuses",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_marital_statuses_MaritalStatusId",
                schema: "HCM",
                table: "Employee",
                column: "MaritalStatusId",
                principalSchema: "HCM",
                principalTable: "marital_statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_military_service_statuses_MilitaryServiceStatusId",
                schema: "HCM",
                table: "Employee",
                column: "MilitaryServiceStatusId",
                principalSchema: "HCM",
                principalTable: "military_service_statuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_marital_statuses_MaritalStatusId",
                schema: "HCM",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_military_service_statuses_MilitaryServiceStatusId",
                schema: "HCM",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_marital_statuses_IsDeleted",
                schema: "HCM",
                table: "marital_statuses");

            migrationBuilder.DropIndex(
                name: "IX_military_service_statuses_IsDeleted",
                schema: "HCM",
                table: "military_service_statuses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "HCM",
                table: "marital_statuses");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "HCM",
                table: "marital_statuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "HCM",
                table: "marital_statuses");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                schema: "HCM",
                table: "marital_statuses");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                schema: "HCM",
                table: "marital_statuses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "HCM",
                table: "marital_statuses");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "HCM",
                table: "military_service_statuses");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "HCM",
                table: "military_service_statuses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "HCM",
                table: "military_service_statuses");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                schema: "HCM",
                table: "military_service_statuses");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                schema: "HCM",
                table: "military_service_statuses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "HCM",
                table: "military_service_statuses");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "HCM",
                table: "marital_statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "HCM",
                table: "military_service_statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.RenameTable(
                name: "marital_statuses",
                schema: "HCM",
                newName: "marital_statuses",
                newSchema: "General");

            migrationBuilder.RenameTable(
                name: "military_service_statuses",
                schema: "HCM",
                newName: "military_service_statuses",
                newSchema: "General");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_marital_statuses_MaritalStatusId",
                schema: "HCM",
                table: "Employee",
                column: "MaritalStatusId",
                principalSchema: "General",
                principalTable: "marital_statuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_military_service_statuses_MilitaryServiceStatusId",
                schema: "HCM",
                table: "Employee",
                column: "MilitaryServiceStatusId",
                principalSchema: "General",
                principalTable: "military_service_statuses",
                principalColumn: "Id");
        }
    }
}
