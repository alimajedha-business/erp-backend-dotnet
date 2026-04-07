using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ItemEntitiesMdf1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemUnitOfMeasurement_Companies_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropTable(
                name: "ItemAttributeValue",
                schema: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_ItemUnitOfMeasurement_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "IsBase",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "IsDefaultIssue",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "IsDefaultPurchase",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.AddColumn<int>(
                name: "UnitOrder",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                schema: "Warehouse",
                table: "Item",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemTypeId",
                schema: "Warehouse",
                table: "Item",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TechnicalNumber",
                schema: "Warehouse",
                table: "Item",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleInEnglish",
                schema: "Warehouse",
                table: "Item",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemType_ItemTypeId",
                schema: "Warehouse",
                table: "Item",
                column: "ItemTypeId",
                principalSchema: "Warehouse",
                principalTable: "ItemType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_UnitOfMeasurement_PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item",
                column: "PrimaryUnitOfMeasurementId",
                principalSchema: "Warehouse",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemType_ItemTypeId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_UnitOfMeasurement_PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropTable(
                name: "ItemAttribute",
                schema: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemTypeId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "UX_Item_Company_Code",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "UnitOrder",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement");

            migrationBuilder.DropColumn(
                name: "Barcode",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ItemTypeId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PrimaryUnitOfMeasurementId",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "TechnicalNumber",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "TitleInEnglish",
                schema: "Warehouse",
                table: "Item");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsBase",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultIssue",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultPurchase",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ItemAttributeValue",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnumValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ValueBoolean = table.Column<bool>(type: "bit", nullable: true),
                    ValueDate = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    ValueDecimal = table.Column<decimal>(type: "decimal(23,8)", nullable: true),
                    ValueInt = table.Column<int>(type: "int", nullable: true),
                    ValueText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasurement_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "CompanyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ItemUnitOfMeasurement_Companies_CompanyId",
                schema: "Warehouse",
                table: "ItemUnitOfMeasurement",
                column: "CompanyId",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
