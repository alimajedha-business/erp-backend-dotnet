using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOrganizationalStructureItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationalStructureItem_OrganizationalStructureItem_OrganizationalStructureItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationalStructureItem_OrganizationalStructureItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem");

            migrationBuilder.DropColumn(
                name: "OrganizationalStructureItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationalStructureItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructureItem_OrganizationalStructureItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "OrganizationalStructureItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationalStructureItem_OrganizationalStructureItem_OrganizationalStructureItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "OrganizationalStructureItemId",
                principalSchema: "HCM",
                principalTable: "OrganizationalStructureItem",
                principalColumn: "Id");
        }
    }
}
