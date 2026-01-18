using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrgStructureModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HCM");

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StatusChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StatusChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalStructure",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NodeType = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ValidFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    ValidTo = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationalStructure_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalStructure_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "HCM",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalStructure_OrganizationalStructure_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "HCM",
                        principalTable: "OrganizationalStructure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalStructure_Position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "HCM",
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyId",
                schema: "HCM",
                table: "Department",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_IsDeleted",
                schema: "HCM",
                table: "Department",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructure_CompanyId",
                schema: "HCM",
                table: "OrganizationalStructure",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructure_DepartmentId",
                schema: "HCM",
                table: "OrganizationalStructure",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructure_IsDeleted",
                schema: "HCM",
                table: "OrganizationalStructure",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructure_ParentId",
                schema: "HCM",
                table: "OrganizationalStructure",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructure_PositionId",
                schema: "HCM",
                table: "OrganizationalStructure",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_CompanyId",
                schema: "HCM",
                table: "Position",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_IsDeleted",
                schema: "HCM",
                table: "Position",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationalStructure",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "HCM");
        }
    }
}
