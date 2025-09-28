using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateSharedSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shared");

            migrationBuilder.CreateTable(
                name: "bank_branches",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<int>(type: "int", nullable: false),
                    branch_number = table.Column<int>(type: "int", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    bank_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bank_bra__3213E83F897FA9C3", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "branches",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    is_static = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__branches__3213E83F5CEAB5D1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cashiers",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cashiers__3213E83FDA6EC626", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company_settings",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cost_center_level = table.Column<int>(type: "int", nullable: false),
                    company_unit_level = table.Column<int>(type: "int", nullable: false),
                    company_product_level = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__company___3213E83F58F5955D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company_units",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level = table.Column<short>(type: "smallint", nullable: false),
                    last_level = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__company___3213E83F1DB44160", x => x.id);
                    table.ForeignKey(
                        name: "company_units_parent_id_c41b621a_fk_company_units_id",
                        column: x => x.parent_id,
                        principalSchema: "shared",
                        principalTable: "company_units",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cost_centers",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level = table.Column<short>(type: "smallint", nullable: false),
                    last_level = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cost_cen__3213E83FF0CF4150", x => x.id);
                    table.ForeignKey(
                        name: "cost_centers_parent_id_eba90f6c_fk_cost_centers_id",
                        column: x => x.parent_id,
                        principalSchema: "shared",
                        principalTable: "cost_centers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "person_companies",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__person_c__3213E83F638FAE93", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_group_members",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    member_id = table.Column<int>(type: "int", nullable: false),
                    person_group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__person_g__3213E83FDFED9134", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "petty_cashiers",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    person_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__petty_ca__3213E83F06194128", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    level = table.Column<short>(type: "smallint", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    first_measurement_id = table.Column<int>(type: "int", nullable: true),
                    forth_measurement_id = table.Column<int>(type: "int", nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    second_measurement_id = table.Column<int>(type: "int", nullable: true),
                    third_measurement_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__products__3213E83F9A5A602E", x => x.id);
                    table.ForeignKey(
                        name: "products_parent_id_6d11d0e1_fk_products_id",
                        column: x => x.parent_id,
                        principalSchema: "shared",
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "report_templates",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    report_key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    template = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    creator_id = table.Column<int>(type: "int", nullable: false),
                    module_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__report_t__3213E83F0D6D3A08", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "restrictions",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__restrict__3213E83F533B4486", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_static = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__roles__3213E83F56963A72", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bank_accounts",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    iban = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
                    account_number = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    card_number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    account_owner = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    account_type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    account_opening_date = table.Column<DateOnly>(type: "date", nullable: true),
                    signature_expiry_date = table.Column<DateOnly>(type: "date", nullable: true),
                    national_code = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    bank_id = table.Column<int>(type: "int", nullable: false),
                    bank_branch_id = table.Column<int>(type: "int", nullable: false),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    currency_id = table.Column<int>(type: "int", nullable: true),
                    type_id = table.Column<int>(type: "int", nullable: false),
                    user_signatory_1_id = table.Column<int>(type: "int", nullable: true),
                    user_signatory_2_id = table.Column<int>(type: "int", nullable: true),
                    user_signatory_3_id = table.Column<int>(type: "int", nullable: true),
                    user_signatory_4_id = table.Column<int>(type: "int", nullable: true),
                    user_signatory_5_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bank_acc__3213E83FEDB13419", x => x.id);
                    table.ForeignKey(
                        name: "bank_accounts_bank_branch_id_f4462cb3_fk_bank_branches_id",
                        column: x => x.bank_branch_id,
                        principalSchema: "shared",
                        principalTable: "bank_branches",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "foos",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__foos__3213E83FB3E52432", x => x.id);
                    table.ForeignKey(
                        name: "foos_product_id_64577d3d_fk_products_id",
                        column: x => x.product_id,
                        principalSchema: "shared",
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Purchase__3213E83F706A62B0", x => x.id);
                    table.ForeignKey(
                        name: "Purchases_product_id_d1413dc0_fk_products_id",
                        column: x => x.product_id,
                        principalSchema: "shared",
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "sales",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sales__3213E83F036DC29E", x => x.id);
                    table.ForeignKey(
                        name: "sales_product_id_a179a813_fk_products_id",
                        column: x => x.product_id,
                        principalSchema: "shared",
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_members",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    authorized_users = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    member_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__role_mem__3213E83FB8167B1B", x => x.id);
                    table.ForeignKey(
                        name: "role_members_role_id_3742ecb7_fk_roles_id",
                        column: x => x.role_id,
                        principalSchema: "shared",
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "shared",
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
                    module_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__role_per__3213E83F54061F44", x => x.id);
                    table.ForeignKey(
                        name: "role_permissions_role_id_216516f2_fk_roles_id",
                        column: x => x.role_id,
                        principalSchema: "shared",
                        principalTable: "roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_permission_commands",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    command_key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    value = table.Column<bool>(type: "bit", nullable: false),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    entity_type_command_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    role_permission_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__role_per__3213E83F15286D44", x => x.id);
                    table.ForeignKey(
                        name: "role_permission_commands_role_id_5d1758cb_fk_roles_id",
                        column: x => x.role_id,
                        principalSchema: "shared",
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "role_permission_commands_role_permission_id_40a8626f_fk_role_permissions_id",
                        column: x => x.role_permission_id,
                        principalSchema: "shared",
                        principalTable: "role_permissions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "role_permission_constraints",
                schema: "shared",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    field_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    read = table.Column<bool>(type: "bit", nullable: false),
                    create = table.Column<bool>(type: "bit", nullable: false),
                    edit = table.Column<bool>(type: "bit", nullable: false),
                    print = table.Column<bool>(type: "bit", nullable: false),
                    imp = table.Column<bool>(type: "bit", nullable: false),
                    exp = table.Column<bool>(type: "bit", nullable: false),
                    entity_type_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    role_permission_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__role_per__3213E83FE3441D6A", x => x.id);
                    table.ForeignKey(
                        name: "role_permission_constraints_role_id_3ad70fc8_fk_roles_id",
                        column: x => x.role_id,
                        principalSchema: "shared",
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "role_permission_constraints_role_permission_id_678eb8f6_fk_role_permissions_id",
                        column: x => x.role_permission_id,
                        principalSchema: "shared",
                        principalTable: "role_permissions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "bank_accounts_bank_branch_id_f4462cb3",
                schema: "shared",
                table: "bank_accounts",
                column: "bank_branch_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_bank_id_76519d0e",
                schema: "shared",
                table: "bank_accounts",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_company_id_a0fdfe1f",
                schema: "shared",
                table: "bank_accounts",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_company_id_bank_id_account_number_dde4bd1e_uniq",
                schema: "shared",
                table: "bank_accounts",
                columns: new[] { "company_id", "bank_id", "account_number" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [account_number] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_company_id_bank_id_card_number_fbf237d1_uniq",
                schema: "shared",
                table: "bank_accounts",
                columns: new[] { "company_id", "bank_id", "card_number" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [card_number] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_company_id_bank_id_iban_077a9abd_uniq",
                schema: "shared",
                table: "bank_accounts",
                columns: new[] { "company_id", "bank_id", "iban" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [iban] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_currency_id_932d5611",
                schema: "shared",
                table: "bank_accounts",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_type_id_50bc1431",
                schema: "shared",
                table: "bank_accounts",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_user_signatory_1_id_0cad9553",
                schema: "shared",
                table: "bank_accounts",
                column: "user_signatory_1_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_user_signatory_2_id_a7593d15",
                schema: "shared",
                table: "bank_accounts",
                column: "user_signatory_2_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_user_signatory_3_id_d48c50a6",
                schema: "shared",
                table: "bank_accounts",
                column: "user_signatory_3_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_user_signatory_4_id_da60748f",
                schema: "shared",
                table: "bank_accounts",
                column: "user_signatory_4_id");

            migrationBuilder.CreateIndex(
                name: "bank_accounts_user_signatory_5_id_0c19f838",
                schema: "shared",
                table: "bank_accounts",
                column: "user_signatory_5_id");

            migrationBuilder.CreateIndex(
                name: "bank_branches_bank_id_5eff2bac",
                schema: "shared",
                table: "bank_branches",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "bank_branches_company_id_4a52d712",
                schema: "shared",
                table: "bank_branches",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "bank_branches_company_id_bank_id_branch_number_f4f0441e_uniq",
                schema: "shared",
                table: "bank_branches",
                columns: new[] { "company_id", "bank_id", "branch_number" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [branch_number] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "bank_branches_company_id_bank_id_name_e96f3efa_uniq",
                schema: "shared",
                table: "bank_branches",
                columns: new[] { "company_id", "bank_id", "name" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "bank_branches_company_id_code_edffeec5_uniq",
                schema: "shared",
                table: "bank_branches",
                columns: new[] { "company_id", "code" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "branches_company_id_c17fd5ca",
                schema: "shared",
                table: "branches",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "branches_company_id_code_2a76aa26_uniq",
                schema: "shared",
                table: "branches",
                columns: new[] { "company_id", "code" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "branches_company_id_name_0b77f2d0_uniq",
                schema: "shared",
                table: "branches",
                columns: new[] { "company_id", "name" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "cashiers_company_id_e24f4c24",
                schema: "shared",
                table: "cashiers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "cashiers_company_id_person_id_f0a0aa02_uniq",
                schema: "shared",
                table: "cashiers",
                columns: new[] { "company_id", "person_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [person_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "cashiers_creator_id_cd8bd2ff",
                schema: "shared",
                table: "cashiers",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "cashiers_person_id_2b0579d0",
                schema: "shared",
                table: "cashiers",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "UQ__company___3E2672348C06AA9B",
                schema: "shared",
                table: "company_settings",
                column: "company_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "company_units_company_id_0a433407",
                schema: "shared",
                table: "company_units",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "company_units_company_id_code_af9699c4_uniq",
                schema: "shared",
                table: "company_units",
                columns: new[] { "company_id", "code" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "company_units_company_id_name_parent_id_131855b6_uniq",
                schema: "shared",
                table: "company_units",
                columns: new[] { "company_id", "name", "parent_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [name] IS NOT NULL AND [parent_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "company_units_parent_id_c41b621a",
                schema: "shared",
                table: "company_units",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "cost_centers_company_id_5390b4ec",
                schema: "shared",
                table: "cost_centers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "cost_centers_company_id_code_153ce7ab_uniq",
                schema: "shared",
                table: "cost_centers",
                columns: new[] { "company_id", "code" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "cost_centers_company_id_name_parent_id_44518991_uniq",
                schema: "shared",
                table: "cost_centers",
                columns: new[] { "company_id", "name", "parent_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [name] IS NOT NULL AND [parent_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "cost_centers_parent_id_eba90f6c",
                schema: "shared",
                table: "cost_centers",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "foos_company_id_1a5e3040",
                schema: "shared",
                table: "foos",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "foos_product_id_64577d3d",
                schema: "shared",
                table: "foos",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "person_companies_company_id_8d893c9c",
                schema: "shared",
                table: "person_companies",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "person_companies_person_id_01c9a592",
                schema: "shared",
                table: "person_companies",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "person_companies_person_id_company_id_7ccc3e32_uniq",
                schema: "shared",
                table: "person_companies",
                columns: new[] { "person_id", "company_id" },
                unique: true,
                filter: "([person_id] IS NOT NULL AND [company_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "person_group_members_company_id_730c5fff",
                schema: "shared",
                table: "person_group_members",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "person_group_members_company_id_person_group_id_member_id_b5c20e26_uniq",
                schema: "shared",
                table: "person_group_members",
                columns: new[] { "company_id", "person_group_id", "member_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [person_group_id] IS NOT NULL AND [member_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "person_group_members_member_id_fb0c85d9",
                schema: "shared",
                table: "person_group_members",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "person_group_members_person_group_id_34698b20",
                schema: "shared",
                table: "person_group_members",
                column: "person_group_id");

            migrationBuilder.CreateIndex(
                name: "petty_cashiers_company_id_adaef187",
                schema: "shared",
                table: "petty_cashiers",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "petty_cashiers_company_id_person_id_d434ff7c_uniq",
                schema: "shared",
                table: "petty_cashiers",
                columns: new[] { "company_id", "person_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [person_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "petty_cashiers_creator_id_d570b446",
                schema: "shared",
                table: "petty_cashiers",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "petty_cashiers_person_id_5a0c025f",
                schema: "shared",
                table: "petty_cashiers",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "products_company_id_85cab11e",
                schema: "shared",
                table: "products",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "products_creator_id_f217ad7d",
                schema: "shared",
                table: "products",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "products_first_measurement_id_c9eb56e6",
                schema: "shared",
                table: "products",
                column: "first_measurement_id");

            migrationBuilder.CreateIndex(
                name: "products_forth_measurement_id_2571e159",
                schema: "shared",
                table: "products",
                column: "forth_measurement_id");

            migrationBuilder.CreateIndex(
                name: "products_parent_id_6d11d0e1",
                schema: "shared",
                table: "products",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "products_second_measurement_id_9e456e28",
                schema: "shared",
                table: "products",
                column: "second_measurement_id");

            migrationBuilder.CreateIndex(
                name: "products_third_measurement_id_40c75e99",
                schema: "shared",
                table: "products",
                column: "third_measurement_id");

            migrationBuilder.CreateIndex(
                name: "Purchases_company_id_a3e8b6cd",
                schema: "shared",
                table: "Purchases",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "Purchases_product_id_d1413dc0",
                schema: "shared",
                table: "Purchases",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "report_templates_company_id_d546fa98",
                schema: "shared",
                table: "report_templates",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "report_templates_company_id_report_key_name_f3282f81_uniq",
                schema: "shared",
                table: "report_templates",
                columns: new[] { "company_id", "report_key", "name" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [report_key] IS NOT NULL AND [name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "report_templates_creator_id_1650956e",
                schema: "shared",
                table: "report_templates",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "report_templates_module_id_e5a4ce8d",
                schema: "shared",
                table: "report_templates",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "restrictions_company_id_e24e8bce",
                schema: "shared",
                table: "restrictions",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "restrictions_company_id_entity_type_id_user_id_2296a366_uniq",
                schema: "shared",
                table: "restrictions",
                columns: new[] { "company_id", "entity_type_id", "user_id" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [entity_type_id] IS NOT NULL AND [user_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "restrictions_entity_type_id_5861bb50",
                schema: "shared",
                table: "restrictions",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "restrictions_user_id_1ef45563",
                schema: "shared",
                table: "restrictions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "role_members_company_id_c28413ce",
                schema: "shared",
                table: "role_members",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "role_members_member_id_0a61f922",
                schema: "shared",
                table: "role_members",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "role_members_role_id_3742ecb7",
                schema: "shared",
                table: "role_members",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_members_role_id_member_id_22328589_uniq",
                schema: "shared",
                table: "role_members",
                columns: new[] { "role_id", "member_id" },
                unique: true,
                filter: "([role_id] IS NOT NULL AND [member_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "role_permission_commands_entity_type_command_id_9e625b38",
                schema: "shared",
                table: "role_permission_commands",
                column: "entity_type_command_id");

            migrationBuilder.CreateIndex(
                name: "role_permission_commands_entity_type_id_a3513f52",
                schema: "shared",
                table: "role_permission_commands",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "role_permission_commands_role_id_5d1758cb",
                schema: "shared",
                table: "role_permission_commands",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_permission_commands_role_permission_id_40a8626f",
                schema: "shared",
                table: "role_permission_commands",
                column: "role_permission_id");

            migrationBuilder.CreateIndex(
                name: "role_permission_commands_role_permission_id_entity_type_command_id_9f7be0a3_uniq",
                schema: "shared",
                table: "role_permission_commands",
                columns: new[] { "role_permission_id", "entity_type_command_id" },
                unique: true,
                filter: "([role_permission_id] IS NOT NULL AND [entity_type_command_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "role_permission_constraints_entity_type_id_183e495f",
                schema: "shared",
                table: "role_permission_constraints",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "role_permission_constraints_role_id_3ad70fc8",
                schema: "shared",
                table: "role_permission_constraints",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_permission_constraints_role_permission_id_678eb8f6",
                schema: "shared",
                table: "role_permission_constraints",
                column: "role_permission_id");

            migrationBuilder.CreateIndex(
                name: "role_permission_constraints_role_permission_id_field_name_6671fb32_uniq",
                schema: "shared",
                table: "role_permission_constraints",
                columns: new[] { "role_permission_id", "field_name" },
                unique: true,
                filter: "([role_permission_id] IS NOT NULL AND [field_name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "role_permissions_entity_type_id_74eb6fa5",
                schema: "shared",
                table: "role_permissions",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "role_permissions_module_id_f6701517",
                schema: "shared",
                table: "role_permissions",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "role_permissions_role_id_216516f2",
                schema: "shared",
                table: "role_permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_permissions_role_id_entity_type_id_285eba22_uniq",
                schema: "shared",
                table: "role_permissions",
                columns: new[] { "role_id", "entity_type_id" },
                unique: true,
                filter: "([role_id] IS NOT NULL AND [entity_type_id] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "roles_company_id_f4c539e9",
                schema: "shared",
                table: "roles",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "roles_company_id_name_35c99075_uniq",
                schema: "shared",
                table: "roles",
                columns: new[] { "company_id", "name" },
                unique: true,
                filter: "([company_id] IS NOT NULL AND [name] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "sales_company_id_1480331f",
                schema: "shared",
                table: "sales",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "sales_product_id_a179a813",
                schema: "shared",
                table: "sales",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_accounts",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "branches",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "cashiers",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "company_settings",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "company_units",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "cost_centers",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "foos",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "person_companies",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "person_group_members",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "petty_cashiers",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "Purchases",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "report_templates",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "restrictions",
                schema: "shared");

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
                name: "sales",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "bank_branches",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "products",
                schema: "shared");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "shared");
        }
    }
}
