using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemPropType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weigh",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "CubeVolume",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                newName: "Volume");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                newName: "Weigh");

            migrationBuilder.RenameColumn(
                name: "Volume",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                newName: "CubeVolume");
        }
    }
}
