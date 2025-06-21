using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("closing_pattern_temporary_accounts", Schema = "accounting")]
[Index("CompanyId", Name = "closing_pattern_temporary_accounts_company_id_952b9b60")]
[Index("DefaultCashierPeriodId", Name = "closing_pattern_temporary_accounts_default_cashier_period_id_d8091266")]
[Index("DefaultCompanyUnitId", Name = "closing_pattern_temporary_accounts_default_company_unit_id_086a631a")]
[Index("DefaultCostCenterId", Name = "closing_pattern_temporary_accounts_default_cost_center_id_76aab24b")]
[Index("DefaultCurrencyId", Name = "closing_pattern_temporary_accounts_default_currency_id_2f9de639")]
[Index("DefaultPersonId", Name = "closing_pattern_temporary_accounts_default_person_id_a4961ab4")]
[Index("DefaultPettyCashierPeriodId", Name = "closing_pattern_temporary_accounts_default_petty_cashier_period_id_4d49f688")]
[Index("DefaultProjectId", Name = "closing_pattern_temporary_accounts_default_project_id_bbbb80e7")]
[Index("DefaultResourceAndExpenditureId", Name = "closing_pattern_temporary_accounts_default_resource_and_expenditure_id_dcf3d48a")]
[Index("DefaultStoreId", Name = "closing_pattern_temporary_accounts_default_store_id_43427fea")]
[Index("LedgerId", Name = "closing_pattern_temporary_accounts_ledger_id_8d2baceb")]
[Index("PeriodId", Name = "closing_pattern_temporary_accounts_period_id_897435aa")]
public partial class ClosingPatternTemporaryAccount
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("float_type_set")]
    [StringLength(20)]
    public string FloatTypeSet { get; set; } = null!;

    [Column("is_set")]
    public bool IsSet { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("default_company_unit_id")]
    public int? DefaultCompanyUnitId { get; set; }

    [Column("default_cost_center_id")]
    public int? DefaultCostCenterId { get; set; }

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

    [Column("default_cashier_period_id")]
    public int? DefaultCashierPeriodId { get; set; }

    [Column("default_petty_cashier_period_id")]
    public int? DefaultPettyCashierPeriodId { get; set; }

    [ForeignKey("DefaultResourceAndExpenditureId")]
    [InverseProperty("ClosingPatternTemporaryAccounts")]
    public virtual ResourceAndExpenditure? DefaultResourceAndExpenditure { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("ClosingPatternTemporaryAccounts")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("ClosingPatternTemporaryAccounts")]
    public virtual Period Period { get; set; } = null!;
}
