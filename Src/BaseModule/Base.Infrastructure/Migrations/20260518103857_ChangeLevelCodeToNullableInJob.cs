using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLevelCodeToNullableInJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.EnsureSchema(
                name: "general");

            /*
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Shared",
                table: "company_units",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "Shared",
                table: "company_units",
                newName: "code");
            */

            migrationBuilder.AlterColumn<int>(
                name: "LevelCode",
                schema: "HCM",
                table: "Job",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            /*
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Shared",
                table: "company_units",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                schema: "Shared",
                table: "company_units",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Shared",
                table: "company_units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifierId",
                schema: "Shared",
                table: "company_units",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                schema: "Shared",
                table: "company_units",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "Shared",
                table: "company_units",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "authorized_users",
                schema: "Shared",
                table: "company_units",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "company_id",
                schema: "Shared",
                table: "company_units",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "Shared",
                table: "company_units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "last_level",
                schema: "Shared",
                table: "company_units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "level",
                schema: "Shared",
                table: "company_units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                schema: "Shared",
                table: "company_units",
                type: "uniqueidentifier",
                nullable: true);
            */

            /*
            migrationBuilder.CreateTable(
                name: "roles",
                schema: "shared",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_static = table.Column<bool>(type: "bit", nullable: false),
                    authorized_users = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    is_superuser = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    person_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_persons_person_id",
                        column: x => x.person_id,
                        principalSchema: "General",
                        principalTable: "persons",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_members",
                schema: "shared",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    member_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    authorized_users = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_members_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "shared",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "shared",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    module_id = table.Column<long>(type: "bigint", nullable: false),
                    entity_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    read = table.Column<bool>(type: "bit", nullable: false),
                    create = table.Column<bool>(type: "bit", nullable: false),
                    edit = table.Column<bool>(type: "bit", nullable: false),
                    delete = table.Column<bool>(type: "bit", nullable: false),
                    log = table.Column<bool>(type: "bit", nullable: false),
                    print = table.Column<bool>(type: "bit", nullable: false),
                    imp = table.Column<bool>(type: "bit", nullable: false),
                    exp = table.Column<bool>(type: "bit", nullable: false),
                    if_not_creator = table.Column<bool>(type: "bit", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_permissions_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permissions_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "shared",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission_commands",
                schema: "shared",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entity_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    command_key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_permission_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entity_type_command_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    value = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission_commands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_permission_commands_entity_type_commands_entity_type_command_id",
                        column: x => x.entity_type_command_id,
                        principalSchema: "general",
                        principalTable: "entity_type_commands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_commands_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_commands_role_permissions_role_permission_id",
                        column: x => x.role_permission_id,
                        principalSchema: "shared",
                        principalTable: "role_permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_commands_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "shared",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission_constraints",
                schema: "shared",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entity_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_permission_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    field_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    read = table.Column<bool>(type: "bit", nullable: false),
                    create = table.Column<bool>(type: "bit", nullable: false),
                    edit = table.Column<bool>(type: "bit", nullable: false),
                    print = table.Column<bool>(type: "bit", nullable: false),
                    imp = table.Column<bool>(type: "bit", nullable: false),
                    exp = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission_constraints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_permission_constraints_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_constraints_role_permissions_role_permission_id",
                        column: x => x.role_permission_id,
                        principalSchema: "shared",
                        principalTable: "role_permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_constraints_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "shared",
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            */

            /*
            migrationBuilder.CreateIndex(
                name: "IX_company_units_company_id",
                schema: "Shared",
                table: "company_units",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_company_units_parent_id",
                schema: "Shared",
                table: "company_units",
                column: "parent_id");
            */

            /*
            migrationBuilder.CreateIndex(
                name: "IX_role_members_role_id",
                schema: "shared",
                table: "role_members",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_commands_entity_type_command_id",
                schema: "shared",
                table: "role_permission_commands",
                column: "entity_type_command_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_commands_entity_type_id",
                schema: "shared",
                table: "role_permission_commands",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_commands_role_id",
                schema: "shared",
                table: "role_permission_commands",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_commands_role_permission_id",
                schema: "shared",
                table: "role_permission_commands",
                column: "role_permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_constraints_entity_type_id",
                schema: "shared",
                table: "role_permission_constraints",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_constraints_role_id",
                schema: "shared",
                table: "role_permission_constraints",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_constraints_role_permission_id",
                schema: "shared",
                table: "role_permission_constraints",
                column: "role_permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_entity_type_id",
                schema: "shared",
                table: "role_permissions",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_module_id",
                schema: "shared",
                table: "role_permissions",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_role_id",
                schema: "shared",
                table: "role_permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_person_id",
                schema: "general",
                table: "users",
                column: "person_id",
                unique: true,
                filter: "[person_id] IS NOT NULL");
            */

            /*
            migrationBuilder.AddForeignKey(
                name: "FK_company_units_Companies_company_id",
                schema: "Shared",
                table: "company_units",
                column: "company_id",
                principalSchema: "General",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_units_company_units_parent_id",
                schema: "Shared",
                table: "company_units",
                column: "parent_id",
                principalSchema: "Shared",
                principalTable: "company_units",
                principalColumn: "Id");
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_company_units_Companies_company_id",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropForeignKey(
                name: "FK_company_units_company_units_parent_id",
                schema: "Shared",
                table: "company_units");
            */

            /*
            migrationBuilder.DropTable(
                name: "role_members",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "role_permission_commands",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "role_permission_constraints",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "users",
                schema: "general");

            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "shared");
            */

            /*
            migrationBuilder.DropIndex(
                name: "IX_company_units_company_id",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropIndex(
                name: "IX_company_units_parent_id",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "ModifierId",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "authorized_users",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "company_id",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "last_level",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "level",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropColumn(
                name: "parent_id",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "Shared",
                table: "company_units",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "Shared",
                table: "company_units",
                newName: "Code");
            */

            migrationBuilder.AlterColumn<int>(
                name: "LevelCode",
                schema: "HCM",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
