using System;
using System.Collections.Generic;
using General.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.DataAccess;

public partial class GeneralDbContext : DbContext
{
    public GeneralDbContext()
    {
    }

    public GeneralDbContext(DbContextOptions<GeneralDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankAccountType> BankAccountTypes { get; set; }

    public virtual DbSet<BankOperationType> BankOperationTypes { get; set; }

    public virtual DbSet<BankStatementPattern> BankStatementPatterns { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyAdmin> CompanyAdmins { get; set; }

    public virtual DbSet<CompanyModule> CompanyModules { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Domain.Entities.Domain> Domains { get; set; }

    public virtual DbSet<EducationalDegree> EducationalDegrees { get; set; }

    public virtual DbSet<EmploymentContractDescription> EmploymentContractDescriptions { get; set; }

    public virtual DbSet<EmploymentContractTitle> EmploymentContractTitles { get; set; }

    public virtual DbSet<EntityType> EntityTypes { get; set; }

    public virtual DbSet<EntityTypeCommand> EntityTypeCommands { get; set; }

    public virtual DbSet<EntityTypeConstraint> EntityTypeConstraints { get; set; }

    public virtual DbSet<EntityTypeDependency> EntityTypeDependencies { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Domain.Entities.File> Files { get; set; }

    public virtual DbSet<FileEntity> FileEntities { get; set; }

    public virtual DbSet<ForeignLanguage> ForeignLanguages { get; set; }

    public virtual DbSet<HousingStatus> HousingStatuses { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<JobRank> JobRanks { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<MilitaryServiceStatus> MilitaryServiceStatuses { get; set; }

    public virtual DbSet<MissionType> MissionTypes { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModulePersonGroup> ModulePersonGroups { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<PersonAddress> PersonAddresses { get; set; }

    public virtual DbSet<PersonBankAccount> PersonBankAccounts { get; set; }

    public virtual DbSet<PersonEducationalDegree> PersonEducationalDegrees { get; set; }

    public virtual DbSet<PersonEmail> PersonEmails { get; set; }

    public virtual DbSet<PersonFaxis> PersonFaxes { get; set; }

    public virtual DbSet<PersonGroup> PersonGroups { get; set; }

    public virtual DbSet<PersonMobile> PersonMobiles { get; set; }

    public virtual DbSet<PersonPhone> PersonPhones { get; set; }

    public virtual DbSet<PersonRelative> PersonRelatives { get; set; }

    public virtual DbSet<PersonWebsite> PersonWebsites { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Religion> Religions { get; set; }

    public virtual DbSet<SelectLog> SelectLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserConfig> UserConfigs { get; set; }

    public virtual DbSet<WorkDepartment> WorkDepartments { get; set; }

    public virtual DbSet<WorkOperation> WorkOperations { get; set; }

    public virtual DbSet<Workplace> Workplaces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\sql19;Database=NGERP;User Id=sa;Password=123;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Persian_100_CI_AI");

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__areas__3213E83F55F5B37B");

            entity.HasOne(d => d.Creator).WithMany(p => p.Areas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("areas_creator_id_9f75521a_fk_users_id");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__banks__3213E83F860C06C1");
        });

        modelBuilder.Entity<BankAccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bank_acc__3213E83F6744CAF0");
        });

        modelBuilder.Entity<BankOperationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bank_ope__3213E83F8731515E");
        });

        modelBuilder.Entity<BankStatementPattern>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bank_sta__3213E83FBC9D1695");

            entity.HasOne(d => d.Bank).WithOne(p => p.BankStatementPattern)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bank_statement_patterns_bank_id_85d1bd4e_fk_banks_id");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cities__3213E83F6830DF8F");

            entity.HasIndex(e => e.Code2, "cities_code2_fc22c31a_uniq")
                .IsUnique()
                .HasFilter("([code2] IS NOT NULL)");

            entity.HasIndex(e => e.Code3, "cities_code3_cfe7131c_uniq")
                .IsUnique()
                .HasFilter("([code3] IS NOT NULL)");

            entity.HasOne(d => d.Province).WithMany(p => p.Cities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cities_province_id_799ae9a0_fk_provinces_id");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__companie__3213E83FB7A94D4E");

            entity.HasOne(d => d.Domain).WithMany(p => p.Companies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("companies_domain_id_51b444a3_fk_domains_id");
        });

        modelBuilder.Entity<CompanyAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__company___3213E83F53708EC0");

            entity.HasIndex(e => new { e.CompanyId, e.AdminId }, "company_admins_company_id_admin_id_53861a81_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [admin_id] IS NOT NULL)");

            entity.HasOne(d => d.Admin).WithMany(p => p.CompanyAdminAdmins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("company_admins_admin_id_7fe4b0ca_fk_users_id");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyAdmins).HasConstraintName("company_admins_company_id_dc4e1bcb_fk_companies_id");

            entity.HasOne(d => d.Creator).WithMany(p => p.CompanyAdminCreators)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("company_admins_creator_id_af4a192b_fk_users_id");
        });

        modelBuilder.Entity<CompanyModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__company___3213E83F1B1C9914");

            entity.HasIndex(e => new { e.CompanyId, e.ModuleId }, "company_modules_company_id_module_id_2dc7a72c_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [module_id] IS NOT NULL)");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyModules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("company_modules_company_id_98c248b4_fk_companies_id");

            entity.HasOne(d => d.Module).WithMany(p => p.CompanyModules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("company_modules_module_id_85bfd47f_fk_modules_id");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__countrie__3213E83FA0D1526C");

            entity.HasIndex(e => e.TaxCode, "countries_tax_code_99e2105c_uniq")
                .IsUnique()
                .HasFilter("([tax_code] IS NOT NULL)");

            entity.HasOne(d => d.Currency).WithMany(p => p.Countries).HasConstraintName("countries_currency_id_3d87434c_fk_currencies_id");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__currenci__3213E83FAD2B263C");
        });

        modelBuilder.Entity<General.Domain.Entities.Domain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__domains__3213E83FF0E52661");

            entity.HasOne(d => d.Language).WithMany(p => p.Domains)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("domains_language_id_a7140b25_fk_languages_id");
        });

        modelBuilder.Entity<EducationalDegree>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__educatio__3213E83FEDB51C60");

            entity.HasOne(d => d.Creator).WithMany(p => p.EducationalDegrees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("educational_degrees_creator_id_693f4fac_fk_users_id");
        });

        modelBuilder.Entity<EmploymentContractDescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employme__3213E83FDBA508FB");

            entity.HasOne(d => d.Creator).WithMany(p => p.EmploymentContractDescriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employment_contract_Descriptions_creator_id_2fcb5dd9_fk_users_id");
        });

        modelBuilder.Entity<EmploymentContractTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employme__3213E83F82C22F82");

            entity.HasOne(d => d.Creator).WithMany(p => p.EmploymentContractTitles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employment_contract_titles_creator_id_f0cd95e9_fk_users_id");
        });

        modelBuilder.Entity<EntityType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__entity_t__3213E83F53BACB44");

            entity.HasIndex(e => new { e.ModuleId, e.Key }, "entity_types_module_id_key_8a42bbcf_uniq")
                .IsUnique()
                .HasFilter("([module_id] IS NOT NULL AND [key] IS NOT NULL)");

            entity.HasIndex(e => new { e.ModuleId, e.NameEn }, "entity_types_module_id_name_en_2858063b_uniq")
                .IsUnique()
                .HasFilter("([module_id] IS NOT NULL AND [name_en] IS NOT NULL)");

            entity.HasIndex(e => new { e.ModuleId, e.NameFa }, "entity_types_module_id_name_fa_060eefd9_uniq")
                .IsUnique()
                .HasFilter("([module_id] IS NOT NULL AND [name_fa] IS NOT NULL)");

            entity.HasOne(d => d.Module).WithMany(p => p.EntityTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entity_types_module_id_55d1da29_fk_modules_id");
        });

        modelBuilder.Entity<EntityTypeCommand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__entity_t__3213E83F70F7B33A");

            entity.HasIndex(e => new { e.EntityTypeId, e.Key }, "entity_type_commands_entity_type_id_key_0d06ffe6_uniq")
                .IsUnique()
                .HasFilter("([entity_type_id] IS NOT NULL AND [key] IS NOT NULL)");

            entity.HasOne(d => d.EntityType).WithMany(p => p.EntityTypeCommands)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entity_type_commands_entity_type_id_c94a0ef5_fk_entity_types_id");
        });

        modelBuilder.Entity<EntityTypeConstraint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__entity_t__3213E83FC1F58758");

            entity.HasIndex(e => new { e.EntityTypeId, e.FieldName }, "entity_type_constraints_entity_type_id_field_name_b4ec14c6_uniq")
                .IsUnique()
                .HasFilter("([entity_type_id] IS NOT NULL AND [field_name] IS NOT NULL)");

            entity.HasOne(d => d.EntityType).WithMany(p => p.EntityTypeConstraints)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entity_type_constraints_entity_type_id_a7b4c730_fk_entity_types_id");
        });

        modelBuilder.Entity<EntityTypeDependency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__entity_t__3213E83F20FF2C75");

            entity.HasOne(d => d.EntityType).WithMany(p => p.EntityTypeDependencyEntityTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entity_type_dependencies_entity_type_id_e631567c_fk_entity_types_id");

            entity.HasOne(d => d.RequiredEntityType).WithMany(p => p.EntityTypeDependencyRequiredEntityTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entity_type_dependencies_required_entity_type_id_64bab813_fk_entity_types_id");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__error_lo__3213E83F9EC25649");

            entity.HasOne(d => d.User).WithMany(p => p.ErrorLogs).HasConstraintName("error_logs_user_id_ac744206_fk_users_id");
        });

        modelBuilder.Entity<General.Domain.Entities.File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__files__3213E83F6974DD91");
        });

        modelBuilder.Entity<FileEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__file_ent__3213E83FF2BFCAC1");

            entity.HasIndex(e => new { e.FileId, e.EntityTypeId, e.ObjectId }, "file_entities_file_id_entity_type_id_object_id_4b21a512_uniq")
                .IsUnique()
                .HasFilter("([file_id] IS NOT NULL AND [entity_type_id] IS NOT NULL AND [object_id] IS NOT NULL)");

            entity.HasOne(d => d.EntityType).WithMany(p => p.FileEntities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("file_entities_entity_type_id_addc8bf3_fk_entity_types_id");

            entity.HasOne(d => d.File).WithMany(p => p.FileEntities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("file_entities_file_id_1c97e002_fk_files_id");
        });

        modelBuilder.Entity<ForeignLanguage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__foreign___3213E83FCB6AAAF4");

            entity.HasOne(d => d.Creator).WithMany(p => p.ForeignLanguages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("foreign_languages_creator_id_f2c0414f_fk_users_id");
        });

        modelBuilder.Entity<HousingStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__housing___3213E83F883A7B34");

            entity.HasOne(d => d.Creator).WithMany(p => p.HousingStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("housing_statuses_creator_id_b7300b34_fk_users_id");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jobs__3213E83F9CDA50FA");

            entity.HasOne(d => d.Creator).WithMany(p => p.Jobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jobs_creator_id_56b9cd05_fk_users_id");
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__job_posi__3213E83F10667423");

            entity.HasOne(d => d.Creator).WithMany(p => p.JobPositions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_positions_creator_id_361c185c_fk_users_id");
        });

        modelBuilder.Entity<JobRank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__job_rank__3213E83FE0C1FE62");

            entity.HasOne(d => d.Creator).WithMany(p => p.JobRanks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_ranks_creator_id_06b134a2_fk_users_id");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__language__3213E83F82D76FFA");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__logs__3213E83F54D67D04");

            entity.HasOne(d => d.Company).WithMany(p => p.Logs).HasConstraintName("logs_company_id_ed2c29a1_fk_companies_id");

            entity.HasOne(d => d.EntityType).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("logs_entity_type_id_87a8fbba_fk_entity_types_id");

            entity.HasOne(d => d.Module).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("logs_module_id_c6447665_fk_modules_id");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("logs_user_id_237f5f83_fk_users_id");
        });

        modelBuilder.Entity<MeasurementUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__measurem__3213E83F2FAD5529");

            entity.HasOne(d => d.Creator).WithMany(p => p.MeasurementUnits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("measurement_units_creator_id_adca9822_fk_users_id");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__menu_ite__3213E83FEE18BEF4");

            entity.HasIndex(e => new { e.ModuleId, e.NameEn, e.ParentId }, "menu_items_module_id_name_en_parent_id_01a9ca6d_uniq")
                .IsUnique()
                .HasFilter("([module_id] IS NOT NULL AND [name_en] IS NOT NULL AND [parent_id] IS NOT NULL)");

            entity.HasIndex(e => new { e.ModuleId, e.NameFa, e.ParentId }, "menu_items_module_id_name_fa_parent_id_3c2380ac_uniq")
                .IsUnique()
                .HasFilter("([module_id] IS NOT NULL AND [name_fa] IS NOT NULL AND [parent_id] IS NOT NULL)");

            entity.HasOne(d => d.EntityTypeCommand).WithMany(p => p.MenuItems).HasConstraintName("menu_items_entity_type_command_id_4319057f_fk_entity_type_commands_id");

            entity.HasOne(d => d.EntityType).WithMany(p => p.MenuItems).HasConstraintName("menu_items_entity_type_id_412db7e8_fk_entity_types_id");

            entity.HasOne(d => d.Module).WithMany(p => p.MenuItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menu_items_module_id_56dd60a4_fk_modules_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("menu_items_parent_id_7032fad2_fk_menu_items_id");
        });

        modelBuilder.Entity<MilitaryServiceStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__military__3213E83F1BCBA74A");

            entity.HasOne(d => d.Creator).WithMany(p => p.MilitaryServiceStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("military_service_statuses_creator_id_2264fea4_fk_users_id");
        });

        modelBuilder.Entity<MissionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mission___3213E83FFEB65382");

            entity.HasOne(d => d.Creator).WithMany(p => p.MissionTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mission_types_creator_id_17915c73_fk_users_id");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__modules__3213E83F5BA46A5C");
        });

        modelBuilder.Entity<ModulePersonGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__module_p__3213E83F21B30295");

            entity.HasIndex(e => new { e.ModuleId, e.PersonGroupId }, "module_person_groups_module_id_person_group_id_0362b292_uniq")
                .IsUnique()
                .HasFilter("([module_id] IS NOT NULL AND [person_group_id] IS NOT NULL)");

            entity.HasOne(d => d.Module).WithMany(p => p.ModulePersonGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("module_person_groups_module_id_64506255_fk_modules_id");

            entity.HasOne(d => d.PersonGroup).WithMany(p => p.ModulePersonGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("module_person_groups_person_group_id_1b684782_fk_person_groups_id");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__notifica__3213E83F652845FC");

            entity.HasOne(d => d.Module).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notifications_module_id_31f93f98_fk_modules_id");

            entity.HasOne(d => d.User2).WithMany(p => p.NotificationUser2s).HasConstraintName("notifications_user2_id_8fe256fe_fk_users_id");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notifications_user_id_468e288d_fk_users_id");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__persons__3213E83F42B94BB7");

            entity.HasIndex(e => e.CitizenCode, "persons_citizen_code_430dc102_uniq")
                .IsUnique()
                .HasFilter("([citizen_code] IS NOT NULL)");

            entity.HasIndex(e => e.LegalNationalCode, "persons_legal_national_code_4397975a_uniq")
                .IsUnique()
                .HasFilter("([legal_national_code] IS NOT NULL)");

            entity.HasIndex(e => e.LegalRegisterNo, "persons_legal_register_no_22c0875c_uniq")
                .IsUnique()
                .HasFilter("([legal_register_no] IS NOT NULL)");

            entity.HasIndex(e => e.NaturalNationalCode, "persons_natural_national_code_49221b45_uniq")
                .IsUnique()
                .HasFilter("([natural_national_code] IS NOT NULL)");

            entity.HasIndex(e => e.PassportNumber, "persons_passport_number_dff3f3fd_uniq")
                .IsUnique()
                .HasFilter("([passport_number] IS NOT NULL)");

            entity.HasOne(d => d.BirthCity).WithMany(p => p.People).HasConstraintName("persons_birth_city_id_9fea3d98_fk_cities_id");

            entity.HasOne(d => d.CitizenNationality).WithMany(p => p.People).HasConstraintName("persons_citizen_nationality_id_ecc47f36_fk_countries_id");

            entity.HasOne(d => d.HousingStatus).WithMany(p => p.People).HasConstraintName("persons_housing_status_id_c8e37e67_fk_housing_statuses_id");

            entity.HasOne(d => d.MilitaryServiceStatus).WithMany(p => p.People).HasConstraintName("persons_military_service_status_id_31e57348_fk_military_service_statuses_id");

            entity.HasOne(d => d.Religion).WithMany(p => p.People).HasConstraintName("persons_religion_id_bf81e08d_fk_religions_id");
        });

        modelBuilder.Entity<PersonAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_a__3213E83F247B7B0B");

            entity.HasIndex(e => new { e.PersonId, e.CityId, e.Address }, "person_addresses_person_id_city_id_address_74e8c5b2_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [city_id] IS NOT NULL AND [address] IS NOT NULL)");

            entity.HasOne(d => d.City).WithMany(p => p.PersonAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_addresses_city_id_bda2a750_fk_cities_id");

            entity.HasOne(d => d.Country).WithMany(p => p.PersonAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_addresses_country_id_1fb41b6e_fk_countries_id");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_addresses_person_id_d67d7fc6_fk_persons_id");

            entity.HasOne(d => d.Province).WithMany(p => p.PersonAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_addresses_province_id_6ed2a37d_fk_provinces_id");
        });

        modelBuilder.Entity<PersonBankAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_b__3213E83FE40439CC");

            entity.HasIndex(e => new { e.PersonId, e.BankId, e.AccountNumber }, "person_bank_accounts_person_id_bank_id_account_number_b2edcc75_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [account_number] IS NOT NULL)");

            entity.HasIndex(e => new { e.PersonId, e.BankId, e.CardNumber }, "person_bank_accounts_person_id_bank_id_card_number_721eadc4_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [card_number] IS NOT NULL)");

            entity.HasIndex(e => new { e.PersonId, e.BankId, e.Iban }, "person_bank_accounts_person_id_bank_id_iban_011ebeeb_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [bank_id] IS NOT NULL AND [iban] IS NOT NULL)");

            entity.HasOne(d => d.Bank).WithMany(p => p.PersonBankAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_bank_accounts_bank_id_f4412c39_fk_banks_id");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonBankAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_bank_accounts_person_id_cea19acc_fk_persons_id");
        });

        modelBuilder.Entity<PersonEducationalDegree>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_e__3213E83F6B2086D0");

            entity.HasOne(d => d.Creator).WithMany(p => p.PersonEducationalDegrees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_educational_degrees_creator_id_29916c83_fk_users_id");

            entity.HasOne(d => d.EducationalDegree).WithMany(p => p.PersonEducationalDegrees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_educational_degrees_educational_degree_id_59f16fbf_fk_educational_degrees_id");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonEducationalDegrees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_educational_degrees_person_id_c8759020_fk_persons_id");
        });

        modelBuilder.Entity<PersonEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_e__3213E83FB51284E9");

            entity.HasIndex(e => new { e.PersonId, e.Email }, "person_emails_person_id_email_09d42bf7_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [email] IS NOT NULL)");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonEmails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_emails_person_id_7ab1a818_fk_persons_id");
        });

        modelBuilder.Entity<PersonFaxis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_f__3213E83F90B59B82");

            entity.HasIndex(e => new { e.PersonId, e.Code, e.Fax }, "person_faxes_person_id_code_fax_2b464685_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [code] IS NOT NULL AND [fax] IS NOT NULL)");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonFaxes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_faxes_person_id_aeabbb62_fk_persons_id");
        });

        modelBuilder.Entity<PersonGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_g__3213E83F713F046A");
        });

        modelBuilder.Entity<PersonMobile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_m__3213E83FC83B454A");

            entity.HasIndex(e => new { e.PersonId, e.Mobile }, "person_mobiles_person_id_mobile_e26483eb_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [mobile] IS NOT NULL)");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonMobiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_mobiles_person_id_ab811f2d_fk_persons_id");
        });

        modelBuilder.Entity<PersonPhone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_p__3213E83F9005824C");

            entity.HasIndex(e => new { e.PersonId, e.Code, e.Phone }, "person_phones_person_id_code_phone_29edc351_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [code] IS NOT NULL AND [phone] IS NOT NULL)");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonPhones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_phones_person_id_ad2cd17d_fk_persons_id");
        });

        modelBuilder.Entity<PersonRelative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_r__3213E83FF87C92B1");

            entity.HasOne(d => d.BirthCity).WithMany(p => p.PersonRelatives).HasConstraintName("person_relatives_birth_city_id_6e4fe77e_fk_cities_id");

            entity.HasOne(d => d.Creator).WithMany(p => p.PersonRelatives)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_relatives_creator_id_2e5156c1_fk_users_id");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonRelatives)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_relatives_person_id_0ab58207_fk_persons_id");
        });

        modelBuilder.Entity<PersonWebsite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__person_w__3213E83F354F2664");

            entity.HasIndex(e => new { e.PersonId, e.Website }, "person_websites_person_id_website_c64285de_uniq")
                .IsUnique()
                .HasFilter("([person_id] IS NOT NULL AND [website] IS NOT NULL)");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonWebsites)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_websites_person_id_2bf2e8a6_fk_persons_id");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__province__3213E83FC4AE030D");

            entity.HasIndex(e => new { e.CountryId, e.Code }, "provinces_country_id_code_8b417d3f_uniq")
                .IsUnique()
                .HasFilter("([country_id] IS NOT NULL AND [code] IS NOT NULL)");

            entity.HasIndex(e => new { e.CountryId, e.Name }, "provinces_country_id_name_9930a650_uniq")
                .IsUnique()
                .HasFilter("([country_id] IS NOT NULL AND [name] IS NOT NULL)");

            entity.HasOne(d => d.Country).WithMany(p => p.Provinces)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("provinces_country_id_8ee0b7b3_fk_countries_id");
        });

        modelBuilder.Entity<Religion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__religion__3213E83F8F5C0419");

            entity.HasOne(d => d.Creator).WithMany(p => p.Religions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("religions_creator_id_65bb4aaf_fk_users_id");
        });

        modelBuilder.Entity<SelectLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__select_l__3213E83F0CA38568");

            entity.HasOne(d => d.Company).WithMany(p => p.SelectLogs).HasConstraintName("select_logs_company_id_36cbe9f3_fk_companies_id");

            entity.HasOne(d => d.EntityType).WithMany(p => p.SelectLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("select_logs_entity_type_id_1ff059a2_fk_entity_types_id");

            entity.HasOne(d => d.Module).WithMany(p => p.SelectLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("select_logs_module_id_94ef8091_fk_modules_id");

            entity.HasOne(d => d.User).WithMany(p => p.SelectLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("select_logs_user_id_e7ccfdfc_fk_users_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FA39B6DD7");

            entity.HasIndex(e => e.UsernameEncrypted, "users_username_encrypted_508d3951_uniq")
                .IsUnique()
                .HasFilter("([username_encrypted] IS NOT NULL)");

            entity.HasOne(d => d.Language).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_language_id_9c707b57_fk_languages_id");

            entity.HasOne(d => d.Person).WithOne(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_person_id_5146b3bd_fk_persons_id");
        });

        modelBuilder.Entity<UserConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_con__3213E83F1E698839");

            entity.HasOne(d => d.Module).WithMany(p => p.UserConfigs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_configs_module_id_015d8e4f_fk_modules_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserConfigs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_configs_user_id_c0e78255_fk_users_id");
        });

        modelBuilder.Entity<WorkDepartment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__work_dep__3213E83F5C9B5BA8");

            entity.HasOne(d => d.Creator).WithMany(p => p.WorkDepartments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_departments_creator_id_7fdcedc2_fk_users_id");

            entity.HasOne(d => d.WorkPlace).WithMany(p => p.WorkDepartments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_departments_work_place_id_182cba09_fk_workplaces_id");
        });

        modelBuilder.Entity<WorkOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__work_ope__3213E83F645BA5D8");

            entity.HasOne(d => d.Creator).WithMany(p => p.WorkOperations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_operations_creator_id_d10b2154_fk_users_id");

            entity.HasOne(d => d.WorkDepartment).WithMany(p => p.WorkOperations).HasConstraintName("work_operations_work_department_id_a9f7d3e8_fk_work_departments_id");
        });

        modelBuilder.Entity<Workplace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__workplac__3213E83F036764E0");

            entity.HasOne(d => d.Creator).WithMany(p => p.Workplaces)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("workplaces_creator_id_0cc80a6d_fk_users_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
