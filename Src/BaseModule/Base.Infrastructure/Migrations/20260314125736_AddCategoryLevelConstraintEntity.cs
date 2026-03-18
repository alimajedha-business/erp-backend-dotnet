using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryLevelConstraintEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryLevelConstraint",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LevelNo = table.Column<int>(type: "int", nullable: false),
                    CodeLength = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CategoryLevelConstraint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryLevelConstraint_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryLevelConstraint_IsDeleted",
                schema: "Warehouse",
                table: "CategoryLevelConstraint",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_CategoryLevelConst_Company_No",
                schema: "Warehouse",
                table: "CategoryLevelConstraint",
                columns: new[] { "CompanyId", "LevelNo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryLevelConstraint",
                schema: "Warehouse");
        }
    }
}
