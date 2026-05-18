using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNextWorkLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkLocation_WorkLocation_NextWorkLocationId",
                schema: "HCM",
                table: "WorkLocation");

            migrationBuilder.DropIndex(
                name: "IX_WorkLocation_NextWorkLocation",
                schema: "HCM",
                table: "WorkLocation");

            migrationBuilder.DropColumn(
                name: "NextWorkLocationId",
                schema: "HCM",
                table: "WorkLocation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NextWorkLocationId",
                schema: "HCM",
                table: "WorkLocation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkLocation_NextWorkLocation",
                schema: "HCM",
                table: "WorkLocation",
                column: "NextWorkLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkLocation_WorkLocation_NextWorkLocationId",
                schema: "HCM",
                table: "WorkLocation",
                column: "NextWorkLocationId",
                principalSchema: "HCM",
                principalTable: "WorkLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
