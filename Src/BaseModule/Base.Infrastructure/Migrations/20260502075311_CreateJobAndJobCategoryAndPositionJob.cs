using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateJobAndJobCategoryAndPositionJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobCategory",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NextJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    JobCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelCode = table.Column<int>(type: "int", nullable: false),
                    Seniority = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_JobCategory_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalSchema: "HCM",
                        principalTable: "JobCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_Job_NextJobId",
                        column: x => x.NextJobId,
                        principalSchema: "HCM",
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_Job_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "HCM",
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PositionJob",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionJob_Job_JobId",
                        column: x => x.JobId,
                        principalSchema: "HCM",
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PositionJob_Position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "HCM",
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_CompanyId",
                schema: "HCM",
                table: "Job",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_IsDeleted",
                schema: "HCM",
                table: "Job",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobCategoryId",
                schema: "HCM",
                table: "Job",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_NextJobId",
                schema: "HCM",
                table: "Job",
                column: "NextJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_ParentId",
                schema: "HCM",
                table: "Job",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "UX_Job_Code",
                schema: "HCM",
                table: "Job",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobCategory_IsDeleted",
                schema: "HCM",
                table: "JobCategory",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_JobCategory_Code",
                schema: "HCM",
                table: "JobCategory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PositionJob_IsDeleted",
                schema: "HCM",
                table: "PositionJob",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PositionJob_JobId",
                schema: "HCM",
                table: "PositionJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionJob_PositionId",
                schema: "HCM",
                table: "PositionJob",
                column: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionJob",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "Job",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "JobCategory",
                schema: "HCM");

        }
    }
}