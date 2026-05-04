using System;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

using NGErp.Base.Infrastructure.DataAccess;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20260503110000_AddEducationLevelAndField")]
    public partial class AddEducationLevelAndField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldCode",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.DropColumn(
                name: "LevelCode",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.AddColumn<Guid>(
                name: "EducationFieldId",
                schema: "HCM",
                table: "EmployeeEducation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "HCM",
                table: "EmployeeEducation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationLevelId",
                schema: "HCM",
                table: "EmployeeEducation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationField",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevel",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationField_IsDeleted",
                schema: "HCM",
                table: "EducationField",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EducationField_Title",
                schema: "HCM",
                table: "EducationField",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevel_IsDeleted",
                schema: "HCM",
                table: "EducationLevel",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevel_Title",
                schema: "HCM",
                table: "EducationLevel",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducation_EducationField",
                schema: "HCM",
                table: "EmployeeEducation",
                column: "EducationFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducation_EducationLevel",
                schema: "HCM",
                table: "EmployeeEducation",
                column: "EducationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEducation_EducationField_EducationFieldId",
                schema: "HCM",
                table: "EmployeeEducation",
                column: "EducationFieldId",
                principalSchema: "HCM",
                principalTable: "EducationField",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEducation_EducationLevel_EducationLevelId",
                schema: "HCM",
                table: "EmployeeEducation",
                column: "EducationLevelId",
                principalSchema: "HCM",
                principalTable: "EducationLevel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducation_EducationField_EducationFieldId",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducation_EducationLevel_EducationLevelId",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.DropTable(
                name: "EducationField",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "EducationLevel",
                schema: "HCM");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEducation_EducationField",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEducation_EducationLevel",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.DropColumn(
                name: "EducationFieldId",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.DropColumn(
                name: "EducationLevelId",
                schema: "HCM",
                table: "EmployeeEducation");

            migrationBuilder.AddColumn<string>(
                name: "FieldCode",
                schema: "HCM",
                table: "EmployeeEducation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LevelCode",
                schema: "HCM",
                table: "EmployeeEducation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
