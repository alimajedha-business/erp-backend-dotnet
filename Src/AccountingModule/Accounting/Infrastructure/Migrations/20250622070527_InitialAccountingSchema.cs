using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialAccountingSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounting");

            migrationBuilder.CreateTable(
                name: "account_categories",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account___3213E83F523151EB", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bank_templates",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    template = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bank_tem__3213E83F444D69A3", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "float_account_types",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_static = table.Column<bool>(type: "bit", nullable: false),
                    name_fa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    voucher_item_float_level = table.Column<short>(type: "smallint", nullable: true),
                    extra_1_title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    extra_1_title2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    extra_1_type = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    extra_1_required = table.Column<bool>(type: "bit", nullable: true),
                    extra_2_title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    extra_2_title2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    extra_2_type = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    extra_2_required = table.Column<bool>(type: "bit", nullable: true),
                    extra_3_title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    extra_3_title2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    extra_3_type = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    extra_3_required = table.Column<bool>(type: "bit", nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__float_ac__3213E83F2EA4CB1D", x => x.id);
                    table.ForeignKey(
                        name: "float_account_types_parent_id_4dd8ba99_fk_float_account_types_id",
                        column: x => x.parent_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ledgers",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_leading = table.Column<bool>(type: "bit", nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ledgers__3213E83F7B5B3C2B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "periods",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__periods__3213E83FFA2811B1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "resource_and_expenditures",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__resource__3213E83F849557AA", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    s1 = table.Column<bool>(type: "bit", nullable: false),
                    domain_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__settings__3213E83F975C56ED", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ttms_product_types",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_pro__3213E83FDC489187", x => x.id);
                    table.UniqueConstraint("AK_ttms_product_types_code", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "voucher_item_standard_descriptions",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__voucher___3213E83F47C24B20", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voucher_types",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_static = table.Column<bool>(type: "bit", nullable: false),
                    name_fa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__voucher___3213E83F3A65AF6E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account_groups",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<short>(type: "smallint", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account___3213E83F4DE2EE95", x => x.id);
                    table.ForeignKey(
                        name: "account_groups_category_id_af0c53e8_fk_account_categories_id",
                        column: x => x.category_id,
                        principalSchema: "accounting",
                        principalTable: "account_categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "manual_float_accounts",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    title2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    float_account_type_id = table.Column<int>(type: "int", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__manual_f__3213E83F79B8ADA2", x => x.id);
                    table.ForeignKey(
                        name: "manual_float_accounts_float_account_type_id_b211d188_fk_float_account_types_id",
                        column: x => x.float_account_type_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "manual_float_accounts_parent_id_4b9bca91_fk_manual_float_accounts_id",
                        column: x => x.parent_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "account_sets",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account___3213E83FB563F35A", x => x.id);
                    table.ForeignKey(
                        name: "account_sets_ledger_id_31846310_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "master_accounts",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    title2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__master_a__3213E83FDB1E8845", x => x.id);
                    table.ForeignKey(
                        name: "master_accounts_ledger_id_b94026a2_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "data_imports",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    related_file = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    error_file = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    file_type = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    status = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    entity_type_command_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__data_imp__3213E83F260CBFC5", x => x.id);
                    table.ForeignKey(
                        name: "data_imports_ledger_id_fd1c5e0d_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "data_imports_period_id_aef725fd_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ledger_periods",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ledger_p__3213E83F6DE635AD", x => x.id);
                    table.ForeignKey(
                        name: "ledger_periods_ledger_id_c70ff025_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ledger_periods_period_id_2953e5ba_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "closing_pattern_temporary_accounts",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    float_type_set = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    is_set = table.Column<bool>(type: "bit", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    default_company_unit_id = table.Column<int>(type: "int", nullable: true),
                    default_cost_center_id = table.Column<int>(type: "int", nullable: true),
                    default_currency_id = table.Column<int>(type: "int", nullable: true),
                    default_person_id = table.Column<int>(type: "int", nullable: true),
                    default_project_id = table.Column<int>(type: "int", nullable: true),
                    default_resource_and_expenditure_id = table.Column<int>(type: "int", nullable: true),
                    default_store_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    default_cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    default_petty_cashier_period_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__closing___3213E83F56719B0B", x => x.id);
                    table.ForeignKey(
                        name: "closing_pattern_temporary_accounts_default_resource_and_expenditure_id_dcf3d48a_fk_resource_and_expenditures_id",
                        column: x => x.default_resource_and_expenditure_id,
                        principalSchema: "accounting",
                        principalTable: "resource_and_expenditures",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "closing_pattern_temporary_accounts_ledger_id_8d2baceb_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "closing_pattern_temporary_accounts_period_id_897435aa_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "closing_patterns",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    float_type_set = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    is_set = table.Column<bool>(type: "bit", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    default_company_unit_id = table.Column<int>(type: "int", nullable: true),
                    default_currency_id = table.Column<int>(type: "int", nullable: true),
                    default_person_id = table.Column<int>(type: "int", nullable: true),
                    default_project_id = table.Column<int>(type: "int", nullable: true),
                    default_resource_and_expenditure_id = table.Column<int>(type: "int", nullable: true),
                    default_store_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    default_cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    default_petty_cashier_period_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__closing___3213E83FF2309BF5", x => x.id);
                    table.ForeignKey(
                        name: "closing_patterns_default_resource_and_expenditure_id_6ef35f22_fk_resource_and_expenditures_id",
                        column: x => x.default_resource_and_expenditure_id,
                        principalSchema: "accounting",
                        principalTable: "resource_and_expenditures",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "closing_patterns_ledger_id_394ee754_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "closing_patterns_period_id_8a00d0d0_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "trash_vouchers",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_id_initial = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    serial = table.Column<long>(type: "bigint", nullable: false),
                    number = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    last_modifier_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__trash_vo__3213E83F2F737595", x => x.id);
                    table.ForeignKey(
                        name: "trash_vouchers_ledger_id_e6d73246_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_vouchers_period_id_f1b414ea_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_vouchers_type_id_62c0b2ab_fk_voucher_types_id",
                        column: x => x.type_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vouchers",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_initial = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    serial = table.Column<long>(type: "bigint", nullable: false),
                    number = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    last_modifier_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__vouchers__3213E83F37737C08", x => x.id);
                    table.ForeignKey(
                        name: "vouchers_ledger_id_25e3f63d_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "vouchers_period_id_800ded6b_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "vouchers_type_id_05bcc3a6_fk_voucher_types_id",
                        column: x => x.type_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "slave_accounts",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    nature = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    detailed_account_1 = table.Column<int>(type: "int", nullable: true),
                    detailed_account_2 = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    title2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    from_company_id_id = table.Column<int>(type: "int", nullable: true),
                    group_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    master_id = table.Column<int>(type: "int", nullable: false),
                    closing_type = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    last_level = table.Column<bool>(type: "bit", nullable: false),
                    level = table.Column<short>(type: "smallint", nullable: false),
                    slave_code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__slave_ac__3213E83FA165931F", x => x.id);
                    table.ForeignKey(
                        name: "slave_accounts_category_id_79276cde_fk_account_categories_id",
                        column: x => x.category_id,
                        principalSchema: "accounting",
                        principalTable: "account_categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_accounts_group_id_d226c356_fk_account_groups_id",
                        column: x => x.group_id,
                        principalSchema: "accounting",
                        principalTable: "account_groups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_accounts_ledger_id_e9bba48c_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_accounts_master_id_d8364647_fk_master_accounts_id",
                        column: x => x.master_id,
                        principalSchema: "accounting",
                        principalTable: "master_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_accounts_parent_id_b5e3fe05_fk_slave_accounts_id",
                        column: x => x.parent_id,
                        principalSchema: "accounting",
                        principalTable: "slave_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "contradictions",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bank_account_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    data_import_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__contradi__3213E83FC9BAF683", x => x.id);
                    table.ForeignKey(
                        name: "contradictions_data_import_id_65a4748b_fk_data_imports_id",
                        column: x => x.data_import_id,
                        principalSchema: "accounting",
                        principalTable: "data_imports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "contradictions_ledger_id_85776d2a_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "contradictions_period_id_538ee815_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ledger_period_companies",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    ledger_period_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ledger_p__3213E83F76D7AAD9", x => x.id);
                    table.ForeignKey(
                        name: "ledger_period_companies_ledger_id_52a61c2a_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ledger_period_companies_ledger_period_id_5656d93c_fk_ledger_periods_id",
                        column: x => x.ledger_period_id,
                        principalSchema: "accounting",
                        principalTable: "ledger_periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ledger_period_companies_period_id_0828d8ca_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "voucher_logs",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_id_initial = table.Column<int>(type: "int", nullable: true),
                    typ = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    serial = table.Column<long>(type: "bigint", nullable: true),
                    number = table.Column<long>(type: "bigint", nullable: true),
                    old_number = table.Column<long>(type: "bigint", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    old_date = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    old_status = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    old_description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    branch_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    modifier_id = table.Column<int>(type: "int", nullable: false),
                    old_branch_id = table.Column<int>(type: "int", nullable: true),
                    old_voucher_type_id = table.Column<int>(type: "int", nullable: true),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    voucher_type_id = table.Column<int>(type: "int", nullable: true),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__voucher___3213E83FCD79D7C6", x => x.id);
                    table.ForeignKey(
                        name: "voucher_logs_ledger_id_b35b1cc4_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_logs_old_voucher_type_id_e9551d26_fk_voucher_types_id",
                        column: x => x.old_voucher_type_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_logs_period_id_02857e11_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_logs_voucher_id_34970cb1_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_logs_voucher_type_id_5ba5b90d_fk_voucher_types_id",
                        column: x => x.voucher_type_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "account_set_items",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_set_id = table.Column<int>(type: "int", nullable: false),
                    master_id = table.Column<int>(type: "int", nullable: true),
                    slave_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__account___3213E83F57D6E072", x => x.id);
                    table.ForeignKey(
                        name: "account_set_items_account_set_id_c6a49338_fk_account_sets_id",
                        column: x => x.account_set_id,
                        principalSchema: "accounting",
                        principalTable: "account_sets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "account_set_items_master_id_e12671e4_fk_master_accounts_id",
                        column: x => x.master_id,
                        principalSchema: "accounting",
                        principalTable: "master_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "account_set_items_slave_id_a5287d0f_fk_slave_accounts_id",
                        column: x => x.slave_id,
                        principalSchema: "accounting",
                        principalTable: "slave_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "balance_related_account_details",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deal_type = table.Column<short>(type: "smallint", nullable: true),
                    cost_center_code = table.Column<int>(type: "int", nullable: true),
                    company_unit_code = table.Column<int>(type: "int", nullable: true),
                    project_date = table.Column<DateOnly>(type: "date", nullable: true),
                    project_status_report = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    project_receipt = table.Column<long>(type: "bigint", nullable: true),
                    store_doc_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    store_good_code = table.Column<int>(type: "int", nullable: true),
                    store_good_unit_price = table.Column<decimal>(type: "numeric(20,6)", nullable: true),
                    store_good_quantity = table.Column<decimal>(type: "numeric(16,4)", nullable: true),
                    currency_amount = table.Column<decimal>(type: "numeric(24,8)", nullable: true),
                    currency_exchange_rate = table.Column<decimal>(type: "numeric(20,4)", nullable: true),
                    bank_operation_receipt = table.Column<long>(type: "bigint", nullable: true),
                    bank_operation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    bank_operation_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    receivable_doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    receivable_doc_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    payable_doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    payable_doc_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    manual_float_1_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_1_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_1_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    debit = table.Column<long>(type: "bigint", nullable: true),
                    credit = table.Column<long>(type: "bigint", nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_tick = table.Column<bool>(type: "bit", nullable: false),
                    bank_branch_for_payable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_branch_for_receivable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_for_payable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_for_receivable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_operation_bank_branch_id = table.Column<int>(type: "int", nullable: true),
                    bank_operation_type_id = table.Column<int>(type: "int", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    company_unit_id = table.Column<int>(type: "int", nullable: true),
                    cost_center_id = table.Column<int>(type: "int", nullable: true),
                    currency_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    manual_float_1_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_2_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_3_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_4_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_5_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_6_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_7_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_8_id = table.Column<int>(type: "int", nullable: true),
                    master_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    project_contract_id = table.Column<int>(type: "int", nullable: true),
                    project_contract_type_id = table.Column<int>(type: "int", nullable: true),
                    resource_and_expenditure_id = table.Column<int>(type: "int", nullable: true),
                    slave_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__balance___3213E83F58334CF7", x => x.id);
                    table.ForeignKey(
                        name: "balance_related_account_details_ledger_id_1fcf3bc8_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_manual_float_1_id_3cf4e319_fk_manual_float_accounts_id",
                        column: x => x.manual_float_1_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_manual_float_2_id_79cca910_fk_manual_float_accounts_id",
                        column: x => x.manual_float_2_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_manual_float_3_id_c7afe8b7_fk_manual_float_accounts_id",
                        column: x => x.manual_float_3_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_manual_float_4_id_bba5925c_fk_manual_float_accounts_id",
                        column: x => x.manual_float_4_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_manual_float_5_id_2f0ad7f7_fk_manual_float_accounts_id",
                        column: x => x.manual_float_5_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_manual_float_6_id_eeaf3bf1_fk_manual_float_accounts_id",
                        column: x => x.manual_float_6_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_manual_float_7_id_c140028e_fk_manual_float_accounts_id",
                        column: x => x.manual_float_7_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_manual_float_8_id_68b0bdc0_fk_manual_float_accounts_id",
                        column: x => x.manual_float_8_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_master_id_f76438d4_fk_master_accounts_id",
                        column: x => x.master_id,
                        principalSchema: "accounting",
                        principalTable: "master_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_period_id_2b2d6794_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_resource_and_expenditure_id_78f2b804_fk_resource_and_expenditures_id",
                        column: x => x.resource_and_expenditure_id,
                        principalSchema: "accounting",
                        principalTable: "resource_and_expenditures",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "balance_related_account_details_slave_id_8bf18f14_fk_slave_accounts_id",
                        column: x => x.slave_id,
                        principalSchema: "accounting",
                        principalTable: "slave_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "slave_account_companies",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    need_ttms = table.Column<bool>(type: "bit", nullable: false),
                    deal_type = table.Column<short>(type: "smallint", nullable: true),
                    has_absolute_deal_type = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    budget = table.Column<double>(type: "float", nullable: true),
                    float_types = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    float_type_set = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    due_date = table.Column<bool>(type: "bit", nullable: false),
                    tag1 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    tag2 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    float_type_1_id = table.Column<int>(type: "int", nullable: true),
                    float_type_2_id = table.Column<int>(type: "int", nullable: true),
                    float_type_3_id = table.Column<int>(type: "int", nullable: true),
                    float_type_4_id = table.Column<int>(type: "int", nullable: true),
                    float_type_5_id = table.Column<int>(type: "int", nullable: true),
                    float_type_6_id = table.Column<int>(type: "int", nullable: true),
                    float_type_7_id = table.Column<int>(type: "int", nullable: true),
                    float_type_8_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    master_id = table.Column<int>(type: "int", nullable: false),
                    slave_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__slave_ac__3213E83FA1D7EC62", x => x.id);
                    table.ForeignKey(
                        name: "slave_account_companies_float_type_1_id_dcee560d_fk_float_account_types_id",
                        column: x => x.float_type_1_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_float_type_2_id_c218860a_fk_float_account_types_id",
                        column: x => x.float_type_2_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_float_type_3_id_653411c6_fk_float_account_types_id",
                        column: x => x.float_type_3_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_float_type_4_id_57254460_fk_float_account_types_id",
                        column: x => x.float_type_4_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_float_type_5_id_896ccdfc_fk_float_account_types_id",
                        column: x => x.float_type_5_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_float_type_6_id_a486a7f8_fk_float_account_types_id",
                        column: x => x.float_type_6_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_float_type_7_id_f5d8293e_fk_float_account_types_id",
                        column: x => x.float_type_7_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_float_type_8_id_ab2b63c8_fk_float_account_types_id",
                        column: x => x.float_type_8_id,
                        principalSchema: "accounting",
                        principalTable: "float_account_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_ledger_id_b40ad4d0_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_master_id_bacd3012_fk_master_accounts_id",
                        column: x => x.master_id,
                        principalSchema: "accounting",
                        principalTable: "master_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "slave_account_companies_slave_id_edd3dcf5_fk_slave_accounts_id",
                        column: x => x.slave_id,
                        principalSchema: "accounting",
                        principalTable: "slave_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "slave_account_standard_descriptions",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    slave_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__slave_ac__3213E83F8F08DB1E", x => x.id);
                    table.ForeignKey(
                        name: "slave_account_standard_descriptions_slave_id_b64d916e_fk_slave_accounts_id",
                        column: x => x.slave_id,
                        principalSchema: "accounting",
                        principalTable: "slave_accounts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "contradiction_items",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    doc_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    debit = table.Column<long>(type: "bigint", nullable: true),
                    credit = table.Column<long>(type: "bigint", nullable: true),
                    is_tick = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    contradiction_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__contradi__3213E83F04D07847", x => x.id);
                    table.ForeignKey(
                        name: "contradiction_items_contradiction_id_1494f375_fk_contradictions_id",
                        column: x => x.contradiction_id,
                        principalSchema: "accounting",
                        principalTable: "contradictions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ledger_period_company_settings",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    is_closed = table.Column<bool>(type: "bit", nullable: false),
                    s1 = table.Column<bool>(type: "bit", nullable: false),
                    s2 = table.Column<bool>(type: "bit", nullable: false),
                    s3 = table.Column<bool>(type: "bit", nullable: false),
                    s4 = table.Column<bool>(type: "bit", nullable: false),
                    s5 = table.Column<bool>(type: "bit", nullable: false),
                    s6 = table.Column<bool>(type: "bit", nullable: false),
                    s7 = table.Column<bool>(type: "bit", nullable: false),
                    s8 = table.Column<bool>(type: "bit", nullable: false),
                    s9 = table.Column<bool>(type: "bit", nullable: false),
                    s10 = table.Column<bool>(type: "bit", nullable: false),
                    s11 = table.Column<bool>(type: "bit", nullable: false),
                    s12 = table.Column<bool>(type: "bit", nullable: false),
                    s13 = table.Column<bool>(type: "bit", nullable: false),
                    s14 = table.Column<short>(type: "smallint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    ledger_period_company_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ledger_p__3213E83FA6E82736", x => x.id);
                    table.ForeignKey(
                        name: "ledger_period_company_settings_ledger_id_c37251eb_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ledger_period_company_settings_ledger_period_company_id_880b5330_fk_ledger_period_companies_id",
                        column: x => x.ledger_period_company_id,
                        principalSchema: "accounting",
                        principalTable: "ledger_period_companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ledger_period_company_settings_period_id_2389d1c0_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "closing_pattern_slave_companies",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    default_company_unit_id = table.Column<int>(type: "int", nullable: true),
                    default_currency_id = table.Column<int>(type: "int", nullable: true),
                    default_person_id = table.Column<int>(type: "int", nullable: true),
                    default_project_id = table.Column<int>(type: "int", nullable: true),
                    default_resource_and_expenditure_id = table.Column<int>(type: "int", nullable: true),
                    default_store_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    slave_company_id = table.Column<int>(type: "int", nullable: false),
                    default_cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    default_petty_cashier_period_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__closing___3213E83F69E4AA3B", x => x.id);
                    table.ForeignKey(
                        name: "closing_pattern_slave_companies_default_resource_and_expenditure_id_d8808ca1_fk_resource_and_expenditures_id",
                        column: x => x.default_resource_and_expenditure_id,
                        principalSchema: "accounting",
                        principalTable: "resource_and_expenditures",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "closing_pattern_slave_companies_ledger_id_b7344906_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "closing_pattern_slave_companies_period_id_7d4ec4d3_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "closing_pattern_slave_companies_slave_company_id_e971e36e_fk_slave_account_companies_id",
                        column: x => x.slave_company_id,
                        principalSchema: "accounting",
                        principalTable: "slave_account_companies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "trash_voucher_items",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_item_id_initial = table.Column<long>(type: "bigint", nullable: true),
                    id_initial = table.Column<long>(type: "bigint", nullable: true),
                    master_code = table.Column<int>(type: "int", nullable: false),
                    slave_code = table.Column<int>(type: "int", nullable: false),
                    sequence = table.Column<int>(type: "int", nullable: false),
                    deal_type = table.Column<short>(type: "smallint", nullable: true),
                    cost_center_code = table.Column<int>(type: "int", nullable: true),
                    company_unit_code = table.Column<int>(type: "int", nullable: true),
                    project_date = table.Column<DateOnly>(type: "date", nullable: true),
                    project_status_report = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    project_receipt = table.Column<long>(type: "bigint", nullable: true),
                    store_doc_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    store_good_code = table.Column<int>(type: "int", nullable: true),
                    store_good_unit_price = table.Column<decimal>(type: "numeric(20,6)", nullable: true),
                    store_good_quantity = table.Column<decimal>(type: "numeric(16,4)", nullable: true),
                    currency_amount = table.Column<decimal>(type: "numeric(24,8)", nullable: true),
                    currency_exchange_rate = table.Column<decimal>(type: "numeric(20,4)", nullable: true),
                    bank_operation_receipt = table.Column<long>(type: "bigint", nullable: true),
                    bank_operation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    bank_operation_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    receivable_doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    receivable_doc_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    payable_doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    payable_doc_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    manual_float_1_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_1_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_1_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    debit = table.Column<long>(type: "bigint", nullable: true),
                    credit = table.Column<long>(type: "bigint", nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    printed = table.Column<bool>(type: "bit", nullable: false),
                    have_attach = table.Column<bool>(type: "bit", nullable: false),
                    attaches = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_tick = table.Column<bool>(type: "bit", nullable: false),
                    bank_branch_for_payable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_branch_for_receivable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_for_payable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_for_receivable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_operation_bank_account_id = table.Column<int>(type: "int", nullable: true),
                    bank_operation_type_id = table.Column<int>(type: "int", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: false),
                    cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    company_unit_id = table.Column<int>(type: "int", nullable: true),
                    cost_center_id = table.Column<int>(type: "int", nullable: true),
                    currency_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    manual_float_1_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_2_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_3_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_4_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_5_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_6_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_7_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_8_id = table.Column<int>(type: "int", nullable: true),
                    master_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: true),
                    petty_cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    project_contract_id = table.Column<int>(type: "int", nullable: true),
                    project_contract_type_id = table.Column<int>(type: "int", nullable: true),
                    resource_and_expenditure_id = table.Column<int>(type: "int", nullable: true),
                    slave_id = table.Column<int>(type: "int", nullable: false),
                    slave_company_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: true),
                    trash_voucher_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__trash_vo__3213E83F3922E482", x => x.id);
                    table.ForeignKey(
                        name: "trash_voucher_items_ledger_id_58204329_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_manual_float_1_id_0cdae92d_fk_manual_float_accounts_id",
                        column: x => x.manual_float_1_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_manual_float_2_id_985905b9_fk_manual_float_accounts_id",
                        column: x => x.manual_float_2_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_manual_float_3_id_11645efa_fk_manual_float_accounts_id",
                        column: x => x.manual_float_3_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_manual_float_4_id_60ae5d18_fk_manual_float_accounts_id",
                        column: x => x.manual_float_4_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_manual_float_5_id_3eb06b68_fk_manual_float_accounts_id",
                        column: x => x.manual_float_5_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_manual_float_6_id_a4afd007_fk_manual_float_accounts_id",
                        column: x => x.manual_float_6_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_manual_float_7_id_85ed9abc_fk_manual_float_accounts_id",
                        column: x => x.manual_float_7_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_manual_float_8_id_d2f59901_fk_manual_float_accounts_id",
                        column: x => x.manual_float_8_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_master_id_0c48bb81_fk_master_accounts_id",
                        column: x => x.master_id,
                        principalSchema: "accounting",
                        principalTable: "master_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_period_id_e1c57ee1_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_resource_and_expenditure_id_fb349f90_fk_resource_and_expenditures_id",
                        column: x => x.resource_and_expenditure_id,
                        principalSchema: "accounting",
                        principalTable: "resource_and_expenditures",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_slave_company_id_976d2e14_fk_slave_account_companies_id",
                        column: x => x.slave_company_id,
                        principalSchema: "accounting",
                        principalTable: "slave_account_companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_slave_id_8ef343db_fk_slave_accounts_id",
                        column: x => x.slave_id,
                        principalSchema: "accounting",
                        principalTable: "slave_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_items_trash_voucher_id_4c9d9a23_fk_trash_vouchers_id",
                        column: x => x.trash_voucher_id,
                        principalSchema: "accounting",
                        principalTable: "trash_vouchers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "voucher_item_logs",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_id_initial = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id_initial = table.Column<long>(type: "bigint", nullable: false),
                    action = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    master_code = table.Column<int>(type: "int", nullable: false),
                    slave_code = table.Column<int>(type: "int", nullable: false),
                    sequence = table.Column<int>(type: "int", nullable: false),
                    old_sequence = table.Column<int>(type: "int", nullable: true),
                    deal_type = table.Column<short>(type: "smallint", nullable: true),
                    cost_center_code = table.Column<int>(type: "int", nullable: true),
                    company_unit_code = table.Column<int>(type: "int", nullable: true),
                    project_date = table.Column<DateOnly>(type: "date", nullable: true),
                    project_status_report = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    project_receipt = table.Column<long>(type: "bigint", nullable: true),
                    store_doc_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    store_good_code = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    store_good_unit_price = table.Column<decimal>(type: "numeric(20,6)", nullable: true),
                    store_good_quantity = table.Column<decimal>(type: "numeric(16,4)", nullable: true),
                    currency_amount = table.Column<decimal>(type: "numeric(24,8)", nullable: true),
                    currency_exchange_rate = table.Column<decimal>(type: "numeric(20,4)", nullable: true),
                    bank_operation_receipt = table.Column<long>(type: "bigint", nullable: true),
                    bank_operation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    bank_operation_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    receivable_doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    receivable_doc_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    payable_doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    payable_doc_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    manual_float_1_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_1_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_1_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    debit = table.Column<long>(type: "bigint", nullable: true),
                    credit = table.Column<long>(type: "bigint", nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    have_attach = table.Column<bool>(type: "bit", nullable: false),
                    attaches = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_tick = table.Column<bool>(type: "bit", nullable: false),
                    is_modify = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bank_branch_for_payable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_branch_for_receivable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_for_payable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_for_receivable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_operation_bank_account_id = table.Column<int>(type: "int", nullable: true),
                    bank_operation_type_id = table.Column<int>(type: "int", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: false),
                    cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    company_unit_id = table.Column<int>(type: "int", nullable: true),
                    cost_center_id = table.Column<int>(type: "int", nullable: true),
                    currency_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    manual_float_1_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_2_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_3_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_4_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_5_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_6_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_7_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_8_id = table.Column<int>(type: "int", nullable: true),
                    master_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: true),
                    petty_cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    project_contract_id = table.Column<int>(type: "int", nullable: true),
                    project_contract_type_id = table.Column<int>(type: "int", nullable: true),
                    resource_and_expenditure_id = table.Column<int>(type: "int", nullable: true),
                    slave_id = table.Column<int>(type: "int", nullable: false),
                    slave_company_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: true),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    voucher_log_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__voucher___3213E83FF23C9CEC", x => x.id);
                    table.ForeignKey(
                        name: "voucher_item_logs_ledger_id_8b5e1cd0_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_manual_float_1_id_58b6802b_fk_manual_float_accounts_id",
                        column: x => x.manual_float_1_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_manual_float_2_id_38f71036_fk_manual_float_accounts_id",
                        column: x => x.manual_float_2_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_manual_float_3_id_aab41fd4_fk_manual_float_accounts_id",
                        column: x => x.manual_float_3_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_manual_float_4_id_f5608169_fk_manual_float_accounts_id",
                        column: x => x.manual_float_4_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_manual_float_5_id_d6f8534e_fk_manual_float_accounts_id",
                        column: x => x.manual_float_5_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_manual_float_6_id_356cdede_fk_manual_float_accounts_id",
                        column: x => x.manual_float_6_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_manual_float_7_id_c6d1e3a4_fk_manual_float_accounts_id",
                        column: x => x.manual_float_7_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_manual_float_8_id_97e9e83b_fk_manual_float_accounts_id",
                        column: x => x.manual_float_8_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_master_id_6b97e99d_fk_master_accounts_id",
                        column: x => x.master_id,
                        principalSchema: "accounting",
                        principalTable: "master_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_period_id_41dea737_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_resource_and_expenditure_id_6baa8522_fk_resource_and_expenditures_id",
                        column: x => x.resource_and_expenditure_id,
                        principalSchema: "accounting",
                        principalTable: "resource_and_expenditures",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_slave_company_id_d30d38dd_fk_slave_account_companies_id",
                        column: x => x.slave_company_id,
                        principalSchema: "accounting",
                        principalTable: "slave_account_companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_slave_id_bcc29d21_fk_slave_accounts_id",
                        column: x => x.slave_id,
                        principalSchema: "accounting",
                        principalTable: "slave_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_voucher_id_37e41a3c_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_logs_voucher_log_id_4528bfd9_fk_voucher_logs_id",
                        column: x => x.voucher_log_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_logs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "voucher_items",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_initial = table.Column<long>(type: "bigint", nullable: true),
                    cross_reference_id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    master_code = table.Column<int>(type: "int", nullable: false),
                    slave_code = table.Column<int>(type: "int", nullable: false),
                    sequence = table.Column<int>(type: "int", nullable: false),
                    deal_type = table.Column<short>(type: "smallint", nullable: true),
                    cost_center_code = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    company_unit_code = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    project_date = table.Column<DateOnly>(type: "date", nullable: true),
                    project_status_report = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    project_receipt = table.Column<long>(type: "bigint", nullable: true),
                    store_doc_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    store_good_code = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    store_good_unit_price = table.Column<decimal>(type: "numeric(20,6)", nullable: true),
                    store_good_quantity = table.Column<decimal>(type: "numeric(16,4)", nullable: true),
                    currency_amount = table.Column<decimal>(type: "numeric(24,8)", nullable: true),
                    currency_exchange_rate = table.Column<decimal>(type: "numeric(20,4)", nullable: true),
                    bank_operation_receipt = table.Column<long>(type: "bigint", nullable: true),
                    bank_operation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    bank_operation_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    receivable_doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    receivable_doc_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    payable_doc_date = table.Column<DateOnly>(type: "date", nullable: true),
                    payable_doc_check_no = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    manual_float_1_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_1_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_1_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_2_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_3_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_4_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_5_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_6_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_7_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    manual_float_8_extra_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    debit = table.Column<long>(type: "bigint", nullable: true),
                    credit = table.Column<long>(type: "bigint", nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    printed = table.Column<bool>(type: "bit", nullable: false),
                    have_attach = table.Column<bool>(type: "bit", nullable: false),
                    attaches = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_tick = table.Column<bool>(type: "bit", nullable: false),
                    bank_branch_for_payable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_branch_for_receivable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_for_payable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_for_receivable_doc_id = table.Column<int>(type: "int", nullable: true),
                    bank_operation_bank_account_id = table.Column<int>(type: "int", nullable: true),
                    bank_operation_type_id = table.Column<int>(type: "int", nullable: true),
                    branch_id = table.Column<int>(type: "int", nullable: false),
                    cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    company_unit_id = table.Column<int>(type: "int", nullable: true),
                    cost_center_id = table.Column<int>(type: "int", nullable: true),
                    currency_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    manual_float_1_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_2_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_3_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_4_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_5_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_6_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_7_id = table.Column<int>(type: "int", nullable: true),
                    manual_float_8_id = table.Column<int>(type: "int", nullable: true),
                    master_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: true),
                    petty_cashier_period_id = table.Column<int>(type: "int", nullable: true),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    project_contract_id = table.Column<int>(type: "int", nullable: true),
                    project_contract_type_id = table.Column<int>(type: "int", nullable: true),
                    resource_and_expenditure_id = table.Column<int>(type: "int", nullable: true),
                    slave_id = table.Column<int>(type: "int", nullable: false),
                    slave_company_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: true),
                    voucher_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__voucher___3213E83F9B13223D", x => x.id);
                    table.ForeignKey(
                        name: "voucher_items_ledger_id_439dcc5d_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_manual_float_1_id_a2c88b6e_fk_manual_float_accounts_id",
                        column: x => x.manual_float_1_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_manual_float_2_id_60bbb623_fk_manual_float_accounts_id",
                        column: x => x.manual_float_2_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_manual_float_3_id_4f9d7506_fk_manual_float_accounts_id",
                        column: x => x.manual_float_3_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_manual_float_4_id_35e1cb30_fk_manual_float_accounts_id",
                        column: x => x.manual_float_4_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_manual_float_5_id_a5393d5c_fk_manual_float_accounts_id",
                        column: x => x.manual_float_5_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_manual_float_6_id_a56fc8dc_fk_manual_float_accounts_id",
                        column: x => x.manual_float_6_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_manual_float_7_id_858fb1f1_fk_manual_float_accounts_id",
                        column: x => x.manual_float_7_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_manual_float_8_id_a738253d_fk_manual_float_accounts_id",
                        column: x => x.manual_float_8_id,
                        principalSchema: "accounting",
                        principalTable: "manual_float_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_master_id_f8105cae_fk_master_accounts_id",
                        column: x => x.master_id,
                        principalSchema: "accounting",
                        principalTable: "master_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_period_id_3525580e_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_resource_and_expenditure_id_c457dd03_fk_resource_and_expenditures_id",
                        column: x => x.resource_and_expenditure_id,
                        principalSchema: "accounting",
                        principalTable: "resource_and_expenditures",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_slave_company_id_51b34aa7_fk_slave_account_companies_id",
                        column: x => x.slave_company_id,
                        principalSchema: "accounting",
                        principalTable: "slave_account_companies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_slave_id_36e4e833_fk_slave_accounts_id",
                        column: x => x.slave_id,
                        principalSchema: "accounting",
                        principalTable: "slave_accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_items_voucher_id_e75336c1_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "trash_voucher_item_attaches",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateOnly>(type: "date", nullable: false),
                    trash_voucher_id = table.Column<int>(type: "int", nullable: false),
                    trash_voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__trash_vo__3213E83F11360E3F", x => x.id);
                    table.ForeignKey(
                        name: "trash_voucher_item_attaches_trash_voucher_id_aea53052_fk_trash_vouchers_id",
                        column: x => x.trash_voucher_id,
                        principalSchema: "accounting",
                        principalTable: "trash_vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "trash_voucher_item_attaches_trash_voucher_item_id_f86212d2_fk_trash_voucher_items_id",
                        column: x => x.trash_voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "trash_voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_buys",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sarjam = table.Column<bool>(type: "bit", nullable: false),
                    is_hagholamal_kari = table.Column<bool>(type: "bit", nullable: false),
                    kala_khadamat_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    kala_code = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    bargasht_type = table.Column<bool>(type: "bit", nullable: false),
                    price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_takhfif_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_sayer_avarez = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_maksoore = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    moadel_riali_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    factor_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    factor_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    foroshande_id = table.Column<int>(type: "int", nullable: true),
                    foroshande_address_id = table.Column<int>(type: "int", nullable: true),
                    foroshande_phone_id = table.Column<int>(type: "int", nullable: true),
                    kala_type_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_buy__3213E83FF4617A16", x => x.id);
                    table.ForeignKey(
                        name: "ttms_buys_kala_type_id_acc1c01e_fk_ttms_product_types_id",
                        column: x => x.kala_type_id,
                        principalSchema: "accounting",
                        principalTable: "ttms_product_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_buys_ledger_id_4592b702_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_buys_period_id_b172660c_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_buys_voucher_id_932f90cb_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_buys_voucher_item_id_f176cf2a_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_contractor_infos",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bargasht_type = table.Column<bool>(type: "bit", nullable: false),
                    gharardad_activity_type_code = table.Column<short>(type: "smallint", nullable: true),
                    total_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    surat_vaziat_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    surat_vaziat_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    pardakht_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    khales_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_maksooreh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_total_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_khales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_maksooreh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_nakhales = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_khales = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_maksooreh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_sayer_avarez = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    moadel_riali_nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maksooreh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_khales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    gharardad_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    peymankar_id = table.Column<int>(type: "int", nullable: true),
                    peymankar_address_id = table.Column<int>(type: "int", nullable: true),
                    peymankar_phone_id = table.Column<int>(type: "int", nullable: true),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_con__3213E83FF95436E1", x => x.id);
                    table.ForeignKey(
                        name: "ttms_contractor_infos_ledger_id_866da6c0_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_contractor_infos_period_id_0b3cc826_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_contractor_infos_voucher_id_4561f0d4_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_contractor_infos_voucher_item_id_f0e62792_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_employer_infos",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_type = table.Column<short>(type: "smallint", nullable: true),
                    bargasht_type = table.Column<bool>(type: "bit", nullable: false),
                    total_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    surat_vaziat_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    surat_vaziat_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    daryaft_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    khales_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_maksooreh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_total_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_khales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_maksooreh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_nakhales = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_khales = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_maksooreh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_sayer_avarez = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    gharardad_activity_type_code = table.Column<short>(type: "smallint", nullable: true),
                    moadel_riali_nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maksooreh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_khales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    gharardad_id = table.Column<int>(type: "int", nullable: true),
                    karfarma_id = table.Column<int>(type: "int", nullable: true),
                    karfarma_address_id = table.Column<int>(type: "int", nullable: true),
                    karfarma_phone_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_emp__3213E83F8C522BC1", x => x.id);
                    table.ForeignKey(
                        name: "ttms_employer_infos_ledger_id_4e6cb6c7_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_employer_infos_period_id_f2de7f69_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_employer_infos_voucher_id_c3d5dd84_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_employer_infos_voucher_item_id_1756b20f_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_exportations",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    foroush_type = table.Column<short>(type: "smallint", nullable: true),
                    hc_kharidar_type_1_code = table.Column<short>(type: "smallint", nullable: true),
                    kala_khadamat_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    kala_code = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: false),
                    price_parvane = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: false),
                    avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: false),
                    sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: false),
                    takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_price_parvane = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_price_parvane = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_takhfif_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_sayer_avarez = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_maksoore = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    moadel_riali_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_price_parvane = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    kotaj_no = table.Column<int>(type: "int", nullable: true),
                    kotaj_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LC_no = table.Column<string>(type: "nvarchar(19)", maxLength: 19, nullable: true),
                    LC_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    gomrok_arzyabi = table.Column<int>(type: "int", nullable: true),
                    gomrok_khoruj = table.Column<int>(type: "int", nullable: true),
                    factor_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    factor_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    tozih = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    kala_type_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    kharidar_id = table.Column<int>(type: "int", nullable: true),
                    kharidar_address_id = table.Column<int>(type: "int", nullable: true),
                    kharidar_country_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_exp__3213E83F07EBF8D8", x => x.id);
                    table.ForeignKey(
                        name: "ttms_exportations_kala_type_id_c8a03da3_fk_ttms_product_types_code",
                        column: x => x.kala_type_id,
                        principalSchema: "accounting",
                        principalTable: "ttms_product_types",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "ttms_exportations_ledger_id_b7fff67d_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_exportations_period_id_7c7cf71b_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_exportations_voucher_id_14d3710f_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_exportations_voucher_item_id_0ac158de_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_importations",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kharid_type = table.Column<short>(type: "smallint", nullable: true),
                    bargasht_az_saderat = table.Column<bool>(type: "bit", nullable: false),
                    HC_foroushande_type_1_code = table.Column<short>(type: "smallint", nullable: true),
                    kala_code = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    kala_khadamat_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    price_parvane = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_price_parvane = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_price_parvane = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_takhfif_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_sayer_avarez = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_maksoore = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    moadel_riali_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_price_parvane = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    kotaj_no = table.Column<int>(type: "int", nullable: true),
                    kotaj_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    lc_no = table.Column<string>(type: "nvarchar(19)", maxLength: 19, nullable: true),
                    lc_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    gomrok_arzyabi = table.Column<int>(type: "int", nullable: true),
                    gomrok_vorud = table.Column<int>(type: "int", nullable: true),
                    factor_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    factor_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    tozih = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    foroshande_id = table.Column<int>(type: "int", nullable: true),
                    foroshande_address_id = table.Column<int>(type: "int", nullable: true),
                    foroshande_country_id = table.Column<int>(type: "int", nullable: true),
                    kala_type_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_imp__3213E83F91DF5323", x => x.id);
                    table.ForeignKey(
                        name: "ttms_importations_kala_type_id_cb9fffb1_fk_ttms_product_types_code",
                        column: x => x.kala_type_id,
                        principalSchema: "accounting",
                        principalTable: "ttms_product_types",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "ttms_importations_ledger_id_a8e87721_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_importations_period_id_e707fd86_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_importations_voucher_id_e6c6d27d_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_importations_voucher_item_id_7d74fcd3_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_lease_agreements",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tarafe_gharardad_type = table.Column<short>(type: "smallint", nullable: true),
                    tedad_shoraka = table.Column<short>(type: "smallint", nullable: true),
                    gharardad_type_code = table.Column<short>(type: "smallint", nullable: true),
                    ejari_type = table.Column<short>(type: "smallint", nullable: true),
                    tozih = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    vasile = table.Column<short>(type: "smallint", nullable: true),
                    karbari_type = table.Column<short>(type: "smallint", nullable: true),
                    melk_address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    melk_post_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    melk_tell_pish_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    melk_tell = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    pelak_sabti_asli = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    pelak_sabti_fari = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    bakhsh_sabti = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    shenase_melk = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    mozu_type = table.Column<short>(type: "smallint", nullable: true),
                    nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_nakhales = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_maksoore = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    moadel_riali_nakhales = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    sanad_no = table.Column<int>(type: "int", nullable: true),
                    sanad_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    gharardad_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    melk_city_id = table.Column<int>(type: "int", nullable: true),
                    melk_state_id = table.Column<int>(type: "int", nullable: true),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    tarafe_gharardad_id = table.Column<int>(type: "int", nullable: true),
                    tarafe_gharardad_address_id = table.Column<int>(type: "int", nullable: true),
                    tarafe_gharardad_phone_id = table.Column<int>(type: "int", nullable: true),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_lea__3213E83FE7DBF176", x => x.id);
                    table.ForeignKey(
                        name: "ttms_lease_agreements_ledger_id_fbcd73fe_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_lease_agreements_period_id_b7dd6e0b_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_lease_agreements_voucher_id_38801563_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_lease_agreements_voucher_item_id_580d9a03_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_pre_sells",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tarafe_gharardad_type = table.Column<short>(type: "smallint", nullable: true),
                    gharardad_type_code = table.Column<short>(type: "smallint", nullable: true),
                    karbari_type = table.Column<short>(type: "smallint", nullable: true),
                    tozih = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    melk_address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    pelak_sabti = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    bargasht_type = table.Column<bool>(type: "bit", nullable: false),
                    pardakht_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    tozihat_pardakht = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: false),
                    arz_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    gharardad_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    melk_city_id = table.Column<int>(type: "int", nullable: true),
                    melk_state_id = table.Column<int>(type: "int", nullable: true),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    t_address_id = table.Column<int>(type: "int", nullable: true),
                    t_phone_id = table.Column<int>(type: "int", nullable: true),
                    tarafe_gharardad_id = table.Column<int>(type: "int", nullable: true),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_pre__3213E83FBF587317", x => x.id);
                    table.ForeignKey(
                        name: "ttms_pre_sells_ledger_id_e56cf010_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_pre_sells_period_id_d371cddf_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_pre_sells_voucher_id_58f4d88f_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_pre_sells_voucher_item_id_13de02d7_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_sells",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sarjam = table.Column<bool>(type: "bit", nullable: false),
                    is_hagholamal_kari = table.Column<bool>(type: "bit", nullable: false),
                    kala_khadamat_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    kala_code = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    bargasht_type = table.Column<bool>(type: "bit", nullable: false),
                    price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_takhfif_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_sayer_avarez = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_maksoore = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    moadel_riali_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_maksoore = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    factor_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    factor_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    kala_type_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    kharidar_id = table.Column<int>(type: "int", nullable: true),
                    kharidar_address_id = table.Column<int>(type: "int", nullable: true),
                    kharidar_phone_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_sel__3213E83FF5A53FF2", x => x.id);
                    table.ForeignKey(
                        name: "ttms_sells_kala_type_id_49581b94_fk_ttms_product_types_code",
                        column: x => x.kala_type_id,
                        principalSchema: "accounting",
                        principalTable: "ttms_product_types",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "ttms_sells_ledger_id_e8e19e1e_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_sells_period_id_bdec3655_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_sells_voucher_id_96da188f_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_sells_voucher_item_id_99608a2c_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ttms_wages",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_type = table.Column<short>(type: "smallint", nullable: true),
                    daryaft_az = table.Column<short>(type: "smallint", nullable: true),
                    sarjam = table.Column<bool>(type: "bit", nullable: false),
                    bargasht_type = table.Column<bool>(type: "bit", nullable: false),
                    s_type_1_code = table.Column<short>(type: "smallint", nullable: true),
                    k_type_1_code = table.Column<short>(type: "smallint", nullable: true),
                    h_nerkh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_arz_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_arz_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_arz_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_arz_barabari_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    h_arz_barabari_takhfif_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    h_arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    h_arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    h_arz_barabari_sayer_avarez = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    h_moadel_riali_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_moadel_riali_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_moadel_riali_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    h_factor_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    h_factor_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    kala_khadamat_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    kala_code = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    arz_barabari_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_takhfif_price = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_maliat_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_avarez_arzesh_afzoodeh = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    arz_barabari_sayer_avarez = table.Column<decimal>(type: "numeric(13,5)", nullable: true),
                    moadel_riali_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_takhfif_price = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_maliat_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_avarez_arzesh_afzoodeh = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    moadel_riali_sayer_avarez = table.Column<string>(type: "nvarchar(23)", maxLength: 23, nullable: true),
                    factor_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    factor_date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    deal_currency = table.Column<short>(type: "smallint", nullable: true),
                    arz_type_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    h_arz_type_id = table.Column<int>(type: "int", nullable: true),
                    kala_type_id = table.Column<int>(type: "int", nullable: true),
                    kharidar_id = table.Column<int>(type: "int", nullable: true),
                    kharidar_address_id = table.Column<int>(type: "int", nullable: true),
                    kharidar_phone_id = table.Column<int>(type: "int", nullable: true),
                    ledger_id = table.Column<int>(type: "int", nullable: false),
                    period_id = table.Column<int>(type: "int", nullable: false),
                    seller_id = table.Column<int>(type: "int", nullable: true),
                    seller_address_id = table.Column<int>(type: "int", nullable: true),
                    seller_phone_id = table.Column<int>(type: "int", nullable: true),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ttms_wag__3213E83F2B6F7066", x => x.id);
                    table.ForeignKey(
                        name: "ttms_wages_kala_type_id_8ee391b0_fk_ttms_product_types_id",
                        column: x => x.kala_type_id,
                        principalSchema: "accounting",
                        principalTable: "ttms_product_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_wages_ledger_id_2e5fbf4c_fk_ledgers_id",
                        column: x => x.ledger_id,
                        principalSchema: "accounting",
                        principalTable: "ledgers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_wages_period_id_7e023cdd_fk_periods_id",
                        column: x => x.period_id,
                        principalSchema: "accounting",
                        principalTable: "periods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_wages_voucher_id_ebeeee11_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ttms_wages_voucher_item_id_6c48028f_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "voucher_item_attaches",
                schema: "accounting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    file = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateOnly>(type: "date", nullable: false),
                    voucher_id = table.Column<int>(type: "int", nullable: false),
                    voucher_item_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__voucher___3213E83F0FFD2452", x => x.id);
                    table.ForeignKey(
                        name: "voucher_item_attaches_voucher_id_05221da5_fk_vouchers_id",
                        column: x => x.voucher_id,
                        principalSchema: "accounting",
                        principalTable: "vouchers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "voucher_item_attaches_voucher_item_id_3f1216ab_fk_voucher_items_id",
                        column: x => x.voucher_item_id,
                        principalSchema: "accounting",
                        principalTable: "voucher_items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__account___72E12F1B3B565389",
                schema: "accounting",
                table: "account_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "account_groups_category_id_af0c53e8",
                schema: "accounting",
                table: "account_groups",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "account_groups_category_id_code_2b62c6b4_uniq",
                schema: "accounting",
                table: "account_groups",
                columns: new[] { "category_id", "code" },
                unique: true,
                filter: "([category_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "account_groups_category_id_name_8880b185_uniq",
                schema: "accounting",
                table: "account_groups",
                columns: new[] { "category_id", "name" },
                unique: true,
                filter: "([category_id] IS NOT NULL AND [name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "account_set_items_account_set_id_c6a49338",
                schema: "accounting",
                table: "account_set_items",
                column: "account_set_id");

            migrationBuilder.CreateIndex(
                name: "account_set_items_account_set_id_master_id_slave_id_c5ba8c71_uniq",
                schema: "accounting",
                table: "account_set_items",
                columns: new[] { "account_set_id", "master_id", "slave_id" },
                unique: true,
                filter: "([account_set_id] IS NOT NULL AND [master_id] IS NOT NULL AND [slave_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "account_set_items_master_id_e12671e4",
                schema: "accounting",
                table: "account_set_items",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "account_set_items_slave_id_a5287d0f",
                schema: "accounting",
                table: "account_set_items",
                column: "slave_id");

            migrationBuilder.CreateIndex(
                name: "account_sets_company_id_227009e4",
                schema: "accounting",
                table: "account_sets",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "account_sets_company_id_title_b34aa998_uniq",
                schema: "accounting",
                table: "account_sets",
                columns: new[] { "company_id", "title" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [title] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "account_sets_ledger_id_31846310",
                schema: "accounting",
                table: "account_sets",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_bank_branch_for_payable_doc_id_f62750bd",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "bank_branch_for_payable_doc_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_bank_branch_for_receivable_doc_id_3f00ae31",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "bank_branch_for_receivable_doc_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_bank_for_payable_doc_id_a61b2771",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "bank_for_payable_doc_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_bank_for_receivable_doc_id_6d949455",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "bank_for_receivable_doc_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_bank_operation_bank_branch_id_b2e2213b",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "bank_operation_bank_branch_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_bank_operation_type_id_12f7966f",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "bank_operation_type_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_branch_id_c4524e9e",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_company_id_f90e854f",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_company_unit_id_a4a8a95f",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "company_unit_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_cost_center_id_1cebfa10",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "cost_center_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_currency_id_a94bd7cd",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_ledger_id_1fcf3bc8",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_manual_float_1_id_3cf4e319",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "manual_float_1_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_manual_float_2_id_79cca910",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "manual_float_2_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_manual_float_3_id_c7afe8b7",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "manual_float_3_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_manual_float_4_id_bba5925c",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "manual_float_4_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_manual_float_5_id_2f0ad7f7",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "manual_float_5_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_manual_float_6_id_eeaf3bf1",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "manual_float_6_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_manual_float_7_id_c140028e",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "manual_float_7_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_manual_float_8_id_68b0bdc0",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "manual_float_8_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_master_id_f76438d4",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_period_id_2b2d6794",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_person_id_e1e5844f",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_project_contract_id_926e56be",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "project_contract_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_project_contract_type_id_4819f840",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "project_contract_type_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_project_id_6072d815",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_resource_and_expenditure_id_78f2b804",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "resource_and_expenditure_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_slave_id_8bf18f14",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "slave_id");

            migrationBuilder.CreateIndex(
                name: "balance_related_account_details_store_id_9f40ae42",
                schema: "accounting",
                table: "balance_related_account_details",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "bank_templates_company_id_2c793700",
                schema: "accounting",
                table: "bank_templates",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "bank_templates_creator_id_3c99db7f",
                schema: "accounting",
                table: "bank_templates",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_company_id_d40fc855",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_company_id_ledger_id_period_id_slave_company_id_772b00bd_uniq",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                columns: new[] { "company_id", "ledger_id", "period_id", "slave_company_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [period_id] IS NOT NULL AND [slave_company_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_default_cashier_period_id_92a586f2",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "default_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_default_company_unit_id_2c7875df",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "default_company_unit_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_default_currency_id_8d86bb77",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "default_currency_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_default_person_id_94d4611e",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "default_person_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_default_petty_cashier_period_id_6135e6e8",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "default_petty_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_default_project_id_becc3a53",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "default_project_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_default_resource_and_expenditure_id_d8808ca1",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "default_resource_and_expenditure_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_default_store_id_2b4de0c5",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "default_store_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_ledger_id_b7344906",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_period_id_7d4ec4d3",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_slave_companies_slave_company_id_e971e36e",
                schema: "accounting",
                table: "closing_pattern_slave_companies",
                column: "slave_company_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_company_id_952b9b60",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_company_id_ledger_id_period_id_float_type_set_2bc966ac_uniq",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                columns: new[] { "company_id", "ledger_id", "period_id", "float_type_set" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [period_id] IS NOT NULL AND [float_type_set] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_cashier_period_id_d8091266",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_company_unit_id_086a631a",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_company_unit_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_cost_center_id_76aab24b",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_cost_center_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_currency_id_2f9de639",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_currency_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_person_id_a4961ab4",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_person_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_petty_cashier_period_id_4d49f688",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_petty_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_project_id_bbbb80e7",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_project_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_resource_and_expenditure_id_dcf3d48a",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_resource_and_expenditure_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_default_store_id_43427fea",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "default_store_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_ledger_id_8d2baceb",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "closing_pattern_temporary_accounts_period_id_897435aa",
                schema: "accounting",
                table: "closing_pattern_temporary_accounts",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_company_id_9cd07d27",
                schema: "accounting",
                table: "closing_patterns",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_company_id_ledger_id_period_id_float_type_set_70e51708_uniq",
                schema: "accounting",
                table: "closing_patterns",
                columns: new[] { "company_id", "ledger_id", "period_id", "float_type_set" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [period_id] IS NOT NULL AND [float_type_set] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_default_cashier_period_id_2116c839",
                schema: "accounting",
                table: "closing_patterns",
                column: "default_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_default_company_unit_id_b57cd2d3",
                schema: "accounting",
                table: "closing_patterns",
                column: "default_company_unit_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_default_currency_id_efd08b2d",
                schema: "accounting",
                table: "closing_patterns",
                column: "default_currency_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_default_person_id_d1e830cb",
                schema: "accounting",
                table: "closing_patterns",
                column: "default_person_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_default_petty_cashier_period_id_8d44d43e",
                schema: "accounting",
                table: "closing_patterns",
                column: "default_petty_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_default_project_id_354a5095",
                schema: "accounting",
                table: "closing_patterns",
                column: "default_project_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_default_resource_and_expenditure_id_6ef35f22",
                schema: "accounting",
                table: "closing_patterns",
                column: "default_resource_and_expenditure_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_default_store_id_0f67c8c0",
                schema: "accounting",
                table: "closing_patterns",
                column: "default_store_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_ledger_id_394ee754",
                schema: "accounting",
                table: "closing_patterns",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "closing_patterns_period_id_8a00d0d0",
                schema: "accounting",
                table: "closing_patterns",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "contradiction_items_contradiction_id_1494f375",
                schema: "accounting",
                table: "contradiction_items",
                column: "contradiction_id");

            migrationBuilder.CreateIndex(
                name: "contradiction_items_creator_id_ee0be02e",
                schema: "accounting",
                table: "contradiction_items",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "contradictions_bank_account_id_e4432ed1",
                schema: "accounting",
                table: "contradictions",
                column: "bank_account_id");

            migrationBuilder.CreateIndex(
                name: "contradictions_company_id_f68c3f68",
                schema: "accounting",
                table: "contradictions",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "contradictions_creator_id_430d02a0",
                schema: "accounting",
                table: "contradictions",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "contradictions_data_import_id_65a4748b",
                schema: "accounting",
                table: "contradictions",
                column: "data_import_id");

            migrationBuilder.CreateIndex(
                name: "contradictions_ledger_id_85776d2a",
                schema: "accounting",
                table: "contradictions",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "contradictions_period_id_538ee815",
                schema: "accounting",
                table: "contradictions",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "data_imports_company_id_eda746f3",
                schema: "accounting",
                table: "data_imports",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "data_imports_creator_id_cba47975",
                schema: "accounting",
                table: "data_imports",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "data_imports_entity_type_command_id_a8a0c6e3",
                schema: "accounting",
                table: "data_imports",
                column: "entity_type_command_id");

            migrationBuilder.CreateIndex(
                name: "data_imports_entity_type_id_904ccc0a",
                schema: "accounting",
                table: "data_imports",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "data_imports_ledger_id_fd1c5e0d",
                schema: "accounting",
                table: "data_imports",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "data_imports_period_id_aef725fd",
                schema: "accounting",
                table: "data_imports",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "float_account_types_parent_id_4dd8ba99",
                schema: "accounting",
                table: "float_account_types",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "float_account_types_voucher_item_float_level_b985d2e4_uniq",
                schema: "accounting",
                table: "float_account_types",
                column: "voucher_item_float_level",
                unique: true,
                filter: "([voucher_item_float_level] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__float_ac__2E0A7AA653BB5215",
                schema: "accounting",
                table: "float_account_types",
                column: "name_fa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ledger_period_companies_company_id_b4e49be1",
                schema: "accounting",
                table: "ledger_period_companies",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ledger_period_companies_company_id_ledger_period_id_08379790_uniq",
                schema: "accounting",
                table: "ledger_period_companies",
                columns: new[] { "company_id", "ledger_period_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [ledger_period_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "ledger_period_companies_ledger_id_52a61c2a",
                schema: "accounting",
                table: "ledger_period_companies",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ledger_period_companies_ledger_period_id_5656d93c",
                schema: "accounting",
                table: "ledger_period_companies",
                column: "ledger_period_id");

            migrationBuilder.CreateIndex(
                name: "ledger_period_companies_period_id_0828d8ca",
                schema: "accounting",
                table: "ledger_period_companies",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ledger_period_company_settings_company_id_a0d2dc4a",
                schema: "accounting",
                table: "ledger_period_company_settings",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ledger_period_company_settings_company_id_ledger_period_company_id_e87be1b5_uniq",
                schema: "accounting",
                table: "ledger_period_company_settings",
                columns: new[] { "company_id", "ledger_period_company_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [ledger_period_company_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "ledger_period_company_settings_ledger_id_c37251eb",
                schema: "accounting",
                table: "ledger_period_company_settings",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ledger_period_company_settings_period_id_2389d1c0",
                schema: "accounting",
                table: "ledger_period_company_settings",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ledger_p__8880943C4AE414BF",
                schema: "accounting",
                table: "ledger_period_company_settings",
                column: "ledger_period_company_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ledger_periods_ledger_id_c70ff025",
                schema: "accounting",
                table: "ledger_periods",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ledger_periods_ledger_id_period_id_5d05e0eb_uniq",
                schema: "accounting",
                table: "ledger_periods",
                columns: new[] { "ledger_id", "period_id" },
                unique: true,
                filter: "([ledger_id] IS NOT NULL AND [period_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "ledger_periods_period_id_2953e5ba",
                schema: "accounting",
                table: "ledger_periods",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ledgers__357D4CF92E60519D",
                schema: "accounting",
                table: "ledgers",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__ledgers__72E12F1B2CE796EF",
                schema: "accounting",
                table: "ledgers",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__ledgers__F0B628432B41C06C",
                schema: "accounting",
                table: "ledgers",
                column: "name2",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "manual_float_accounts_company_id_c4223ce2",
                schema: "accounting",
                table: "manual_float_accounts",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "manual_float_accounts_company_id_float_account_type_id_code_7e28c5e8_uniq",
                schema: "accounting",
                table: "manual_float_accounts",
                columns: new[] { "company_id", "float_account_type_id", "code" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [float_account_type_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "manual_float_accounts_company_id_float_account_type_id_title_ad80fe99_uniq",
                schema: "accounting",
                table: "manual_float_accounts",
                columns: new[] { "company_id", "float_account_type_id", "title" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [float_account_type_id] IS NOT NULL AND [title] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "manual_float_accounts_company_id_float_account_type_id_title2_11d5aa07_uniq",
                schema: "accounting",
                table: "manual_float_accounts",
                columns: new[] { "company_id", "float_account_type_id", "title2" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [float_account_type_id] IS NOT NULL AND [title2] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "manual_float_accounts_float_account_type_id_b211d188",
                schema: "accounting",
                table: "manual_float_accounts",
                column: "float_account_type_id");

            migrationBuilder.CreateIndex(
                name: "manual_float_accounts_parent_id_4b9bca91",
                schema: "accounting",
                table: "manual_float_accounts",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "master_accounts_ledger_id_b94026a2",
                schema: "accounting",
                table: "master_accounts",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "master_accounts_ledger_id_code_7b97c8b2_uniq",
                schema: "accounting",
                table: "master_accounts",
                columns: new[] { "ledger_id", "code" },
                unique: true,
                filter: "([ledger_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__periods__72E12F1BE18188D5",
                schema: "accounting",
                table: "periods",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "resource_and_expenditures_company_id_5d08b6e8",
                schema: "accounting",
                table: "resource_and_expenditures",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "resource_and_expenditures_company_id_code_75192806_uniq",
                schema: "accounting",
                table: "resource_and_expenditures",
                columns: new[] { "company_id", "code" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "resource_and_expenditures_company_id_name_1605dd8e_uniq",
                schema: "accounting",
                table: "resource_and_expenditures",
                columns: new[] { "company_id", "name" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ__settings__E72BC7677365AE8A",
                schema: "accounting",
                table: "settings",
                column: "domain_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_company_id_10f63c16",
                schema: "accounting",
                table: "slave_account_companies",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_float_type_1_id_dcee560d",
                schema: "accounting",
                table: "slave_account_companies",
                column: "float_type_1_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_float_type_2_id_c218860a",
                schema: "accounting",
                table: "slave_account_companies",
                column: "float_type_2_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_float_type_3_id_653411c6",
                schema: "accounting",
                table: "slave_account_companies",
                column: "float_type_3_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_float_type_4_id_57254460",
                schema: "accounting",
                table: "slave_account_companies",
                column: "float_type_4_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_float_type_5_id_896ccdfc",
                schema: "accounting",
                table: "slave_account_companies",
                column: "float_type_5_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_float_type_6_id_a486a7f8",
                schema: "accounting",
                table: "slave_account_companies",
                column: "float_type_6_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_float_type_7_id_f5d8293e",
                schema: "accounting",
                table: "slave_account_companies",
                column: "float_type_7_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_float_type_8_id_ab2b63c8",
                schema: "accounting",
                table: "slave_account_companies",
                column: "float_type_8_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_ledger_id_b40ad4d0",
                schema: "accounting",
                table: "slave_account_companies",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_master_id_bacd3012",
                schema: "accounting",
                table: "slave_account_companies",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_slave_id_company_id_d7bc5f10_uniq",
                schema: "accounting",
                table: "slave_account_companies",
                columns: new[] { "slave_id", "company_id" },
                unique: true,
                filter: "([slave_id] IS NOT NULL AND [company_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "slave_account_companies_slave_id_edd3dcf5",
                schema: "accounting",
                table: "slave_account_companies",
                column: "slave_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_standard_descriptions_company_id_a2d12c8c",
                schema: "accounting",
                table: "slave_account_standard_descriptions",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "slave_account_standard_descriptions_company_id_slave_id_description_fcd17f45_uniq",
                schema: "accounting",
                table: "slave_account_standard_descriptions",
                columns: new[] { "company_id", "slave_id", "description" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [slave_id] IS NOT NULL AND [description] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "slave_account_standard_descriptions_slave_id_b64d916e",
                schema: "accounting",
                table: "slave_account_standard_descriptions",
                column: "slave_id");

            migrationBuilder.CreateIndex(
                name: "slave_accounts_category_id_79276cde",
                schema: "accounting",
                table: "slave_accounts",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "slave_accounts_category_id_group_id_ledger_id_master_id_code_detailed_account_1_2ebf0ffa_uniq",
                schema: "accounting",
                table: "slave_accounts",
                columns: new[] { "category_id", "group_id", "ledger_id", "master_id", "code", "detailed_account_1" },
                unique: true,
                filter: "([category_id] IS NOT NULL AND [group_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [master_id] IS NOT NULL AND [code] IS NOT NULL AND [detailed_account_1] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "slave_accounts_category_id_group_id_ledger_id_master_id_code_detailed_account_1_detailed_account_2_1409df48_uniq",
                schema: "accounting",
                table: "slave_accounts",
                columns: new[] { "category_id", "group_id", "ledger_id", "master_id", "code", "detailed_account_1", "detailed_account_2" },
                unique: true,
                filter: "([category_id] IS NOT NULL AND [group_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [master_id] IS NOT NULL AND [code] IS NOT NULL AND [detailed_account_1] IS NOT NULL AND [detailed_account_2] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "slave_accounts_from_company_id_id_be582ecc",
                schema: "accounting",
                table: "slave_accounts",
                column: "from_company_id_id");

            migrationBuilder.CreateIndex(
                name: "slave_accounts_group_id_d226c356",
                schema: "accounting",
                table: "slave_accounts",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "slave_accounts_ledger_id_e9bba48c",
                schema: "accounting",
                table: "slave_accounts",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "slave_accounts_master_id_d8364647",
                schema: "accounting",
                table: "slave_accounts",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "slave_accounts_parent_id_b5e3fe05",
                schema: "accounting",
                table: "slave_accounts",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_item_attaches_trash_voucher_id_aea53052",
                schema: "accounting",
                table: "trash_voucher_item_attaches",
                column: "trash_voucher_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_item_attaches_trash_voucher_item_id_f86212d2",
                schema: "accounting",
                table: "trash_voucher_item_attaches",
                column: "trash_voucher_item_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_bank_branch_for_payable_doc_id_c7192fe1",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "bank_branch_for_payable_doc_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_bank_branch_for_receivable_doc_id_18f99fa4",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "bank_branch_for_receivable_doc_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_bank_for_payable_doc_id_b0e53bf9",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "bank_for_payable_doc_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_bank_for_receivable_doc_id_52a2c187",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "bank_for_receivable_doc_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_bank_operation_bank_account_id_2cdf8f2c",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "bank_operation_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_bank_operation_type_id_4f027778",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "bank_operation_type_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_branch_id_1cd5a4ea",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_cashier_period_id_ec0ac32d",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_company_id_b595ca9c",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_company_unit_id_ab43aa30",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "company_unit_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_cost_center_id_7225ec9a",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "cost_center_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_currency_id_bf31a904",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_ledger_id_58204329",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_manual_float_1_id_0cdae92d",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "manual_float_1_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_manual_float_2_id_985905b9",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "manual_float_2_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_manual_float_3_id_11645efa",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "manual_float_3_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_manual_float_4_id_60ae5d18",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "manual_float_4_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_manual_float_5_id_3eb06b68",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "manual_float_5_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_manual_float_6_id_a4afd007",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "manual_float_6_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_manual_float_7_id_85ed9abc",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "manual_float_7_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_manual_float_8_id_d2f59901",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "manual_float_8_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_master_id_0c48bb81",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_period_id_e1c57ee1",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_person_id_0cca821d",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_petty_cashier_period_id_1e714a51",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "petty_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_project_contract_id_967b39b0",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "project_contract_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_project_contract_type_id_4e9e97ee",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "project_contract_type_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_project_id_e5915af2",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_resource_and_expenditure_id_fb349f90",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "resource_and_expenditure_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_slave_company_id_976d2e14",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "slave_company_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_slave_id_8ef343db",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "slave_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_store_id_93066542",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "trash_voucher_items_trash_voucher_id_4c9d9a23",
                schema: "accounting",
                table: "trash_voucher_items",
                column: "trash_voucher_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_branch_id_4da4e4fa",
                schema: "accounting",
                table: "trash_vouchers",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_company_id_01f6a6f1",
                schema: "accounting",
                table: "trash_vouchers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_creator_id_50cfb8a4",
                schema: "accounting",
                table: "trash_vouchers",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_last_modifier_id_35d033e0",
                schema: "accounting",
                table: "trash_vouchers",
                column: "last_modifier_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_ledger_id_e6d73246",
                schema: "accounting",
                table: "trash_vouchers",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_module_id_3481836f",
                schema: "accounting",
                table: "trash_vouchers",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_period_id_f1b414ea",
                schema: "accounting",
                table: "trash_vouchers",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_type_id_62c0b2ab",
                schema: "accounting",
                table: "trash_vouchers",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "trash_vouchers_voucher_id_initial_f5cefa98",
                schema: "accounting",
                table: "trash_vouchers",
                column: "voucher_id_initial");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_arz_type_id_842752a5",
                schema: "accounting",
                table: "ttms_buys",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_company_id_86d0df71",
                schema: "accounting",
                table: "ttms_buys",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_foroshande_address_id_f6676def",
                schema: "accounting",
                table: "ttms_buys",
                column: "foroshande_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_foroshande_id_899255d3",
                schema: "accounting",
                table: "ttms_buys",
                column: "foroshande_id");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_foroshande_phone_id_85d73354",
                schema: "accounting",
                table: "ttms_buys",
                column: "foroshande_phone_id");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_kala_type_id_acc1c01e",
                schema: "accounting",
                table: "ttms_buys",
                column: "kala_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_ledger_id_4592b702",
                schema: "accounting",
                table: "ttms_buys",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_period_id_b172660c",
                schema: "accounting",
                table: "ttms_buys",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_buys_voucher_id_932f90cb",
                schema: "accounting",
                table: "ttms_buys",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_buy__A2D3D11B3AE62649",
                schema: "accounting",
                table: "ttms_buys",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_arz_type_id_2564bca6",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_company_id_2e0427bc",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_gharardad_id_46fdf4d6",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "gharardad_id");

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_ledger_id_866da6c0",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_period_id_0b3cc826",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_peymankar_address_id_f058807c",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "peymankar_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_peymankar_id_0c4a252f",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "peymankar_id");

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_peymankar_phone_id_aeeaee77",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "peymankar_phone_id");

            migrationBuilder.CreateIndex(
                name: "ttms_contractor_infos_voucher_id_4561f0d4",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_con__A2D3D11B3E04D101",
                schema: "accounting",
                table: "ttms_contractor_infos",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_arz_type_id_64a2746a",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_company_id_f5ced1a0",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_gharardad_id_38dc7897",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "gharardad_id");

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_karfarma_address_id_fbf74ad6",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "karfarma_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_karfarma_id_e737c2aa",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "karfarma_id");

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_karfarma_phone_id_a109c622",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "karfarma_phone_id");

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_ledger_id_4e6cb6c7",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_period_id_f2de7f69",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_employer_infos_voucher_id_c3d5dd84",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_emp__A2D3D11B5AB028CE",
                schema: "accounting",
                table: "ttms_employer_infos",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_arz_type_id_a88c4e64",
                schema: "accounting",
                table: "ttms_exportations",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_company_id_d4e9b554",
                schema: "accounting",
                table: "ttms_exportations",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_kala_type_id_c8a03da3",
                schema: "accounting",
                table: "ttms_exportations",
                column: "kala_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_kharidar_address_id_b43af6ed",
                schema: "accounting",
                table: "ttms_exportations",
                column: "kharidar_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_kharidar_country_id_5b8e0edc",
                schema: "accounting",
                table: "ttms_exportations",
                column: "kharidar_country_id");

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_kharidar_id_f33336bf",
                schema: "accounting",
                table: "ttms_exportations",
                column: "kharidar_id");

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_ledger_id_b7fff67d",
                schema: "accounting",
                table: "ttms_exportations",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_period_id_7c7cf71b",
                schema: "accounting",
                table: "ttms_exportations",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_exportations_voucher_id_14d3710f",
                schema: "accounting",
                table: "ttms_exportations",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_exp__A2D3D11B14C08510",
                schema: "accounting",
                table: "ttms_exportations",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ttms_importations_arz_type_id_66a0f015",
                schema: "accounting",
                table: "ttms_importations",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_importations_company_id_d3aecf45",
                schema: "accounting",
                table: "ttms_importations",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_importations_foroshande_address_id_c9332593",
                schema: "accounting",
                table: "ttms_importations",
                column: "foroshande_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_importations_foroshande_country_id_7cf0dc0a",
                schema: "accounting",
                table: "ttms_importations",
                column: "foroshande_country_id");

            migrationBuilder.CreateIndex(
                name: "ttms_importations_foroshande_id_681a08be",
                schema: "accounting",
                table: "ttms_importations",
                column: "foroshande_id");

            migrationBuilder.CreateIndex(
                name: "ttms_importations_kala_type_id_cb9fffb1",
                schema: "accounting",
                table: "ttms_importations",
                column: "kala_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_importations_ledger_id_a8e87721",
                schema: "accounting",
                table: "ttms_importations",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_importations_period_id_e707fd86",
                schema: "accounting",
                table: "ttms_importations",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_importations_voucher_id_e6c6d27d",
                schema: "accounting",
                table: "ttms_importations",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_imp__A2D3D11B86124CAE",
                schema: "accounting",
                table: "ttms_importations",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_arz_type_id_eb4b0083",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_company_id_fe87aa2f",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_gharardad_id_9c794542",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "gharardad_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_ledger_id_fbcd73fe",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_melk_city_id_c6850824",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "melk_city_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_melk_state_id_970e540d",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "melk_state_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_period_id_b7dd6e0b",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_tarafe_gharardad_address_id_cf429de4",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "tarafe_gharardad_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_tarafe_gharardad_id_9d1d1f02",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "tarafe_gharardad_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_tarafe_gharardad_phone_id_c4fefd27",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "tarafe_gharardad_phone_id");

            migrationBuilder.CreateIndex(
                name: "ttms_lease_agreements_voucher_id_38801563",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_lea__A2D3D11BF7748A1E",
                schema: "accounting",
                table: "ttms_lease_agreements",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_arz_type_id_141bbcfb",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_company_id_2a037b71",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_gharardad_id_c447c81b",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "gharardad_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_ledger_id_e56cf010",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_melk_city_id_83ae0854",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "melk_city_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_melk_state_id_b05f8d9c",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "melk_state_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_period_id_d371cddf",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_t_address_id_c02594e3",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "t_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_t_phone_id_97c78fc6",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "t_phone_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_tarafe_gharardad_id_bcffbf4c",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "tarafe_gharardad_id");

            migrationBuilder.CreateIndex(
                name: "ttms_pre_sells_voucher_id_58f4d88f",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_pre__A2D3D11BC3C56D57",
                schema: "accounting",
                table: "ttms_pre_sells",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_pro__357D4CF9F1A81110",
                schema: "accounting",
                table: "ttms_product_types",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ttms_sells_arz_type_id_6035a895",
                schema: "accounting",
                table: "ttms_sells",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_sells_company_id_f2b729b6",
                schema: "accounting",
                table: "ttms_sells",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_sells_kala_type_id_49581b94",
                schema: "accounting",
                table: "ttms_sells",
                column: "kala_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_sells_kharidar_address_id_8e5898be",
                schema: "accounting",
                table: "ttms_sells",
                column: "kharidar_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_sells_kharidar_id_28d5fad0",
                schema: "accounting",
                table: "ttms_sells",
                column: "kharidar_id");

            migrationBuilder.CreateIndex(
                name: "ttms_sells_kharidar_phone_id_00e26fff",
                schema: "accounting",
                table: "ttms_sells",
                column: "kharidar_phone_id");

            migrationBuilder.CreateIndex(
                name: "ttms_sells_ledger_id_e8e19e1e",
                schema: "accounting",
                table: "ttms_sells",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_sells_period_id_bdec3655",
                schema: "accounting",
                table: "ttms_sells",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_sells_voucher_id_96da188f",
                schema: "accounting",
                table: "ttms_sells",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_sel__A2D3D11B5AFD5124",
                schema: "accounting",
                table: "ttms_sells",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ttms_wages_arz_type_id_aefc4dc1",
                schema: "accounting",
                table: "ttms_wages",
                column: "arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_company_id_fbb2bfe2",
                schema: "accounting",
                table: "ttms_wages",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_h_arz_type_id_fc57f533",
                schema: "accounting",
                table: "ttms_wages",
                column: "h_arz_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_kala_type_id_8ee391b0",
                schema: "accounting",
                table: "ttms_wages",
                column: "kala_type_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_kharidar_address_id_e43b08df",
                schema: "accounting",
                table: "ttms_wages",
                column: "kharidar_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_kharidar_id_78670155",
                schema: "accounting",
                table: "ttms_wages",
                column: "kharidar_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_kharidar_phone_id_11a031a9",
                schema: "accounting",
                table: "ttms_wages",
                column: "kharidar_phone_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_ledger_id_2e5fbf4c",
                schema: "accounting",
                table: "ttms_wages",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_period_id_7e023cdd",
                schema: "accounting",
                table: "ttms_wages",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_seller_address_id_ee67712a",
                schema: "accounting",
                table: "ttms_wages",
                column: "seller_address_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_seller_id_a18419a3",
                schema: "accounting",
                table: "ttms_wages",
                column: "seller_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_seller_phone_id_985624b3",
                schema: "accounting",
                table: "ttms_wages",
                column: "seller_phone_id");

            migrationBuilder.CreateIndex(
                name: "ttms_wages_voucher_id_ebeeee11",
                schema: "accounting",
                table: "ttms_wages",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "UQ__ttms_wag__A2D3D11BC43A0393",
                schema: "accounting",
                table: "ttms_wages",
                column: "voucher_item_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "voucher_item_attaches_voucher_id_05221da5",
                schema: "accounting",
                table: "voucher_item_attaches",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_attaches_voucher_item_id_3f1216ab",
                schema: "accounting",
                table: "voucher_item_attaches",
                column: "voucher_item_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_bank_branch_for_payable_doc_id_2db629ab",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "bank_branch_for_payable_doc_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_bank_branch_for_receivable_doc_id_64132689",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "bank_branch_for_receivable_doc_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_bank_for_payable_doc_id_61e85f05",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "bank_for_payable_doc_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_bank_for_receivable_doc_id_bf6c6f31",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "bank_for_receivable_doc_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_bank_operation_bank_account_id_44a79bf8",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "bank_operation_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_bank_operation_type_id_4dcc6132",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "bank_operation_type_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_branch_id_2c9e0bff",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_cashier_period_id_c64d915f",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_company_id_b3a353fc",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_company_unit_id_624a2c28",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "company_unit_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_cost_center_id_24cca585",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "cost_center_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_currency_id_e08cc027",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_ledger_id_8b5e1cd0",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_manual_float_1_id_58b6802b",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "manual_float_1_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_manual_float_2_id_38f71036",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "manual_float_2_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_manual_float_3_id_aab41fd4",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "manual_float_3_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_manual_float_4_id_f5608169",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "manual_float_4_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_manual_float_5_id_d6f8534e",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "manual_float_5_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_manual_float_6_id_356cdede",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "manual_float_6_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_manual_float_7_id_c6d1e3a4",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "manual_float_7_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_manual_float_8_id_97e9e83b",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "manual_float_8_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_master_id_6b97e99d",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_period_id_41dea737",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_person_id_d42c3aaa",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_petty_cashier_period_id_6ca1cb8e",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "petty_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_project_contract_id_e8fd3c8e",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "project_contract_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_project_contract_type_id_0a5b7cbb",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "project_contract_type_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_project_id_d5ecf069",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_resource_and_expenditure_id_6baa8522",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "resource_and_expenditure_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_slave_company_id_d30d38dd",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "slave_company_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_slave_id_bcc29d21",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "slave_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_store_id_c9d70513",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_voucher_id_37e41a3c",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_voucher_id_initial_eae29ba4",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "voucher_id_initial");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_voucher_item_id_initial_38823caf",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "voucher_item_id_initial");

            migrationBuilder.CreateIndex(
                name: "voucher_item_logs_voucher_log_id_4528bfd9",
                schema: "accounting",
                table: "voucher_item_logs",
                column: "voucher_log_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_standard_descriptions_company_id_c35a06a8",
                schema: "accounting",
                table: "voucher_item_standard_descriptions",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "voucher_item_standard_descriptions_company_id_description_40767b2a_uniq",
                schema: "accounting",
                table: "voucher_item_standard_descriptions",
                columns: new[] { "company_id", "description" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [description] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "voucher_items_bank_branch_for_payable_doc_id_305a9b46",
                schema: "accounting",
                table: "voucher_items",
                column: "bank_branch_for_payable_doc_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_bank_branch_for_receivable_doc_id_7e2ff8a1",
                schema: "accounting",
                table: "voucher_items",
                column: "bank_branch_for_receivable_doc_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_bank_for_payable_doc_id_e602a063",
                schema: "accounting",
                table: "voucher_items",
                column: "bank_for_payable_doc_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_bank_for_receivable_doc_id_5b97c79f",
                schema: "accounting",
                table: "voucher_items",
                column: "bank_for_receivable_doc_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_bank_operation_bank_account_id_5fe511b5",
                schema: "accounting",
                table: "voucher_items",
                column: "bank_operation_bank_account_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_bank_operation_type_id_2ea895a7",
                schema: "accounting",
                table: "voucher_items",
                column: "bank_operation_type_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_branch_id_e26c223a",
                schema: "accounting",
                table: "voucher_items",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_cashier_period_id_a6ab7714",
                schema: "accounting",
                table: "voucher_items",
                column: "cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_company_id_6d32d694",
                schema: "accounting",
                table: "voucher_items",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_company_unit_id_0a963bbd",
                schema: "accounting",
                table: "voucher_items",
                column: "company_unit_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_cost_center_id_4afbb6e5",
                schema: "accounting",
                table: "voucher_items",
                column: "cost_center_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_cross_reference_id_4dd4e885_uniq",
                schema: "accounting",
                table: "voucher_items",
                column: "cross_reference_id",
                unique: true,
                filter: "([cross_reference_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "voucher_items_currency_id_85cae847",
                schema: "accounting",
                table: "voucher_items",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_id_initial_3062338f_uniq",
                schema: "accounting",
                table: "voucher_items",
                column: "id_initial",
                unique: true,
                filter: "([id_initial] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "voucher_items_ledger_id_439dcc5d",
                schema: "accounting",
                table: "voucher_items",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_manual_float_1_id_a2c88b6e",
                schema: "accounting",
                table: "voucher_items",
                column: "manual_float_1_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_manual_float_2_id_60bbb623",
                schema: "accounting",
                table: "voucher_items",
                column: "manual_float_2_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_manual_float_3_id_4f9d7506",
                schema: "accounting",
                table: "voucher_items",
                column: "manual_float_3_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_manual_float_4_id_35e1cb30",
                schema: "accounting",
                table: "voucher_items",
                column: "manual_float_4_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_manual_float_5_id_a5393d5c",
                schema: "accounting",
                table: "voucher_items",
                column: "manual_float_5_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_manual_float_6_id_a56fc8dc",
                schema: "accounting",
                table: "voucher_items",
                column: "manual_float_6_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_manual_float_7_id_858fb1f1",
                schema: "accounting",
                table: "voucher_items",
                column: "manual_float_7_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_manual_float_8_id_a738253d",
                schema: "accounting",
                table: "voucher_items",
                column: "manual_float_8_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_master_id_f8105cae",
                schema: "accounting",
                table: "voucher_items",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_period_id_3525580e",
                schema: "accounting",
                table: "voucher_items",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_person_id_754c8c99",
                schema: "accounting",
                table: "voucher_items",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_petty_cashier_period_id_df390d00",
                schema: "accounting",
                table: "voucher_items",
                column: "petty_cashier_period_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_project_contract_id_90384ab2",
                schema: "accounting",
                table: "voucher_items",
                column: "project_contract_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_project_contract_type_id_4a970f8e",
                schema: "accounting",
                table: "voucher_items",
                column: "project_contract_type_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_project_id_420c3c83",
                schema: "accounting",
                table: "voucher_items",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_resource_and_expenditure_id_c457dd03",
                schema: "accounting",
                table: "voucher_items",
                column: "resource_and_expenditure_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_slave_company_id_51b34aa7",
                schema: "accounting",
                table: "voucher_items",
                column: "slave_company_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_slave_id_36e4e833",
                schema: "accounting",
                table: "voucher_items",
                column: "slave_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_store_id_76c76e09",
                schema: "accounting",
                table: "voucher_items",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "voucher_items_voucher_id_e75336c1",
                schema: "accounting",
                table: "voucher_items",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_branch_id_099c72df",
                schema: "accounting",
                table: "voucher_logs",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_company_id_9a183a61",
                schema: "accounting",
                table: "voucher_logs",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_ledger_id_b35b1cc4",
                schema: "accounting",
                table: "voucher_logs",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_modifier_id_b948dfef",
                schema: "accounting",
                table: "voucher_logs",
                column: "modifier_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_old_branch_id_fbd37c8c",
                schema: "accounting",
                table: "voucher_logs",
                column: "old_branch_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_old_voucher_type_id_e9551d26",
                schema: "accounting",
                table: "voucher_logs",
                column: "old_voucher_type_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_period_id_02857e11",
                schema: "accounting",
                table: "voucher_logs",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_voucher_id_34970cb1",
                schema: "accounting",
                table: "voucher_logs",
                column: "voucher_id");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_voucher_id_initial_60e0fc0b",
                schema: "accounting",
                table: "voucher_logs",
                column: "voucher_id_initial");

            migrationBuilder.CreateIndex(
                name: "voucher_logs_voucher_type_id_5ba5b90d",
                schema: "accounting",
                table: "voucher_logs",
                column: "voucher_type_id");

            migrationBuilder.CreateIndex(
                name: "UQ__voucher___2E0A7AA62E206246",
                schema: "accounting",
                table: "voucher_types",
                column: "name_fa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "vouchers_branch_id_f6f281ae",
                schema: "accounting",
                table: "vouchers",
                column: "branch_id");

            migrationBuilder.CreateIndex(
                name: "vouchers_company_id_87927ee9",
                schema: "accounting",
                table: "vouchers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "vouchers_company_id_ledger_id_period_id_number_10cfc9fc_uniq",
                schema: "accounting",
                table: "vouchers",
                columns: new[] { "company_id", "ledger_id", "period_id", "number" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [period_id] IS NOT NULL AND [number] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "vouchers_creator_id_b7f0b4f8",
                schema: "accounting",
                table: "vouchers",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "vouchers_last_modifier_id_61732409",
                schema: "accounting",
                table: "vouchers",
                column: "last_modifier_id");

            migrationBuilder.CreateIndex(
                name: "vouchers_ledger_id_25e3f63d",
                schema: "accounting",
                table: "vouchers",
                column: "ledger_id");

            migrationBuilder.CreateIndex(
                name: "vouchers_module_id_65b2cf76",
                schema: "accounting",
                table: "vouchers",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "vouchers_period_id_800ded6b",
                schema: "accounting",
                table: "vouchers",
                column: "period_id");

            migrationBuilder.CreateIndex(
                name: "vouchers_type_id_05bcc3a6",
                schema: "accounting",
                table: "vouchers",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_set_items",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "balance_related_account_details",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "bank_templates",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "closing_pattern_slave_companies",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "closing_pattern_temporary_accounts",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "closing_patterns",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "contradiction_items",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ledger_period_company_settings",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "settings",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "slave_account_standard_descriptions",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "trash_voucher_item_attaches",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_buys",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_contractor_infos",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_employer_infos",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_exportations",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_importations",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_lease_agreements",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_pre_sells",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_sells",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_wages",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "voucher_item_attaches",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "voucher_item_logs",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "voucher_item_standard_descriptions",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "account_sets",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "contradictions",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ledger_period_companies",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "trash_voucher_items",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ttms_product_types",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "voucher_items",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "voucher_logs",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "data_imports",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ledger_periods",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "trash_vouchers",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "manual_float_accounts",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "resource_and_expenditures",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "slave_account_companies",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "vouchers",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "float_account_types",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "slave_accounts",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "periods",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "voucher_types",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "account_groups",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "master_accounts",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "account_categories",
                schema: "accounting");

            migrationBuilder.DropTable(
                name: "ledgers",
                schema: "accounting");
        }
    }
}
