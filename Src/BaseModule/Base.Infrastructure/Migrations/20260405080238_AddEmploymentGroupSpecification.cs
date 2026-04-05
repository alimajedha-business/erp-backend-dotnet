using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmploymentGroupSpecification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmploymentGroupSpecification",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EmploymentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonthType = table.Column<int>(type: "int", nullable: false),
                    WorkMinutes = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    ValidTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentGroupSpecification", x => x.Id);
                    table.CheckConstraint("CK_EmploymentGroupSpecification_MonthType", "[MonthType] Between 1 and 2");
                    table.CheckConstraint("CK_EmploymentGroupSpecification_ValidRange", "[ValidTo] IS NULL OR [ValidTo] >= [ValidFrom]");
                    table.CheckConstraint("CK_EmploymentGroupSpecification_WorkMinutes", "[WorkMinutes] >=0 AND [WorkMinutes] <= 720");
                    table.ForeignKey(
                        name: "FK_EmploymentGroupSpecification_EmploymentGroup_EmploymentGroupId",
                        column: x => x.EmploymentGroupId,
                        principalSchema: "HCM",
                        principalTable: "EmploymentGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpGroupSpec_Validity",
                schema: "HCM",
                table: "EmploymentGroupSpecification",
                columns: new[] { "EmploymentGroupId", "ValidFrom", "ValidTo" });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentGroupSpecification_IsDeleted",
                schema: "HCM",
                table: "EmploymentGroupSpecification",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmploymentGroupSpecification",
                schema: "HCM");
        }
    }
}
