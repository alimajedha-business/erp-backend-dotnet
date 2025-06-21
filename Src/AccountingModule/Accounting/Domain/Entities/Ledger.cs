using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("ledgers", Schema = "accounting")]
[Index("Code", Name = "UQ__ledgers__357D4CF92E60519D", IsUnique = true)]
[Index("Name", Name = "UQ__ledgers__72E12F1B2CE796EF", IsUnique = true)]
[Index("Name2", Name = "UQ__ledgers__F0B628432B41C06C", IsUnique = true)]
public partial class Ledger
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    public short Code { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("name2")]
    [StringLength(100)]
    public string Name2 { get; set; } = null!;

    [Column("is_leading")]
    public bool IsLeading { get; set; }

    [Column("description")]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Ledger")]
    public virtual ICollection<AccountSet> AccountSets { get; set; } = new List<AccountSet>();

    [InverseProperty("Ledger")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetails { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("Ledger")]
    public virtual ICollection<ClosingPatternSlaveCompany> ClosingPatternSlaveCompanies { get; set; } = new List<ClosingPatternSlaveCompany>();

    [InverseProperty("Ledger")]
    public virtual ICollection<ClosingPatternTemporaryAccount> ClosingPatternTemporaryAccounts { get; set; } = new List<ClosingPatternTemporaryAccount>();

    [InverseProperty("Ledger")]
    public virtual ICollection<ClosingPattern> ClosingPatterns { get; set; } = new List<ClosingPattern>();

    [InverseProperty("Ledger")]
    public virtual ICollection<Contradiction> Contradictions { get; set; } = new List<Contradiction>();

    [InverseProperty("Ledger")]
    public virtual ICollection<DataImport> DataImports { get; set; } = new List<DataImport>();

    [InverseProperty("Ledger")]
    public virtual ICollection<LedgerPeriodCompany> LedgerPeriodCompanies { get; set; } = new List<LedgerPeriodCompany>();

    [InverseProperty("Ledger")]
    public virtual ICollection<LedgerPeriodCompanySetting> LedgerPeriodCompanySettings { get; set; } = new List<LedgerPeriodCompanySetting>();

    [InverseProperty("Ledger")]
    public virtual ICollection<LedgerPeriod> LedgerPeriods { get; set; } = new List<LedgerPeriod>();

    [InverseProperty("Ledger")]
    public virtual ICollection<MasterAccount> MasterAccounts { get; set; } = new List<MasterAccount>();

    [InverseProperty("Ledger")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanies { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("Ledger")]
    public virtual ICollection<SlaveAccount> SlaveAccounts { get; set; } = new List<SlaveAccount>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItems { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TrashVoucher> TrashVouchers { get; set; } = new List<TrashVoucher>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsBuy> TtmsBuys { get; set; } = new List<TtmsBuy>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsContractorInfo> TtmsContractorInfos { get; set; } = new List<TtmsContractorInfo>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsEmployerInfo> TtmsEmployerInfos { get; set; } = new List<TtmsEmployerInfo>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsExportation> TtmsExportations { get; set; } = new List<TtmsExportation>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsImportation> TtmsImportations { get; set; } = new List<TtmsImportation>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsLeaseAgreement> TtmsLeaseAgreements { get; set; } = new List<TtmsLeaseAgreement>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsPreSell> TtmsPreSells { get; set; } = new List<TtmsPreSell>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsSell> TtmsSells { get; set; } = new List<TtmsSell>();

    [InverseProperty("Ledger")]
    public virtual ICollection<TtmsWage> TtmsWages { get; set; } = new List<TtmsWage>();

    [InverseProperty("Ledger")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogs { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("Ledger")]
    public virtual ICollection<VoucherItem> VoucherItems { get; set; } = new List<VoucherItem>();

    [InverseProperty("Ledger")]
    public virtual ICollection<VoucherLog> VoucherLogs { get; set; } = new List<VoucherLog>();

    [InverseProperty("Ledger")]
    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
