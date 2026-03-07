using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMeasurementDimensionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_Uom_Dimension_Title",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "IsDiscrete",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.RenameColumn(
                name: "Dimension",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                newName: "Code");

            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementDimensionId",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MeasurementDimension",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDiscrete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementDimension", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UX_Uom_Dimension_Title",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                columns: new[] { "MeasurementDimensionId", "Title" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementDimension_IsDeleted",
                schema: "Warehouse",
                table: "MeasurementDimension",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOfMeasurement_MeasurementDimension_MeasurementDimensionId",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                column: "MeasurementDimensionId",
                principalSchema: "Warehouse",
                principalTable: "MeasurementDimension",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitOfMeasurement_MeasurementDimension_MeasurementDimensionId",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.DropTable(
                name: "MeasurementDimension",
                schema: "Warehouse");

            migrationBuilder.DropIndex(
                name: "UX_Uom_Dimension_Title",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "MeasurementDimensionId",
                schema: "Warehouse",
                table: "UnitOfMeasurement");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                newName: "Dimension");

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscrete",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "UX_Uom_Dimension_Title",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                columns: new[] { "Dimension", "Title" },
                unique: true);
        }
    }
}
