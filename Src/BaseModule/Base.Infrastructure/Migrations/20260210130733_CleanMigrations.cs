using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CleanMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Warehouse");

            migrationBuilder.EnsureSchema(
                name: "General");

            migrationBuilder.EnsureSchema(
                name: "HCM");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovementType",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryMovementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    IsItemAttribute = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsStockDimension = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LevelNo = table.Column<int>(type: "int", nullable: false),
                    IsLastLevel = table.Column<bool>(type: "bit", nullable: false),
                    CategoryPath = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.CheckConstraint("CK_Category_LevelNo", "LevelNo BETWEEN 1 AND 7");
                    table.CheckConstraint("CK_Category_LevelNo_LastLevel", "(LevelNo = 1 AND IsLastLevel = 0) OR LevelNo <> 1");
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalSchema: "Warehouse",
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Category_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StatusChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalStructure",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
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
                    table.PrimaryKey("PK_OrganizationalStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationalStructure_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StatusChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurement",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Dimension = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDiscrete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.PrimaryKey("PK_UnitOfMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasurement_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
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
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributeEnumValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_AttributeEnumValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeEnumValue_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Warehouse",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeEnumValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryAttributeRule",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    IsItemAttribute = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsStockDimension = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAttributeRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryAttributeRule_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Warehouse",
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryAttributeRule_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Warehouse",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Warehouse",
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationNode",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NodeType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_OrganizationNode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationNode_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationNode_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "HCM",
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationNode_Position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "HCM",
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurementConversion",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Factor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    FromUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_UnitOfMeasurementConversion", x => x.Id);
                    table.CheckConstraint("CK_UomConv_Factor", "Factor > 0");
                    table.ForeignKey(
                        name: "FK_UnitOfMeasurementConversion_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "WarehouseLocation",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParentLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_WarehouseLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseLocation_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLocation_WarehouseLocation_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseLocation_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "Warehouse",
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryLot",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DimHash = table.Column<byte[]>(type: "varbinary(32)", maxLength: 32, nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "ItemAttributeValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ValueInt = table.Column<int>(type: "int", nullable: true),
                    ValueDecimal = table.Column<decimal>(type: "decimal(23,8)", nullable: true),
                    ValueDate = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    ValueBoolean = table.Column<bool>(type: "bit", nullable: true),
                    EnumValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_ItemAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAttributeValue_AttributeEnumValue_EnumValueId",
                        column: x => x.EnumValueId,
                        principalSchema: "Warehouse",
                        principalTable: "AttributeEnumValue",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemAttributeValue_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Warehouse",
                        principalTable: "Attribute",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemAttributeValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemAttributeValue_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemUnitOfMeasurement",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsBase = table.Column<bool>(type: "bit", nullable: false),
                    IsDefaultPurchase = table.Column<bool>(type: "bit", nullable: false),
                    IsDefaultIssue = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_ItemUnitOfMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurement_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurement_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurement_UnitOfMeasurement_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemUnitOfMeasurementConversion",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Factor = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ItemUnitOfMeasurementConversion", x => x.Id);
                    table.CheckConstraint("CK_ItemUomConv_Factor", "Factor > 0");
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurementConversion_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurementConversion_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_FromUnitOfMeasurementId",
                        column: x => x.FromUnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_ToUnitOfMeasurementId",
                        column: x => x.ToUnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalStructureItem",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    OrganizationalStructureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationalStructureItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_OrganizationalStructureItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationalStructureItem_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalStructureItem_OrganizationNode_NodeId",
                        column: x => x.NodeId,
                        principalSchema: "HCM",
                        principalTable: "OrganizationNode",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalStructureItem_OrganizationalStructureItem_OrganizationalStructureItemId",
                        column: x => x.OrganizationalStructureItemId,
                        principalSchema: "HCM",
                        principalTable: "OrganizationalStructureItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalStructureItem_OrganizationalStructureItem_ParentItemId",
                        column: x => x.ParentItemId,
                        principalSchema: "HCM",
                        principalTable: "OrganizationalStructureItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationalStructureItem_OrganizationalStructure_OrganizationalStructureId",
                        column: x => x.OrganizationalStructureId,
                        principalSchema: "HCM",
                        principalTable: "OrganizationalStructure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryLotAttributeValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueText = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ValueInt = table.Column<int>(type: "int", nullable: true),
                    ValueDecimal = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    ValueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValueBoolean = table.Column<bool>(type: "bit", nullable: true),
                    EnumValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovement",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MovementDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    ReferenceDocId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityBase = table.Column<decimal>(type: "decimal(23,8)", nullable: false),
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
                name: "IX_Attribute_IsDeleted",
                schema: "Warehouse",
                table: "Attribute",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_Attribute_Company_Code",
                schema: "Warehouse",
                table: "Attribute",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeEnumValue_CompanyId",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeEnumValue_IsDeleted",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_AttributeEnum_Attribute_Code",
                schema: "Warehouse",
                table: "AttributeEnumValue",
                columns: new[] { "AttributeId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CompanyId",
                schema: "Warehouse",
                table: "Category",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IsDeleted",
                schema: "Warehouse",
                table: "Category",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                schema: "Warehouse",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "UX_Attribute_Company_Code",
                schema: "Warehouse",
                table: "Category",
                columns: new[] { "Code", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAttributeRule_AttributeId",
                schema: "Warehouse",
                table: "CategoryAttributeRule",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAttributeRule_IsDeleted",
                schema: "Warehouse",
                table: "CategoryAttributeRule",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_CategoryAttribute_Category_Attribute",
                schema: "Warehouse",
                table: "CategoryAttributeRule",
                columns: new[] { "CategoryId", "AttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyId",
                schema: "HCM",
                table: "Department",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_IsDeleted",
                schema: "HCM",
                table: "Department",
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

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovementType_IsDeleted",
                schema: "Warehouse",
                table: "InventoryMovementType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Category",
                schema: "Warehouse",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_IsDeleted",
                schema: "Warehouse",
                table: "Item",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_Item_Company_Sku",
                schema: "Warehouse",
                table: "Item",
                columns: new[] { "CompanyId", "Sku" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeValue_AttributeId",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeValue_CompanyId",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeValue_EnumValueId",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "EnumValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeValue_IsDeleted",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributeValue_ItemId",
                schema: "Warehouse",
                table: "ItemAttributeValue",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_IsDeleted",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "UnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_FromUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "FromUnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_IsDeleted",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_ToUnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "ToUnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "UX_ItemUomConv_Unique",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                columns: new[] { "ItemId", "FromUnitOfMeasurementId", "ToUnitOfMeasurementId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructure_CompanyId",
                schema: "HCM",
                table: "OrganizationalStructure",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructure_IsDeleted",
                schema: "HCM",
                table: "OrganizationalStructure",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructureItem_CompanyId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructureItem_IsDeleted",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructureItem_NodeId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructureItem_OrganizationalStructureId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "OrganizationalStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructureItem_OrganizationalStructureItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "OrganizationalStructureItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalStructureItem_ParentItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "ParentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_CompanyId",
                schema: "HCM",
                table: "OrganizationNode",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_DepartmentId",
                schema: "HCM",
                table: "OrganizationNode",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_IsDeleted",
                schema: "HCM",
                table: "OrganizationNode",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_PositionId",
                schema: "HCM",
                table: "OrganizationNode",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_CompanyId",
                schema: "HCM",
                table: "Position",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_IsDeleted",
                schema: "HCM",
                table: "Position",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurement_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurement_IsDeleted",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_Uom_Dimension_Title",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                columns: new[] { "Dimension", "Title" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurementConversion_CompanyId",
                schema: "Warehouse",
                table: "UnitOfMeasurementConversion",
                column: "CompanyId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_IsDeleted",
                schema: "Warehouse",
                table: "Warehouse",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_Warehouse_Company_Code",
                schema: "Warehouse",
                table: "Warehouse",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Warehouse_Parent",
                schema: "Warehouse",
                table: "WarehouseLocation",
                columns: new[] { "WarehouseId", "ParentLocationId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_CompanyId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_IsDeleted",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_ParentLocationId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "ParentLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryAttributeRule",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryLotAttributeValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryMovement",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemAttributeValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemUnitOfMeasurement",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemUnitOfMeasurementConversion",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "OrganizationalStructureItem",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurementConversion",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryLot",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryMovementType",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "WarehouseLocation",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "AttributeEnumValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "OrganizationNode",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "OrganizationalStructure",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurement",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Item",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Warehouse",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Attribute",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "General");
        }
    }
}