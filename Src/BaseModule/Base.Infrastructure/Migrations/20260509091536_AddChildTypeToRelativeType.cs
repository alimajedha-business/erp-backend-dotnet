using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChildTypeToRelativeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_RelativeType_Type",
                schema: "HCM",
                table: "RelativeType");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RelativeType_Type",
                schema: "HCM",
                table: "RelativeType",
                sql: "[Type] BETWEEN 1 AND 7");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_RelativeType_Type",
                schema: "HCM",
                table: "RelativeType");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RelativeType_Type",
                schema: "HCM",
                table: "RelativeType",
                sql: "[Type] BETWEEN 1 AND 6");
        }
    }
}
