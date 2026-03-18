using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseLocationCodeConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UX_WarehouseLocation_Warehouse_Code",
                schema: "Warehouse",
                table: "WarehouseLocation",
                columns: new[] { "WarehouseId", "Code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_WarehouseLocation_Warehouse_Code",
                schema: "Warehouse",
                table: "WarehouseLocation");
        }
    }
}
