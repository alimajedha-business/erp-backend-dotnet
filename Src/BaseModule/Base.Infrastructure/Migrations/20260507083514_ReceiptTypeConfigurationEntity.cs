using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReceiptTypeConfigurationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTypeFieldConfiguration_ReceiptType_ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTypeFieldConfiguration_CompanyId_ReceiptTypeId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.CreateTable(
                name: "ReceiptTypeConfiguration",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ReceiptTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ReceiptTypeConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptTypeConfiguration_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptTypeConfiguration_ReceiptType_ReceiptTypeId",
                        column: x => x.ReceiptTypeId,
                        principalSchema: "Warehouse",
                        principalTable: "ReceiptType",
                        principalColumn: "Id");
                });

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.Sql("""
                INSERT INTO [Warehouse].[ReceiptTypeConfiguration] ([CompanyId], [ReceiptTypeId], [IsDeleted])
                SELECT DISTINCT [CompanyId], [ReceiptTypeId], CAST(0 AS bit)
                FROM [Warehouse].[ReceiptTypeFieldConfiguration] AS [fieldConfiguration]
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM [Warehouse].[ReceiptTypeConfiguration] AS [configuration]
                    WHERE [configuration].[CompanyId] = [fieldConfiguration].[CompanyId]
                        AND [configuration].[ReceiptTypeId] = [fieldConfiguration].[ReceiptTypeId]
                );
                """);

            migrationBuilder.Sql("""
                UPDATE [fieldConfiguration]
                SET [ReceiptTypeConfigurationId] = [configuration].[Id]
                FROM [Warehouse].[ReceiptTypeFieldConfiguration] AS [fieldConfiguration]
                INNER JOIN [Warehouse].[ReceiptTypeConfiguration] AS [configuration]
                    ON [configuration].[CompanyId] = [fieldConfiguration].[CompanyId]
                    AND [configuration].[ReceiptTypeId] = [fieldConfiguration].[ReceiptTypeId];
                """);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "ReceiptTypeConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_CompanyId_ReceiptTypeConfigurationId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                columns: new[] { "CompanyId", "ReceiptTypeConfigurationId", "FieldDefinitionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeConfiguration_IsDeleted",
                schema: "Warehouse",
                table: "ReceiptTypeConfiguration",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeConfiguration_ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeConfiguration",
                column: "ReceiptTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_ReceiptTypeConfiguration_Company_ReceiptType",
                schema: "Warehouse",
                table: "ReceiptTypeConfiguration",
                columns: new[] { "CompanyId", "ReceiptTypeId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTypeFieldConfiguration_ReceiptTypeConfiguration_ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "ReceiptTypeConfigurationId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptTypeConfiguration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTypeFieldConfiguration_ReceiptTypeConfiguration_ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptTypeFieldConfiguration_CompanyId_ReceiptTypeConfigurationId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.AddColumn<Guid>(
                name: "ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE [fieldConfiguration]
                SET [ReceiptTypeId] = [configuration].[ReceiptTypeId]
                FROM [Warehouse].[ReceiptTypeFieldConfiguration] AS [fieldConfiguration]
                INNER JOIN [Warehouse].[ReceiptTypeConfiguration] AS [configuration]
                    ON [configuration].[Id] = [fieldConfiguration].[ReceiptTypeConfigurationId];
                """);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "ReceiptTypeConfigurationId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration");

            migrationBuilder.DropTable(
                name: "ReceiptTypeConfiguration",
                schema: "Warehouse");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTypeFieldConfiguration_CompanyId_ReceiptTypeId_FieldDefinitionId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                columns: new[] { "CompanyId", "ReceiptTypeId", "FieldDefinitionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTypeFieldConfiguration_ReceiptType_ReceiptTypeId",
                schema: "Warehouse",
                table: "ReceiptTypeFieldConfiguration",
                column: "ReceiptTypeId",
                principalSchema: "Warehouse",
                principalTable: "ReceiptType",
                principalColumn: "Id");
        }
    }
}
