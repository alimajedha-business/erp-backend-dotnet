using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_PositionJob_PositionId",
                schema: "HCM",
                table: "PositionJob",
                newName: "IX_PositionJob_Position");

            migrationBuilder.RenameIndex(
                name: "IX_PositionJob_JobId",
                schema: "HCM",
                table: "PositionJob",
                newName: "IX_PositionJob_Job");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_PositionJob_Position",
                schema: "HCM",
                table: "PositionJob",
                newName: "IX_PositionJob_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_PositionJob_Job",
                schema: "HCM",
                table: "PositionJob",
                newName: "IX_PositionJob_JobId");
        }
    }
}
