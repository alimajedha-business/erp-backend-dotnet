using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemUomConversionEqPrp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasNextLevel",
                schema: "Warehouse",
                table: "WarehouseLocation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ConversionEquation",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasNextLevel",
                schema: "Warehouse",
                table: "WarehouseLocation");

            migrationBuilder.DropColumn(
                name: "ConversionEquation",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion");
        }
    }
}
