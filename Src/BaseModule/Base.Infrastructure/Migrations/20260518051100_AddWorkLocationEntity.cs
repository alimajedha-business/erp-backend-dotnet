using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkLocationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.EnsureSchema(
                name: "general");

            migrationBuilder.Sql("IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'company_units_company_id_name_parent_id_131855b6_uniq') DROP INDEX [company_units_company_id_name_parent_id_131855b6_uniq] ON [Shared].[company_units];");
            migrationBuilder.Sql("IF EXISTS (SELECT name FROM sys.indexes WHERE name = 'company_units_company_id_code_af9699c4_uniq') DROP INDEX [company_units_company_id_code_af9699c4_uniq] ON [Shared].[company_units];");

            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'Name') EXEC sp_rename 'Shared.company_units.Name', 'name', 'COLUMN';");
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'Code') EXEC sp_rename 'Shared.company_units.Code', 'code', 'COLUMN';");

            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'CreatedAt') ALTER TABLE [Shared].[company_units] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'CreatorId') ALTER TABLE [Shared].[company_units] ADD [CreatorId] uniqueidentifier NULL;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'IsDeleted') ALTER TABLE [Shared].[company_units] ADD [IsDeleted] bit NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'ModifierId') ALTER TABLE [Shared].[company_units] ADD [ModifierId] uniqueidentifier NULL;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'TimeZone') ALTER TABLE [Shared].[company_units] ADD [TimeZone] nvarchar(max) NULL;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'UpdatedAt') ALTER TABLE [Shared].[company_units] ADD [UpdatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'authorized_users') ALTER TABLE [Shared].[company_units] ADD [authorized_users] nvarchar(max) NULL;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'company_id') ALTER TABLE [Shared].[company_units] ADD [company_id] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'is_active') ALTER TABLE [Shared].[company_units] ADD [is_active] bit NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'last_level') ALTER TABLE [Shared].[company_units] ADD [last_level] bit NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'level') ALTER TABLE [Shared].[company_units] ADD [level] int NOT NULL DEFAULT 0;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[Shared].[company_units]') AND name = 'parent_id') ALTER TABLE [Shared].[company_units] ADD [parent_id] uniqueidentifier NULL;");

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

            migrationBuilder.Sql(@"
IF OBJECT_ID('[shared].[roles]', 'U') IS NULL
BEGIN
    CREATE TABLE [shared].[roles] (
        [Id] uniqueidentifier NOT NULL,
        [name] nvarchar(max) NOT NULL,
        [is_static] bit NOT NULL,
        [authorized_users] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [TimeZone] nvarchar(max) NULL,
        [CreatorId] uniqueidentifier NULL,
        [IsDeleted] bit NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [ModifierId] uniqueidentifier NULL,
        [CompanyId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_roles] PRIMARY KEY ([Id])
    );
END");

            migrationBuilder.Sql(@"
IF OBJECT_ID('[general].[users]', 'U') IS NULL
BEGIN
    CREATE TABLE [general].[users] (
        [id] uniqueidentifier NOT NULL,
        [username] nvarchar(150) NOT NULL,
        [password] nvarchar(128) NOT NULL,
        [is_superuser] bit NOT NULL,
        [is_active] bit NOT NULL,
        [person_id] uniqueidentifier NULL,
        CONSTRAINT [PK_users] PRIMARY KEY ([id]),
        CONSTRAINT [FK_users_persons_person_id] FOREIGN KEY ([person_id]) REFERENCES [General].[persons] ([id])
    );
END");

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
            */

            migrationBuilder.Sql(@"
IF OBJECT_ID('[HCM].[WorkLocation]', 'U') IS NULL
BEGIN
    CREATE TABLE [HCM].[WorkLocation] (
        [Id] uniqueidentifier NOT NULL DEFAULT (NEWID()),
        [ParentId] uniqueidentifier NULL,
        [NextWorkLocationId] uniqueidentifier NULL,
        [Title] nvarchar(250) NOT NULL,
        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [TimeZone] nvarchar(50) NULL,
        [CreatorId] uniqueidentifier NULL,
        [IsDeleted] bit NOT NULL,
        [UpdatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
        [ModifierId] uniqueidentifier NULL,
        CONSTRAINT [PK_WorkLocation] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WorkLocation_WorkLocation_NextWorkLocationId] FOREIGN KEY ([NextWorkLocationId]) REFERENCES [HCM].[WorkLocation] ([Id]),
        CONSTRAINT [FK_WorkLocation_WorkLocation_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [HCM].[WorkLocation] ([Id])
    );
END");

            migrationBuilder.Sql(@"
IF OBJECT_ID('[shared].[role_members]', 'U') IS NULL
BEGIN
    CREATE TABLE [shared].[role_members] (
        [Id] uniqueidentifier NOT NULL,
        [company_id] uniqueidentifier NOT NULL,
        [role_id] uniqueidentifier NOT NULL,
        [member_id] uniqueidentifier NOT NULL,
        [authorized_users] nvarchar(max) NULL,
        [CreatedAt] datetime2 NOT NULL,
        [TimeZone] nvarchar(max) NULL,
        [CreatorId] uniqueidentifier NULL,
        [IsDeleted] bit NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [ModifierId] uniqueidentifier NULL,
        CONSTRAINT [PK_role_members] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_role_members_roles_role_id] FOREIGN KEY ([role_id]) REFERENCES [shared].[roles] ([Id]) ON DELETE CASCADE
    );
END");

            migrationBuilder.Sql(@"
IF OBJECT_ID('[shared].[role_permissions]', 'U') IS NULL
BEGIN
    CREATE TABLE [shared].[role_permissions] (
        [Id] uniqueidentifier NOT NULL,
        [module_id] bigint NOT NULL,
        [entity_type_id] uniqueidentifier NOT NULL,
        [read] bit NOT NULL,
        [create] bit NOT NULL,
        [edit] bit NOT NULL,
        [delete] bit NOT NULL,
        [log] bit NOT NULL,
        [print] bit NOT NULL,
        [imp] bit NOT NULL,
        [exp] bit NOT NULL,
        [if_not_creator] bit NOT NULL,
        [role_id] uniqueidentifier NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [TimeZone] nvarchar(max) NULL,
        [CreatorId] uniqueidentifier NULL,
        [IsDeleted] bit NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [ModifierId] uniqueidentifier NULL,
        CONSTRAINT [PK_role_permissions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_role_permissions_entity_types_entity_type_id] FOREIGN KEY ([entity_type_id]) REFERENCES [general].[entity_types] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_role_permissions_modules_module_id] FOREIGN KEY ([module_id]) REFERENCES [general].[modules] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_role_permissions_roles_role_id] FOREIGN KEY ([role_id]) REFERENCES [shared].[roles] ([Id]) ON DELETE CASCADE
    );
END");

            migrationBuilder.Sql(@"
IF OBJECT_ID('[shared].[role_permission_commands]', 'U') IS NULL
BEGIN
    CREATE TABLE [shared].[role_permission_commands] (
        [Id] uniqueidentifier NOT NULL,
        [role_id] uniqueidentifier NOT NULL,
        [entity_type_id] uniqueidentifier NOT NULL,
        [command_key] nvarchar(max) NOT NULL,
        [role_permission_id] uniqueidentifier NOT NULL,
        [entity_type_command_id] uniqueidentifier NOT NULL,
        [value] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [TimeZone] nvarchar(max) NULL,
        [CreatorId] uniqueidentifier NULL,
        [IsDeleted] bit NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [ModifierId] uniqueidentifier NULL,
        CONSTRAINT [PK_role_permission_commands] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_role_permission_commands_entity_type_commands_entity_type_command_id] FOREIGN KEY ([entity_type_command_id]) REFERENCES [general].[entity_type_commands] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_role_permission_commands_entity_types_entity_type_id] FOREIGN KEY ([entity_type_id]) REFERENCES [general].[entity_types] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_role_permission_commands_role_permissions_role_permission_id] FOREIGN KEY ([role_permission_id]) REFERENCES [shared].[role_permissions] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_role_permission_commands_roles_role_id] FOREIGN KEY ([role_id]) REFERENCES [shared].[roles] ([Id]) ON DELETE CASCADE
    );
END");

            migrationBuilder.Sql(@"
IF OBJECT_ID('[shared].[role_permission_constraints]', 'U') IS NULL
BEGIN
    CREATE TABLE [shared].[role_permission_constraints] (
        [Id] uniqueidentifier NOT NULL,
        [role_id] uniqueidentifier NOT NULL,
        [entity_type_id] uniqueidentifier NOT NULL,
        [role_permission_id] uniqueidentifier NOT NULL,
        [field_name] nvarchar(max) NOT NULL,
        [read] bit NOT NULL,
        [create] bit NOT NULL,
        [edit] bit NOT NULL,
        [print] bit NOT NULL,
        [imp] bit NOT NULL,
        [exp] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [TimeZone] nvarchar(max) NULL,
        [CreatorId] uniqueidentifier NULL,
        [IsDeleted] bit NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [ModifierId] uniqueidentifier NULL,
        CONSTRAINT [PK_role_permission_constraints] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_role_permission_constraints_entity_types_entity_type_id] FOREIGN KEY ([entity_type_id]) REFERENCES [general].[entity_types] ([id]) ON DELETE CASCADE,
        CONSTRAINT [FK_role_permission_constraints_role_permissions_role_permission_id] FOREIGN KEY ([role_permission_id]) REFERENCES [shared].[role_permissions] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_role_permission_constraints_roles_role_id] FOREIGN KEY ([role_id]) REFERENCES [shared].[roles] ([Id]) ON DELETE CASCADE
    );
END");

            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_company_units_company_id' AND object_id = OBJECT_ID('[Shared].[company_units]')) CREATE INDEX [IX_company_units_company_id] ON [Shared].[company_units] ([company_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_company_units_parent_id' AND object_id = OBJECT_ID('[Shared].[company_units]')) CREATE INDEX [IX_company_units_parent_id] ON [Shared].[company_units] ([parent_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_members_role_id' AND object_id = OBJECT_ID('[shared].[role_members]')) CREATE INDEX [IX_role_members_role_id] ON [shared].[role_members] ([role_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permission_commands_entity_type_command_id' AND object_id = OBJECT_ID('[shared].[role_permission_commands]')) CREATE INDEX [IX_role_permission_commands_entity_type_command_id] ON [shared].[role_permission_commands] ([entity_type_command_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permission_commands_entity_type_id' AND object_id = OBJECT_ID('[shared].[role_permission_commands]')) CREATE INDEX [IX_role_permission_commands_entity_type_id] ON [shared].[role_permission_commands] ([entity_type_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permission_commands_role_id' AND object_id = OBJECT_ID('[shared].[role_permission_commands]')) CREATE INDEX [IX_role_permission_commands_role_id] ON [shared].[role_permission_commands] ([role_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permission_commands_role_permission_id' AND object_id = OBJECT_ID('[shared].[role_permission_commands]')) CREATE INDEX [IX_role_permission_commands_role_permission_id] ON [shared].[role_permission_commands] ([role_permission_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permission_constraints_entity_type_id' AND object_id = OBJECT_ID('[shared].[role_permission_constraints]')) CREATE INDEX [IX_role_permission_constraints_entity_type_id] ON [shared].[role_permission_constraints] ([entity_type_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permission_constraints_role_id' AND object_id = OBJECT_ID('[shared].[role_permission_constraints]')) CREATE INDEX [IX_role_permission_constraints_role_id] ON [shared].[role_permission_constraints] ([role_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permission_constraints_role_permission_id' AND object_id = OBJECT_ID('[shared].[role_permission_constraints]')) CREATE INDEX [IX_role_permission_constraints_role_permission_id] ON [shared].[role_permission_constraints] ([role_permission_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permissions_entity_type_id' AND object_id = OBJECT_ID('[shared].[role_permissions]')) CREATE INDEX [IX_role_permissions_entity_type_id] ON [shared].[role_permissions] ([entity_type_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permissions_module_id' AND object_id = OBJECT_ID('[shared].[role_permissions]')) CREATE INDEX [IX_role_permissions_module_id] ON [shared].[role_permissions] ([module_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_role_permissions_role_id' AND object_id = OBJECT_ID('[shared].[role_permissions]')) CREATE INDEX [IX_role_permissions_role_id] ON [shared].[role_permissions] ([role_id]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_users_person_id' AND object_id = OBJECT_ID('[general].[users]')) CREATE UNIQUE INDEX [IX_users_person_id] ON [general].[users] ([person_id]) WHERE [person_id] IS NOT NULL;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_WorkLocation_IsDeleted' AND object_id = OBJECT_ID('[HCM].[WorkLocation]')) CREATE INDEX [IX_WorkLocation_IsDeleted] ON [HCM].[WorkLocation] ([IsDeleted]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_WorkLocation_NextWorkLocation' AND object_id = OBJECT_ID('[HCM].[WorkLocation]')) CREATE INDEX [IX_WorkLocation_NextWorkLocation] ON [HCM].[WorkLocation] ([NextWorkLocationId]);");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IX_WorkLocation_Parent' AND object_id = OBJECT_ID('[HCM].[WorkLocation]')) CREATE INDEX [IX_WorkLocation_Parent] ON [HCM].[WorkLocation] ([ParentId]);");

            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_company_units_Companies_company_id' AND parent_object_id = OBJECT_ID('[Shared].[company_units]')) ALTER TABLE [Shared].[company_units] ADD CONSTRAINT [FK_company_units_Companies_company_id] FOREIGN KEY ([company_id]) REFERENCES [General].[Companies] ([Id]) ON DELETE CASCADE;");
            migrationBuilder.Sql("IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_company_units_company_units_parent_id' AND parent_object_id = OBJECT_ID('[Shared].[company_units]')) ALTER TABLE [Shared].[company_units] ADD CONSTRAINT [FK_company_units_company_units_parent_id] FOREIGN KEY ([parent_id]) REFERENCES [Shared].[company_units] ([Id]);");

            /*
            migrationBuilder.CreateTable(
                name: "WorkLocation",
                schema: "HCM",
                ...
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_units_Companies_company_id",
                schema: "Shared",
                table: "company_units");

            migrationBuilder.DropForeignKey(
                name: "FK_company_units_company_units_parent_id",
                schema: "Shared",
                table: "company_units");

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
                name: "WorkLocation",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "shared");

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
        }
    }
}
