using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSaleAccountingFromWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportSaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ExportSaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ReturnFromSaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "SaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExportSaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportSaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportSaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportSaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExportSaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromSaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromSaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromSaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnFromSaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReturnFromSaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleAccountDetailed1Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleAccountDetailed2Value",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleAccountMasterValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleAccountSlaveValue",
                schema: "Warehouse",
                table: "Warehouse",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleSlaveAccountCompanyId",
                schema: "Warehouse",
                table: "Warehouse",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
