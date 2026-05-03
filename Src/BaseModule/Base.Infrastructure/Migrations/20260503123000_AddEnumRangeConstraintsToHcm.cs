using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

using NGErp.Base.Infrastructure.DataAccess;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20260503123000_AddEnumRangeConstraintsToHcm")]
    public partial class AddEnumRangeConstraintsToHcm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_EducationalStatus_Type",
                schema: "HCM",
                table: "EducationalStatus",
                sql: "[Type] BETWEEN 1 AND 2");

            migrationBuilder.AddCheckConstraint(
                name: "CK_EducationLevel_Type",
                schema: "HCM",
                table: "EducationLevel",
                sql: "[Type] BETWEEN 1 AND 7");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MaritalStatus_Type",
                schema: "HCM",
                table: "MaritalStatus",
                sql: "[Type] BETWEEN 1 AND 3");

            migrationBuilder.AddCheckConstraint(
                name: "CK_MilitaryServiceStatus_Type",
                schema: "HCM",
                table: "MilitaryServiceStatus",
                sql: "[Type] BETWEEN 1 AND 7");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RelativeType_Type",
                schema: "HCM",
                table: "RelativeType",
                sql: "[Type] BETWEEN 1 AND 5");

            migrationBuilder.AddCheckConstraint(
                name: "CK_EmployeeWarriorRecord_VeteranServiceType",
                schema: "HCM",
                table: "EmployeeWarriorRecord",
                sql: "[VeteranServiceType] BETWEEN 1 AND 2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_EducationalStatus_Type",
                schema: "HCM",
                table: "EducationalStatus");

            migrationBuilder.DropCheckConstraint(
                name: "CK_EducationLevel_Type",
                schema: "HCM",
                table: "EducationLevel");

            migrationBuilder.DropCheckConstraint(
                name: "CK_MaritalStatus_Type",
                schema: "HCM",
                table: "MaritalStatus");

            migrationBuilder.DropCheckConstraint(
                name: "CK_MilitaryServiceStatus_Type",
                schema: "HCM",
                table: "MilitaryServiceStatus");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RelativeType_Type",
                schema: "HCM",
                table: "RelativeType");

            migrationBuilder.DropCheckConstraint(
                name: "CK_EmployeeWarriorRecord_VeteranServiceType",
                schema: "HCM",
                table: "EmployeeWarriorRecord");
        }
    }
}
