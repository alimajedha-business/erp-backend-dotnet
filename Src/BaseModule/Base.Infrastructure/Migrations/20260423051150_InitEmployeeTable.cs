using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "LevelNo",
            //    schema: "Warehouse",
            //    table: "WarehouseLocation",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "currencies",
            //    schema: "General",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Name2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Code = table.Column<int>(type: "int", nullable: false),
            //        Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Iso = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DecimalPlaces = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        RoundMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FractionalCurrencyUnit = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_currencies", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "MaritalStatus",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaritalStatus_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            //migrationBuilder.CreateTable(
            //    name: "military_service_statuses",
            //    schema: "General",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_military_service_statuses", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "religions",
            //    schema: "General",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_religions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "countries",
            //    schema: "General",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Code = table.Column<int>(type: "int", nullable: false),
            //        CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        TaxCode = table.Column<int>(type: "int", nullable: true),
            //        Iso = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_countries", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_countries_currencies_CurrencyId",
            //            column: x => x.CurrencyId,
            //            principalSchema: "General",
            //            principalTable: "currencies",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "provinces",
            //    schema: "General",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Code = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_provinces", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_provinces_countries_CountryId",
            //            column: x => x.CountryId,
            //            principalSchema: "General",
            //            principalTable: "countries",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
                //});

            //migrationBuilder.CreateTable(
            //    name: "cities",
            //    schema: "General",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Code = table.Column<int>(type: "int", nullable: false),
            //        Code2 = table.Column<int>(type: "int", nullable: true),
            //        Code3 = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_cities", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_cities_provinces_ProvinceId",
            //            column: x => x.ProvinceId,
            //            principalSchema: "General",
            //            principalTable: "provinces",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "persons",
            //    schema: "General",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Typ = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        NaturalFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Code = table.Column<long>(type: "bigint", nullable: false),
            //        EconomicCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EconomicCodeOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsInternalCitizenship = table.Column<bool>(type: "bit", nullable: false),
            //        CitizenCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NaturalFatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NaturalNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BirthCityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        NaturalBirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        NaturalSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LegalManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LegalManagerFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LegalNationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LegalRegisterNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LegalEstablishmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsGovernmental = table.Column<bool>(type: "bit", nullable: false),
            //        ReligionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_persons", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_persons_cities_BirthCityId",
            //            column: x => x.BirthCityId,
            //            principalSchema: "General",
            //            principalTable: "cities",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_persons_religions_ReligionId",
            //            column: x => x.ReligionId,
            //            principalSchema: "General",
            //            principalTable: "religions",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "HCM",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MilitaryServiceStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaritalStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarriageDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "General",
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_MaritalStatus_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalSchema: "HCM",
                        principalTable: "MaritalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_military_service_statuses_MilitaryServiceStatusId",
                        column: x => x.MilitaryServiceStatusId,
                        principalSchema: "General",
                        principalTable: "military_service_statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "General",
                        principalTable: "persons",
                        principalColumn: "Id");
                });

            //migrationBuilder.AddCheckConstraint(
            //    name: "CK_CategoryLevelConstraint_CodeLength",
            //    schema: "Warehouse",
            //    table: "CategoryLevelConstraint",
            //    sql: "CodeLength BETWEEN 1 AND 5");

            //migrationBuilder.CreateIndex(
            //    name: "IX_cities_ProvinceId",
            //    schema: "General",
            //    table: "cities",
            //    column: "ProvinceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_countries_CurrencyId",
            //    schema: "General",
            //    table: "countries",
            //    column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_IsDeleted",
                schema: "HCM",
                table: "Employee",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_MaritalStatus",
                schema: "HCM",
                table: "Employee",
                column: "MaritalStatusId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Employee_MilitaryServiceStatus",
            //    schema: "HCM",
            //    table: "Employee",
            //    column: "MilitaryServiceStatusId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Employee_Person",
            //    schema: "HCM",
            //    table: "Employee",
            //    column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "UX_Employee_Company_Code",
                schema: "HCM",
                table: "Employee",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatus_IsDeleted",
                schema: "HCM",
                table: "MaritalStatus",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "UX_MaritalStatus_CompanyId_Type",
                schema: "HCM",
                table: "MaritalStatus",
                columns: new[] { "CompanyId", "Type" },
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_persons_BirthCityId",
            //    schema: "General",
            //    table: "persons",
            //    column: "BirthCityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_persons_ReligionId",
            //    schema: "General",
            //    table: "persons",
            //    column: "ReligionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_provinces_CountryId",
            //    schema: "General",
            //    table: "provinces",
            //    column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "MaritalStatus",
                schema: "HCM");

            migrationBuilder.DropTable(
                name: "military_service_statuses",
                schema: "General");

            migrationBuilder.DropTable(
                name: "persons",
                schema: "General");

            migrationBuilder.DropTable(
                name: "cities",
                schema: "General");

            migrationBuilder.DropTable(
                name: "religions",
                schema: "General");

            migrationBuilder.DropTable(
                name: "provinces",
                schema: "General");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "General");

            migrationBuilder.DropTable(
                name: "currencies",
                schema: "General");

            //migrationBuilder.DropCheckConstraint(
            //    name: "CK_CategoryLevelConstraint_CodeLength",
            //    schema: "Warehouse",
            //    table: "CategoryLevelConstraint");

            //migrationBuilder.DropColumn(
            //    name: "LevelNo",
            //    schema: "Warehouse",
            //    table: "WarehouseLocation");
        }
    }
}
