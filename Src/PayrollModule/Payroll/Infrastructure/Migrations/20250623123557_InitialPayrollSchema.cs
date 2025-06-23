using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialPayrollSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "annual_benefit_settings",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "benefit_calculations",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "deduction_calculations",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "missions",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "personnel_contract_items",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "personnel_loans",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "personnel_supplementary_insurance_items",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "salary_calculation_factors",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "salary_increase_formulas",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "tax_table_items",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "benefits",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "deductions",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "work_records",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "loans",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "personnel_supplementary_insurances",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "supplementary_insurances",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "employment_contract_template_items",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "salary_increases",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "tax_tables",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "benefit_groups",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "deduction_groups",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "personnel_contracts",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "employment_contract_template_item_groups",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "employment_contract_templates",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "personnel_financial_information",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "personnel_insurances",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "personnel_jobs",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "insurance_workshops",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "employment_groups",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "tax_workshops",
                schema: "payroll");
        }
    }
}
