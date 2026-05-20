using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRemittanceTypeConfigurationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Remittance",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    RemittanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RemittanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Remittance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remittance_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Remittance_RemittanceType_RemittanceTypeId",
                        column: x => x.RemittanceTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "RemittanceType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RemittanceTypeConfiguration",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RemittanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_RemittanceTypeConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceTypeConfiguration_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceTypeConfiguration_RemittanceType_RemittanceTypeId",
                        column: x => x.RemittanceTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "RemittanceType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RemittanceLine",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RemittanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: true),
                    PreferredMassUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PreferredVolumeUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_RemittanceLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceLine_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceLine_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RemittanceLine_Remittance_RemittanceId",
                        column: x => x.RemittanceId,
                        principalSchema: "Warehouse",
                        principalTable: "Remittance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceLine_SiUnit_PreferredMassUnitId",
                        column: x => x.PreferredMassUnitId,
                        principalSchema: "Warehouse",
                        principalTable: "SiUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceLine_SiUnit_PreferredVolumeUnitId",
                        column: x => x.PreferredVolumeUnitId,
                        principalSchema: "Warehouse",
                        principalTable: "SiUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceLine_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RemittanceTypeFieldConfiguration",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RemittanceTypeConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Exists = table.Column<bool>(type: "bit", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Placement = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemittanceTypeFieldConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceTypeFieldConfiguration_ReceiptFieldDefinition_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptFieldDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceTypeFieldConfiguration_RemittanceTypeConfiguration_RemittanceTypeConfigurationId",
                        column: x => x.RemittanceTypeConfigurationId,
                        principalSchema: "Warehouse",
                        principalTable: "RemittanceTypeConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemittanceFieldValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RemittanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemittanceLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StringValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntValue = table.Column<int>(type: "int", nullable: true),
                    DecimalValue = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: true),
                    DateValue = table.Column<DateOnly>(type: "date", nullable: true),
                    DateTimeValue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BooleanValue = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_RemittanceFieldValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceFieldValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceFieldValue_ReceiptFieldDefinition_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptFieldDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RemittanceFieldValue_RemittanceLine_RemittanceLineId",
                        column: x => x.RemittanceLineId,
                        principalSchema: "Warehouse",
                        principalTable: "RemittanceLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemittanceFieldValue_Remittance_RemittanceId",
                        column: x => x.RemittanceId,
                        principalSchema: "Warehouse",
                        principalTable: "Remittance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RemittanceLineAttributeValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RemittanceLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemAttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StringValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecimalValue = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: true),
                    DateValue = table.Column<DateOnly>(type: "date", nullable: true),
                    DateTimeValue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BooleanValue = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_RemittanceLineAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceLineAttributeValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceLineAttributeValue_ItemAttribute_ItemAttributeId",
                        column: x => x.ItemAttributeId,
                        principalSchema: "Warehouse",
                        principalTable: "ItemAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RemittanceLineAttributeValue_RemittanceLine_RemittanceLineId",
                        column: x => x.RemittanceLineId,
                        principalSchema: "Warehouse",
                        principalTable: "RemittanceLine",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RemittanceLineMeasurementValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RemittanceLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(28,14)", precision: 28, scale: 14, nullable: false),
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
                    table.PrimaryKey("PK_RemittanceLineMeasurementValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceLineMeasurementValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RemittanceLineMeasurementValue_ItemUnitOfMeasurement_ItemUnitOfMeasurementId",
                        column: x => x.ItemUnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "ItemUnitOfMeasurement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RemittanceLineMeasurementValue_RemittanceLine_RemittanceLineId",
                        column: x => x.RemittanceLineId,
                        principalSchema: "Warehouse",
                        principalTable: "RemittanceLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_CompanyId_Number",
                schema: "Warehouse",
                table: "Remittance",
                columns: new[] { "CompanyId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_CompanyId_RemittanceTypeId_RemittanceDate",
                schema: "Warehouse",
                table: "Remittance",
                columns: new[] { "CompanyId", "RemittanceTypeId", "RemittanceDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_IsDeleted",
                schema: "Warehouse",
                table: "Remittance",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Remittance_RemittanceTypeId",
                schema: "Warehouse",
                table: "Remittance",
                column: "RemittanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceFieldValue_CompanyId",
                schema: "Warehouse",
                table: "RemittanceFieldValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceFieldValue_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceFieldValue",
                column: "FieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceFieldValue_IsDeleted",
                schema: "Warehouse",
                table: "RemittanceFieldValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceFieldValue_RemittanceId_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceFieldValue",
                columns: new[] { "RemittanceId", "FieldDefinitionId" },
                unique: true,
                filter: "[RemittanceLineId] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceFieldValue_RemittanceLineId_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceFieldValue",
                columns: new[] { "RemittanceLineId", "FieldDefinitionId" },
                unique: true,
                filter: "[RemittanceLineId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLine_CompanyId_ItemId_WarehouseLocationId",
                schema: "Warehouse",
                table: "RemittanceLine",
                columns: new[] { "CompanyId", "ItemId", "WarehouseLocationId" });

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLine_IsDeleted",
                schema: "Warehouse",
                table: "RemittanceLine",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLine_ItemId",
                schema: "Warehouse",
                table: "RemittanceLine",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLine_PreferredMassUnitId",
                schema: "Warehouse",
                table: "RemittanceLine",
                column: "PreferredMassUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLine_PreferredVolumeUnitId",
                schema: "Warehouse",
                table: "RemittanceLine",
                column: "PreferredVolumeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLine_RemittanceId_RowNumber",
                schema: "Warehouse",
                table: "RemittanceLine",
                columns: new[] { "RemittanceId", "RowNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLine_WarehouseLocationId",
                schema: "Warehouse",
                table: "RemittanceLine",
                column: "WarehouseLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLineAttributeValue_CompanyId",
                schema: "Warehouse",
                table: "RemittanceLineAttributeValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLineAttributeValue_IsDeleted",
                schema: "Warehouse",
                table: "RemittanceLineAttributeValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLineAttributeValue_ItemAttributeId",
                schema: "Warehouse",
                table: "RemittanceLineAttributeValue",
                column: "ItemAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLineAttributeValue_RemittanceLineId_ItemAttributeId",
                schema: "Warehouse",
                table: "RemittanceLineAttributeValue",
                columns: new[] { "RemittanceLineId", "ItemAttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLineMeasurementValue_CompanyId",
                schema: "Warehouse",
                table: "RemittanceLineMeasurementValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLineMeasurementValue_IsDeleted",
                schema: "Warehouse",
                table: "RemittanceLineMeasurementValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLineMeasurementValue_ItemUnitOfMeasurementId",
                schema: "Warehouse",
                table: "RemittanceLineMeasurementValue",
                column: "ItemUnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceLineMeasurementValue_RemittanceLineId_ItemUnitOfMeasurementId",
                schema: "Warehouse",
                table: "RemittanceLineMeasurementValue",
                columns: new[] { "RemittanceLineId", "ItemUnitOfMeasurementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceTypeConfiguration_IsDeleted",
                schema: "Warehouse",
                table: "RemittanceTypeConfiguration",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceTypeConfiguration_RemittanceTypeId",
                schema: "Warehouse",
                table: "RemittanceTypeConfiguration",
                column: "RemittanceTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_RemittanceTypeConfiguration_Company_RemittanceType",
                schema: "Warehouse",
                table: "RemittanceTypeConfiguration",
                columns: new[] { "CompanyId", "RemittanceTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceTypeFieldConfiguration_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceTypeFieldConfiguration",
                column: "FieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceTypeFieldConfiguration_IsDeleted",
                schema: "Warehouse",
                table: "RemittanceTypeFieldConfiguration",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceTypeFieldConfiguration_RemittanceTypeConfigurationId_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceTypeFieldConfiguration",
                columns: new[] { "RemittanceTypeConfigurationId", "FieldDefinitionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemittanceFieldValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "RemittanceLineAttributeValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "RemittanceLineMeasurementValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "RemittanceTypeFieldConfiguration",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "RemittanceLine",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "RemittanceTypeConfiguration",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Remittance",
                schema: "Warehouse");
        }
    }
}
