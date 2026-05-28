using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReceiptLineRowNumberProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RowNumber",
                schema: "Warehouse",
                table: "ReceiptLine",
                newName: "Sequence");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptLine_ReceiptId_RowNumber",
                schema: "Warehouse",
                table: "ReceiptLine",
                newName: "IX_ReceiptLine_ReceiptId_Sequence");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Warehouse",
                table: "Receipt",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "Warehouse",
                table: "Receipt");

            migrationBuilder.RenameColumn(
                name: "Sequence",
                schema: "Warehouse",
                table: "ReceiptLine",
                newName: "RowNumber");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptLine_ReceiptId_Sequence",
                schema: "Warehouse",
                table: "ReceiptLine",
                newName: "IX_ReceiptLine_ReceiptId_RowNumber");
        }
    }
}
