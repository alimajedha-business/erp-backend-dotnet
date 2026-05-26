using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityReferenceToFieldValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceDisplayValue",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReferenceEntityType",
                schema: "Warehouse",
                table: "ReceiptFieldDefinition",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceDisplayValue",
                schema: "Warehouse",
                table: "ReceiptFieldValue");

            migrationBuilder.DropColumn(
                name: "ReferenceEntityType",
                schema: "Warehouse",
                table: "ReceiptFieldDefinition");
        }
    }
}
