using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("closing_patterns", Schema = "accounting")]
[Index("CompanyId", Name = "closing_patterns_company_id_9cd07d27")]
[Index("DefaultCashierPeriodId", Name = "closing_patterns_default_cashier_period_id_2116c839")]
[Index("DefaultCompanyUnitId", Name = "closing_patterns_default_company_unit_id_b57cd2d3")]
[Index("DefaultCurrencyId", Name = "closing_patterns_default_currency_id_efd08b2d")]
[Index("DefaultPersonId", Name = "closing_patterns_default_person_id_d1e830cb")]
[Index("DefaultPettyCashierPeriodId", Name = "closing_patterns_default_petty_cashier_period_id_8d44d43e")]
[Index("DefaultProjectId", Name = "closing_patterns_default_project_id_354a5095")]
[Index("DefaultResourceAndExpenditureId", Name = "closing_patterns_default_resource_and_expenditure_id_6ef35f22")]
[Index("DefaultStoreId", Name = "closing_patterns_default_store_id_0f67c8c0")]
[Index("LedgerId", Name = "closing_patterns_ledger_id_394ee754")]
[Index("PeriodId", Name = "closing_patterns_period_id_8a00d0d0")]
public partial class ClosingPattern
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("float_type_set")]
    [StringLength(18)]
    public string FloatTypeSet { get; set; } = null!;

    [Column("is_set")]
    public bool IsSet { get; set; }

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

    [Column("default_cashier_period_id")]
    public int? DefaultCashierPeriodId { get; set; }

    [Column("default_petty_cashier_period_id")]
    public int? DefaultPettyCashierPeriodId { get; set; }

    [ForeignKey("DefaultResourceAndExpenditureId")]
    [InverseProperty("ClosingPatterns")]
    public virtual ResourceAndExpenditure? DefaultResourceAndExpenditure { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("ClosingPatterns")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("ClosingPatterns")]
    public virtual Period Period { get; set; } = null!;
}
