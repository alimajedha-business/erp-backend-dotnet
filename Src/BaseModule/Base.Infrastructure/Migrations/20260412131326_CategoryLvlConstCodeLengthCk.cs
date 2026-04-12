using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryLvlConstCodeLengthCk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttribute_Item_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CategoryLevelConstraint_CodeLength",
                schema: "Warehouse",
                table: "CategoryLevelConstraint",
                sql: "CodeLength BETWEEN 1 AND 5");

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

            migrationBuilder.DropCheckConstraint(
                name: "CK_CategoryLevelConstraint_CodeLength",
                schema: "Warehouse",
                table: "CategoryLevelConstraint");

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
