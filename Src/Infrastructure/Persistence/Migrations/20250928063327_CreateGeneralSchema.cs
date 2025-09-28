using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateGeneralSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "general");

            migrationBuilder.CreateTable(
                name: "bank_account_types",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    has_cheque_book = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bank_acc__3213E83F1A2A955F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bank_operation_types",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bank_ope__3213E83FB66226B4", x => x.id);
                });

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
                    table.PrimaryKey("PK__banks__3213E83F1399B003", x => x.id);
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
                    iso = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__currenci__3213E83F9B15A5E8", x => x.id);
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
                    table.PrimaryKey("PK__language__3213E83F0C66BE2A", x => x.id);
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
                    table.PrimaryKey("PK__modules__3213E83F0BA52FF9", x => x.id);
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
                    type = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__person_g__3213E83FD9150C37", x => x.id);
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
                    table.PrimaryKey("PK__bank_sta__3213E83F9F2ED696", x => x.id);
                    table.ForeignKey(
                        name: "bank_statement_patterns_bank_id_85d1bd4e_fk_banks_id",
                        column: x => x.bank_id,
                        principalSchema: "general",
                        principalTable: "banks",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__countrie__3213E83FA8F8A627", x => x.id);
                    table.ForeignKey(
                        name: "countries_currency_id_3d87434c_fk_currencies_id",
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
                    allowed_ips = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    password_level = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    auth_method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    auth_send_type = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    api_key = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    template = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    max_failed_login_attempts = table.Column<int>(type: "int", nullable: true),
                    block_duration = table.Column<TimeOnly>(type: "time", nullable: true),
                    sender_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    is_unique_person = table.Column<bool>(type: "bit", nullable: false),
                    architecture_type = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    default_currency_id = table.Column<int>(type: "int", nullable: false),
                    language_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__domains__3213E83F7880908D", x => x.id);
                    table.ForeignKey(
                        name: "domains_default_currency_id_06c302a4_fk_currencies_id",
                        column: x => x.default_currency_id,
                        principalSchema: "general",
                        principalTable: "currencies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "domains_language_id_a7140b25_fk_languages_id",
                        column: x => x.language_id,
                        principalSchema: "general",
                        principalTable: "languages",
                        principalColumn: "id");
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
                    ordering = table.Column<short>(type: "smallint", nullable: true),
                    content_type_id = table.Column<int>(type: "int", nullable: true),
                    module_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__entity_t__3213E83F1AD03745", x => x.id);
                    table.ForeignKey(
                        name: "entity_types_module_id_55d1da29_fk_modules_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__module_p__3213E83F28F3FE6D", x => x.id);
                    table.ForeignKey(
                        name: "module_person_groups_module_id_64506255_fk_modules_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "module_person_groups_person_group_id_1b684782_fk_person_groups_id",
                        column: x => x.person_group_id,
                        principalSchema: "general",
                        principalTable: "person_groups",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__province__3213E83F6DDFBA22", x => x.id);
                    table.ForeignKey(
                        name: "provinces_country_id_8ee0b7b3_fk_countries_id",
                        column: x => x.country_id,
                        principalSchema: "general",
                        principalTable: "countries",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__companie__3213E83FBE2524AA", x => x.id);
                    table.ForeignKey(
                        name: "companies_domain_id_51b444a3_fk_domains_id",
                        column: x => x.domain_id,
                        principalSchema: "general",
                        principalTable: "domains",
                        principalColumn: "id");
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
                    ordering = table.Column<short>(type: "smallint", nullable: true),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    permissible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__entity_t__3213E83FCFCBD04F", x => x.id);
                    table.ForeignKey(
                        name: "entity_type_commands_entity_type_id_c94a0ef5_fk_entity_types_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id");
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
                    ordering = table.Column<short>(type: "smallint", nullable: true),
                    entity_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__entity_t__3213E83FF883D863", x => x.id);
                    table.ForeignKey(
                        name: "entity_type_constraints_entity_type_id_a7b4c730_fk_entity_types_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__entity_t__3213E83F6CD8EDE8", x => x.id);
                    table.ForeignKey(
                        name: "entity_type_dependencies_entity_type_id_e631567c_fk_entity_types_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "entity_type_dependencies_required_entity_type_id_64bab813_fk_entity_types_id",
                        column: x => x.required_entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__cities__3213E83F57F6C537", x => x.id);
                    table.ForeignKey(
                        name: "cities_province_id_799ae9a0_fk_provinces_id",
                        column: x => x.province_id,
                        principalSchema: "general",
                        principalTable: "provinces",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__company___3213E83F5A305B17", x => x.id);
                    table.ForeignKey(
                        name: "company_modules_company_id_98c248b4_fk_companies_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "company_modules_module_id_85bfd47f_fk_modules_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__menu_ite__3213E83F467DEF67", x => x.id);
                    table.ForeignKey(
                        name: "menu_items_entity_type_command_id_4319057f_fk_entity_type_commands_id",
                        column: x => x.entity_type_command_id,
                        principalSchema: "general",
                        principalTable: "entity_type_commands",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "menu_items_entity_type_id_412db7e8_fk_entity_types_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "menu_items_module_id_56dd60a4_fk_modules_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "menu_items_parent_id_7032fad2_fk_menu_items_id",
                        column: x => x.parent_id,
                        principalSchema: "general",
                        principalTable: "menu_items",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__areas__3213E83F4E6D45E1", x => x.id);
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
                    table.PrimaryKey("PK__company___3213E83F107ADA07", x => x.id);
                    table.ForeignKey(
                        name: "company_admins_company_id_dc4e1bcb_fk_companies_id",
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
                    table.PrimaryKey("PK__educatio__3213E83F5CD5C6F4", x => x.id);
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
                    table.PrimaryKey("PK__employme__3213E83FDDA602DA", x => x.id);
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
                    table.PrimaryKey("PK__employme__3213E83F56B8CDCE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "error_logs",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    method = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    status_code = table.Column<int>(type: "int", nullable: false),
                    error_message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    traceback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    extra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    curl_command = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__error_lo__3213E83F5550F3FA", x => x.id);
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
                    table.PrimaryKey("PK__foreign___3213E83F15916AB6", x => x.id);
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
                    table.PrimaryKey("PK__housing___3213E83FD3EDA6F5", x => x.id);
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
                    table.PrimaryKey("PK__job_posi__3213E83FC70FA7E0", x => x.id);
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
                    table.PrimaryKey("PK__job_rank__3213E83F299081EA", x => x.id);
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
                    table.PrimaryKey("PK__jobs__3213E83F4FDD96B8", x => x.id);
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
                    period_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__logs__3213E83FB734E2C5", x => x.id);
                    table.ForeignKey(
                        name: "logs_company_id_ed2c29a1_fk_companies_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "logs_entity_type_id_87a8fbba_fk_entity_types_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "logs_module_id_c6447665_fk_modules_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__measurem__3213E83F9EFC8026", x => x.id);
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
                    table.PrimaryKey("PK__military__3213E83F50066676", x => x.id);
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
                    table.PrimaryKey("PK__mission___3213E83F055055A9", x => x.id);
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
                    table.PrimaryKey("PK__notifica__3213E83FF9C80CEB", x => x.id);
                    table.ForeignKey(
                        name: "notifications_module_id_31f93f98_fk_modules_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__person_a__3213E83FE16C68D2", x => x.id);
                    table.ForeignKey(
                        name: "person_addresses_city_id_bda2a750_fk_cities_id",
                        column: x => x.city_id,
                        principalSchema: "general",
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "person_addresses_country_id_1fb41b6e_fk_countries_id",
                        column: x => x.country_id,
                        principalSchema: "general",
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "person_addresses_province_id_6ed2a37d_fk_provinces_id",
                        column: x => x.province_id,
                        principalSchema: "general",
                        principalTable: "provinces",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "person_bank_accounts",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    bank_branch_code = table.Column<int>(type: "int", nullable: true),
                    bank_branch_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
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
                    table.PrimaryKey("PK__person_b__3213E83FBCD68BC6", x => x.id);
                    table.ForeignKey(
                        name: "person_bank_accounts_bank_id_f4412c39_fk_banks_id",
                        column: x => x.bank_id,
                        principalSchema: "general",
                        principalTable: "banks",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__person_e__3213E83F256860BE", x => x.id);
                    table.ForeignKey(
                        name: "person_educational_degrees_educational_degree_id_59f16fbf_fk_educational_degrees_id",
                        column: x => x.educational_degree_id,
                        principalSchema: "general",
                        principalTable: "educational_degrees",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__person_e__3213E83F3B78C1DD", x => x.id);
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
                    table.PrimaryKey("PK__person_f__3213E83F6572A638", x => x.id);
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
                    table.PrimaryKey("PK__person_m__3213E83F324FA343", x => x.id);
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
                    table.PrimaryKey("PK__person_p__3213E83FE7054445", x => x.id);
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
                    table.PrimaryKey("PK__person_r__3213E83F488EC393", x => x.id);
                    table.ForeignKey(
                        name: "person_relatives_birth_city_id_6e4fe77e_fk_cities_id",
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
                    table.PrimaryKey("PK__person_w__3213E83F3EE363BA", x => x.id);
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
                    table.PrimaryKey("PK__persons__3213E83FA537E099", x => x.id);
                    table.ForeignKey(
                        name: "persons_birth_city_id_9fea3d98_fk_cities_id",
                        column: x => x.birth_city_id,
                        principalSchema: "general",
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "persons_citizen_nationality_id_ecc47f36_fk_countries_id",
                        column: x => x.citizen_nationality_id,
                        principalSchema: "general",
                        principalTable: "countries",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "persons_housing_status_id_c8e37e67_fk_housing_statuses_id",
                        column: x => x.housing_status_id,
                        principalSchema: "general",
                        principalTable: "housing_statuses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "persons_military_service_status_id_31e57348_fk_military_service_statuses_id",
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
                    signature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    access_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refresh_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    access_start_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    access_end_time = table.Column<TimeOnly>(type: "time", nullable: true),
                    expire_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    allowed_ips = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    language_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F46F42FAE", x => x.id);
                    table.ForeignKey(
                        name: "users_language_id_9c707b57_fk_languages_id",
                        column: x => x.language_id,
                        principalSchema: "general",
                        principalTable: "languages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "users_person_id_5146b3bd_fk_persons_id",
                        column: x => x.person_id,
                        principalSchema: "general",
                        principalTable: "persons",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__religion__3213E83F14A489F8", x => x.id);
                    table.ForeignKey(
                        name: "religions_creator_id_65bb4aaf_fk_users_id",
                        column: x => x.creator_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "select_logs",
                schema: "general",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filters_used = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    command = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ip = table.Column<string>(type: "nvarchar(39)", maxLength: 39, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__select_l__3213E83F9F91C3A2", x => x.id);
                    table.ForeignKey(
                        name: "select_logs_company_id_36cbe9f3_fk_companies_id",
                        column: x => x.company_id,
                        principalSchema: "general",
                        principalTable: "companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "select_logs_entity_type_id_1ff059a2_fk_entity_types_id",
                        column: x => x.entity_type_id,
                        principalSchema: "general",
                        principalTable: "entity_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "select_logs_module_id_94ef8091_fk_modules_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "select_logs_user_id_e7ccfdfc_fk_users_id",
                        column: x => x.user_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__user_con__3213E83F56E35490", x => x.id);
                    table.ForeignKey(
                        name: "user_configs_module_id_015d8e4f_fk_modules_id",
                        column: x => x.module_id,
                        principalSchema: "general",
                        principalTable: "modules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_configs_user_id_c0e78255_fk_users_id",
                        column: x => x.user_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__workplac__3213E83FA78C10C5", x => x.id);
                    table.ForeignKey(
                        name: "workplaces_creator_id_0cc80a6d_fk_users_id",
                        column: x => x.creator_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__work_dep__3213E83F31AD08A1", x => x.id);
                    table.ForeignKey(
                        name: "work_departments_creator_id_7fdcedc2_fk_users_id",
                        column: x => x.creator_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "work_departments_work_place_id_182cba09_fk_workplaces_id",
                        column: x => x.work_place_id,
                        principalSchema: "general",
                        principalTable: "workplaces",
                        principalColumn: "id");
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
                    table.PrimaryKey("PK__work_ope__3213E83F77B06DBE", x => x.id);
                    table.ForeignKey(
                        name: "work_operations_creator_id_d10b2154_fk_users_id",
                        column: x => x.creator_id,
                        principalSchema: "general",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "work_operations_work_department_id_a9f7d3e8_fk_work_departments_id",
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
                name: "UQ__bank_ope__357D4CF98B86A8C8",
                schema: "general",
                table: "bank_operation_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__bank_ope__72E12F1BB777BDE5",
                schema: "general",
                table: "bank_operation_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__bank_sta__4076F70269DE7D7F",
                schema: "general",
                table: "bank_statement_patterns",
                column: "bank_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__banks__72E12F1BAEFE9E5E",
                schema: "general",
                table: "banks",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "cities_code2_fc22c31a_uniq",
                schema: "general",
                table: "cities",
                column: "code2",
                unique: true,
                filter: "([code2] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "cities_code3_cfe7131c_uniq",
                schema: "general",
                table: "cities",
                column: "code3",
                unique: true,
                filter: "([code3] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "cities_province_id_799ae9a0",
                schema: "general",
                table: "cities",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "UQ__cities__357D4CF98A5D25D9",
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
                name: "company_admins_company_id_admin_id_53861a81_uniq",
                schema: "general",
                table: "company_admins",
                columns: new[] { "company_id", "admin_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [admin_id] IS NOT NULL)");

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
                name: "company_modules_company_id_module_id_2dc7a72c_uniq",
                schema: "general",
                table: "company_modules",
                columns: new[] { "company_id", "module_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [module_id] IS NOT NULL)");

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
                name: "countries_tax_code_99e2105c_uniq",
                schema: "general",
                table: "countries",
                column: "tax_code",
                unique: true,
                filter: "([tax_code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__countrie__357D4CF992BD5944",
                schema: "general",
                table: "countries",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__countrie__72E12F1B07ACC871",
                schema: "general",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__currenci__357D4CF9D837DF27",
                schema: "general",
                table: "currencies",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "domains_default_currency_id_06c302a4",
                schema: "general",
                table: "domains",
                column: "default_currency_id");

            migrationBuilder.CreateIndex(
                name: "domains_language_id_a7140b25",
                schema: "general",
                table: "domains",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "UQ__domains__DA92E43327641AAF",
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
                name: "UQ__educatio__72E12F1BED95894B",
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
                name: "UQ__employme__72E12F1BF5BAE67B",
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
                name: "entity_type_commands_entity_type_id_key_0d06ffe6_uniq",
                schema: "general",
                table: "entity_type_commands",
                columns: new[] { "entity_type_id", "key" },
                unique: true,
                filter: "([entity_type_id] IS NOT NULL AND [key] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "entity_type_constraints_entity_type_id_a7b4c730",
                schema: "general",
                table: "entity_type_constraints",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "entity_type_constraints_entity_type_id_field_name_b4ec14c6_uniq",
                schema: "general",
                table: "entity_type_constraints",
                columns: new[] { "entity_type_id", "field_name" },
                unique: true,
                filter: "([entity_type_id] IS NOT NULL AND [field_name] IS NOT NULL)");

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
                name: "entity_types_module_id_key_8a42bbcf_uniq",
                schema: "general",
                table: "entity_types",
                columns: new[] { "module_id", "key" },
                unique: true,
                filter: "([module_id] IS NOT NULL AND [key] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "entity_types_module_id_name_en_2858063b_uniq",
                schema: "general",
                table: "entity_types",
                columns: new[] { "module_id", "name_en" },
                unique: true,
                filter: "([module_id] IS NOT NULL AND [name_en] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "entity_types_module_id_name_fa_060eefd9_uniq",
                schema: "general",
                table: "entity_types",
                columns: new[] { "module_id", "name_fa" },
                unique: true,
                filter: "([module_id] IS NOT NULL AND [name_fa] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "error_logs_user_id_ac744206",
                schema: "general",
                table: "error_logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "foreign_languages_creator_id_f2c0414f",
                schema: "general",
                table: "foreign_languages",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__foreign___72E12F1BBAF9E6BE",
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
                name: "UQ__housing___72E12F1B5272DF3E",
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
                name: "UQ__job_posi__72E12F1B0D466C43",
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
                name: "UQ__job_rank__72E12F1B165A18B3",
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
                name: "UQ__jobs__72E12F1BEA327090",
                schema: "general",
                table: "jobs",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__language__357D4CF92530AEF6",
                schema: "general",
                table: "languages",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__language__72E12F1B290D8129",
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
                name: "logs_period_id_ea07e698",
                schema: "general",
                table: "logs",
                column: "period_id");

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
                name: "UQ__measurem__72E12F1BC848976E",
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
                name: "menu_items_module_id_name_en_parent_id_01a9ca6d_uniq",
                schema: "general",
                table: "menu_items",
                columns: new[] { "module_id", "name_en", "parent_id" },
                unique: true,
                filter: "([module_id] IS NOT NULL AND [name_en] IS NOT NULL AND [parent_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "menu_items_module_id_name_fa_parent_id_3c2380ac_uniq",
                schema: "general",
                table: "menu_items",
                columns: new[] { "module_id", "name_fa", "parent_id" },
                unique: true,
                filter: "([module_id] IS NOT NULL AND [name_fa] IS NOT NULL AND [parent_id] IS NOT NULL)");

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
                name: "UQ__military__72E12F1B10BA3724",
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
                name: "UQ__mission___72E12F1B61A08770",
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
                name: "module_person_groups_module_id_person_group_id_0362b292_uniq",
                schema: "general",
                table: "module_person_groups",
                columns: new[] { "module_id", "person_group_id" },
                unique: true,
                filter: "([module_id] IS NOT NULL AND [person_group_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "module_person_groups_person_group_id_1b684782",
                schema: "general",
                table: "module_person_groups",
                column: "person_group_id");

            migrationBuilder.CreateIndex(
                name: "UQ__modules__2E0A72B054632985",
                schema: "general",
                table: "modules",
                column: "name_en",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__modules__2E0A7AA60B667EF1",
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
                name: "person_addresses_person_id_city_id_address_74e8c5b2_uniq",
                schema: "general",
                table: "person_addresses",
                columns: new[] { "person_id", "city_id", "address" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [city_id] IS NOT NULL AND [address] IS NOT NULL)");

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
                name: "person_bank_accounts_person_id_bank_id_account_number_b2edcc75_uniq",
                schema: "general",
                table: "person_bank_accounts",
                columns: new[] { "person_id", "bank_id", "account_number" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [account_number] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "person_bank_accounts_person_id_bank_id_card_number_721eadc4_uniq",
                schema: "general",
                table: "person_bank_accounts",
                columns: new[] { "person_id", "bank_id", "card_number" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [card_number] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "person_bank_accounts_person_id_bank_id_iban_011ebeeb_uniq",
                schema: "general",
                table: "person_bank_accounts",
                columns: new[] { "person_id", "bank_id", "iban" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [iban] IS NOT NULL)");

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
                name: "person_emails_person_id_email_09d42bf7_uniq",
                schema: "general",
                table: "person_emails",
                columns: new[] { "person_id", "email" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [email] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "person_faxes_person_id_aeabbb62",
                schema: "general",
                table: "person_faxes",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_faxes_person_id_code_fax_2b464685_uniq",
                schema: "general",
                table: "person_faxes",
                columns: new[] { "person_id", "code", "fax" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [code] IS NOT NULL AND [fax] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "person_mobiles_person_id_ab811f2d",
                schema: "general",
                table: "person_mobiles",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_mobiles_person_id_mobile_e26483eb_uniq",
                schema: "general",
                table: "person_mobiles",
                columns: new[] { "person_id", "mobile" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [mobile] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "person_phones_person_id_ad2cd17d",
                schema: "general",
                table: "person_phones",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_phones_person_id_code_phone_29edc351_uniq",
                schema: "general",
                table: "person_phones",
                columns: new[] { "person_id", "code", "phone" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [code] IS NOT NULL AND [phone] IS NOT NULL)");

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
                name: "person_websites_person_id_website_c64285de_uniq",
                schema: "general",
                table: "person_websites",
                columns: new[] { "person_id", "website" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [website] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "persons_birth_city_id_9fea3d98",
                schema: "general",
                table: "persons",
                column: "birth_city_id");

            migrationBuilder.CreateIndex(
                name: "persons_citizen_code_430dc102_uniq",
                schema: "general",
                table: "persons",
                column: "citizen_code",
                unique: true,
                filter: "([citizen_code] IS NOT NULL)");

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
                name: "persons_legal_register_no_22c0875c_uniq",
                schema: "general",
                table: "persons",
                column: "legal_register_no",
                unique: true,
                filter: "([legal_register_no] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "persons_military_service_status_id_31e57348",
                schema: "general",
                table: "persons",
                column: "military_service_status_id");

            migrationBuilder.CreateIndex(
                name: "persons_natural_national_code_49221b45",
                schema: "general",
                table: "persons",
                column: "natural_national_code");

            migrationBuilder.CreateIndex(
                name: "persons_passport_number_dff3f3fd_uniq",
                schema: "general",
                table: "persons",
                column: "passport_number",
                unique: true,
                filter: "([passport_number] IS NOT NULL)");

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
                name: "UQ__persons__357D4CF9A4AE2BBE",
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
                name: "provinces_country_id_code_8b417d3f_uniq",
                schema: "general",
                table: "provinces",
                columns: new[] { "country_id", "code" },
                unique: true,
                filter: "([country_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "provinces_country_id_name_9930a650_uniq",
                schema: "general",
                table: "provinces",
                columns: new[] { "country_id", "name" },
                unique: true,
                filter: "([country_id] IS NOT NULL AND [name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "religions_creator_id_65bb4aaf",
                schema: "general",
                table: "religions",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "UQ__religion__72E12F1B76222047",
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
                name: "UQ__users__543848DEDC3064CE",
                schema: "general",
                table: "users",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__users__F3DBC572C97AEEDB",
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
                name: "users_username_encrypted_508d3951_uniq",
                schema: "general",
                table: "users",
                column: "username_encrypted",
                unique: true,
                filter: "([username_encrypted] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__work_dep__72E12F1B8BC40DD8",
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
                name: "UQ__work_ope__737584F614BC657A",
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
                name: "UQ__workplac__72E12F1B6E1EA09C",
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
                name: "areas_creator_id_9f75521a_fk_users_id",
                schema: "general",
                table: "areas",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "company_admins_admin_id_7fe4b0ca_fk_users_id",
                schema: "general",
                table: "company_admins",
                column: "admin_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "company_admins_creator_id_af4a192b_fk_users_id",
                schema: "general",
                table: "company_admins",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "educational_degrees_creator_id_693f4fac_fk_users_id",
                schema: "general",
                table: "educational_degrees",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "employment_contract_Descriptions_creator_id_2fcb5dd9_fk_users_id",
                schema: "general",
                table: "employment_contract_Descriptions",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "employment_contract_titles_creator_id_f0cd95e9_fk_users_id",
                schema: "general",
                table: "employment_contract_titles",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "error_logs_user_id_ac744206_fk_users_id",
                schema: "general",
                table: "error_logs",
                column: "user_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "foreign_languages_creator_id_f2c0414f_fk_users_id",
                schema: "general",
                table: "foreign_languages",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "housing_statuses_creator_id_b7300b34_fk_users_id",
                schema: "general",
                table: "housing_statuses",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "job_positions_creator_id_361c185c_fk_users_id",
                schema: "general",
                table: "job_positions",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "job_ranks_creator_id_06b134a2_fk_users_id",
                schema: "general",
                table: "job_ranks",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "jobs_creator_id_56b9cd05_fk_users_id",
                schema: "general",
                table: "jobs",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "logs_user_id_237f5f83_fk_users_id",
                schema: "general",
                table: "logs",
                column: "user_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "measurement_units_creator_id_adca9822_fk_users_id",
                schema: "general",
                table: "measurement_units",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "military_service_statuses_creator_id_2264fea4_fk_users_id",
                schema: "general",
                table: "military_service_statuses",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "mission_types_creator_id_17915c73_fk_users_id",
                schema: "general",
                table: "mission_types",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "notifications_user2_id_8fe256fe_fk_users_id",
                schema: "general",
                table: "notifications",
                column: "user2_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "notifications_user_id_468e288d_fk_users_id",
                schema: "general",
                table: "notifications",
                column: "user_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_addresses_person_id_d67d7fc6_fk_persons_id",
                schema: "general",
                table: "person_addresses",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_bank_accounts_person_id_cea19acc_fk_persons_id",
                schema: "general",
                table: "person_bank_accounts",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_educational_degrees_creator_id_29916c83_fk_users_id",
                schema: "general",
                table: "person_educational_degrees",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_educational_degrees_person_id_c8759020_fk_persons_id",
                schema: "general",
                table: "person_educational_degrees",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_emails_person_id_7ab1a818_fk_persons_id",
                schema: "general",
                table: "person_emails",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_faxes_person_id_aeabbb62_fk_persons_id",
                schema: "general",
                table: "person_faxes",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_mobiles_person_id_ab811f2d_fk_persons_id",
                schema: "general",
                table: "person_mobiles",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_phones_person_id_ad2cd17d_fk_persons_id",
                schema: "general",
                table: "person_phones",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_relatives_creator_id_2e5156c1_fk_users_id",
                schema: "general",
                table: "person_relatives",
                column: "creator_id",
                principalSchema: "general",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_relatives_person_id_0ab58207_fk_persons_id",
                schema: "general",
                table: "person_relatives",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "person_websites_person_id_2bf2e8a6_fk_persons_id",
                schema: "general",
                table: "person_websites",
                column: "person_id",
                principalSchema: "general",
                principalTable: "persons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "persons_religion_id_bf81e08d_fk_religions_id",
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
                name: "housing_statuses_creator_id_b7300b34_fk_users_id",
                schema: "general",
                table: "housing_statuses");

            migrationBuilder.DropForeignKey(
                name: "military_service_statuses_creator_id_2264fea4_fk_users_id",
                schema: "general",
                table: "military_service_statuses");

            migrationBuilder.DropForeignKey(
                name: "religions_creator_id_65bb4aaf_fk_users_id",
                schema: "general",
                table: "religions");

            migrationBuilder.DropTable(
                name: "areas",
                schema: "general");

            migrationBuilder.DropTable(
                name: "bank_account_types",
                schema: "general");

            migrationBuilder.DropTable(
                name: "bank_operation_types",
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
