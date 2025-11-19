using Accounting.Domain.Entities;
using Accounting.Infrastructure.DataAccess.Configurations;
using Common.Application.Interfaces;
using General.Domain.Entities;
using General.Infrastructure.DataAccess.Configurations;
using HCM.Domain.Entities;
using HCM.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities;
using Shared.Infrastructure.DataAccess.Configurations;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.DataAccess.Configurations;
using Weighing.Domain.Entities;
using Weighing.Infrastructure.DataAccess.Configurations;


namespace Persistence.DataAccess
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        public MainDbContext()
        {

        }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        #region Accounting
        public virtual DbSet<AccountCategory> AccountCategories { get; set; }

        public virtual DbSet<AccountGroup> AccountGroups { get; set; }

        public virtual DbSet<AccountSet> AccountSets { get; set; }

        public virtual DbSet<AccountSetItem> AccountSetItems { get; set; }

        public virtual DbSet<BalanceRelatedAccountDetail> BalanceRelatedAccountDetails { get; set; }

        public virtual DbSet<BankTemplate> BankTemplates { get; set; }

        public virtual DbSet<ClosingPattern> ClosingPatterns { get; set; }

        public virtual DbSet<ClosingPatternSlaveCompany> ClosingPatternSlaveCompanies { get; set; }

        public virtual DbSet<ClosingPatternTemporaryAccount> ClosingPatternTemporaryAccounts { get; set; }

        public virtual DbSet<Contradiction> Contradictions { get; set; }

        public virtual DbSet<ContradictionItem> ContradictionItems { get; set; }

        public virtual DbSet<DataImport> DataImports { get; set; }

        public virtual DbSet<FloatAccountType> FloatAccountTypes { get; set; }

        public virtual DbSet<Ledger> Ledgers { get; set; }

        public virtual DbSet<LedgerPeriod> LedgerPeriods { get; set; }

        public virtual DbSet<LedgerPeriodCompany> LedgerPeriodCompanies { get; set; }

        public virtual DbSet<LedgerPeriodCompanySetting> LedgerPeriodCompanySettings { get; set; }

        public virtual DbSet<ManualFloatAccount> ManualFloatAccounts { get; set; }

        public virtual DbSet<MasterAccount> MasterAccounts { get; set; }

        public virtual DbSet<Period> Periods { get; set; }

        public virtual DbSet<ResourceAndExpenditure> ResourceAndExpenditures { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<SlaveAccount> SlaveAccounts { get; set; }

        public virtual DbSet<SlaveAccountCompany> SlaveAccountCompanies { get; set; }

        public virtual DbSet<SlaveAccountStandardDescription> SlaveAccountStandardDescriptions { get; set; }

        public virtual DbSet<TrashVoucher> TrashVouchers { get; set; }

        public virtual DbSet<TrashVoucherItem> TrashVoucherItems { get; set; }

        public virtual DbSet<TrashVoucherItemAttach> TrashVoucherItemAttaches { get; set; }

        public virtual DbSet<TtmsBuy> TtmsBuys { get; set; }

        public virtual DbSet<TtmsContractorInfo> TtmsContractorInfo { get; set; }

        public virtual DbSet<TtmsEmployerInfo> TtmsEmployerInfo { get; set; }

        public virtual DbSet<TtmsExportation> TtmsExportation { get; set; }

        public virtual DbSet<TtmsImportation> TtmsImportations { get; set; }

        public virtual DbSet<TtmsLeaseAgreement> TtmsLeaseAgreements { get; set; }

        public virtual DbSet<TtmsPreSell> TtmsPreSells { get; set; }

        public virtual DbSet<TtmsProductType> TtmsProductTypes { get; set; }

        public virtual DbSet<TtmsSell> TtmsSells { get; set; }

        public virtual DbSet<TtmsWage> TtmsWages { get; set; }

        public virtual DbSet<Voucher> Vouchers { get; set; }

        public virtual DbSet<VoucherItem> VoucherItems { get; set; }

        public virtual DbSet<VoucherItemAttach> VoucherItemAttaches { get; set; }

        public virtual DbSet<VoucherItemLog> VoucherItemLogs { get; set; }

        public virtual DbSet<VoucherItemStandardDescription> VoucherItemStandardDescriptions { get; set; }

        public virtual DbSet<VoucherLog> VoucherLogs { get; set; }

        public virtual DbSet<VoucherType> VoucherTypes { get; set; }


        #endregion

        #region General
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

        public virtual DbSet<General.Domain.Entities.Domain> Domains { get; set; }

        public virtual DbSet<EducationalDegree> EducationalDegrees { get; set; }

        public virtual DbSet<EmploymentContractDescription> EmploymentContractDescriptions { get; set; }

        public virtual DbSet<EmploymentContractTitle> EmploymentContractTitles { get; set; }

        public virtual DbSet<EntityType> EntityTypes { get; set; }

        public virtual DbSet<EntityTypeCommand> EntityTypeCommands { get; set; }

        public virtual DbSet<EntityTypeConstraint> EntityTypeConstraints { get; set; }

        public virtual DbSet<EntityTypeDependency> EntityTypeDependencies { get; set; }

        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

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
        #endregion

        #region HCM
        public DbSet<Department> Departments { get; set; }
        public DbSet<Post> Posts { get; set; }

        #endregion

        #region Shared

        public virtual DbSet<BankAccount> BankAccounts { get; set; }

        public virtual DbSet<BankBranch> BankBranches { get; set; }

        public virtual DbSet<Branch> Branches { get; set; }

        public virtual DbSet<Cashier> Cashiers { get; set; }

        public virtual DbSet<CompanySetting> CompanySettings { get; set; }

        public virtual DbSet<CompanyUnit> CompanyUnits { get; set; }

        public virtual DbSet<CostCenter> CostCenters { get; set; }

        public virtual DbSet<Foo> Foos { get; set; }

        public virtual DbSet<PersonCompany> PersonCompanies { get; set; }

        public virtual DbSet<PersonGroupMember> PersonGroupMembers { get; set; }

        public virtual DbSet<PettyCashier> PettyCashiers { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }

        public virtual DbSet<ReportTemplate> ReportTemplates { get; set; }

        public virtual DbSet<Restriction> Restrictions { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RoleMember> RoleMembers { get; set; }

        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        public virtual DbSet<RolePermissionCommand> RolePermissionCommands { get; set; }

        public virtual DbSet<RolePermissionConstraint> RolePermissionConstraints { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        #endregion

        #region Warehouse
        public DbSet<ProductCode> ProductCodes { get; set; }

        public DbSet<WarehouseType> WarehouseTypes { get; set; }

        public DbSet<WarehouseStock> WarehouseStocks { get; set; }

        public DbSet<ProductHierarchy> ProductHierarchies { get; set; }
        #endregion

        #region Weighing
        public DbSet<DischargeStation> DischargeStations { get; set; } = null!;
        public DbSet<PackageType> PackageTypes { get; set; } = null!;
        public DbSet<PersonDriver> PersonDrivers { get; set; } = null!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Persian_100_CI_AI");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeneralConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SharedConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountingConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WeighingConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HCMConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }

}
