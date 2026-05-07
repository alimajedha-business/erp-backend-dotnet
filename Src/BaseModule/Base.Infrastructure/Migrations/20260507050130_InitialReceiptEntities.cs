using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialReceiptEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobCategory",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    ReceiptDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReceiptTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipt_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receipt_ReceiptType_ReceiptTypeId",
                        column: x => x.ReceiptTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptFieldDefinition",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    AllowedPlacement = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
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
                    table.PrimaryKey("PK_ReceiptFieldDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptFieldDefinition_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Job",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NextJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    JobCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LevelCode = table.Column<int>(type: "int", nullable: false),
                    Seniority = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_Job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Job_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_JobCategory_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalSchema: "HCM",
                        principalTable: "JobCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_Job_NextJobId",
                        column: x => x.NextJobId,
                        principalSchema: "HCM",
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Job_Job_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "HCM",
                        principalTable: "Job",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptLine",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(22,4)", precision: 22, scale: 4, nullable: true),
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
                    table.PrimaryKey("PK_ReceiptLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptLine_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptLine_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Warehouse",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptLine_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalSchema: "Warehouse",
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptLine_UnitOfMeasurement_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalSchema: "Warehouse",
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTypeFieldConfiguration",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ReceiptTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FieldDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Exists = table.Column<bool>(type: "bit", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    Placement = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_ReceiptTypeFieldConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptTypeFieldConfiguration_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptTypeFieldConfiguration_ReceiptFieldDefinition_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptFieldDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTypeFieldConfiguration_ReceiptType_ReceiptTypeId",
                        column: x => x.ReceiptTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PositionJob",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionJob_Job_JobId",
                        column: x => x.JobId,
                        principalSchema: "HCM",
                        principalTable: "Job",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PositionJob_Position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "HCM",
                        principalTable: "Position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptFieldValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_ReceiptFieldValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptFieldValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptFieldValue_ReceiptFieldDefinition_FieldDefinitionId",
                        column: x => x.FieldDefinitionId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptFieldDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptFieldValue_ReceiptLine_ReceiptLineId",
                        column: x => x.ReceiptLineId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptLine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptFieldValue_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalSchema: "Warehouse",
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptLineAttributeValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ReceiptLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ReceiptLineAttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptLineAttributeValue_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptLineAttributeValue_ItemAttribute_ItemAttributeId",
                        column: x => x.ItemAttributeId,
                        principalSchema: "Warehouse",
                        principalTable: "ItemAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptLineAttributeValue_ReceiptLine_ReceiptLineId",
                        column: x => x.ReceiptLineId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_IsDeleted",
                schema: "HCM",
                table: "Job",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobCategory",
                schema: "HCM",
                table: "Job",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_NextJob",
                schema: "HCM",
                table: "Job",
                column: "NextJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Parent",
                schema: "HCM",
                table: "Job",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "UX_Job_CompanyId_Code",
                schema: "HCM",
                table: "Job",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobCategory_IsDeleted",
                schema: "HCM",
                table: "JobCategory",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_JobCategory_Code",
                schema: "HCM",
                table: "JobCategory",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PositionJob_IsDeleted",
                schema: "HCM",
                table: "PositionJob",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PositionJob_JobId",
                schema: "HCM",
                table: "PositionJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionJob_PositionId",
                schema: "HCM",
                table: "PositionJob",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_CompanyId_Number",
                schema: "Warehouse",
                table: "Receipt",
                columns: new[] { "CompanyId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_CompanyId_ReceiptTypeId_ReceiptDate",
                schema: "Warehouse",
                table: "Receipt",
                columns: new[] { "CompanyId", "ReceiptTypeId", "ReceiptDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_IsDeleted",
                schema: "Warehouse",
                table: "Receipt",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_ReceiptTypeId",
                schema: "Warehouse",
                table: "Receipt",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFieldDefinition_CompanyId_Key",
                schema: "Warehouse",
                table: "ReceiptFieldDefinition",
                columns: new[] { "CompanyId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFieldDefinition_IsDeleted",
                schema: "Warehouse",
                table: "ReceiptFieldDefinition",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFieldValue_CompanyId",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFieldValue_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                column: "FieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFieldValue_IsDeleted",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFieldValue_ReceiptId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                columns: new[] { "ReceiptId", "FieldDefinitionId" },
                unique: true,
                filter: "[ReceiptLineId] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFieldValue_ReceiptLineId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptFieldValue",
                columns: new[] { "ReceiptLineId", "FieldDefinitionId" },
                unique: true,
                filter: "[ReceiptLineId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLine_CompanyId_ItemId",
                schema: "Warehouse",
                table: "ReceiptLine",
                columns: new[] { "CompanyId", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLine_IsDeleted",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLine_ItemId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLine_ReceiptId_RowNumber",
                schema: "Warehouse",
                table: "ReceiptLine",
                columns: new[] { "ReceiptId", "RowNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLine_UnitOfMeasurementId",
                schema: "Warehouse",
                table: "ReceiptLine",
                column: "UnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLineAttributeValue_CompanyId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLineAttributeValue_IsDeleted",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLineAttributeValue_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                column: "ItemAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptLineAttributeValue_ReceiptLineId_ItemAttributeId",
                schema: "Warehouse",
                table: "ReceiptLineAttributeValue",
                columns: new[] { "ReceiptLineId", "ItemAttributeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_CompanyId_ReceiptTypeId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                columns: new[] { "CompanyId", "ReceiptTypeId", "FieldDefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "FieldDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_IsDeleted",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "ReceiptTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionJob",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "ReceiptFieldValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ReceiptLineAttributeValue",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ReceiptTypeFieldConfiguration",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "Job",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "ReceiptLine",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "ReceiptFieldDefinition",
                schema: "Warehouse");

            migrationBuilder.DropTable(
                name: "JobCategory",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "Receipt",
                schema: "Warehouse");
        }
    }
}
