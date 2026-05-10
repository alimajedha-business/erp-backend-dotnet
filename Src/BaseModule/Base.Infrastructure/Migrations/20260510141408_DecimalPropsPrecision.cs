using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DecimalPropsPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FactorToBase",
                schema: "Warehouse",
                table: "Unit",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "Width",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weigh",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Length",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Height",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CubeVolume",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FactorToBase",
                schema: "Warehouse",
                table: "Unit",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,14)",
                oldPrecision: 28,
                oldScale: 14);

            migrationBuilder.AlterColumn<decimal>(
                name: "Width",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,14)",
                oldPrecision: 28,
                oldScale: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Weigh",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,14)",
                oldPrecision: 28,
                oldScale: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Length",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,14)",
                oldPrecision: 28,
                oldScale: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Height",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,14)",
                oldPrecision: 28,
                oldScale: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CubeVolume",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(28,14)",
                oldPrecision: 28,
                oldScale: 14,
                oldNullable: true);
        }
    }
}
