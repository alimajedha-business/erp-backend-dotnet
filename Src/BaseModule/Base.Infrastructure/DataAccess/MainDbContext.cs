using NGErp.General.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace NGErp.Base.Infrastructure.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {

        }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Persian_100_CI_AI");
           // modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeneralConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }

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

    }

}
