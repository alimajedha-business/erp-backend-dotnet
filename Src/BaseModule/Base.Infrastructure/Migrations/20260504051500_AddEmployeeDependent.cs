using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeDependent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeDependant",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EmployeeRelativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDependant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDependant_EmployeeRelative_EmployeeRelativeId",
                        column: x => x.EmployeeRelativeId,
                        principalSchema: "HCM",
                        principalTable: "EmployeeRelative",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDependant_EmployeeRelative",
                schema: "HCM",
                table: "EmployeeDependant",
                column: "EmployeeRelativeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDependant_IsDeleted",
                schema: "HCM",
                table: "EmployeeDependant",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDependant",
                schema: "HCM");
        }
    }
}
