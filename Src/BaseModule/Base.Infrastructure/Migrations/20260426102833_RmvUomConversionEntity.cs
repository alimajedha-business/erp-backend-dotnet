using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RmvUomConversionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitOfMeasurementConversion",
                schema: "Warehouse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitOfMeasurementConversion",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FromUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Factor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurementConversion", x => x.Id);
                    table.CheckConstraint("CK_UomConv_Factor", "Factor > 0");
                    table.ForeignKey(
                        name: "FK_UnitOfMeasurementConversion_UnitOfMeasurement_FromUnitOfMeasurementId",
                        column: x => x.FromUnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitOfMeasurementConversion_UnitOfMeasurement_ToUnitOfMeasurementId",
                        column: x => x.ToUnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurementConversion_IsDeleted",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurementConversion_ToUnitOfMeasurementId",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                column: "ToUnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "UX_UomConv_Unique",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                columns: new[] { "FromUnitOfMeasurementId", "ToUnitOfMeasurementId" },
                unique: true);
        }
    }
}
