using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntitiesRelatedtoEmployeeEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                schema: "HCM",
                table: "EmployeeWarriorRecord",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IncentiveGroup",
                schema: "HCM",
                table: "EmployeeWarriorRecord",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EducationalStatus",
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
                    table.PrimaryKey("PK_EducationalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelativeType",
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
                    table.PrimaryKey("PK_RelativeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRelative",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MaritalStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RelativeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhysicalCondition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EducationalStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRelative", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeRelative_EducationalStatus_EducationalStatusId",
                        column: x => x.EducationalStatusId,
                        principalSchema: "HCM",
                        principalTable: "EducationalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeRelative_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "HCM",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeRelative_MaritalStatus_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalSchema: "HCM",
                        principalTable: "MaritalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeRelative_RelativeType_RelativeTypeId",
                        column: x => x.RelativeTypeId,
                        principalSchema: "HCM",
                        principalTable: "RelativeType",
                        principalColumn: "Id");
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_EmployeeWarriorRecord_IncentiveGroup_Range",
                schema: "HCM",
                table: "EmployeeWarriorRecord",
                sql: "[IncentiveGroup] IS NULL OR ([IncentiveGroup] BETWEEN 1 AND 20)");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalStatus_IsDeleted",
                schema: "HCM",
                table: "EducationalStatus",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalStatus_Title",
                schema: "HCM",
                table: "EducationalStatus",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelative_EducationalStatus",
                schema: "HCM",
                table: "EmployeeRelative",
                column: "EducationalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelative_Employee",
                schema: "HCM",
                table: "EmployeeRelative",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelative_IsDeleted",
                schema: "HCM",
                table: "EmployeeRelative",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelative_MaritalStatus",
                schema: "HCM",
                table: "EmployeeRelative",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelative_RelativeType",
                schema: "HCM",
                table: "EmployeeRelative",
                column: "RelativeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RelativeType_IsDeleted",
                schema: "HCM",
                table: "RelativeType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RelativeType_Title",
                schema: "HCM",
                table: "RelativeType",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRelative",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "EducationalStatus",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "RelativeType",
                schema: "HCM");

            migrationBuilder.DropCheckConstraint(
                name: "CK_EmployeeWarriorRecord_IncentiveGroup_Range",
                schema: "HCM",
                table: "EmployeeWarriorRecord");

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                schema: "HCM",
                table: "EmployeeWarriorRecord",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IncentiveGroup",
                schema: "HCM",
                table: "EmployeeWarriorRecord",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
