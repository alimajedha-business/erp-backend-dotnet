using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRemittanceFieldDefinitionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RemittanceFieldValue_ReceiptFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceFieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_RemittanceTypeFieldConfiguration_ReceiptFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceTypeFieldConfiguration");

            migrationBuilder.CreateTable(
                name: "RemittanceFieldDefinition",
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
                    table.PrimaryKey("PK_RemittanceFieldDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemittanceFieldDefinition_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceFieldDefinition_CompanyId_Key",
                schema: "Warehouse",
                table: "RemittanceFieldDefinition",
                columns: new[] { "CompanyId", "Key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RemittanceFieldDefinition_IsDeleted",
                schema: "Warehouse",
                table: "RemittanceFieldDefinition",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_RemittanceFieldValue_RemittanceFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceFieldValue",
                column: "FieldDefinitionId",
                principalSchema: "Warehouse",
                principalTable: "RemittanceFieldDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RemittanceTypeFieldConfiguration_RemittanceFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceTypeFieldConfiguration",
                column: "FieldDefinitionId",
                principalSchema: "Warehouse",
                principalTable: "RemittanceFieldDefinition",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RemittanceFieldValue_RemittanceFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceFieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_RemittanceTypeFieldConfiguration_RemittanceFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceTypeFieldConfiguration");

            migrationBuilder.DropTable(
                name: "RemittanceFieldDefinition",
                schema: "Warehouse");

            migrationBuilder.AddForeignKey(
                name: "FK_RemittanceFieldValue_ReceiptFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceFieldValue",
                column: "FieldDefinitionId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptFieldDefinition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RemittanceTypeFieldConfiguration_ReceiptFieldDefinition_FieldDefinitionId",
                schema: "Warehouse",
                table: "RemittanceTypeFieldConfiguration",
                column: "FieldDefinitionId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptFieldDefinition",
                principalColumn: "Id");
        }
    }
}
