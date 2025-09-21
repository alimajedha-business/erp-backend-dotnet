using System;
using System.Collections.Generic;
using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.DataAccess;

public partial class AccountingDbContext : DbContext
{
    public AccountingDbContext()
    {
    }

    public AccountingDbContext(DbContextOptions<AccountingDbContext> options)
        : base(options)
    {
    }

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

    public virtual DbSet<TtmsContractorInfo> TtmsContractorInfos { get; set; }

    public virtual DbSet<TtmsEmployerInfo> TtmsEmployerInfos { get; set; }

    public virtual DbSet<TtmsExportation> TtmsExportations { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\sql19;Database=NGERP;User Id=sa;Password=123;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Persian_100_CI_AI");

        modelBuilder.Entity<AccountCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83F523151EB");
        });

        modelBuilder.Entity<AccountGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83F4DE2EE95");

            entity.HasIndex(e => new { e.CategoryId, e.Code }, "account_groups_category_id_code_2b62c6b4_uniq")
                .IsUnique()
                .HasFilter("([category_id] IS NOT NULL AND [code] IS NOT NULL)");

            entity.HasIndex(e => new { e.CategoryId, e.Name }, "account_groups_category_id_name_8880b185_uniq")
                .IsUnique()
                .HasFilter("([category_id] IS NOT NULL AND [name] IS NOT NULL)");

            entity.HasOne(d => d.Category).WithMany(p => p.AccountGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_groups_category_id_af0c53e8_fk_account_categories_id");
        });

        modelBuilder.Entity<AccountSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83FB563F35A");

            entity.HasIndex(e => new { e.CompanyId, e.Title }, "account_sets_company_id_title_b34aa998_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [title] IS NOT NULL)");

            entity.HasOne(d => d.Ledger).WithMany(p => p.AccountSets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_sets_ledger_id_31846310_fk_ledgers_id");
        });

        modelBuilder.Entity<AccountSetItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83F57D6E072");

            entity.HasIndex(e => new { e.AccountSetId, e.MasterId, e.SlaveId }, "account_set_items_account_set_id_master_id_slave_id_c5ba8c71_uniq")
                .IsUnique().HasFilter("([account_set_id] IS NOT NULL AND [master_id] IS NOT NULL AND [slave_id] IS NOT NULL)");

            entity.HasOne(d => d.AccountSet).WithMany(p => p.AccountSetItems).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("account_set_items_account_set_id_c6a49338_fk_account_sets_id");

            entity.HasOne(d => d.Master).WithMany(p => p.AccountSetItems).HasConstraintName("account_set_items_master_id_e12671e4_fk_master_accounts_id");

            entity.HasOne(d => d.Slave).WithMany(p => p.AccountSetItems).HasConstraintName("account_set_items_slave_id_a5287d0f_fk_slave_accounts_id");
        });

        modelBuilder.Entity<BalanceRelatedAccountDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__balance___3213E83F58334CF7");

            entity.HasOne(d => d.Ledger).WithMany(p => p.BalanceRelatedAccountDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("balance_related_account_details_ledger_id_1fcf3bc8_fk_ledgers_id");

            entity.HasOne(d => d.ManualFloat1).WithMany(p => p.BalanceRelatedAccountDetailManualFloat1s).HasConstraintName("balance_related_account_details_manual_float_1_id_3cf4e319_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat2).WithMany(p => p.BalanceRelatedAccountDetailManualFloat2s).HasConstraintName("balance_related_account_details_manual_float_2_id_79cca910_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat3).WithMany(p => p.BalanceRelatedAccountDetailManualFloat3s).HasConstraintName("balance_related_account_details_manual_float_3_id_c7afe8b7_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat4).WithMany(p => p.BalanceRelatedAccountDetailManualFloat4s).HasConstraintName("balance_related_account_details_manual_float_4_id_bba5925c_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat5).WithMany(p => p.BalanceRelatedAccountDetailManualFloat5s).HasConstraintName("balance_related_account_details_manual_float_5_id_2f0ad7f7_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat6).WithMany(p => p.BalanceRelatedAccountDetailManualFloat6s).HasConstraintName("balance_related_account_details_manual_float_6_id_eeaf3bf1_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat7).WithMany(p => p.BalanceRelatedAccountDetailManualFloat7s).HasConstraintName("balance_related_account_details_manual_float_7_id_c140028e_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat8).WithMany(p => p.BalanceRelatedAccountDetailManualFloat8s).HasConstraintName("balance_related_account_details_manual_float_8_id_68b0bdc0_fk_manual_float_accounts_id");

            entity.HasOne(d => d.Master).WithMany(p => p.BalanceRelatedAccountDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("balance_related_account_details_master_id_f76438d4_fk_master_accounts_id");

            entity.HasOne(d => d.Period).WithMany(p => p.BalanceRelatedAccountDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("balance_related_account_details_period_id_2b2d6794_fk_periods_id");

            entity.HasOne(d => d.ResourceAndExpenditure).WithMany(p => p.BalanceRelatedAccountDetails).HasConstraintName("balance_related_account_details_resource_and_expenditure_id_78f2b804_fk_resource_and_expenditures_id");

            entity.HasOne(d => d.Slave).WithMany(p => p.BalanceRelatedAccountDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("balance_related_account_details_slave_id_8bf18f14_fk_slave_accounts_id");
        });

        modelBuilder.Entity<BankTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bank_tem__3213E83F444D69A3");
        });

        modelBuilder.Entity<ClosingPattern>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__closing___3213E83FF2309BF5");

            entity.HasIndex(e => new { e.CompanyId, e.LedgerId, e.PeriodId, e.FloatTypeSet }, "closing_patterns_company_id_ledger_id_period_id_float_type_set_70e51708_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [period_id] IS NOT NULL AND [float_type_set] IS NOT NULL)");

            entity.HasOne(d => d.DefaultResourceAndExpenditure).WithMany(p => p.ClosingPatterns).HasConstraintName("closing_patterns_default_resource_and_expenditure_id_6ef35f22_fk_resource_and_expenditures_id");

            entity.HasOne(d => d.Ledger).WithMany(p => p.ClosingPatterns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("closing_patterns_ledger_id_394ee754_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.ClosingPatterns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("closing_patterns_period_id_8a00d0d0_fk_periods_id");
        });

        modelBuilder.Entity<ClosingPatternSlaveCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__closing___3213E83F69E4AA3B");

            entity.HasIndex(e => new { e.CompanyId, e.LedgerId, e.PeriodId, e.SlaveCompanyId }, "closing_pattern_slave_companies_company_id_ledger_id_period_id_slave_company_id_772b00bd_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [period_id] IS NOT NULL AND [slave_company_id] IS NOT NULL)");

            entity.HasOne(d => d.DefaultResourceAndExpenditure).WithMany(p => p.ClosingPatternSlaveCompanies).HasConstraintName("closing_pattern_slave_companies_default_resource_and_expenditure_id_d8808ca1_fk_resource_and_expenditures_id");

            entity.HasOne(d => d.Ledger).WithMany(p => p.ClosingPatternSlaveCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("closing_pattern_slave_companies_ledger_id_b7344906_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.ClosingPatternSlaveCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("closing_pattern_slave_companies_period_id_7d4ec4d3_fk_periods_id");

            entity.HasOne(d => d.SlaveCompany).WithMany(p => p.ClosingPatternSlaveCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("closing_pattern_slave_companies_slave_company_id_e971e36e_fk_slave_account_companies_id");
        });

        modelBuilder.Entity<ClosingPatternTemporaryAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__closing___3213E83F56719B0B");

            entity.HasIndex(e => new { e.CompanyId, e.LedgerId, e.PeriodId, e.FloatTypeSet }, "closing_pattern_temporary_accounts_company_id_ledger_id_period_id_float_type_set_2bc966ac_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [period_id] IS NOT NULL AND [float_type_set] IS NOT NULL)");

            entity.HasOne(d => d.DefaultResourceAndExpenditure).WithMany(p => p.ClosingPatternTemporaryAccounts).HasConstraintName("closing_pattern_temporary_accounts_default_resource_and_expenditure_id_dcf3d48a_fk_resource_and_expenditures_id");

            entity.HasOne(d => d.Ledger).WithMany(p => p.ClosingPatternTemporaryAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("closing_pattern_temporary_accounts_ledger_id_8d2baceb_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.ClosingPatternTemporaryAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("closing_pattern_temporary_accounts_period_id_897435aa_fk_periods_id");
        });

        modelBuilder.Entity<Contradiction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contradi__3213E83FC9BAF683");

            entity.HasOne(d => d.DataImport).WithMany(p => p.Contradictions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contradictions_data_import_id_65a4748b_fk_data_imports_id");

            entity.HasOne(d => d.Ledger).WithMany(p => p.Contradictions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contradictions_ledger_id_85776d2a_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.Contradictions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contradictions_period_id_538ee815_fk_periods_id");
        });

        modelBuilder.Entity<ContradictionItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contradi__3213E83F04D07847");

            entity.HasOne(d => d.Contradiction).WithMany(p => p.ContradictionItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contradiction_items_contradiction_id_1494f375_fk_contradictions_id");
        });

        modelBuilder.Entity<DataImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__data_imp__3213E83F260CBFC5");

            entity.HasOne(d => d.Ledger).WithMany(p => p.DataImports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_imports_ledger_id_fd1c5e0d_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.DataImports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_imports_period_id_aef725fd_fk_periods_id");
        });

        modelBuilder.Entity<FloatAccountType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__float_ac__3213E83F2EA4CB1D");

            entity.HasIndex(e => e.VoucherItemFloatLevel, "float_account_types_voucher_item_float_level_b985d2e4_uniq")
                .IsUnique()
                .HasFilter("([voucher_item_float_level] IS NOT NULL)");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("float_account_types_parent_id_4dd8ba99_fk_float_account_types_id");
        });

        modelBuilder.Entity<Ledger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ledgers__3213E83F7B5B3C2B");
        });

        modelBuilder.Entity<LedgerPeriod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ledger_p__3213E83F6DE635AD");

            entity.HasIndex(e => new { e.LedgerId, e.PeriodId }, "ledger_periods_ledger_id_period_id_5d05e0eb_uniq")
                .IsUnique()
                .HasFilter("([ledger_id] IS NOT NULL AND [period_id] IS NOT NULL)");

            entity.HasOne(d => d.Ledger).WithMany(p => p.LedgerPeriods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_periods_ledger_id_c70ff025_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.LedgerPeriods)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_periods_period_id_2953e5ba_fk_periods_id");
        });

        modelBuilder.Entity<LedgerPeriodCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ledger_p__3213E83F76D7AAD9");

            entity.HasIndex(e => new { e.CompanyId, e.LedgerPeriodId }, "ledger_period_companies_company_id_ledger_period_id_08379790_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [ledger_period_id] IS NOT NULL)");

            entity.HasOne(d => d.Ledger).WithMany(p => p.LedgerPeriodCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_period_companies_ledger_id_52a61c2a_fk_ledgers_id");

            entity.HasOne(d => d.LedgerPeriod).WithMany(p => p.LedgerPeriodCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_period_companies_ledger_period_id_5656d93c_fk_ledger_periods_id");

            entity.HasOne(d => d.Period).WithMany(p => p.LedgerPeriodCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_period_companies_period_id_0828d8ca_fk_periods_id");
        });

        modelBuilder.Entity<LedgerPeriodCompanySetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ledger_p__3213E83FA6E82736");

            entity.HasIndex(e => new { e.CompanyId, e.LedgerPeriodCompanyId }, "ledger_period_company_settings_company_id_ledger_period_company_id_e87be1b5_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [ledger_period_company_id] IS NOT NULL)");

            entity.HasOne(d => d.Ledger).WithMany(p => p.LedgerPeriodCompanySettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_period_company_settings_ledger_id_c37251eb_fk_ledgers_id");

            entity.HasOne(d => d.LedgerPeriodCompany).WithOne(p => p.LedgerPeriodCompanySetting)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_period_company_settings_ledger_period_company_id_880b5330_fk_ledger_period_companies_id");

            entity.HasOne(d => d.Period).WithMany(p => p.LedgerPeriodCompanySettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ledger_period_company_settings_period_id_2389d1c0_fk_periods_id");
        });

        modelBuilder.Entity<ManualFloatAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__manual_f__3213E83F79B8ADA2");

            entity.HasIndex(e => new { e.CompanyId, e.FloatAccountTypeId, e.Code }, "manual_float_accounts_company_id_float_account_type_id_code_7e28c5e8_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [float_account_type_id] IS NOT NULL AND [code] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.FloatAccountTypeId, e.Title2 }, "manual_float_accounts_company_id_float_account_type_id_title2_11d5aa07_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [float_account_type_id] IS NOT NULL AND [title2] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.FloatAccountTypeId, e.Title }, "manual_float_accounts_company_id_float_account_type_id_title_ad80fe99_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [float_account_type_id] IS NOT NULL AND [title] IS NOT NULL)");

            entity.HasOne(d => d.FloatAccountType).WithMany(p => p.ManualFloatAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("manual_float_accounts_float_account_type_id_b211d188_fk_float_account_types_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("manual_float_accounts_parent_id_4b9bca91_fk_manual_float_accounts_id");
        });

        modelBuilder.Entity<MasterAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__master_a__3213E83FDB1E8845");

            entity.HasIndex(e => new { e.LedgerId, e.Code }, "master_accounts_ledger_id_code_7b97c8b2_uniq")
                .IsUnique()
                .HasFilter("([ledger_id] IS NOT NULL AND [code] IS NOT NULL)");

            entity.HasOne(d => d.Ledger).WithMany(p => p.MasterAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("master_accounts_ledger_id_b94026a2_fk_ledgers_id");
        });

        modelBuilder.Entity<Period>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__periods__3213E83FFA2811B1");
        });

        modelBuilder.Entity<ResourceAndExpenditure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__resource__3213E83F849557AA");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "resource_and_expenditures_company_id_code_75192806_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [code] IS NOT NULL)");

            entity.HasIndex(e => new { e.CompanyId, e.Name }, "resource_and_expenditures_company_id_name_1605dd8e_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [name] IS NOT NULL)");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__settings__3213E83F975C56ED");
        });

        modelBuilder.Entity<SlaveAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__slave_ac__3213E83FA165931F");

            entity.HasIndex(e => new { e.CategoryId, e.GroupId, e.LedgerId, e.MasterId, e.Code, e.DetailedAccount1 }, "slave_accounts_category_id_group_id_ledger_id_master_id_code_detailed_account_1_2ebf0ffa_uniq")
                .IsUnique()
                .HasFilter("([category_id] IS NOT NULL AND [group_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [master_id] IS NOT NULL AND [code] IS NOT NULL AND [detailed_account_1] IS NOT NULL)");

            entity.HasIndex(e => new { e.CategoryId, e.GroupId, e.LedgerId, e.MasterId, e.Code, e.DetailedAccount1, e.DetailedAccount2 }, "slave_accounts_category_id_group_id_ledger_id_master_id_code_detailed_account_1_detailed_account_2_1409df48_uniq")
                .IsUnique()
                .HasFilter("([category_id] IS NOT NULL AND [group_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [master_id] IS NOT NULL AND [code] IS NOT NULL AND [detailed_account_1] IS NOT NULL AND [detailed_account_2] IS NOT NULL)");

            entity.HasOne(d => d.Category).WithMany(p => p.SlaveAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slave_accounts_category_id_79276cde_fk_account_categories_id");

            entity.HasOne(d => d.Group).WithMany(p => p.SlaveAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slave_accounts_group_id_d226c356_fk_account_groups_id");

            entity.HasOne(d => d.Ledger).WithMany(p => p.SlaveAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slave_accounts_ledger_id_e9bba48c_fk_ledgers_id");

            entity.HasOne(d => d.Master).WithMany(p => p.SlaveAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slave_accounts_master_id_d8364647_fk_master_accounts_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("slave_accounts_parent_id_b5e3fe05_fk_slave_accounts_id");
        });

        modelBuilder.Entity<SlaveAccountCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__slave_ac__3213E83FA1D7EC62");

            entity.HasIndex(e => new { e.SlaveId, e.CompanyId }, "slave_account_companies_slave_id_company_id_d7bc5f10_uniq")
                .IsUnique()
                .HasFilter("([slave_id] IS NOT NULL AND [company_id] IS NOT NULL)");

            entity.HasOne(d => d.FloatType1).WithMany(p => p.SlaveAccountCompanyFloatType1s).HasConstraintName("slave_account_companies_float_type_1_id_dcee560d_fk_float_account_types_id");

            entity.HasOne(d => d.FloatType2).WithMany(p => p.SlaveAccountCompanyFloatType2s).HasConstraintName("slave_account_companies_float_type_2_id_c218860a_fk_float_account_types_id");

            entity.HasOne(d => d.FloatType3).WithMany(p => p.SlaveAccountCompanyFloatType3s).HasConstraintName("slave_account_companies_float_type_3_id_653411c6_fk_float_account_types_id");

            entity.HasOne(d => d.FloatType4).WithMany(p => p.SlaveAccountCompanyFloatType4s).HasConstraintName("slave_account_companies_float_type_4_id_57254460_fk_float_account_types_id");

            entity.HasOne(d => d.FloatType5).WithMany(p => p.SlaveAccountCompanyFloatType5s).HasConstraintName("slave_account_companies_float_type_5_id_896ccdfc_fk_float_account_types_id");

            entity.HasOne(d => d.FloatType6).WithMany(p => p.SlaveAccountCompanyFloatType6s).HasConstraintName("slave_account_companies_float_type_6_id_a486a7f8_fk_float_account_types_id");

            entity.HasOne(d => d.FloatType7).WithMany(p => p.SlaveAccountCompanyFloatType7s).HasConstraintName("slave_account_companies_float_type_7_id_f5d8293e_fk_float_account_types_id");

            entity.HasOne(d => d.FloatType8).WithMany(p => p.SlaveAccountCompanyFloatType8s).HasConstraintName("slave_account_companies_float_type_8_id_ab2b63c8_fk_float_account_types_id");

            entity.HasOne(d => d.Ledger).WithMany(p => p.SlaveAccountCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slave_account_companies_ledger_id_b40ad4d0_fk_ledgers_id");

            entity.HasOne(d => d.Master).WithMany(p => p.SlaveAccountCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slave_account_companies_master_id_bacd3012_fk_master_accounts_id");

            entity.HasOne(d => d.Slave).WithMany(p => p.SlaveAccountCompanies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slave_account_companies_slave_id_edd3dcf5_fk_slave_accounts_id");
        });

        modelBuilder.Entity<SlaveAccountStandardDescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__slave_ac__3213E83F8F08DB1E");

            entity.HasIndex(e => new { e.CompanyId, e.SlaveId, e.Description }, "slave_account_standard_descriptions_company_id_slave_id_description_fcd17f45_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [slave_id] IS NOT NULL AND [description] IS NOT NULL)");

            entity.HasOne(d => d.Slave).WithMany(p => p.SlaveAccountStandardDescriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("slave_account_standard_descriptions_slave_id_b64d916e_fk_slave_accounts_id");
        });

        modelBuilder.Entity<TrashVoucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__trash_vo__3213E83F2F737595");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TrashVouchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_vouchers_ledger_id_e6d73246_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TrashVouchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_vouchers_period_id_f1b414ea_fk_periods_id");

            entity.HasOne(d => d.Type).WithMany(p => p.TrashVouchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_vouchers_type_id_62c0b2ab_fk_voucher_types_id");
        });

        modelBuilder.Entity<TrashVoucherItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__trash_vo__3213E83F3922E482");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TrashVoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_voucher_items_ledger_id_58204329_fk_ledgers_id");

            entity.HasOne(d => d.ManualFloat1).WithMany(p => p.TrashVoucherItemManualFloat1s).HasConstraintName("trash_voucher_items_manual_float_1_id_0cdae92d_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat2).WithMany(p => p.TrashVoucherItemManualFloat2s).HasConstraintName("trash_voucher_items_manual_float_2_id_985905b9_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat3).WithMany(p => p.TrashVoucherItemManualFloat3s).HasConstraintName("trash_voucher_items_manual_float_3_id_11645efa_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat4).WithMany(p => p.TrashVoucherItemManualFloat4s).HasConstraintName("trash_voucher_items_manual_float_4_id_60ae5d18_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat5).WithMany(p => p.TrashVoucherItemManualFloat5s).HasConstraintName("trash_voucher_items_manual_float_5_id_3eb06b68_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat6).WithMany(p => p.TrashVoucherItemManualFloat6s).HasConstraintName("trash_voucher_items_manual_float_6_id_a4afd007_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat7).WithMany(p => p.TrashVoucherItemManualFloat7s).HasConstraintName("trash_voucher_items_manual_float_7_id_85ed9abc_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat8).WithMany(p => p.TrashVoucherItemManualFloat8s).HasConstraintName("trash_voucher_items_manual_float_8_id_d2f59901_fk_manual_float_accounts_id");

            entity.HasOne(d => d.Master).WithMany(p => p.TrashVoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_voucher_items_master_id_0c48bb81_fk_master_accounts_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TrashVoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_voucher_items_period_id_e1c57ee1_fk_periods_id");

            entity.HasOne(d => d.ResourceAndExpenditure).WithMany(p => p.TrashVoucherItems).HasConstraintName("trash_voucher_items_resource_and_expenditure_id_fb349f90_fk_resource_and_expenditures_id");

            entity.HasOne(d => d.SlaveCompany).WithMany(p => p.TrashVoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_voucher_items_slave_company_id_976d2e14_fk_slave_account_companies_id");

            entity.HasOne(d => d.Slave).WithMany(p => p.TrashVoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_voucher_items_slave_id_8ef343db_fk_slave_accounts_id");

            entity.HasOne(d => d.TrashVoucher).WithMany(p => p.TrashVoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_voucher_items_trash_voucher_id_4c9d9a23_fk_trash_vouchers_id");
        });

        modelBuilder.Entity<TrashVoucherItemAttach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__trash_vo__3213E83F11360E3F");

            entity.HasOne(d => d.TrashVoucher).WithMany(p => p.TrashVoucherItemAttaches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_voucher_item_attaches_trash_voucher_id_aea53052_fk_trash_vouchers_id");

            entity.HasOne(d => d.TrashVoucherItem).WithMany(p => p.TrashVoucherItemAttaches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("trash_voucher_item_attaches_trash_voucher_item_id_f86212d2_fk_trash_voucher_items_id");
        });

        modelBuilder.Entity<TtmsBuy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_buy__3213E83FF4617A16");

            entity.HasOne(d => d.KalaType).WithMany(p => p.TtmsBuys).HasConstraintName("ttms_buys_kala_type_id_acc1c01e_fk_ttms_product_types_id");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsBuys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_buys_ledger_id_4592b702_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsBuys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_buys_period_id_b172660c_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsBuys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_buys_voucher_id_932f90cb_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsBuy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_buys_voucher_item_id_f176cf2a_fk_voucher_items_id");
        });

        modelBuilder.Entity<TtmsContractorInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_con__3213E83FF95436E1");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsContractorInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_contractor_infos_ledger_id_866da6c0_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsContractorInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_contractor_infos_period_id_0b3cc826_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsContractorInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_contractor_infos_voucher_id_4561f0d4_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsContractorInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_contractor_infos_voucher_item_id_f0e62792_fk_voucher_items_id");
        });

        modelBuilder.Entity<TtmsEmployerInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_emp__3213E83F8C522BC1");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsEmployerInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_employer_infos_ledger_id_4e6cb6c7_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsEmployerInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_employer_infos_period_id_f2de7f69_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsEmployerInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_employer_infos_voucher_id_c3d5dd84_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsEmployerInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_employer_infos_voucher_item_id_1756b20f_fk_voucher_items_id");
        });

        modelBuilder.Entity<TtmsExportation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_exp__3213E83F07EBF8D8");

            entity.HasOne(d => d.KalaType).WithMany(p => p.TtmsExportations)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.KalaTypeId)
                .HasConstraintName("ttms_exportations_kala_type_id_c8a03da3_fk_ttms_product_types_code");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsExportations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_exportations_ledger_id_b7fff67d_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsExportations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_exportations_period_id_7c7cf71b_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsExportations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_exportations_voucher_id_14d3710f_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsExportation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_exportations_voucher_item_id_0ac158de_fk_voucher_items_id");
        });

        modelBuilder.Entity<TtmsImportation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_imp__3213E83F91DF5323");

            entity.HasOne(d => d.KalaType).WithMany(p => p.TtmsImportations)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.KalaTypeId)
                .HasConstraintName("ttms_importations_kala_type_id_cb9fffb1_fk_ttms_product_types_code");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsImportations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_importations_ledger_id_a8e87721_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsImportations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_importations_period_id_e707fd86_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsImportations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_importations_voucher_id_e6c6d27d_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsImportation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_importations_voucher_item_id_7d74fcd3_fk_voucher_items_id");
        });

        modelBuilder.Entity<TtmsLeaseAgreement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_lea__3213E83FE7DBF176");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsLeaseAgreements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_lease_agreements_ledger_id_fbcd73fe_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsLeaseAgreements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_lease_agreements_period_id_b7dd6e0b_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsLeaseAgreements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_lease_agreements_voucher_id_38801563_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsLeaseAgreement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_lease_agreements_voucher_item_id_580d9a03_fk_voucher_items_id");
        });

        modelBuilder.Entity<TtmsPreSell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_pre__3213E83FBF587317");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsPreSells)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_pre_sells_ledger_id_e56cf010_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsPreSells)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_pre_sells_period_id_d371cddf_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsPreSells)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_pre_sells_voucher_id_58f4d88f_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsPreSell)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_pre_sells_voucher_item_id_13de02d7_fk_voucher_items_id");
        });

        modelBuilder.Entity<TtmsProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_pro__3213E83FDC489187");
        });

        modelBuilder.Entity<TtmsSell>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_sel__3213E83FF5A53FF2");

            entity.HasOne(d => d.KalaType).WithMany(p => p.TtmsSells)
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.KalaTypeId)
                .HasConstraintName("ttms_sells_kala_type_id_49581b94_fk_ttms_product_types_code");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsSells)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_sells_ledger_id_e8e19e1e_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsSells)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_sells_period_id_bdec3655_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsSells)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_sells_voucher_id_96da188f_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsSell)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_sells_voucher_item_id_99608a2c_fk_voucher_items_id");
        });

        modelBuilder.Entity<TtmsWage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ttms_wag__3213E83F2B6F7066");

            entity.HasOne(d => d.KalaType).WithMany(p => p.TtmsWages).HasConstraintName("ttms_wages_kala_type_id_8ee391b0_fk_ttms_product_types_id");

            entity.HasOne(d => d.Ledger).WithMany(p => p.TtmsWages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_wages_ledger_id_2e5fbf4c_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.TtmsWages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_wages_period_id_7e023cdd_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.TtmsWages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_wages_voucher_id_ebeeee11_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithOne(p => p.TtmsWage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ttms_wages_voucher_item_id_6c48028f_fk_voucher_items_id");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vouchers__3213E83F37737C08");

            entity.HasIndex(e => new { e.CompanyId, e.LedgerId, e.PeriodId, e.Number }, "vouchers_company_id_ledger_id_period_id_number_10cfc9fc_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [ledger_id] IS NOT NULL AND [period_id] IS NOT NULL AND [number] IS NOT NULL)");

            entity.HasOne(d => d.Ledger).WithMany(p => p.Vouchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vouchers_ledger_id_25e3f63d_fk_ledgers_id");

            entity.HasOne(d => d.Period).WithMany(p => p.Vouchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vouchers_period_id_800ded6b_fk_periods_id");

            entity.HasOne(d => d.Type).WithMany(p => p.Vouchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vouchers_type_id_05bcc3a6_fk_voucher_types_id");
        });

        modelBuilder.Entity<VoucherItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__voucher___3213E83F9B13223D");

            entity.HasIndex(e => e.CrossReferenceId, "voucher_items_cross_reference_id_4dd4e885_uniq")
                .IsUnique()
                .HasFilter("([cross_reference_id] IS NOT NULL)");

            entity.HasIndex(e => e.IdInitial, "voucher_items_id_initial_3062338f_uniq")
                .IsUnique()
                .HasFilter("([id_initial] IS NOT NULL)");

            entity.HasOne(d => d.Ledger).WithMany(p => p.VoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_items_ledger_id_439dcc5d_fk_ledgers_id");

            entity.HasOne(d => d.ManualFloat1).WithMany(p => p.VoucherItemManualFloat1s).HasConstraintName("voucher_items_manual_float_1_id_a2c88b6e_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat2).WithMany(p => p.VoucherItemManualFloat2s).HasConstraintName("voucher_items_manual_float_2_id_60bbb623_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat3).WithMany(p => p.VoucherItemManualFloat3s).HasConstraintName("voucher_items_manual_float_3_id_4f9d7506_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat4).WithMany(p => p.VoucherItemManualFloat4s).HasConstraintName("voucher_items_manual_float_4_id_35e1cb30_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat5).WithMany(p => p.VoucherItemManualFloat5s).HasConstraintName("voucher_items_manual_float_5_id_a5393d5c_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat6).WithMany(p => p.VoucherItemManualFloat6s).HasConstraintName("voucher_items_manual_float_6_id_a56fc8dc_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat7).WithMany(p => p.VoucherItemManualFloat7s).HasConstraintName("voucher_items_manual_float_7_id_858fb1f1_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat8).WithMany(p => p.VoucherItemManualFloat8s).HasConstraintName("voucher_items_manual_float_8_id_a738253d_fk_manual_float_accounts_id");

            entity.HasOne(d => d.Master).WithMany(p => p.VoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_items_master_id_f8105cae_fk_master_accounts_id");

            entity.HasOne(d => d.Period).WithMany(p => p.VoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_items_period_id_3525580e_fk_periods_id");

            entity.HasOne(d => d.ResourceAndExpenditure).WithMany(p => p.VoucherItems).HasConstraintName("voucher_items_resource_and_expenditure_id_c457dd03_fk_resource_and_expenditures_id");

            entity.HasOne(d => d.SlaveCompany).WithMany(p => p.VoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_items_slave_company_id_51b34aa7_fk_slave_account_companies_id");

            entity.HasOne(d => d.Slave).WithMany(p => p.VoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_items_slave_id_36e4e833_fk_slave_accounts_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.VoucherItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_items_voucher_id_e75336c1_fk_vouchers_id");
        });

        modelBuilder.Entity<VoucherItemAttach>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__voucher___3213E83F0FFD2452");

            entity.HasOne(d => d.Voucher).WithMany(p => p.VoucherItemAttaches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_item_attaches_voucher_id_05221da5_fk_vouchers_id");

            entity.HasOne(d => d.VoucherItem).WithMany(p => p.VoucherItemAttaches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_item_attaches_voucher_item_id_3f1216ab_fk_voucher_items_id");
        });

        modelBuilder.Entity<VoucherItemLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__voucher___3213E83FF23C9CEC");

            entity.HasOne(d => d.Ledger).WithMany(p => p.VoucherItemLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_item_logs_ledger_id_8b5e1cd0_fk_ledgers_id");

            entity.HasOne(d => d.ManualFloat1).WithMany(p => p.VoucherItemLogManualFloat1s).HasConstraintName("voucher_item_logs_manual_float_1_id_58b6802b_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat2).WithMany(p => p.VoucherItemLogManualFloat2s).HasConstraintName("voucher_item_logs_manual_float_2_id_38f71036_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat3).WithMany(p => p.VoucherItemLogManualFloat3s).HasConstraintName("voucher_item_logs_manual_float_3_id_aab41fd4_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat4).WithMany(p => p.VoucherItemLogManualFloat4s).HasConstraintName("voucher_item_logs_manual_float_4_id_f5608169_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat5).WithMany(p => p.VoucherItemLogManualFloat5s).HasConstraintName("voucher_item_logs_manual_float_5_id_d6f8534e_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat6).WithMany(p => p.VoucherItemLogManualFloat6s).HasConstraintName("voucher_item_logs_manual_float_6_id_356cdede_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat7).WithMany(p => p.VoucherItemLogManualFloat7s).HasConstraintName("voucher_item_logs_manual_float_7_id_c6d1e3a4_fk_manual_float_accounts_id");

            entity.HasOne(d => d.ManualFloat8).WithMany(p => p.VoucherItemLogManualFloat8s).HasConstraintName("voucher_item_logs_manual_float_8_id_97e9e83b_fk_manual_float_accounts_id");

            entity.HasOne(d => d.Master).WithMany(p => p.VoucherItemLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_item_logs_master_id_6b97e99d_fk_master_accounts_id");

            entity.HasOne(d => d.Period).WithMany(p => p.VoucherItemLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_item_logs_period_id_41dea737_fk_periods_id");

            entity.HasOne(d => d.ResourceAndExpenditure).WithMany(p => p.VoucherItemLogs).HasConstraintName("voucher_item_logs_resource_and_expenditure_id_6baa8522_fk_resource_and_expenditures_id");

            entity.HasOne(d => d.SlaveCompany).WithMany(p => p.VoucherItemLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_item_logs_slave_company_id_d30d38dd_fk_slave_account_companies_id");

            entity.HasOne(d => d.Slave).WithMany(p => p.VoucherItemLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_item_logs_slave_id_bcc29d21_fk_slave_accounts_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.VoucherItemLogs).HasConstraintName("voucher_item_logs_voucher_id_37e41a3c_fk_vouchers_id");

            entity.HasOne(d => d.VoucherLog).WithMany(p => p.VoucherItemLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_item_logs_voucher_log_id_4528bfd9_fk_voucher_logs_id");
        });

        modelBuilder.Entity<VoucherItemStandardDescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__voucher___3213E83F47C24B20");

            entity.HasIndex(e => new { e.CompanyId, e.Description }, "voucher_item_standard_descriptions_company_id_description_40767b2a_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [description] IS NOT NULL)");
        });

        modelBuilder.Entity<VoucherLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__voucher___3213E83FCD79D7C6");

            entity.HasOne(d => d.Ledger).WithMany(p => p.VoucherLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_logs_ledger_id_b35b1cc4_fk_ledgers_id");

            entity.HasOne(d => d.OldVoucherType).WithMany(p => p.VoucherLogOldVoucherTypes).HasConstraintName("voucher_logs_old_voucher_type_id_e9551d26_fk_voucher_types_id");

            entity.HasOne(d => d.Period).WithMany(p => p.VoucherLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("voucher_logs_period_id_02857e11_fk_periods_id");

            entity.HasOne(d => d.Voucher).WithMany(p => p.VoucherLogs).HasConstraintName("voucher_logs_voucher_id_34970cb1_fk_vouchers_id");

            entity.HasOne(d => d.VoucherType).WithMany(p => p.VoucherLogVoucherTypes).HasConstraintName("voucher_logs_voucher_type_id_5ba5b90d_fk_voucher_types_id");
        });

        modelBuilder.Entity<VoucherType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__voucher___3213E83F3A65AF6E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
