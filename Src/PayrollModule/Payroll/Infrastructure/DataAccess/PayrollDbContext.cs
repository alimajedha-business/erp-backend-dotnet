using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Payroll.Domain.Entities;

namespace Payroll.Infrastructure.DataAccess;

public partial class PayrollDbContext : DbContext
{
    public PayrollDbContext()
    {
    }

    public PayrollDbContext(DbContextOptions<PayrollDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnnualBenefitSetting> AnnualBenefitSettings { get; set; }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<BenefitCalculation> BenefitCalculations { get; set; }

    public virtual DbSet<BenefitGroup> BenefitGroups { get; set; }

    public virtual DbSet<Deduction> Deductions { get; set; }

    public virtual DbSet<DeductionCalculation> DeductionCalculations { get; set; }

    public virtual DbSet<DeductionGroup> DeductionGroups { get; set; }

    public virtual DbSet<EmploymentContractTemplate> EmploymentContractTemplates { get; set; }

    public virtual DbSet<EmploymentContractTemplateItem> EmploymentContractTemplateItems { get; set; }

    public virtual DbSet<EmploymentContractTemplateItemGroup> EmploymentContractTemplateItemGroups { get; set; }

    public virtual DbSet<EmploymentGroup> EmploymentGroups { get; set; }

    public virtual DbSet<InsuranceWorkshop> InsuranceWorkshops { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<PersonnelContract> PersonnelContracts { get; set; }

    public virtual DbSet<PersonnelContractItem> PersonnelContractItems { get; set; }

    public virtual DbSet<PersonnelFinancialInformation> PersonnelFinancialInformations { get; set; }

    public virtual DbSet<PersonnelInsurance> PersonnelInsurances { get; set; }

    public virtual DbSet<PersonnelJob> PersonnelJobs { get; set; }

    public virtual DbSet<PersonnelLoan> PersonnelLoans { get; set; }

    public virtual DbSet<PersonnelSupplementaryInsurance> PersonnelSupplementaryInsurances { get; set; }

    public virtual DbSet<PersonnelSupplementaryInsuranceItem> PersonnelSupplementaryInsuranceItems { get; set; }

    public virtual DbSet<SalaryCalculationFactor> SalaryCalculationFactors { get; set; }

    public virtual DbSet<SalaryIncrease> SalaryIncreases { get; set; }

    public virtual DbSet<SalaryIncreaseFormula> SalaryIncreaseFormulas { get; set; }

    public virtual DbSet<SupplementaryInsurance> SupplementaryInsurances { get; set; }

    public virtual DbSet<TaxTable> TaxTables { get; set; }

    public virtual DbSet<TaxTableItem> TaxTableItems { get; set; }

    public virtual DbSet<TaxWorkshop> TaxWorkshops { get; set; }

    public virtual DbSet<WorkRecord> WorkRecords { get; set; }

    public virtual DbSet<PayrollTest> PayrollTest { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\sql19;Database=NGERP;User Id=sa;Password=123;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Persian_100_CI_AI");

        modelBuilder.Entity<AnnualBenefitSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__annual_b__3213E83F824C3055");

            entity.HasOne(d => d.EmploymentGroup).WithMany(p => p.AnnualBenefitSettings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("annual_benefit_settings_employment_group_id_9f1f84fc_fk_employment_groups_id");
        });

        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__benefits__3213E83FEB503324");

            entity.HasOne(d => d.BenefitGroup).WithMany(p => p.Benefits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("benefits_benefit_group_id_2bee0bf8_fk_benefit_groups_id");
        });

        modelBuilder.Entity<BenefitCalculation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__benefit___3213E83FC2D64274");

            entity.HasIndex(e => new { e.CompanyId, e.WorkRecordId, e.BenefitId }, "benefit_calculations_company_id_work_record_id_benefit_id_63d802c2_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [work_record_id] IS NOT NULL AND [benefit_id] IS NOT NULL)");

            entity.HasOne(d => d.Benefit).WithMany(p => p.BenefitCalculations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("benefit_calculations_benefit_id_12da378e_fk_benefits_id");

            entity.HasOne(d => d.WorkRecord).WithMany(p => p.BenefitCalculations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("benefit_calculations_work_record_id_cbcdd8b9_fk_work_records_id");
        });

        modelBuilder.Entity<BenefitGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__benefit___3213E83FA9D20E38");
        });

        modelBuilder.Entity<Deduction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__deductio__3213E83F0DAB33E8");

            entity.HasOne(d => d.DeductionGroup).WithMany(p => p.Deductions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deductions_deduction_group_id_87a9be7d_fk_deduction_groups_id");
        });

        modelBuilder.Entity<DeductionCalculation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__deductio__3213E83FA425E34E");

            entity.HasIndex(e => new { e.CompanyId, e.WorkRecordId, e.DeductionId }, "deduction_calculations_company_id_work_record_id_deduction_id_3581dacd_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [work_record_id] IS NOT NULL AND [deduction_id] IS NOT NULL)");

            entity.HasOne(d => d.Deduction).WithMany(p => p.DeductionCalculations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deduction_calculations_deduction_id_7989dbc7_fk_deductions_id");

            entity.HasOne(d => d.WorkRecord).WithMany(p => p.DeductionCalculations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deduction_calculations_work_record_id_c2e19b2c_fk_work_records_id");
        });

        modelBuilder.Entity<DeductionGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__deductio__3213E83FA3772BEE");
        });

        modelBuilder.Entity<EmploymentContractTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employme__3213E83F1F7777B4");

            entity.HasOne(d => d.EmploymentGroup).WithMany(p => p.EmploymentContractTemplates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employment_contract_templates_employment_group_id_7b23ef4f_fk_employment_groups_id");
        });

        modelBuilder.Entity<EmploymentContractTemplateItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employme__3213E83FC1710177");

            entity.HasOne(d => d.EctItemGroup).WithMany(p => p.EmploymentContractTemplateItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employment_contract_template_items_ect_item_group_id_7f2f5383_fk_employment_contract_template_item_groups_id");

            entity.HasOne(d => d.EmploymentContractTemplate).WithMany(p => p.EmploymentContractTemplateItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employment_contract_template_items_employment_contract_template_id_c433e8e3_fk_employment_contract_templates_id");
        });

        modelBuilder.Entity<EmploymentContractTemplateItemGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employme__3213E83F1EA0C149");
        });

        modelBuilder.Entity<EmploymentGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__employme__3213E83FB0AB0244");
        });

        modelBuilder.Entity<InsuranceWorkshop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__insuranc__3213E83F4B156684");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__loans__3213E83F08578BD1");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__missions__3213E83F087A8A07");

            entity.HasOne(d => d.WorkRecord).WithMany(p => p.Missions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("missions_work_record_id_71024543_fk_work_records_id");
        });

        modelBuilder.Entity<PersonnelContract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83F2E47C359");

            entity.HasOne(d => d.EmploymentContractTemplate).WithMany(p => p.PersonnelContracts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_contracts_employment_contract_template_id_57942850_fk_employment_contract_templates_id");

            entity.HasOne(d => d.PersonnelFinancialInformation).WithMany(p => p.PersonnelContracts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_contracts_personnel_financial_information_id_413e25a0_fk_personnel_financial_information_id");

            entity.HasOne(d => d.PersonnelInsurance).WithMany(p => p.PersonnelContracts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_contracts_personnel_insurance_id_f9e2914b_fk_personnel_insurances_id");

            entity.HasOne(d => d.PersonnelJob).WithMany(p => p.PersonnelContracts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_contracts_personnel_job_id_2561e515_fk_personnel_jobs_id");
        });

        modelBuilder.Entity<PersonnelContractItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83F91E2C5CC");

            entity.HasOne(d => d.EmploymentContractTemplateItem).WithMany(p => p.PersonnelContractItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_contract_items_employment_contract_template_item_id_46dfb850_fk_employment_contract_template_items_id");

            entity.HasOne(d => d.PersonnelContract).WithMany(p => p.PersonnelContractItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_contract_items_personnel_contract_id_06379213_fk_personnel_contracts_id");
        });

        modelBuilder.Entity<PersonnelFinancialInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83F2675954D");
        });

        modelBuilder.Entity<PersonnelInsurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83F1C0DED88");

            entity.HasOne(d => d.InsuranceWorkshop).WithMany(p => p.PersonnelInsurances).HasConstraintName("personnel_insurances_insurance_workshop_id_944b8b6b_fk_insurance_workshops_id");
        });

        modelBuilder.Entity<PersonnelJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83FC2B0A9C8");

            entity.HasOne(d => d.EmploymentGroup).WithMany(p => p.PersonnelJobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_jobs_employment_group_id_eef6c278_fk_employment_groups_id");

            entity.HasOne(d => d.TaxWorkshop).WithMany(p => p.PersonnelJobs).HasConstraintName("personnel_jobs_tax_workshop_id_82dbb82b_fk_tax_workshops_id");
        });

        modelBuilder.Entity<PersonnelLoan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83F3C08604D");

            entity.HasOne(d => d.Loan).WithMany(p => p.PersonnelLoans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_loans_loan_id_0a1a63ce_fk_loans_id");
        });

        modelBuilder.Entity<PersonnelSupplementaryInsurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83FCED332CC");
        });

        modelBuilder.Entity<PersonnelSupplementaryInsuranceItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personne__3213E83F50FF55B5");

            entity.HasOne(d => d.PersonnelSupplementaryInsurance).WithMany(p => p.PersonnelSupplementaryInsuranceItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_supplementary_insurance_items_personnel_supplementary_insurance_id_94a5d15d_fk_personnel_supplementary_insurances_id");

            entity.HasOne(d => d.SupplementaryInsurance).WithMany(p => p.PersonnelSupplementaryInsuranceItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personnel_supplementary_insurance_items_supplementary_insurance_id_07fa3946_fk_supplementary_insurances_id");
        });

        modelBuilder.Entity<SalaryCalculationFactor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__salary_c__3213E83FEBD30915");

            entity.HasOne(d => d.EmploymentGroup).WithMany(p => p.SalaryCalculationFactors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salary_calculation_factors_employment_group_id_156b0b52_fk_employment_groups_id");
        });

        modelBuilder.Entity<SalaryIncrease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__salary_i__3213E83F161E00D8");

            entity.HasOne(d => d.EmploymentGroup).WithMany(p => p.SalaryIncreases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salary_increases_employment_group_id_75fe1456_fk_employment_groups_id");
        });

        modelBuilder.Entity<SalaryIncreaseFormula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__salary_i__3213E83F7C2E05C3");

            entity.HasOne(d => d.EmploymentContractTemplateItem).WithMany(p => p.SalaryIncreaseFormulas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salary_increase_formulas_employment_contract_template_item_id_3d8ec62b_fk_employment_contract_template_items_id");

            entity.HasOne(d => d.SalaryIncrease).WithMany(p => p.SalaryIncreaseFormulas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salary_increase_formulas_salary_increase_id_10108bf7_fk_salary_increases_id");
        });

        modelBuilder.Entity<SupplementaryInsurance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__suppleme__3213E83F453AB650");

            entity.HasOne(d => d.EmploymentGroup).WithMany(p => p.SupplementaryInsurances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplementary_insurances_employment_group_id_2794e678_fk_employment_groups_id");
        });

        modelBuilder.Entity<TaxTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tax_tabl__3213E83FB393A022");

            entity.HasOne(d => d.EmploymentGroup).WithMany(p => p.TaxTables)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tax_tables_employment_group_id_40d614e6_fk_employment_groups_id");
        });

        modelBuilder.Entity<TaxTableItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tax_tabl__3213E83F50E9B26F");

            entity.HasOne(d => d.TaxTable).WithMany(p => p.TaxTableItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tax_table_items_tax_table_id_c6856ca1_fk_tax_tables_id");
        });

        modelBuilder.Entity<TaxWorkshop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tax_work__3213E83F4DF7908B");
        });

        modelBuilder.Entity<WorkRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__work_rec__3213E83FFF9C9EF3");

            entity.HasOne(d => d.PersonnelContract).WithMany(p => p.WorkRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_records_personnel_contract_id_ed43badc_fk_personnel_contracts_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
