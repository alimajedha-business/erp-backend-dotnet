using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    public partial class FixHCMRedundantColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Fix MilitaryServiceStatus: drop old snake_case columns
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[HCM].[MilitaryServiceStatus]') AND name = 'created_at')
                BEGIN
                    ALTER TABLE [HCM].[MilitaryServiceStatus] DROP COLUMN [created_at];
                END
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[HCM].[MilitaryServiceStatus]') AND name = 'creator_id')
                BEGIN
                    ALTER TABLE [HCM].[MilitaryServiceStatus] DROP COLUMN [creator_id];
                END
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[HCM].[MilitaryServiceStatus]') AND name = 'name')
                BEGIN
                    ALTER TABLE [HCM].[MilitaryServiceStatus] DROP COLUMN [name];
                END
            ");

            // Fix MaritalStatus: drop old snake_case columns
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[HCM].[MaritalStatus]') AND name = 'created_at')
                BEGIN
                    ALTER TABLE [HCM].[MaritalStatus] DROP COLUMN [created_at];
                END
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[HCM].[MaritalStatus]') AND name = 'creator_id')
                BEGIN
                    ALTER TABLE [HCM].[MaritalStatus] DROP COLUMN [creator_id];
                END
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[HCM].[MaritalStatus]') AND name = 'name')
                BEGIN
                    ALTER TABLE [HCM].[MaritalStatus] DROP COLUMN [name];
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-adding is not strictly necessary for this fix, but keeping the structure
        }
    }
}
