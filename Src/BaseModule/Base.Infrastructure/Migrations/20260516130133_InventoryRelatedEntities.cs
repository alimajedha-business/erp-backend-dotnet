using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InventoryRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMovement_InventoryMovementType_MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMovement_FromLocation",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMovement_ToLocation",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToLocationId",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "FromLocationId",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "Direction",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Mass",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "InventoryLotLocationBalance",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(23,8)", precision: 23, scale: 8, nullable: false),
                    Mass = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLotLocationBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLotLocationBalance_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryLotLocationBalance_InventoryLot_LotId",
                        column: x => x.LotId,
                        principalSchema: "Warehouse",
                        principalTable: "InventoryLot",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryLotLocationBalance_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_FromLocation",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "FromLocationId")
                .Annotation("SqlServer:Include", new[] { "Quantity", "Mass", "Volume" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_SourceDocument",
                schema: "Warehouse",
                table: "InventoryMovement",
                columns: new[] { "SourceDocumentId", "SourceDocumentLineId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_ToLocation",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "ToLocationId")
                .Annotation("SqlServer:Include", new[] { "Quantity", "Mass", "Volume" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotLocationBalance_IsDeleted",
                schema: "Warehouse",
                table: "InventoryLotLocationBalance",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotLocationBalance_Location",
                schema: "Warehouse",
                table: "InventoryLotLocationBalance",
                columns: new[] { "CompanyId", "WarehouseLocationId" })
                .Annotation("SqlServer:Include", new[] { "Quantity", "Mass", "Volume" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotLocationBalance_Lot",
                schema: "Warehouse",
                table: "InventoryLotLocationBalance",
                columns: new[] { "CompanyId", "LotId" })
                .Annotation("SqlServer:Include", new[] { "Quantity", "Mass", "Volume" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotLocationBalance_LotId",
                schema: "Warehouse",
                table: "InventoryLotLocationBalance",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotLocationBalance_WarehouseLocationId",
                schema: "Warehouse",
                table: "InventoryLotLocationBalance",
                column: "WarehouseLocationId");

            migrationBuilder.CreateIndex(
                name: "UX_InventoryLotLocationBalance_Company_Lot_Location",
                schema: "Warehouse",
                table: "InventoryLotLocationBalance",
                columns: new[] { "CompanyId", "LotId", "WarehouseLocationId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMovement_InventoryMovementType_MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "MovementTypeId",
                principalSchema: "Warehouse",
                principalTable: "InventoryMovementType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryMovement_InventoryMovementType_MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropTable(
                name: "InventoryLotLocationBalance",
                schema: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMovement_FromLocation",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMovement_SourceDocument",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropIndex(
                name: "IX_InventoryMovement_ToLocation",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropColumn(
                name: "Direction",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropColumn(
                name: "Mass",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.DropColumn(
                name: "Volume",
                schema: "Warehouse",
                table: "InventoryMovement");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToLocationId",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FromLocationId",
                schema: "Warehouse",
                table: "InventoryMovement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_FromLocation",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "FromLocationId")
                .Annotation("SqlServer:Include", new[] { "Quantity" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_ToLocation",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "ToLocationId")
                .Annotation("SqlServer:Include", new[] { "Quantity" });

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryMovement_InventoryMovementType_MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "MovementTypeId",
                principalSchema: "Warehouse",
                principalTable: "InventoryMovementType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
