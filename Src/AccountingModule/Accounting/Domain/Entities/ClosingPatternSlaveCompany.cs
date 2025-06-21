using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("closing_pattern_slave_companies", Schema = "accounting")]
[Index("CompanyId", Name = "closing_pattern_slave_companies_company_id_d40fc855")]
[Index("DefaultCashierPeriodId", Name = "closing_pattern_slave_companies_default_cashier_period_id_92a586f2")]
[Index("DefaultCompanyUnitId", Name = "closing_pattern_slave_companies_default_company_unit_id_2c7875df")]
[Index("DefaultCurrencyId", Name = "closing_pattern_slave_companies_default_currency_id_8d86bb77")]
[Index("DefaultPersonId", Name = "closing_pattern_slave_companies_default_person_id_94d4611e")]
[Index("DefaultPettyCashierPeriodId", Name = "closing_pattern_slave_companies_default_petty_cashier_period_id_6135e6e8")]
[Index("DefaultProjectId", Name = "closing_pattern_slave_companies_default_project_id_becc3a53")]
[Index("DefaultResourceAndExpenditureId", Name = "closing_pattern_slave_companies_default_resource_and_expenditure_id_d8808ca1")]
[Index("DefaultStoreId", Name = "closing_pattern_slave_companies_default_store_id_2b4de0c5")]
[Index("LedgerId", Name = "closing_pattern_slave_companies_ledger_id_b7344906")]
[Index("PeriodId", Name = "closing_pattern_slave_companies_period_id_7d4ec4d3")]
[Index("SlaveCompanyId", Name = "closing_pattern_slave_companies_slave_company_id_e971e36e")]
public partial class ClosingPatternSlaveCompany
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("default_company_unit_id")]
    public int? DefaultCompanyUnitId { get; set; }

    [Column("default_currency_id")]
    public int? DefaultCurrencyId { get; set; }

    [Column("default_person_id")]
    public int? DefaultPersonId { get; set; }

    [Column("default_project_id")]
    public int? DefaultProjectId { get; set; }

    [Column("default_resource_and_expenditure_id")]
    public int? DefaultResourceAndExpenditureId { get; set; }

    [Column("default_store_id")]
    public int? DefaultStoreId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("slave_company_id")]
    public int SlaveCompanyId { get; set; }

    [Column("default_cashier_period_id")]
    public int? DefaultCashierPeriodId { get; set; }

    [Column("default_petty_cashier_period_id")]
    public int? DefaultPettyCashierPeriodId { get; set; }

    [ForeignKey("DefaultResourceAndExpenditureId")]
    [InverseProperty("ClosingPatternSlaveCompanies")]
    public virtual ResourceAndExpenditure? DefaultResourceAndExpenditure { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("ClosingPatternSlaveCompanies")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("ClosingPatternSlaveCompanies")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("SlaveCompanyId")]
    [InverseProperty("ClosingPatternSlaveCompanies")]
    public virtual SlaveAccountCompany SlaveCompany { get; set; } = null!;
}
