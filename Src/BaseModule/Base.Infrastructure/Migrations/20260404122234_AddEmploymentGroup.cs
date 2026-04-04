using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmploymentGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Position_CompanyId",
                schema: "HCM",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Department_CompanyId",
                schema: "HCM",
                table: "Department");

            migrationBuilder.CreateTable(
                name: "EmploymentGroup",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentGroup", x => x.Id);
                    table.CheckConstraint("CK_Name_Min_Length", "LEN(Name) >= 2");
                    table.ForeignKey(
                        name: "FK_EmploymentGroup_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UX_Position_CompanyId_Code",
                schema: "HCM",
                table: "Position",
                columns: new[] { "CompanyId", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Position_StatusChangeDate",
                schema: "HCM",
                table: "Position",
                sql: "([Status] = 0 AND [StatusChangeDate] IS NOT NULL) OR ([Status] = 1 AND [StatusChangeDate] IS NULL)");

            migrationBuilder.CreateIndex(
                name: "UX_Department_CompanyId_Code",
                schema: "HCM",
                table: "Department",
                columns: new[] { "CompanyId", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Department_StatusChangeDate",
                schema: "HCM",
                table: "Department",
                sql: "([Status] = 0 AND [StatusChangeDate] IS NOT NULL) OR ([Status] = 1 AND [StatusChangeDate] IS NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentGroup_CompanyId",
                schema: "HCM",
                table: "EmploymentGroup",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentGroup_IsDeleted",
                schema: "HCM",
                table: "EmploymentGroup",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmploymentGroup",
                schema: "HCM");

            migrationBuilder.DropIndex(
                name: "UX_Position_CompanyId_Code",
                schema: "HCM",
                table: "Position");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Position_StatusChangeDate",
                schema: "HCM",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "UX_Department_CompanyId_Code",
                schema: "HCM",
                table: "Department");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Department_StatusChangeDate",
                schema: "HCM",
                table: "Department");

            migrationBuilder.CreateIndex(
                name: "IX_Position_CompanyId",
                schema: "HCM",
                table: "Position",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyId",
                schema: "HCM",
                table: "Department",
                column: "CompanyId");
        }
    }
}
