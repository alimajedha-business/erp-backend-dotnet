using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReceiptRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ItemAttributeId",
                principalSchema: "Warehouse",
                principalTable: "ItemAttribute",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ReceiptLineId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ItemAttributeId",
                principalSchema: "Warehouse",
                principalTable: "ItemAttribute",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ReceiptLineId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptLine",
                principalColumn: "Id");
        }
    }
}
