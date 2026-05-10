using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RmvDimensionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    Code = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDiscrete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
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
    }
}
