using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NGErp.Base.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorPersoEntityAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF COL_LENGTH(N'General.persons', N'Typ') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'typ') IS NULL
                    EXEC sp_rename N'[General].[persons].[Typ]', N'typ', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'Photo') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'photo') IS NULL
                    EXEC sp_rename N'[General].[persons].[Photo]', N'photo', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'Name') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'name') IS NULL
                    EXEC sp_rename N'[General].[persons].[Name]', N'name', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'Code') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'code') IS NULL
                    EXEC sp_rename N'[General].[persons].[Code]', N'code', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'Id') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'id') IS NULL
                    EXEC sp_rename N'[General].[persons].[Id]', N'id', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'ReligionId') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'religion_id') IS NULL
                    EXEC sp_rename N'[General].[persons].[ReligionId]', N'religion_id', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'PassportNumber') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'passport_number') IS NULL
                    EXEC sp_rename N'[General].[persons].[PassportNumber]', N'passport_number', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'NaturalSex') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'natural_sex') IS NULL
                    EXEC sp_rename N'[General].[persons].[NaturalSex]', N'natural_sex', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'NaturalNationalCode') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'natural_national_code') IS NULL
                    EXEC sp_rename N'[General].[persons].[NaturalNationalCode]', N'natural_national_code', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'NaturalFatherName') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'natural_father_name') IS NULL
                    EXEC sp_rename N'[General].[persons].[NaturalFatherName]', N'natural_father_name', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'NaturalFamily') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'natural_family') IS NULL
                    EXEC sp_rename N'[General].[persons].[NaturalFamily]', N'natural_family', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'NaturalBirthDate') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'natural_birth_date') IS NULL
                    EXEC sp_rename N'[General].[persons].[NaturalBirthDate]', N'natural_birth_date', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'LegalRegisterNo') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'legal_register_no') IS NULL
                    EXEC sp_rename N'[General].[persons].[LegalRegisterNo]', N'legal_register_no', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'LegalNationalCode') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'legal_national_code') IS NULL
                    EXEC sp_rename N'[General].[persons].[LegalNationalCode]', N'legal_national_code', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'LegalManagerName') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'legal_manager_name') IS NULL
                    EXEC sp_rename N'[General].[persons].[LegalManagerName]', N'legal_manager_name', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'LegalManagerFamily') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'legal_manager_family') IS NULL
                    EXEC sp_rename N'[General].[persons].[LegalManagerFamily]', N'legal_manager_family', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'LegalEstablishmentDate') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'legal_establishment_date') IS NULL
                    EXEC sp_rename N'[General].[persons].[LegalEstablishmentDate]', N'legal_establishment_date', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'IsInternalCitizenship') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'is_internal_citizenship') IS NULL
                    EXEC sp_rename N'[General].[persons].[IsInternalCitizenship]', N'is_internal_citizenship', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'IsGovernmental') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'is_governmental') IS NULL
                    EXEC sp_rename N'[General].[persons].[IsGovernmental]', N'is_governmental', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'IdNumber') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'id_number') IS NULL
                    EXEC sp_rename N'[General].[persons].[IdNumber]', N'id_number', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'EconomicCodeOld') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'economic_code_old') IS NULL
                    EXEC sp_rename N'[General].[persons].[EconomicCodeOld]', N'economic_code_old', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'EconomicCode') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'economic_code') IS NULL
                    EXEC sp_rename N'[General].[persons].[EconomicCode]', N'economic_code', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'CitizenCode') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'citizen_code') IS NULL
                    EXEC sp_rename N'[General].[persons].[CitizenCode]', N'citizen_code', N'COLUMN';
                IF COL_LENGTH(N'General.persons', N'BirthCityId') IS NOT NULL
                   AND COL_LENGTH(N'General.persons', N'birth_city_id') IS NULL
                    EXEC sp_rename N'[General].[persons].[BirthCityId]', N'birth_city_id', N'COLUMN';
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
