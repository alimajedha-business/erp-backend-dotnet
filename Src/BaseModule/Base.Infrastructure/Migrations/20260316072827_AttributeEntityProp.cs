using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AttributeEntityProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsItemAttribute",
                schema: "Warehouse",
                table: "Attribute");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "CanStoreItem",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AttributeEntity",
                schema: "Warehouse",
                table: "Attribute",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanStoreItem",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "AttributeEntity",
                schema: "Warehouse",
                table: "Attribute");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsItemAttribute",
                schema: "Warehouse",
                table: "Attribute",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
