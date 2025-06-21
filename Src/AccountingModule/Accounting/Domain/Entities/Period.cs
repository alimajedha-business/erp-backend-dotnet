using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("periods", Schema = "accounting")]
[Index("Name", Name = "UQ__periods__72E12F1BE18188D5", IsUnique = true)]
public partial class Period
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column("description")]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Period")]
    public virtual ICollection<BalanceRelatedAccountDetail> BalanceRelatedAccountDetails { get; set; } = new List<BalanceRelatedAccountDetail>();

    [InverseProperty("Period")]
    public virtual ICollection<ClosingPatternSlaveCompany> ClosingPatternSlaveCompanies { get; set; } = new List<ClosingPatternSlaveCompany>();

    [InverseProperty("Period")]
    public virtual ICollection<ClosingPatternTemporaryAccount> ClosingPatternTemporaryAccounts { get; set; } = new List<ClosingPatternTemporaryAccount>();

    [InverseProperty("Period")]
    public virtual ICollection<ClosingPattern> ClosingPatterns { get; set; } = new List<ClosingPattern>();

    [InverseProperty("Period")]
    public virtual ICollection<Contradiction> Contradictions { get; set; } = new List<Contradiction>();

    [InverseProperty("Period")]
    public virtual ICollection<DataImport> DataImports { get; set; } = new List<DataImport>();

    [InverseProperty("Period")]
    public virtual ICollection<LedgerPeriodCompany> LedgerPeriodCompanies { get; set; } = new List<LedgerPeriodCompany>();

    [InverseProperty("Period")]
    public virtual ICollection<LedgerPeriodCompanySetting> LedgerPeriodCompanySettings { get; set; } = new List<LedgerPeriodCompanySetting>();

    [InverseProperty("Period")]
    public virtual ICollection<LedgerPeriod> LedgerPeriods { get; set; } = new List<LedgerPeriod>();

    [InverseProperty("Period")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItems { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("Period")]
    public virtual ICollection<TrashVoucher> TrashVouchers { get; set; } = new List<TrashVoucher>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsBuy> TtmsBuys { get; set; } = new List<TtmsBuy>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsContractorInfo> TtmsContractorInfos { get; set; } = new List<TtmsContractorInfo>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsEmployerInfo> TtmsEmployerInfos { get; set; } = new List<TtmsEmployerInfo>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsExportation> TtmsExportations { get; set; } = new List<TtmsExportation>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsImportation> TtmsImportations { get; set; } = new List<TtmsImportation>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsLeaseAgreement> TtmsLeaseAgreements { get; set; } = new List<TtmsLeaseAgreement>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsPreSell> TtmsPreSells { get; set; } = new List<TtmsPreSell>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsSell> TtmsSells { get; set; } = new List<TtmsSell>();

    [InverseProperty("Period")]
    public virtual ICollection<TtmsWage> TtmsWages { get; set; } = new List<TtmsWage>();

    [InverseProperty("Period")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogs { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("Period")]
    public virtual ICollection<VoucherItem> VoucherItems { get; set; } = new List<VoucherItem>();

    [InverseProperty("Period")]
    public virtual ICollection<VoucherLog> VoucherLogs { get; set; } = new List<VoucherLog>();

    [InverseProperty("Period")]
    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
