using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialBaseOld : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "general");

           

            migrationBuilder.CreateTable(
                name: "banks",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currencies",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    symbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    iso = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "files",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guid = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    file = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    filesize = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sha1 = table.Column<string>(type: "nvarchar(42)", maxLength: 42, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    direction = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_fa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    color = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    prefix = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_groups",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_static = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bank_statement_patterns",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    column_cheque_no = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    column_cheque_date = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    column_debit = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    column_credit = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    first_row_no = table.Column<short>(type: "smallint", nullable: false),
                    ignore_latest_rows = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bank_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank_statement_patterns", x => x.id);
                    table.ForeignKey(
                        name: "FK_bank_statement_patterns_banks_bank_id",
                        column: x => x.bank_id,
                        principalSchema: "general",
                        principalTable: "banks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    tax_code = table.Column<int>(type: "int", nullable: true),
                    currency_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                    table.ForeignKey(
                        name: "FK_countries_currencies_currency_id",
                        column: x => x.currency_id,
                        principalSchema: "general",
                        principalTable: "currencies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "domains",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hostname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    holding = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    password_level = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    auth_method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    auth_send_type = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    api_key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    template_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    language_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_domains", x => x.id);
                    table.ForeignKey(
                        name: "FK_domains_languages_language_id",
                        column: x => x.language_id,
                        principalSchema: "general",
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_types",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_fa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    readable = table.Column<bool>(type: "bit", nullable: false),
                    creatable = table.Column<bool>(type: "bit", nullable: false),
                    editable = table.Column<bool>(type: "bit", nullable: false),
                    deletable = table.Column<bool>(type: "bit", nullable: false),
                    loggable = table.Column<bool>(type: "bit", nullable: false),
                    printable = table.Column<bool>(type: "bit", nullable: false),
                    importable = table.Column<bool>(type: "bit", nullable: false),
                    exportable = table.Column<bool>(type: "bit", nullable: false),
                    if_not_creator = table.Column<bool>(type: "bit", nullable: false),
                    has_restriction = table.Column<bool>(type: "bit", nullable: false),
                    permissible = table.Column<bool>(type: "bit", nullable: false),
                    has_constraint = table.Column<bool>(type: "bit", nullable: false),
                    content_type_id = table.Column<int>(type: "int", nullable: true),
                    module_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_types", x => x.id);
                    table.ForeignKey(
                        name: "FK_entity_types_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "module_person_groups",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    person_group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module_person_groups", x => x.id);
                    table.ForeignKey(
                        name: "FK_module_person_groups_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_module_person_groups_person_groups_person_group_id",
                        column: x => x.person_group_id,
                        principalSchema: "general",
                        principalTable: "person_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinces", x => x.id);
                    table.ForeignKey(
                        name: "FK_provinces_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "general",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    domain_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.id);
                    table.ForeignKey(
                        name: "FK_companies_domains_domain_id",
                        column: x => x.domain_id,
                        principalSchema: "general",
                        principalTable: "domains",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_type_commands",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_fa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    entity_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_type_commands", x => x.id);
                    table.ForeignKey(
                        name: "FK_entity_type_commands_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_type_constraints",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    field_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_fa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    readable = table.Column<bool>(type: "bit", nullable: false),
                    creatable = table.Column<bool>(type: "bit", nullable: false),
                    editable = table.Column<bool>(type: "bit", nullable: false),
                    printable = table.Column<bool>(type: "bit", nullable: false),
                    importable = table.Column<bool>(type: "bit", nullable: false),
                    exportable = table.Column<bool>(type: "bit", nullable: false),
                    entity_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_type_constraints", x => x.id);
                    table.ForeignKey(
                        name: "FK_entity_type_constraints_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entity_type_dependencies",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    read = table.Column<bool>(type: "bit", nullable: false),
                    create = table.Column<bool>(type: "bit", nullable: false),
                    edit = table.Column<bool>(type: "bit", nullable: false),
                    delete = table.Column<bool>(type: "bit", nullable: false),
                    log = table.Column<bool>(type: "bit", nullable: false),
                    print = table.Column<bool>(type: "bit", nullable: false),
                    imp = table.Column<bool>(type: "bit", nullable: false),
                    exp = table.Column<bool>(type: "bit", nullable: false),
                    if_not_creator = table.Column<bool>(type: "bit", nullable: false),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    required_entity_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_type_dependencies", x => x.id);
                    table.ForeignKey(
                        name: "FK_entity_type_dependencies_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entity_type_dependencies_entity_types_required_entity_type_id",
                        column: x => x.required_entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "file_entities",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    object_id = table.Column<long>(type: "bigint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    file_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_entities", x => x.id);
                    table.ForeignKey(
                        name: "FK_file_entities_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_file_entities_files_file_id",
                        column: x => x.file_id,
                        principalSchema: "general",
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    code2 = table.Column<int>(type: "int", nullable: true),
                    code3 = table.Column<int>(type: "int", nullable: true),
                    province_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.id);
                    table.ForeignKey(
                        name: "FK_cities_provinces_province_id",
                        column: x => x.province_id,
                        principalSchema: "general",
                        principalTable: "provinces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "company_modules",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_modules", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_modules_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_company_modules_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

         
            migrationBuilder.CreateTable(
                name: "menu_items",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order = table.Column<short>(type: "smallint", nullable: false),
                    name_fa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    link = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    short_key = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    new_tab = table.Column<bool>(type: "bit", nullable: false),
                    standard_page = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    meta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    entity_type_id = table.Column<int>(type: "int", nullable: true),
                    entity_type_command_id = table.Column<int>(type: "int", nullable: true),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_menu_items_entity_type_commands_entity_type_command_id",
                        column: x => x.entity_type_command_id,
                        principalSchema: "general",
                        principalTable: "entity_type_commands",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_menu_items_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_menu_items_menu_items_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "general",
                        principalTable: "menu_items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_menu_items_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "areas",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company_admins",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_admins", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_admins_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "educational_degrees",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tax_educational_degree = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educational_degrees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employment_contract_Descriptions",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employment_contract_Descriptions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employment_contract_titles",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employment_contract_titles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "error_logs",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    status_code = table.Column<int>(type: "int", nullable: false),
                    error_message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    traceback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    extra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    curl_command = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_error_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "foreign_languages",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foreign_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "housing_statuses",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_housing_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "job_positions",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "job_ranks",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_ranks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "jobs",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    object_id = table.Column<int>(type: "int", nullable: false),
                    object_repr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    action = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ip = table.Column<string>(type: "nvarchar(39)", maxLength: 39, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_logs_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_logs_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_logs_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "measurement_units",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tax_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measurement_units", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "military_service_statuses",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_military_service_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mission_types",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    has_formula = table.Column<bool>(type: "bit", nullable: false),
                    formula = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    multiplier = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    fixed_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    parametric_custom_formula = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    is_hourly = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mission_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    count = table.Column<int>(type: "int", nullable: true),
                    display_count = table.Column<bool>(type: "bit", nullable: false),
                    read_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    user2_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_notifications_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_addresses",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    postal_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    city_id = table.Column<int>(type: "int", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false),
                    province_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_addresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_person_addresses_cities_city_id",
                        column: x => x.city_id,
                        principalSchema: "general",
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_addresses_countries_country_id",
                        column: x => x.country_id,
                        principalSchema: "general",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_person_addresses_provinces_province_id",
                        column: x => x.province_id,
                        principalSchema: "general",
                        principalTable: "provinces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_bank_accounts",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    iban = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
                    account_number = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    card_number = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bank_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_bank_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_person_bank_accounts_banks_bank_id",
                        column: x => x.bank_id,
                        principalSchema: "general",
                        principalTable: "banks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_educational_degrees",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    study_field = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    educational_institution = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    gpa = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    educational_degree_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_educational_degrees", x => x.id);
                    table.ForeignKey(
                        name: "FK_person_educational_degrees_educational_degrees_educational_degree_id",
                        column: x => x.educational_degree_id,
                        principalSchema: "general",
                        principalTable: "educational_degrees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "person_emails",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_emails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_faxes",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    fax = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_faxes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_mobiles",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_mobiles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_phones",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_phones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_relatives",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    gender = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    father_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    citizen_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    id_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    relationship_type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    under_guardianship = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    birth_city_id = table.Column<int>(type: "int", nullable: true),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_relatives", x => x.id);
                    table.ForeignKey(
                        name: "FK_person_relatives_cities_birth_city_id",
                        column: x => x.birth_city_id,
                        principalSchema: "general",
                        principalTable: "cities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "person_websites",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_websites", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typ = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<long>(type: "bigint", nullable: false),
                    economic_code = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    economic_code_old = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    ttms_buyer_type = table.Column<short>(type: "smallint", nullable: true),
                    ttms_seller_type = table.Column<short>(type: "smallint", nullable: true),
                    is_internal_citizenship = table.Column<bool>(type: "bit", nullable: false),
                    citizen_code = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    natural_family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    natural_father_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    natural_national_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    passport_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    natural_birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    natural_sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    legal_manager_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    legal_manager_family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    legal_national_code = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    legal_register_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    legal_establishment_date = table.Column<DateOnly>(type: "date", nullable: true),
                    personnel_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    file_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    id_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    id_issuance_place = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    id_issuance_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_governmental = table.Column<bool>(type: "bit", nullable: false),
                    marital_status = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    marriage_date = table.Column<DateOnly>(type: "date", nullable: true),
                    photo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    service_years = table.Column<short>(type: "smallint", nullable: true),
                    service_months = table.Column<short>(type: "smallint", nullable: true),
                    service_days = table.Column<short>(type: "smallint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    birth_city_id = table.Column<int>(type: "int", nullable: true),
                    citizen_nationality_id = table.Column<int>(type: "int", nullable: true),
                    housing_status_id = table.Column<int>(type: "int", nullable: true),
                    military_service_status_id = table.Column<int>(type: "int", nullable: true),
                    religion_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons", x => x.id);
                    table.ForeignKey(
                        name: "FK_persons_cities_birth_city_id",
                        column: x => x.birth_city_id,
                        principalSchema: "general",
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_persons_countries_citizen_nationality_id",
                        column: x => x.citizen_nationality_id,
                        principalSchema: "general",
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_persons_housing_statuses_housing_status_id",
                        column: x => x.housing_status_id,
                        principalSchema: "general",
                        principalTable: "housing_statuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_persons_military_service_statuses_military_service_status_id",
                        column: x => x.military_service_status_id,
                        principalSchema: "general",
                        principalTable: "military_service_statuses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    username_encrypted = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    is_superuser = table.Column<bool>(type: "bit", nullable: false),
                    is_staff = table.Column<bool>(type: "bit", nullable: false),
                    is_report_viewer = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    auth_send_type = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    color_pallet = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    font_family = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    font_size = table.Column<double>(type: "float", nullable: false),
                    new_tab = table.Column<bool>(type: "bit", nullable: false),
                    last_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    language_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false),
                    access_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_languages_language_id",
                        column: x => x.language_id,
                        principalSchema: "general",
                        principalTable: "languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_persons_person_id",
                        column: x => x.person_id,
                        principalSchema: "general",
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "religions",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_religions", x => x.id);
                    table.ForeignKey(
                        name: "FK_religions_users_creator_id",
                        column: x => x.creator_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "select_logs",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filters_used = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    command = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ip = table.Column<string>(type: "nvarchar(39)", maxLength: 39, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_select_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_select_logs_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_select_logs_entity_types_entity_type_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_select_logs_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_select_logs_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_configs",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_configs", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_configs_modules_module_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_configs_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workplaces",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workplaces", x => x.id);
                    table.ForeignKey(
                        name: "FK_workplaces_users_creator_id",
                        column: x => x.creator_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_departments",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    work_place_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_departments", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_departments_users_creator_id",
                        column: x => x.creator_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_work_departments_workplaces_work_place_id",
                        column: x => x.work_place_id,
                        principalSchema: "general",
                        principalTable: "workplaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_operations",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    work_department_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_operations", x => x.id);
                    table.ForeignKey(
                        name: "FK_work_operations_users_creator_id",
                        column: x => x.creator_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_work_operations_work_departments_work_department_id",
                        column: x => x.work_department_id,
                        principalSchema: "general",
                        principalTable: "work_departments",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "areas_creator_id_9f75521a",
                schema: "general",
                table: "areas",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__bank_sta__4076F702CA9D216F",
                schema: "general",
                table: "bank_statement_patterns",
                column: "bank_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__banks__72E12F1BD4395708",
                schema: "general",
                table: "banks",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "cities_province_id_799ae9a0",
                schema: "general",
                table: "cities",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "UQ__cities__357D4CF991D1106C",
                schema: "general",
                table: "cities",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "companies_domain_id_51b444a3",
                schema: "general",
                table: "companies",
                column: "domain_id");

            migrationBuilder.CreateIndex(
                name: "company_admins_admin_id_7fe4b0ca",
                schema: "general",
                table: "company_admins",
                column: "admin_id");

            migrationBuilder.CreateIndex(
                name: "company_admins_company_id_dc4e1bcb",
                schema: "general",
                table: "company_admins",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "company_admins_creator_id_af4a192b",
                schema: "general",
                table: "company_admins",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "company_modules_company_id_98c248b4",
                schema: "general",
                table: "company_modules",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "company_modules_module_id_85bfd47f",
                schema: "general",
                table: "company_modules",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "countries_currency_id_3d87434c",
                schema: "general",
                table: "countries",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "UQ__countrie__357D4CF98C0F73DF",
                schema: "general",
                table: "countries",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__countrie__72E12F1B7CCF347E",
                schema: "general",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__currenci__357D4CF907683F55",
                schema: "general",
                table: "currencies",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "domains_language_id_a7140b25",
                schema: "general",
                table: "domains",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "UQ__domains__DA92E433B526F86E",
                schema: "general",
                table: "domains",
                column: "hostname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "educational_degrees_creator_id_693f4fac",
                schema: "general",
                table: "educational_degrees",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__educatio__72E12F1B3436D9CA",
                schema: "general",
                table: "educational_degrees",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "employment_contract_Descriptions_creator_id_2fcb5dd9",
                schema: "general",
                table: "employment_contract_Descriptions",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "employment_contract_titles_creator_id_f0cd95e9",
                schema: "general",
                table: "employment_contract_titles",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__employme__72E12F1B6B3538D6",
                schema: "general",
                table: "employment_contract_titles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "entity_type_commands_entity_type_id_c94a0ef5",
                schema: "general",
                table: "entity_type_commands",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "entity_type_constraints_entity_type_id_a7b4c730",
                schema: "general",
                table: "entity_type_constraints",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "entity_type_dependencies_entity_type_id_e631567c",
                schema: "general",
                table: "entity_type_dependencies",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "entity_type_dependencies_required_entity_type_id_64bab813",
                schema: "general",
                table: "entity_type_dependencies",
                column: "required_entity_type_id");

            migrationBuilder.CreateIndex(
                name: "entity_types_content_type_id_b7d4130b",
                schema: "general",
                table: "entity_types",
                column: "content_type_id");

            migrationBuilder.CreateIndex(
                name: "entity_types_module_id_55d1da29",
                schema: "general",
                table: "entity_types",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "error_logs_user_id_ac744206",
                schema: "general",
                table: "error_logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "file_entities_entity_type_id_addc8bf3",
                schema: "general",
                table: "file_entities",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "file_entities_file_id_1c97e002",
                schema: "general",
                table: "file_entities",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "UQ__files__2FC437B2639CC3A2",
                schema: "general",
                table: "files",
                column: "sha1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__files__497F6CB5C8798DFD",
                schema: "general",
                table: "files",
                column: "guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "foreign_languages_creator_id_f2c0414f",
                schema: "general",
                table: "foreign_languages",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__foreign___72E12F1B6F35B462",
                schema: "general",
                table: "foreign_languages",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "housing_statuses_creator_id_b7300b34",
                schema: "general",
                table: "housing_statuses",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__housing___72E12F1BF71088F1",
                schema: "general",
                table: "housing_statuses",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "job_positions_creator_id_361c185c",
                schema: "general",
                table: "job_positions",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__job_posi__72E12F1B18289596",
                schema: "general",
                table: "job_positions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "job_ranks_creator_id_06b134a2",
                schema: "general",
                table: "job_ranks",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__job_rank__72E12F1B78A994B0",
                schema: "general",
                table: "job_ranks",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "jobs_creator_id_56b9cd05",
                schema: "general",
                table: "jobs",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__jobs__72E12F1BF4AE1530",
                schema: "general",
                table: "jobs",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__language__357D4CF9757EF7F8",
                schema: "general",
                table: "languages",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__language__72E12F1BD0EA1B2E",
                schema: "general",
                table: "languages",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "logs_company_id_ed2c29a1",
                schema: "general",
                table: "logs",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "logs_entity_type_id_87a8fbba",
                schema: "general",
                table: "logs",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "logs_module_id_c6447665",
                schema: "general",
                table: "logs",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "logs_user_id_237f5f83",
                schema: "general",
                table: "logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "measurement_units_creator_id_adca9822",
                schema: "general",
                table: "measurement_units",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__measurem__72E12F1BFC5E9180",
                schema: "general",
                table: "measurement_units",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "menu_items_entity_type_command_id_4319057f",
                schema: "general",
                table: "menu_items",
                column: "entity_type_command_id");

            migrationBuilder.CreateIndex(
                name: "menu_items_entity_type_id_412db7e8",
                schema: "general",
                table: "menu_items",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "menu_items_module_id_56dd60a4",
                schema: "general",
                table: "menu_items",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "menu_items_parent_id_7032fad2",
                schema: "general",
                table: "menu_items",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "military_service_statuses_creator_id_2264fea4",
                schema: "general",
                table: "military_service_statuses",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__military__72E12F1BB332E6F2",
                schema: "general",
                table: "military_service_statuses",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "mission_types_creator_id_17915c73",
                schema: "general",
                table: "mission_types",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__mission___72E12F1BF6C8C347",
                schema: "general",
                table: "mission_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "module_person_groups_module_id_64506255",
                schema: "general",
                table: "module_person_groups",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "module_person_groups_person_group_id_1b684782",
                schema: "general",
                table: "module_person_groups",
                column: "person_group_id");

            migrationBuilder.CreateIndex(
                name: "UQ__modules__2E0A72B0D7D1DAD7",
                schema: "general",
                table: "modules",
                column: "name_en",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__modules__2E0A7AA66BFF07F4",
                schema: "general",
                table: "modules",
                column: "name_fa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "notifications_module_id_31f93f98",
                schema: "general",
                table: "notifications",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "notifications_user_id_468e288d",
                schema: "general",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "notifications_user2_id_8fe256fe",
                schema: "general",
                table: "notifications",
                column: "user2_id");

           

            migrationBuilder.CreateIndex(
                name: "person_addresses_city_id_bda2a750",
                schema: "general",
                table: "person_addresses",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "person_addresses_country_id_1fb41b6e",
                schema: "general",
                table: "person_addresses",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "person_addresses_person_id_d67d7fc6",
                schema: "general",
                table: "person_addresses",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_addresses_province_id_6ed2a37d",
                schema: "general",
                table: "person_addresses",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "person_bank_accounts_bank_id_f4412c39",
                schema: "general",
                table: "person_bank_accounts",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "person_bank_accounts_person_id_cea19acc",
                schema: "general",
                table: "person_bank_accounts",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_educational_degrees_creator_id_29916c83",
                schema: "general",
                table: "person_educational_degrees",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "person_educational_degrees_educational_degree_id_59f16fbf",
                schema: "general",
                table: "person_educational_degrees",
                column: "educational_degree_id");

            migrationBuilder.CreateIndex(
                name: "person_educational_degrees_person_id_c8759020",
                schema: "general",
                table: "person_educational_degrees",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_emails_person_id_7ab1a818",
                schema: "general",
                table: "person_emails",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_faxes_person_id_aeabbb62",
                schema: "general",
                table: "person_faxes",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_mobiles_person_id_ab811f2d",
                schema: "general",
                table: "person_mobiles",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_phones_person_id_ad2cd17d",
                schema: "general",
                table: "person_phones",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_relatives_birth_city_id_6e4fe77e",
                schema: "general",
                table: "person_relatives",
                column: "birth_city_id");

            migrationBuilder.CreateIndex(
                name: "person_relatives_creator_id_2e5156c1",
                schema: "general",
                table: "person_relatives",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "person_relatives_person_id_0ab58207",
                schema: "general",
                table: "person_relatives",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_websites_person_id_2bf2e8a6",
                schema: "general",
                table: "person_websites",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "persons_birth_city_id_9fea3d98",
                schema: "general",
                table: "persons",
                column: "birth_city_id");

            migrationBuilder.CreateIndex(
                name: "persons_citizen_nationality_id_ecc47f36",
                schema: "general",
                table: "persons",
                column: "citizen_nationality_id");

            migrationBuilder.CreateIndex(
                name: "persons_housing_status_id_c8e37e67",
                schema: "general",
                table: "persons",
                column: "housing_status_id");

            migrationBuilder.CreateIndex(
                name: "persons_military_service_status_id_31e57348",
                schema: "general",
                table: "persons",
                column: "military_service_status_id");

            migrationBuilder.CreateIndex(
                name: "persons_religion_id_bf81e08d",
                schema: "general",
                table: "persons",
                column: "religion_id");

            migrationBuilder.CreateIndex(
                name: "persons_typ_611e3c8e",
                schema: "general",
                table: "persons",
                column: "typ");

            migrationBuilder.CreateIndex(
                name: "UQ__persons__357D4CF9AE8920EA",
                schema: "general",
                table: "persons",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "provinces_country_id_8ee0b7b3",
                schema: "general",
                table: "provinces",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "religions_creator_id_65bb4aaf",
                schema: "general",
                table: "religions",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__religion__72E12F1B16AF9102",
                schema: "general",
                table: "religions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "select_logs_company_id_36cbe9f3",
                schema: "general",
                table: "select_logs",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "select_logs_entity_type_id_1ff059a2",
                schema: "general",
                table: "select_logs",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "select_logs_module_id_94ef8091",
                schema: "general",
                table: "select_logs",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "select_logs_user_id_e7ccfdfc",
                schema: "general",
                table: "select_logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "user_configs_module_id_015d8e4f",
                schema: "general",
                table: "user_configs",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "user_configs_user_id_c0e78255",
                schema: "general",
                table: "user_configs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__users__543848DE030F1F1D",
                schema: "general",
                table: "users",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__users__F3DBC57279382736",
                schema: "general",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_language_id_9c707b57",
                schema: "general",
                table: "users",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "UQ__work_dep__72E12F1B9A5F7AEA",
                schema: "general",
                table: "work_departments",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "work_departments_creator_id_7fdcedc2",
                schema: "general",
                table: "work_departments",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "work_departments_work_place_id_182cba09",
                schema: "general",
                table: "work_departments",
                column: "work_place_id");

            migrationBuilder.CreateIndex(
                name: "UQ__work_ope__737584F65F7907DF",
                schema: "general",
                table: "work_operations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "work_operations_creator_id_d10b2154",
                schema: "general",
                table: "work_operations",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "work_operations_work_department_id_a9f7d3e8",
                schema: "general",
                table: "work_operations",
                column: "work_department_id");

            migrationBuilder.CreateIndex(
                name: "UQ__workplac__72E12F1B6F317AC7",
                schema: "general",
                table: "workplaces",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "workplaces_creator_id_0cc80a6d",
                schema: "general",
                table: "workplaces",
                column: "creator_id");

            migrationBuilder.AddForeignKey(
                name: "FK_areas_users_creator_id",
                schema: "general",
                table: "areas",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_admins_users_admin_id",
                schema: "general",
                table: "company_admins",
                column: "admin_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_admins_users_creator_id",
                schema: "general",
                table: "company_admins",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_educational_degrees_users_creator_id",
                schema: "general",
                table: "educational_degrees",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employment_contract_Descriptions_users_creator_id",
                schema: "general",
                table: "employment_contract_Descriptions",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employment_contract_titles_users_creator_id",
                schema: "general",
                table: "employment_contract_titles",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_error_logs_users_user_id",
                schema: "general",
                table: "error_logs",
                column: "user_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_foreign_languages_users_creator_id",
                schema: "general",
                table: "foreign_languages",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_housing_statuses_users_creator_id",
                schema: "general",
                table: "housing_statuses",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_positions_users_creator_id",
                schema: "general",
                table: "job_positions",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_ranks_users_creator_id",
                schema: "general",
                table: "job_ranks",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jobs_users_creator_id",
                schema: "general",
                table: "jobs",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_logs_users_user_id",
                schema: "general",
                table: "logs",
                column: "user_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_measurement_units_users_creator_id",
                schema: "general",
                table: "measurement_units",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_military_service_statuses_users_creator_id",
                schema: "general",
                table: "military_service_statuses",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mission_types_users_creator_id",
                schema: "general",
                table: "mission_types",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_users_user2_id",
                schema: "general",
                table: "notifications",
                column: "user2_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_users_user_id",
                schema: "general",
                table: "notifications",
                column: "user_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_addresses_persons_person_id",
                schema: "general",
                table: "person_addresses",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_bank_accounts_persons_person_id",
                schema: "general",
                table: "person_bank_accounts",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_educational_degrees_persons_person_id",
                schema: "general",
                table: "person_educational_degrees",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_educational_degrees_users_creator_id",
                schema: "general",
                table: "person_educational_degrees",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_emails_persons_person_id",
                schema: "general",
                table: "person_emails",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_faxes_persons_person_id",
                schema: "general",
                table: "person_faxes",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_mobiles_persons_person_id",
                schema: "general",
                table: "person_mobiles",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_phones_persons_person_id",
                schema: "general",
                table: "person_phones",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_relatives_persons_person_id",
                schema: "general",
                table: "person_relatives",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_relatives_users_creator_id",
                schema: "general",
                table: "person_relatives",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_person_websites_persons_person_id",
                schema: "general",
                table: "person_websites",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_persons_religions_religion_id",
                schema: "general",
                table: "persons",
                column: "religion_id",
                principalSchema: "general",
                principalTable: "religions",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_housing_statuses_users_creator_id",
                schema: "general",
                table: "housing_statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_military_service_statuses_users_creator_id",
                schema: "general",
                table: "military_service_statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_religions_users_creator_id",
                schema: "general",
                table: "religions");

            migrationBuilder.DropTable(
                name: "areas",
                schema: "general");

            migrationBuilder.DropTable(
                name: "bank_statement_patterns",
                schema: "general");

            migrationBuilder.DropTable(
                name: "company_admins",
                schema: "general");

            migrationBuilder.DropTable(
                name: "company_modules",
                schema: "general");

            migrationBuilder.DropTable(
                name: "employment_contract_Descriptions",
                schema: "general");

            migrationBuilder.DropTable(
                name: "employment_contract_titles",
                schema: "general");

            migrationBuilder.DropTable(
                name: "entity_type_constraints",
                schema: "general");

            migrationBuilder.DropTable(
                name: "entity_type_dependencies",
                schema: "general");

            migrationBuilder.DropTable(
                name: "error_logs",
                schema: "general");

            migrationBuilder.DropTable(
                name: "file_entities",
                schema: "general");

            migrationBuilder.DropTable(
                name: "foreign_languages",
                schema: "general");

            migrationBuilder.DropTable(
                name: "job_positions",
                schema: "general");

            migrationBuilder.DropTable(
                name: "job_ranks",
                schema: "general");

            migrationBuilder.DropTable(
                name: "jobs",
                schema: "general");

            migrationBuilder.DropTable(
                name: "logs",
                schema: "general");

            migrationBuilder.DropTable(
                name: "measurement_units",
                schema: "general");

            migrationBuilder.DropTable(
                name: "menu_items",
                schema: "general");

            migrationBuilder.DropTable(
                name: "mission_types",
                schema: "general");

            migrationBuilder.DropTable(
                name: "module_person_groups",
                schema: "general");

            migrationBuilder.DropTable(
                name: "notifications",
                schema: "general");

          
            migrationBuilder.DropTable(
                name: "person_addresses",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_bank_accounts",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_educational_degrees",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_emails",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_faxes",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_mobiles",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_phones",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_relatives",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_websites",
                schema: "general");

            migrationBuilder.DropTable(
                name: "select_logs",
                schema: "general");

            migrationBuilder.DropTable(
                name: "user_configs",
                schema: "general");

            migrationBuilder.DropTable(
                name: "work_operations",
                schema: "general");

            migrationBuilder.DropTable(
                name: "files",
                schema: "general");

            migrationBuilder.DropTable(
                name: "entity_type_commands",
                schema: "general");

            migrationBuilder.DropTable(
                name: "person_groups",
                schema: "general");

            migrationBuilder.DropTable(
                name: "banks",
                schema: "general");

            migrationBuilder.DropTable(
                name: "educational_degrees",
                schema: "general");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "general");

            migrationBuilder.DropTable(
                name: "work_departments",
                schema: "general");

            migrationBuilder.DropTable(
                name: "entity_types",
                schema: "general");

            migrationBuilder.DropTable(
                name: "domains",
                schema: "general");

            migrationBuilder.DropTable(
                name: "workplaces",
                schema: "general");

            migrationBuilder.DropTable(
                name: "modules",
                schema: "general");

            migrationBuilder.DropTable(
                name: "users",
                schema: "general");

            migrationBuilder.DropTable(
                name: "languages",
                schema: "general");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "general");

            migrationBuilder.DropTable(
                name: "cities",
                schema: "general");

            migrationBuilder.DropTable(
                name: "housing_statuses",
                schema: "general");

            migrationBuilder.DropTable(
                name: "military_service_statuses",
                schema: "general");

            migrationBuilder.DropTable(
                name: "religions",
                schema: "general");

            migrationBuilder.DropTable(
                name: "provinces",
                schema: "general");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "general");

            migrationBuilder.DropTable(
                name: "currencies",
                schema: "general");
        }
    }
}
