using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEmployeeRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_EmploymentGroup_CompanyId'
                      AND object_id = OBJECT_ID(N'[HCM].[EmploymentGroup]')
                )
                DROP INDEX [IX_EmploymentGroup_CompanyId] ON [HCM].[EmploymentGroup];
                """);

            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    schema: "General",
            //    table: "military_service_statuses",
            //    newName: "Title");

            //migrationBuilder.AddColumn<int>(
            //    name: "Type",
            //    schema: "General",
            //    table: "military_service_statuses",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.Sql("""
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'UX_EmploymentGroup_CompanyId_Name'
                      AND object_id = OBJECT_ID(N'[HCM].[EmploymentGroup]')
                )
                CREATE UNIQUE INDEX [UX_EmploymentGroup_CompanyId_Name]
                ON [HCM].[EmploymentGroup] ([CompanyId], [Name]);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'UX_EmploymentGroup_CompanyId_Name'
                      AND object_id = OBJECT_ID(N'[HCM].[EmploymentGroup]')
                )
                DROP INDEX [UX_EmploymentGroup_CompanyId_Name] ON [HCM].[EmploymentGroup];
                """);

            //migrationBuilder.DropColumn(
            //    name: "Type",
            //    schema: "General",
            //    table: "military_service_statuses");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    schema: "General",
            //    table: "military_service_statuses",
            //    newName: "Name");

            migrationBuilder.Sql("""
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = N'IX_EmploymentGroup_CompanyId'
                      AND object_id = OBJECT_ID(N'[HCM].[EmploymentGroup]')
                )
                CREATE INDEX [IX_EmploymentGroup_CompanyId]
                ON [HCM].[EmploymentGroup] ([CompanyId]);
                """);
        }
    }
}
