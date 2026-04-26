using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Warehouse");

            migrationBuilder.EnsureSchema(
                name: "General");

            migrationBuilder.EnsureSchema(
                name: "Shared");

            migrationBuilder.EnsureSchema(
                name: "HCM");

//            migrationBuilder.CreateTable(
//                name: "Companies",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Companies", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "company_units",
//                schema: "Shared",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_company_units", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "currencies",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Name2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Code = table.Column<int>(type: "int", nullable: false),
//                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Iso = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    DecimalPlaces = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    RoundMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    FractionalCurrencyUnit = table.Column<string>(type: "nvarchar(max)", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_currencies", x => x.Id);
//                });

            migrationBuilder.CreateTable(
                name: "ItemType",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType", x => x.Id);
                });

//            migrationBuilder.CreateTable(
//                name: "marital_statuses",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Type = table.Column<int>(type: "int", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_marital_statuses", x => x.Id);
//                });

            migrationBuilder.CreateTable(
                name: "MeasurementDimension",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
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

//            migrationBuilder.CreateTable(
//                name: "military_service_statuses",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_military_service_statuses", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "religions",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_religions", x => x.Id);
//                });

            migrationBuilder.CreateTable(
                name: "ShippingCompany",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ManagerFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManagerLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseType",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    AttributeEntity = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsStockDimension = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
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
                    HasNextLevel = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    table.CheckConstraint("CK_Category_LevelNo", "LevelNo BETWEEN 1 AND 6");
                    table.CheckConstraint("CK_Category_LevelNo_HasNextLevel", "(LevelNo = 1 AND HasNextLevel = 1) OR (LevelNo = 6 AND HasNextLevel = 0) OR (LevelNo > 1 AND LevelNo < 6 AND HasNextLevel IN (0, 1))");
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
                name: "CategoryLevelConstraint",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LevelNo = table.Column<int>(type: "int", nullable: false),
                    CodeLength = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CategoryLevelConstraint", x => x.Id);
                    table.CheckConstraint("CK_CategoryLevelConstraint_CodeLength", "CodeLength BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "FK_CategoryLevelConstraint_Companies_CompanyId",
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
                    table.CheckConstraint("CK_Department_StatusChangeDate", "([Status] = 0 AND [StatusChangeDate] IS NOT NULL) OR ([Status] = 1 AND [StatusChangeDate] IS NULL)");
                    table.ForeignKey(
                        name: "FK_Department_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmploymentGroup",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_EmploymentGroup", x => x.Id);
                    table.CheckConstraint("CK_Name_Min_Length", "LEN(Name) >= 2");
                    table.ForeignKey(
                        name: "FK_EmploymentGroup_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovementType",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_InventoryMovementType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryMovementType_Companies_CompanyId",
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
                    table.CheckConstraint("CK_Position_StatusChangeDate", "([Status] = 0 AND [StatusChangeDate] IS NOT NULL) OR ([Status] = 1 AND [StatusChangeDate] IS NULL)");
                    table.ForeignKey(
                        name: "FK_Position_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

//            migrationBuilder.CreateTable(
//                name: "countries",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Code = table.Column<int>(type: "int", nullable: false),
//                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
//                    TaxCode = table.Column<int>(type: "int", nullable: true),
//                    Iso = table.Column<string>(type: "nvarchar(max)", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_countries", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_countries_currencies_CurrencyId",
//                        column: x => x.CurrencyId,
//                        principalSchema: "General",
//                        principalTable: "currencies",
//                        principalColumn: "Id");
//                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurement",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MeasurementDimensionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasurement_MeasurementDimension_MeasurementDimensionId",
                        column: x => x.MeasurementDimensionId,
                        principalSchema: "Warehouse",
                        principalTable: "MeasurementDimension",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    MaxMonetaryValue = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: false),
                    WarehouseTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseSlaveAccountCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarehouseAccountMasterValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WarehouseAccountSlaveValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WarehouseAccountDetailed1Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WarehouseAccountDetailed2Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SaleSlaveAccountCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleAccountMasterValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SaleAccountSlaveValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SaleAccountDetailed1Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SaleAccountDetailed2Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExportSaleSlaveAccountCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExportSaleAccountMasterValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExportSaleAccountSlaveValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExportSaleAccountDetailed1Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExportSaleAccountDetailed2Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReturnFromSaleSlaveAccountCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReturnFromSaleAccountMasterValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReturnFromSaleAccountSlaveValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReturnFromSaleAccountDetailed1Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReturnFromSaleAccountDetailed2Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReturnFromPurchaseSlaveAccountCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReturnFromPurchaseAccountMasterValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReturnFromPurchaseAccountSlaveValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReturnFromPurchaseAccountDetailed1Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReturnFromPurchaseAccountDetailed2Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Warehouse_WarehouseType_WarehouseTypeId",
                        column: x => x.WarehouseTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Warehouse_company_units_CompanyUnitId",
                        column: x => x.CompanyUnitId,
                        principalSchema: "Shared",
                        principalTable: "company_units",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributeEnumValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
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
                    table.PrimaryKey("PK_AttributeEnumValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeEnumValue_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Warehouse",
                        principalTable: "Attribute",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryAttributeRule",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsItemAttribute = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsStockDimension = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoryAttributeRule_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Warehouse",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentGroupSpecification",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EmploymentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MonthType = table.Column<int>(type: "int", nullable: false),
                    WorkMinutes = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    ValidTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentGroupSpecification", x => x.Id);
                    table.CheckConstraint("CK_EmploymentGroupSpecification_MonthType", "[MonthType] Between 1 and 2");
                    table.CheckConstraint("CK_EmploymentGroupSpecification_ValidRange", "[ValidTo] IS NULL OR [ValidTo] >= [ValidFrom]");
                    table.CheckConstraint("CK_EmploymentGroupSpecification_WorkMinutes", "[WorkMinutes] >=0 AND [WorkMinutes] <= 720");
                    table.ForeignKey(
                        name: "FK_EmploymentGroupSpecification_EmploymentGroup_EmploymentGroupId",
                        column: x => x.EmploymentGroupId,
                        principalSchema: "HCM",
                        principalTable: "EmploymentGroup",
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
                    table.CheckConstraint("CK_OrganizationNode_OnlyOneReference", "(\r\n                ([DepartmentId] IS NOT NULL AND [PositionId] IS NULL AND [NodeType] = 1)\r\n                OR\r\n                ([DepartmentId] IS NULL AND [PositionId] IS NOT NULL AND [NodeType] = 2)\r\n            )");
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

//            migrationBuilder.CreateTable(
//                name: "provinces",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Code = table.Column<int>(type: "int", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_provinces", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_provinces_countries_CountryId",
//                        column: x => x.CountryId,
//                        principalSchema: "General",
//                        principalTable: "countries",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TitleInEnglish = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TechnicalNumber = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PrimaryUnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Item_ItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "ItemType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_UnitOfMeasurement_PrimaryUnitOfMeasurementId",
                        column: x => x.PrimaryUnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "UnitOfMeasurement",
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
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "WarehouseLocation",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParentLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanStoreItem = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LevelNo = table.Column<int>(type: "int", nullable: false),
                    HasNextLevel = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseLocation", x => x.Id);
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
                name: "OrganizationalStructureItem",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    OrganizationalStructureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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

//            migrationBuilder.CreateTable(
//                name: "cities",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Code = table.Column<int>(type: "int", nullable: false),
//                    Code2 = table.Column<int>(type: "int", nullable: true),
//                    Code3 = table.Column<int>(type: "int", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_cities", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_cities_provinces_ProvinceId",
//                        column: x => x.ProvinceId,
//                        principalSchema: "General",
//                        principalTable: "provinces",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

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
                name: "ItemAttribute",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ItemAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAttribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalSchema: "Warehouse",
                        principalTable: "Attribute",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemAttribute_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemUnitOfMeasurement",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnitOfMeasurement", x => x.Id);
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
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversionEquation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnitOfMeasurementConversion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurementConversion_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasurementConversion_UnitOfMeasurement_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemWarehouse",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReorderPoint = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: false),
                    CriticalPoint = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: false),
                    ReorderQuantity = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: false),
                    MaxStockLevel = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemWarehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemWarehouse_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemWarehouse_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "Warehouse",
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

//            migrationBuilder.CreateTable(
//                name: "persons",
//                schema: "General",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    NaturalFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Code = table.Column<long>(type: "bigint", nullable: false),
//                    EconomicCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    EconomicCodeOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    IsInternalCitizenship = table.Column<bool>(type: "bit", nullable: false),
//                    CitizenCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    NaturalFatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    NaturalNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    BirthCityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
//                    NaturalBirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
//                    NaturalSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    LegalManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    LegalManagerFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    LegalNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    LegalRegisterNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    LegalEstablishmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
//                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    IsGovernmental = table.Column<bool>(type: "bit", nullable: false),
//                    ReligionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
//                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_persons", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_persons_cities_BirthCityId",
//                        column: x => x.BirthCityId,
//                        principalSchema: "General",
//                        principalTable: "cities",
//                        principalColumn: "Id");
//                    table.ForeignKey(
//                        name: "FK_persons_religions_ReligionId",
//                        column: x => x.ReligionId,
//                        principalSchema: "General",
//                        principalTable: "religions",
//                        principalColumn: "Id");
//                });

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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovement",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MovementDate = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: false),
                    ReferenceDocId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityBase = table.Column<decimal>(type: "decimal(23,8)", precision: 23, scale: 8, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ItemWarehouseLocation",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ItemWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemWarehouseLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemWarehouseLocation_ItemWarehouse_ItemWarehouseId",
                        column: x => x.ItemWarehouseId,
                        principalSchema: "Warehouse",
                        principalTable: "ItemWarehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemWarehouseLocation_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Warehouse",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MilitaryServiceStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaritalStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarriageDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_marital_statuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalSchema: "General",
                        principalTable: "marital_statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_military_service_statuses_MilitaryServiceStatusId",
                        column: x => x.MilitaryServiceStatusId,
                        principalSchema: "General",
                        principalTable: "military_service_statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "General",
                        principalTable: "persons",
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
                name: "UX_Category_Company_Code",
                schema: "Warehouse",
                table: "Category",
                columns: new[] { "CompanyId", "Code" },
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
                name: "IX_CategoryLevelConstraint_IsDeleted",
                schema: "Warehouse",
                table: "CategoryLevelConstraint",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_CategoryLevelConst_Company_No",
                schema: "Warehouse",
                table: "CategoryLevelConstraint",
                columns: new[] { "CompanyId", "LevelNo" },
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_cities_ProvinceId",
            //    schema: "General",
            //    table: "cities",
            //    column: "ProvinceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_countries_CurrencyId",
            //    schema: "General",
            //    table: "countries",
            //    column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_IsDeleted",
                schema: "HCM",
                table: "Department",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_Department_CompanyId_Code",
                schema: "HCM",
                table: "Department",
                columns: new[] { "CompanyId", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IsDeleted",
                schema: "HCM",
                table: "Employee",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_MaritalStatus",
                schema: "HCM",
                table: "Employee",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_MilitaryServiceStatus",
                schema: "HCM",
                table: "Employee",
                column: "MilitaryServiceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Person",
                schema: "HCM",
                table: "Employee",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "UX_Employee_Company_Code",
                schema: "HCM",
                table: "Employee",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentGroup_IsDeleted",
                schema: "HCM",
                table: "EmploymentGroup",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_EmploymentGroup_CompanyId_Name",
                schema: "HCM",
                table: "EmploymentGroup",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpGroupSpec_Validity",
                schema: "HCM",
                table: "EmploymentGroupSpecification",
                columns: new[] { "EmploymentGroupId", "ValidFrom", "ValidTo" });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentGroupSpecification_IsDeleted",
                schema: "HCM",
                table: "EmploymentGroupSpecification",
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
                name: "IX_InventoryMovementType_CompanyId",
                schema: "Warehouse",
                table: "InventoryMovementType",
                column: "CompanyId");

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
                name: "IX_Item_ItemTypeId",
                schema: "Warehouse",
                table: "Item",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item",
                column: "PrimaryUnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "UX_Item_Company_Code",
                schema: "Warehouse",
                table: "Item",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Item_Company_Sku",
                schema: "Warehouse",
                table: "Item",
                columns: new[] { "CompanyId", "Sku" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttribute_AttributeId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttribute_IsDeleted",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttribute_ItemId",
                schema: "Warehouse",
                table: "ItemAttribute",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemType_IsDeleted",
                schema: "Warehouse",
                table: "ItemType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_ItemType_Code",
                schema: "Warehouse",
                table: "ItemType",
                column: "Code",
                unique: true);

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
                name: "IX_ItemUnitOfMeasurementConversion_IsDeleted",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_ItemId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurementConversion_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurementConversion",
                column: "UnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouse_IsDeleted",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouse_ItemId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouse_WarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouse",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_IsDeleted",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_ItemWarehouseId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "ItemWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouseLocation_WarehouseLocationId",
                schema: "Warehouse",
                table: "ItemWarehouseLocation",
                column: "WarehouseLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementDimension_IsDeleted",
                schema: "Warehouse",
                table: "MeasurementDimension",
                column: "IsDeleted");

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
                name: "IX_OrganizationalStructureItem_ParentItemId",
                schema: "HCM",
                table: "OrganizationalStructureItem",
                column: "ParentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_CompanyId_DepartmentId",
                schema: "HCM",
                table: "OrganizationNode",
                columns: new[] { "CompanyId", "DepartmentId" },
                unique: true,
                filter: "[DepartmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNode_CompanyId_PositionId",
                schema: "HCM",
                table: "OrganizationNode",
                columns: new[] { "CompanyId", "PositionId" },
                unique: true,
                filter: "[PositionId] IS NOT NULL");

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

            //migrationBuilder.CreateIndex(
            //    name: "IX_persons_BirthCityId",
            //    schema: "General",
            //    table: "persons",
            //    column: "BirthCityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_persons_ReligionId",
            //    schema: "General",
            //    table: "persons",
            //    column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_IsDeleted",
                schema: "HCM",
                table: "Position",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_Position_CompanyId_Code",
                schema: "HCM",
                table: "Position",
                columns: new[] { "CompanyId", "Code" },
                unique: true,
                filter: "[Code] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_provinces_CountryId",
            //    schema: "General",
            //    table: "provinces",
            //    column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCompany_IsDeleted",
                schema: "Warehouse",
                table: "ShippingCompany",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_ShippingCompany_Code",
                schema: "Warehouse",
                table: "ShippingCompany",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasurement_IsDeleted",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_UnitOfMeasurement_Code",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Uom_Dimension_Title",
                schema: "Warehouse",
                table: "UnitOfMeasurement",
                columns: new[] { "MeasurementDimensionId", "Title" },
                unique: true);

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
                name: "IX_Warehouse_CompanyUnitId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "CompanyUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_IsDeleted",
                schema: "Warehouse",
                table: "Warehouse",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_WarehouseTypeId",
                schema: "Warehouse",
                table: "Warehouse",
                column: "WarehouseTypeId");

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
                name: "IX_WarehouseLocation_IsDeleted",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_ParentLocationId",
                schema: "Warehouse",
                table: "WarehouseLocation",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "UX_WarehouseLocation_Warehouse_Code",
                schema: "Warehouse",
                table: "WarehouseLocation",
                columns: new[] { "WarehouseId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseType_IsDeleted",
                schema: "Warehouse",
                table: "WarehouseType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_WarehouseType_Code",
                schema: "Warehouse",
                table: "WarehouseType",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryAttributeRule",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "CategoryLevelConstraint",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "EmploymentGroupSpecification",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "InventoryLotAttributeValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryMovement",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemAttribute",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemUnitOfMeasurement",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemUnitOfMeasurementConversion",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemWarehouseLocation",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "OrganizationalStructureItem",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "ShippingCompany",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurementConversion",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "marital_statuses",
                schema: "General");

            migrationBuilder.DropTable(
                name: "military_service_statuses",
                schema: "General");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "General");

            migrationBuilder.DropTable(
                name: "EmploymentGroup",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "AttributeEnumValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryLot",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "InventoryMovementType",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemWarehouse",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "WarehouseLocation",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "OrganizationNode",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "OrganizationalStructure",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "cities",
                schema: "General");

            migrationBuilder.DropTable(
                name: "religions",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Attribute",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Item",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Warehouse",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "provinces",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ItemType",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurement",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "WarehouseType",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "company_units",
                schema: "Shared");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "General");

            migrationBuilder.DropTable(
                name: "MeasurementDimension",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "currencies",
                schema: "General");
        }
    }
}
