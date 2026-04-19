using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationNodeConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationNode_CompanyId",
                schema: "HCM",
                table: "OrganizationNode");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_CompanyId_DepartmentId",
                schema: "HCM",
                table: "OrganizationNode",
                columns: new[] { "CompanyId", "DepartmentId" },
                unique: true,
                filter: "[DepartmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_CompanyId_PositionId",
                schema: "HCM",
                table: "OrganizationNode",
                columns: new[] { "CompanyId", "PositionId" },
                unique: true,
                filter: "[PositionId] IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrganizationNode_OnlyOneReference",
                schema: "HCM",
                table: "OrganizationNode",
                sql: "(\r\n                ([DepartmentId] IS NOT NULL AND [PositionId] IS NULL AND [NodeType] = 1)\r\n                OR\r\n                ([DepartmentId] IS NULL AND [PositionId] IS NOT NULL AND [NodeType] = 2)\r\n            )");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationNode_CompanyId_DepartmentId",
                schema: "HCM",
                table: "OrganizationNode");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationNode_CompanyId_PositionId",
                schema: "HCM",
                table: "OrganizationNode");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrganizationNode_OnlyOneReference",
                schema: "HCM",
                table: "OrganizationNode");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_CompanyId",
                schema: "HCM",
                table: "OrganizationNode",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "ItemId",
                principalSchema: "Warehouse",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
