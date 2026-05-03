using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorMilitaryAndMaritalStatuses : Migration
    {
        /// <inheritdoc />
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

            // Drop old MaritalStatus PK dynamically
            migrationBuilder.Sql(@"
DECLARE @pkName sysname;

SELECT @pkName = kc.name
FROM sys.key_constraints kc
INNER JOIN sys.tables t ON kc.parent_object_id = t.object_id
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE s.name = 'General'
  AND t.name = 'marital_statuses'
  AND kc.type = 'PK';

IF @pkName IS NOT NULL
BEGIN
    EXEC(N'ALTER TABLE [General].[marital_statuses] DROP CONSTRAINT [' + @pkName + N']');
END
");

            // Drop old MilitaryServiceStatus PK dynamically
            migrationBuilder.Sql(@"
DECLARE @pkName sysname;

SELECT @pkName = kc.name
FROM sys.key_constraints kc
INNER JOIN sys.tables t ON kc.parent_object_id = t.object_id
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE s.name = 'General'
  AND t.name = 'military_service_statuses'
  AND kc.type = 'PK';

IF @pkName IS NOT NULL
BEGIN
    EXEC(N'ALTER TABLE [General].[military_service_statuses] DROP CONSTRAINT [' + @pkName + N']');
END
");

            migrationBuilder.RenameTable(
                name: "marital_statuses",
                schema: "General",
                newName: "MaritalStatus",
                newSchema: "HCM");

            migrationBuilder.RenameTable(
                name: "military_service_statuses",
                schema: "General",
                newName: "MilitaryServiceStatus",
                newSchema: "HCM");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "HCM",
                table: "MaritalStatus",
                type: "nvarchar(100)",
                maxLength: 100,
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

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaritalStatus",
                schema: "HCM",
                table: "MaritalStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MilitaryServiceStatus",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatus_IsDeleted",
                schema: "HCM",
                table: "MaritalStatus",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatus_Title",
                schema: "HCM",
                table: "MaritalStatus",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryServiceStatus_IsDeleted",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryServiceStatus_Title",
                schema: "HCM",
                table: "MilitaryServiceStatus",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_MilitaryServiceStatus_MilitaryServiceStatusId",
                schema: "HCM",
                table: "Employee",
                column: "MilitaryServiceStatusId",
                principalSchema: "HCM",
                principalTable: "MilitaryServiceStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_MaritalStatus_MaritalStatusId",
                schema: "HCM",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_MilitaryServiceStatus_MilitaryServiceStatusId",
                schema: "HCM",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaritalStatus",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MilitaryServiceStatus",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatus_IsDeleted",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropIndex(
                name: "IX_MaritalStatus_Title",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropIndex(
                name: "IX_MilitaryServiceStatus_IsDeleted",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.DropIndex(
                name: "IX_MilitaryServiceStatus_Title",
                schema: "HCM",
                table: "MilitaryServiceStatus");

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

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.RenameTable(
                name: "MaritalStatus",
                schema: "HCM",
                newName: "marital_statuses",
                newSchema: "General");

            migrationBuilder.RenameTable(
                name: "MilitaryServiceStatus",
                schema: "HCM",
                newName: "military_service_statuses",
                newSchema: "General");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "General",
                table: "marital_statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "General",
                table: "marital_statuses",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "General",
                table: "military_service_statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "General",
                table: "military_service_statuses",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_military_service_statuses",
                schema: "General",
                table: "military_service_statuses",
                column: "Id");

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
