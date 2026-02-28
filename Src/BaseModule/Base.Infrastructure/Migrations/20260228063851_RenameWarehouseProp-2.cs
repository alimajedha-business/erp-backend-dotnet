using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameWarehouseProp2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_WarehouseType_TypeId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                schema: "Warehouse",
                table: "Warehouse",
                newName: "WarehouseTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_TypeId",
                schema: "Warehouse",
                table: "Warehouse",
                newName: "IX_Warehouse_WarehouseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_WarehouseType_WarehouseTypeId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "WarehouseTypeId",
                principalSchema: "Warehouse",
                principalTable: "WarehouseType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_WarehouseType_WarehouseTypeId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.RenameColumn(
                name: "WarehouseTypeId",
                schema: "Warehouse",
                table: "Warehouse",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Warehouse_WarehouseTypeId",
                schema: "Warehouse",
                table: "Warehouse",
                newName: "IX_Warehouse_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_WarehouseType_TypeId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "TypeId",
                principalSchema: "Warehouse",
                principalTable: "WarehouseType",
                principalColumn: "Id");
        }
    }
}
