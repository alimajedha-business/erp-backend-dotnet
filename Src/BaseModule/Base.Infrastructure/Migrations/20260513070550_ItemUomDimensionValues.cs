using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemUomDimensionValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            DropForeignKeyIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "FK_ItemUnitOfMeasurement_Unit_PreferredLengthUnitId");
            DropForeignKeyIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "FK_ItemUnitOfMeasurement_Unit_PreferredMassUnitId");
            DropForeignKeyIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "FK_ItemUnitOfMeasurement_Unit_PreferredVolumeUnitId");
            DropForeignKeyIfExists(migrationBuilder, "Warehouse", "ReceiptLine", "FK_ReceiptLine_UnitOfMeasurement_UnitOfMeasurementId");

            DropIndexIfExists(migrationBuilder, "Warehouse", "ReceiptLine", "IX_ReceiptLine_CompanyId_ItemId");
            DropIndexIfExists(migrationBuilder, "Warehouse", "ItemWarehouseLocation", "IX_ItemWarehouseLocation_ItemWarehouseId");
            DropIndexIfExists(migrationBuilder, "Warehouse", "ItemWarehouse", "IX_ItemWarehouse_ItemId");
            DropIndexIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "IX_ItemUnitOfMeasurement_ItemId");
            DropIndexIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "IX_ItemUnitOfMeasurement_PreferredLengthUnitId");
            DropIndexIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "IX_ItemUnitOfMeasurement_PreferredMassUnitId");
            DropIndexIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "IX_ItemUnitOfMeasurement_PreferredVolumeUnitId");
            DropIndexIfExists(migrationBuilder, "Warehouse", "ItemAttribute", "IX_ItemAttribute_ItemId");

            PreserveReceiptLineUnitOfMeasurement(migrationBuilder);

            DropColumnIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "Height");
            DropColumnIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "Length");
            DropColumnIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "PreferredLengthUnitId");
            DropColumnIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "PreferredMassUnitId");
            DropColumnIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "PreferredVolumeUnitId");
            DropColumnIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "Volume");
            DropColumnIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "Weight");
            DropColumnIfExists(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "Width");

            RenameColumnIfExists(migrationBuilder, "Warehouse", "ReceiptLine", "UnitOfMeasurementId", "WarehouseLocationId");
            RenameIndexIfExists(migrationBuilder, "Warehouse", "ReceiptLine", "IX_ReceiptLine_UnitOfMeasurementId", "IX_ReceiptLine_WarehouseLocationId");

            RecreateAsRowVersionIfNeeded(migrationBuilder, "Warehouse", "WarehouseLocationUsage", "RowVersion");

            AddColumnIfMissing(migrationBuilder, "Warehouse", "Receipt", "Status", "int NOT NULL CONSTRAINT [DF_Receipt_Status] DEFAULT 0");

            AddColumnIfMissing(migrationBuilder, "Warehouse", "Item", "Height", "decimal(28,14) NULL");
            AddColumnIfMissing(migrationBuilder, "Warehouse", "Item", "Length", "decimal(28,14) NULL");
            AddColumnIfMissing(migrationBuilder, "Warehouse", "Item", "PreferredLengthUnitId", "uniqueidentifier NULL");
            AddColumnIfMissing(migrationBuilder, "Warehouse", "Item", "PreferredMassUnitId", "uniqueidentifier NULL");
            AddColumnIfMissing(migrationBuilder, "Warehouse", "Item", "PreferredVolumeUnitId", "uniqueidentifier NULL");
            AddColumnIfMissing(migrationBuilder, "Warehouse", "Item", "Volume", "decimal(28,14) NULL");
            AddColumnIfMissing(migrationBuilder, "Warehouse", "Item", "Weight", "decimal(28,14) NULL");
            AddColumnIfMissing(migrationBuilder, "Warehouse", "Item", "Width", "decimal(28,14) NULL");

            migrationBuilder.CreateTable(
                name: "InventoryBalance",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryLotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OnHandQuantity = table.Column<decimal>(type: "decimal(22,14)", precision: 22, scale: 14, nullable: false),
                    ReservedQuantity = table.Column<decimal>(type: "decimal(22,14)", precision: 22, scale: 14, nullable: false),
                    BlockedQuantity = table.Column<decimal>(type: "decimal(22,14)", precision: 22, scale: 14, nullable: false),
                    OccupiedMass = table.Column<decimal>(type: "decimal(22,14)", precision: 22, scale: 14, nullable: false),
                    OccupiedVolume = table.Column<decimal>(type: "decimal(22,14)", precision: 22, scale: 14, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
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
                    table.PrimaryKey("PK_InventoryBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryBalance_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryLot",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockKeyHash = table.Column<byte[]>(type: "varbinary(32)", maxLength: 32, nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                name: "ReceiptLineMeasurementValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ReceiptLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: true),
                    Length = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: true),
                    Width = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: true),
                    Height = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptLineMeasurementValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptLineMeasurementValue_ItemUnitOfMeasurement_ItemUnitOfMeasurementId",
                        column: x => x.ItemUnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "ItemUnitOfMeasurement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptLineMeasurementValue_ReceiptLine_ReceiptLineId",
                        column: x => x.ReceiptLineId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryLotAttributeValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StringValue = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IntValue = table.Column<int>(type: "int", nullable: true),
                    DecimalValue = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    DateTimeValue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BooleanValue = table.Column<bool>(type: "bit", nullable: true),
                    EnumReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InventoryLotId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_InventoryLotAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLotAttributeValue_AttributeEnumValue_EnumReferenceId",
                        column: x => x.EnumReferenceId,
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
                        name: "FK_InventoryLotAttributeValue_InventoryLot_InventoryLotId",
                        column: x => x.InventoryLotId,
                        principalSchema: "Warehouse",
                        principalTable: "InventoryLot",
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
                    SourceDocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceDocumentLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MovementDate = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(23,8)", precision: 23, scale: 8, nullable: false),
                    MovementTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_InventoryMovement", x => x.Id);
                    table.CheckConstraint("CK_InventoryMovement_Qty", "Quantity <> 0");
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

            MigrateReceiptLineMeasurementValues(migrationBuilder);

            CreateIndexIfMissing(migrationBuilder, "Warehouse", "ReceiptLine", "IX_ReceiptLine_CompanyId_ItemId_WarehouseLocationId", "[CompanyId], [ItemId], [WarehouseLocationId]");
            CreateIndexIfMissing(migrationBuilder, "Warehouse", "ItemWarehouseLocation", "UX_ItemWarehouseLocation_ItemWarehouse_Location", "[ItemWarehouseId], [WarehouseLocationId]", unique: true);
            CreateIndexIfMissing(migrationBuilder, "Warehouse", "ItemWarehouse", "UX_ItemWarehouse_Item_Warehouse", "[ItemId], [WarehouseId]", unique: true);
            CreateIndexIfMissing(migrationBuilder, "Warehouse", "ItemUnitOfMeasurement", "UX_ItemUnitOfMeasurement_Item_Uom", "[ItemId], [UnitOfMeasurementId]", unique: true);
            CreateIndexIfMissing(migrationBuilder, "Warehouse", "ItemAttribute", "UX_ItemAttribute_Item_Attribute", "[ItemId], [AttributeId]", unique: true);
            CreateIndexIfMissing(migrationBuilder, "Warehouse", "Item", "IX_Item_PreferredLengthUnitId", "[PreferredLengthUnitId]");
            CreateIndexIfMissing(migrationBuilder, "Warehouse", "Item", "IX_Item_PreferredMassUnitId", "[PreferredMassUnitId]");
            CreateIndexIfMissing(migrationBuilder, "Warehouse", "Item", "IX_Item_PreferredVolumeUnitId", "[PreferredVolumeUnitId]");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBalance_CompanyId_ItemId_WarehouseId_WarehouseLocationId_InventoryLotId",
                schema: "Warehouse",
                table: "InventoryBalance",
                columns: new[] { "CompanyId", "ItemId", "WarehouseId", "WarehouseLocationId", "InventoryLotId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryBalance_IsDeleted",
                schema: "Warehouse",
                table: "InventoryBalance",
                column: "IsDeleted");

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
                columns: new[] { "ItemId", "SerialNumber", "StockKeyHash" },
                unique: true,
                filter: "[SerialNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_BooleanValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "BooleanValue" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_DateTimeValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "DateTimeValue" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_DecimalValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "DecimalValue" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_EnumReferenceId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "EnumReferenceId" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_IntValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "IntValue" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_AttributeId_StringValue",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "AttributeId", "StringValue" })
                .Annotation("SqlServer:Include", new[] { "LotId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_CompanyId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_EnumReferenceId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "EnumReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_InventoryLotId",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "InventoryLotId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotAttributeValue_IsDeleted",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_InventoryLotAttributeValue_Lot_Attribute",
                schema: "Warehouse",
                table: "InventoryLotAttributeValue",
                columns: new[] { "LotId", "AttributeId" },
                unique: true);

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
                .Annotation("SqlServer:Include", new[] { "Quantity" });

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
                .Annotation("SqlServer:Include", new[] { "Quantity" });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLineMeasurementValue_IsDeleted",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLineMeasurementValue_ItemUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                column: "ItemUnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLineMeasurementValue_ReceiptLineId_ItemUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ReceiptLineMeasurementValue",
                columns: new[] { "ReceiptLineId", "ItemUnitOfMeasurementId" },
                unique: true);

            AddForeignKeyIfMissing(migrationBuilder, "Warehouse", "Item", "FK_Item_Unit_PreferredLengthUnitId", "PreferredLengthUnitId", "Warehouse", "Unit", "Id");
            AddForeignKeyIfMissing(migrationBuilder, "Warehouse", "Item", "FK_Item_Unit_PreferredMassUnitId", "PreferredMassUnitId", "Warehouse", "Unit", "Id");
            AddForeignKeyIfMissing(migrationBuilder, "Warehouse", "Item", "FK_Item_Unit_PreferredVolumeUnitId", "PreferredVolumeUnitId", "Warehouse", "Unit", "Id");
            AddForeignKeyIfMissing(migrationBuilder, "Warehouse", "ReceiptLine", "FK_ReceiptLine_WarehouseLocation_WarehouseLocationId", "WarehouseLocationId", "Warehouse", "WarehouseLocation", "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Unit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptLine_WarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropTable(
                name: "InventoryBalance",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryLotAttributeValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryMovement",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ReceiptLineMeasurementValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryLot",
                schema: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptLine_CompanyId_ItemId_WarehouseLocationId",
                schema: "Warehouse",
                table: "ReceiptLine");

            migrationBuilder.DropIndex(
                name: "UX_ItemWarehouseLocation_ItemWarehouse_Location",
                schema: "Warehouse",
                table: "ItemWarehouseLocation");

            migrationBuilder.DropIndex(
                name: "UX_ItemWarehouse_Item_Warehouse",
                schema: "Warehouse",
                table: "ItemWarehouse");

            migrationBuilder.DropIndex(
                name: "UX_ItemUnitOfMeasurement_Item_Uom",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropIndex(
                name: "UX_ItemAttribute_Item_Attribute",
                schema: "Warehouse",
                table: "ItemAttribute");

            migrationBuilder.DropIndex(
                name: "IX_Item_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_PreferredMassUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Warehouse",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Length",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PreferredLengthUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PreferredMassUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Volume",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Width",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "WarehouseLocationId",
                schema: "Warehouse",
                table: "ReceiptLine",
                newName: "UnitOfMeasurementId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptLine_WarehouseLocationId",
                schema: "Warehouse",
                table: "ReceiptLine",
                newName: "IX_ReceiptLine_UnitOfMeasurementId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                schema: "Warehouse",
                table: "WarehouseLocationUsage",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "rowversion",
                oldRowVersion: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                schema: "Warehouse",
                table: "ReceiptLine",
                type: "decimal(22,4)",
                precision: 22,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Volume",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "decimal(28,14)",
                precision: 28,
                scale: 14,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLine_CompanyId_ItemId",
                schema: "Warehouse",
                table: "ReceiptLine",
                columns: new[] { "CompanyId", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_ItemWarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "ItemWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouse_ItemId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredLengthUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredMassUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredVolumeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttribute_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredLengthUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredLengthUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredMassUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredMassUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_Unit_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "PreferredVolumeUnitId",
                principalSchema: "Warehouse",
                principalTable: "Unit",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptLine_UnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "UnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id");
        }

        private static void DropForeignKeyIfExists(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string foreignKey)
        {
            migrationBuilder.Sql($@"
IF OBJECT_ID(N'[{schema}].[{foreignKey}]', N'F') IS NOT NULL
BEGIN
    ALTER TABLE [{schema}].[{table}] DROP CONSTRAINT [{foreignKey}];
END");
        }

        private static void AddForeignKeyIfMissing(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string foreignKey,
            string column,
            string principalSchema,
            string principalTable,
            string principalColumn)
        {
            migrationBuilder.Sql($@"
IF OBJECT_ID(N'[{schema}].[{foreignKey}]', N'F') IS NULL
    AND COL_LENGTH(N'{schema}.{table}', N'{column}') IS NOT NULL
    AND OBJECT_ID(N'[{principalSchema}].[{principalTable}]', N'U') IS NOT NULL
BEGIN
    ALTER TABLE [{schema}].[{table}]
        ADD CONSTRAINT [{foreignKey}]
        FOREIGN KEY ([{column}]) REFERENCES [{principalSchema}].[{principalTable}] ([{principalColumn}]);
END");
        }

        private static void DropIndexIfExists(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string index)
        {
            migrationBuilder.Sql($@"
IF EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'{index}'
      AND object_id = OBJECT_ID(N'[{schema}].[{table}]')
)
BEGIN
    DROP INDEX [{index}] ON [{schema}].[{table}];
END");
        }

        private static void CreateIndexIfMissing(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string index,
            string columns,
            bool unique = false)
        {
            var uniqueSql = unique ? "UNIQUE " : string.Empty;

            migrationBuilder.Sql($@"
IF OBJECT_ID(N'[{schema}].[{table}]', N'U') IS NOT NULL
    AND NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE name = N'{index}'
          AND object_id = OBJECT_ID(N'[{schema}].[{table}]')
    )
BEGIN
    CREATE {uniqueSql}INDEX [{index}] ON [{schema}].[{table}] ({columns});
END");
        }

        private static void DropColumnIfExists(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string column)
        {
            migrationBuilder.Sql($@"
IF COL_LENGTH(N'{schema}.{table}', N'{column}') IS NOT NULL
BEGIN
    ALTER TABLE [{schema}].[{table}] DROP COLUMN [{column}];
END");
        }

        private static void AddColumnIfMissing(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string column,
            string definition)
        {
            migrationBuilder.Sql($@"
IF COL_LENGTH(N'{schema}.{table}', N'{column}') IS NULL
BEGIN
    ALTER TABLE [{schema}].[{table}] ADD [{column}] {definition};
END");
        }

        private static void RenameColumnIfExists(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string oldName,
            string newName)
        {
            migrationBuilder.Sql($@"
IF COL_LENGTH(N'{schema}.{table}', N'{oldName}') IS NOT NULL
    AND COL_LENGTH(N'{schema}.{table}', N'{newName}') IS NULL
BEGIN
    EXEC sp_rename N'[{schema}].[{table}].[{oldName}]', N'{newName}', N'COLUMN';
END");
        }

        private static void RenameIndexIfExists(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string oldName,
            string newName)
        {
            migrationBuilder.Sql($@"
IF EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'{oldName}'
      AND object_id = OBJECT_ID(N'[{schema}].[{table}]')
)
AND NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'{newName}'
      AND object_id = OBJECT_ID(N'[{schema}].[{table}]')
)
BEGIN
    EXEC sp_rename N'[{schema}].[{table}].[{oldName}]', N'{newName}', N'INDEX';
END");
        }

        private static void RecreateAsRowVersionIfNeeded(
            MigrationBuilder migrationBuilder,
            string schema,
            string table,
            string column)
        {
            migrationBuilder.Sql($@"
IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID(N'[{schema}].[{table}]')
      AND name = N'{column}'
      AND system_type_id <> 189
)
BEGIN
    ALTER TABLE [{schema}].[{table}] DROP COLUMN [{column}];
    ALTER TABLE [{schema}].[{table}] ADD [{column}] rowversion NOT NULL;
END");
        }

        private static void PreserveReceiptLineUnitOfMeasurement(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'Warehouse.ReceiptLine', N'UnitOfMeasurementId') IS NOT NULL
BEGIN
    IF COL_LENGTH(N'Warehouse.ReceiptLine', N'MigrationUnitOfMeasurementId') IS NULL
    BEGIN
        ALTER TABLE [Warehouse].[ReceiptLine] ADD [MigrationUnitOfMeasurementId] uniqueidentifier NULL;
    END;

    EXEC(N'
        UPDATE [Warehouse].[ReceiptLine]
        SET [MigrationUnitOfMeasurementId] = [UnitOfMeasurementId]
        WHERE [MigrationUnitOfMeasurementId] IS NULL;
    ');
END");
        }

        private static void MigrateReceiptLineMeasurementValues(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[Warehouse].[ReceiptLineMeasurementValue]', N'U') IS NOT NULL
    AND COL_LENGTH(N'Warehouse.ReceiptLine', N'MigrationUnitOfMeasurementId') IS NOT NULL
    AND COL_LENGTH(N'Warehouse.ReceiptLine', N'Quantity') IS NOT NULL
BEGIN
    INSERT INTO [Warehouse].[ItemUnitOfMeasurement]
        ([Id], [ItemId], [UnitOfMeasurementId], [UnitOrder], [CreatedAt], [IsDeleted], [UpdatedAt])
    SELECT
        NEWID(),
        source.[ItemId],
        source.[MigrationUnitOfMeasurementId],
        ISNULL((
            SELECT MAX(existing.[UnitOrder]) + 1
            FROM [Warehouse].[ItemUnitOfMeasurement] existing
            WHERE existing.[ItemId] = source.[ItemId]
        ), 1),
        SYSUTCDATETIME(),
        0,
        SYSUTCDATETIME()
    FROM (
        SELECT DISTINCT [ItemId], [MigrationUnitOfMeasurementId]
        FROM [Warehouse].[ReceiptLine]
        WHERE [MigrationUnitOfMeasurementId] IS NOT NULL
    ) source
    WHERE NOT EXISTS (
        SELECT 1
        FROM [Warehouse].[ItemUnitOfMeasurement] existing
        WHERE existing.[ItemId] = source.[ItemId]
          AND existing.[UnitOfMeasurementId] = source.[MigrationUnitOfMeasurementId]
    );

    INSERT INTO [Warehouse].[ReceiptLineMeasurementValue]
        ([Id], [ReceiptLineId], [ItemUnitOfMeasurementId], [Quantity], [CreatedAt], [IsDeleted], [UpdatedAt])
    SELECT
        NEWID(),
        receiptLine.[Id],
        itemUnit.[Id],
        CAST(receiptLine.[Quantity] AS decimal(28,14)),
        SYSUTCDATETIME(),
        0,
        SYSUTCDATETIME()
    FROM [Warehouse].[ReceiptLine] receiptLine
    INNER JOIN [Warehouse].[ItemUnitOfMeasurement] itemUnit
        ON itemUnit.[ItemId] = receiptLine.[ItemId]
       AND itemUnit.[UnitOfMeasurementId] = receiptLine.[MigrationUnitOfMeasurementId]
    WHERE NOT EXISTS (
        SELECT 1
        FROM [Warehouse].[ReceiptLineMeasurementValue] existing
        WHERE existing.[ReceiptLineId] = receiptLine.[Id]
          AND existing.[ItemUnitOfMeasurementId] = itemUnit.[Id]
    );
END;

IF COL_LENGTH(N'Warehouse.ReceiptLine', N'WarehouseLocationId') IS NOT NULL
BEGIN
    IF EXISTS (
        SELECT 1
        FROM [Warehouse].[ReceiptLine] receiptLine
        WHERE NOT EXISTS (
            SELECT 1
            FROM [Warehouse].[WarehouseLocation] warehouseLocation
            WHERE warehouseLocation.[Id] = receiptLine.[WarehouseLocationId]
        )
    )
    BEGIN
        DECLARE @DefaultWarehouseLocationId uniqueidentifier;

        SELECT TOP (1) @DefaultWarehouseLocationId = [Id]
        FROM [Warehouse].[WarehouseLocation]
        WHERE [CanStoreItem] = 1
          AND [IsDeleted] = 0
        ORDER BY [UpdatedAt] DESC, [CreatedAt] DESC;

        IF @DefaultWarehouseLocationId IS NULL
        BEGIN
            THROW 51000, 'Cannot migrate ReceiptLine.WarehouseLocationId because no active storable WarehouseLocation exists.', 1;
        END;

        UPDATE receiptLine
        SET [WarehouseLocationId] = @DefaultWarehouseLocationId
        FROM [Warehouse].[ReceiptLine] receiptLine
        WHERE NOT EXISTS (
            SELECT 1
            FROM [Warehouse].[WarehouseLocation] warehouseLocation
            WHERE warehouseLocation.[Id] = receiptLine.[WarehouseLocationId]
        );
    END;
END;

IF COL_LENGTH(N'Warehouse.ReceiptLine', N'Quantity') IS NOT NULL
BEGIN
    ALTER TABLE [Warehouse].[ReceiptLine] DROP COLUMN [Quantity];
END;

IF COL_LENGTH(N'Warehouse.ReceiptLine', N'MigrationUnitOfMeasurementId') IS NOT NULL
BEGIN
    ALTER TABLE [Warehouse].[ReceiptLine] DROP COLUMN [MigrationUnitOfMeasurementId];
END");
        }
    }
}
