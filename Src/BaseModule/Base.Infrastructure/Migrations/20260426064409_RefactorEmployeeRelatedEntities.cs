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
            migrationBuilder.DropIndex(
                name: "IX_EmploymentGroup_CompanyId",
                schema: "HCM",
                table: "EmploymentGroup");

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

            migrationBuilder.CreateIndex(
                name: "UX_EmploymentGroup_CompanyId_Name",
                schema: "HCM",
                table: "EmploymentGroup",
                columns: new[] { "CompanyId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_EmploymentGroup_CompanyId_Name",
                schema: "HCM",
                table: "EmploymentGroup");

            //migrationBuilder.DropColumn(
            //    name: "Type",
            //    schema: "General",
            //    table: "military_service_statuses");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    schema: "General",
            //    table: "military_service_statuses",
            //    newName: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentGroup_CompanyId",
                schema: "HCM",
                table: "EmploymentGroup",
                column: "CompanyId");
        }
    }
}
