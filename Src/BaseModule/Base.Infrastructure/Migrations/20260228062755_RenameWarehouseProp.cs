using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameWarehouseProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUnit_Companies_CompanyId",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_CompanyUnit_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyUnit",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropIndex(
                name: "IX_CompanyUnit_CompanyId",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropIndex(
                name: "IX_CompanyUnit_IsDeleted",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "General",
                table: "CompanyUnit");

            migrationBuilder.EnsureSchema(
                name: "Shared");

            migrationBuilder.RenameTable(
                name: "CompanyUnit",
                schema: "General",
                newName: "company_units",
                newSchema: "Shared");

            migrationBuilder.RenameColumn(
                name: "MaxRialValue",
                schema: "Warehouse",
                table: "Warehouse",
                newName: "MaxMonetaryValue");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Shared",
                table: "company_units",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Shared",
                table: "company_units",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "Shared",
                table: "company_units",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_company_units",
                schema: "Shared",
                table: "company_units",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_company_units_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "CompanyUnitId",
                principalSchema: "Shared",
                principalTable: "company_units",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_company_units_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_company_units",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.RenameTable(
                name: "company_units",
                schema: "Shared",
                newName: "CompanyUnit",
                newSchema: "General");

            migrationBuilder.RenameColumn(
                name: "MaxMonetaryValue",
                schema: "Warehouse",
                table: "Warehouse",
                newName: "MaxRialValue");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "General",
                table: "CompanyUnit",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "General",
                table: "CompanyUnit",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "General",
                table: "CompanyUnit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "General",
                table: "CompanyUnit",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "General",
                table: "CompanyUnit",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "General",
                table: "CompanyUnit",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "General",
                table: "CompanyUnit",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "General",
                table: "CompanyUnit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                schema: "General",
                table: "CompanyUnit",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                schema: "General",
                table: "CompanyUnit",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "General",
                table: "CompanyUnit",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyUnit",
                schema: "General",
                table: "CompanyUnit",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUnit_CompanyId",
                schema: "General",
                table: "CompanyUnit",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUnit_IsDeleted",
                schema: "General",
                table: "CompanyUnit",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUnit_Companies_CompanyId",
                schema: "General",
                table: "CompanyUnit",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_CompanyUnit_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "CompanyUnitId",
                principalSchema: "General",
                principalTable: "CompanyUnit",
                principalColumn: "Id");
        }
    }
}
