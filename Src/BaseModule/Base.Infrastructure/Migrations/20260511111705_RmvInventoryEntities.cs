using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RmvInventoryEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryLotAttributeValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryMovement",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryLot",
                schema: "Warehouse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryLot",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DimHash = table.Column<byte[]>(type: "varbinary(32)", maxLength: 32, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLot_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryLot_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryLotAttributeValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnumValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ValueBoolean = table.Column<bool>(type: "bit", nullable: true),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValueDecimal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    ValueInt = table.Column<int>(type: "int", nullable: true),
                    ValueText = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLotAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLotAttributeValue_AttributeEnumValue_EnumValueId",
                        column: x => x.EnumValueId,
                        principalSchema: "Warehouse",
                        principalTable: "AttributeEnumValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryLotAttributeValue_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Warehouse",
                        principalTable: "Attribute",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryLotAttributeValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryLotAttributeValue_InventoryLot_LotId",
                        column: x => x.LotId,
                        principalSchema: "Warehouse",
                        principalTable: "InventoryLot",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovement",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FromLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovementTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MovementDate = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    QuantityBase = table.Column<decimal>(type: "decimal(23,8)", precision: 23, scale: 8, nullable: false),
                    ReferenceDocId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryMovement", x => x.Id);
                    table.CheckConstraint("CK_InventoryMovement_Qty", "QuantityBase <> 0");
                    table.ForeignKey(
                        name: "FK_InventoryMovement_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryMovement_InventoryLot_LotId",
                        column: x => x.LotId,
                        principalSchema: "Warehouse",
                        principalTable: "InventoryLot",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryMovement_InventoryMovementType_MovementTypeId",
                        column: x => x.MovementTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "InventoryMovementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryMovement_WarehouseLocation_FromLocationId",
                        column: x => x.FromLocationId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryMovement_WarehouseLocation_ToLocationId",
                        column: x => x.ToLocationId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLot_CompanyId",
                schema: "Warehouse",
                table: "InventoryLot",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLot_IsDeleted",
                schema: "Warehouse",
                table: "InventoryLot",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLot_Item",
                schema: "Warehouse",
                table: "InventoryLot",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "UX_InvLot_Item_DimHash",
                schema: "Warehouse",
                table: "InventoryLot",
                columns: new[] { "ItemId", "Serial", "DimHash" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_EnumValueId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "EnumValueId" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_ValueBoolean",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "ValueBoolean" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_ValueDate",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "ValueDate" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_ValueDecimal",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "ValueDecimal" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_ValueInt",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "ValueInt" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_ValueText",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "ValueText" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_CompanyId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_EnumValueId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "EnumValueId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_IsDeleted",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_LotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_Company_Date",
                schema: "Warehouse",
                table: "InventoryMovement",
                columns: new[] { "CompanyId", "MovementDate" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_FromLocation",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "FromLocationId")
                .Annotation("SqlServer:Include", new[] { "QuantityBase" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_IsDeleted",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_Lot",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_MovementTypeId",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "MovementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovement_ToLocation",
                schema: "Warehouse",
                table: "InventoryMovement",
                column: "ToLocationId")
                .Annotation("SqlServer:Include", new[] { "QuantityBase" });
        }
    }
}
