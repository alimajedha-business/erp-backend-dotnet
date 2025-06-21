using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("slave_account_companies", Schema = "accounting")]
[Index("CompanyId", Name = "slave_account_companies_company_id_10f63c16")]
[Index("FloatType1Id", Name = "slave_account_companies_float_type_1_id_dcee560d")]
[Index("FloatType2Id", Name = "slave_account_companies_float_type_2_id_c218860a")]
[Index("FloatType3Id", Name = "slave_account_companies_float_type_3_id_653411c6")]
[Index("FloatType4Id", Name = "slave_account_companies_float_type_4_id_57254460")]
[Index("FloatType5Id", Name = "slave_account_companies_float_type_5_id_896ccdfc")]
[Index("FloatType6Id", Name = "slave_account_companies_float_type_6_id_a486a7f8")]
[Index("FloatType7Id", Name = "slave_account_companies_float_type_7_id_f5d8293e")]
[Index("FloatType8Id", Name = "slave_account_companies_float_type_8_id_ab2b63c8")]
[Index("LedgerId", Name = "slave_account_companies_ledger_id_b40ad4d0")]
[Index("MasterId", Name = "slave_account_companies_master_id_bacd3012")]
[Index("SlaveId", Name = "slave_account_companies_slave_id_edd3dcf5")]
public partial class SlaveAccountCompany
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("need_ttms")]
    public bool NeedTtms { get; set; }

    [Column("deal_type")]
    public short? DealType { get; set; }

    [Column("has_absolute_deal_type")]
    public bool HasAbsoluteDealType { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("budget")]
    public double? Budget { get; set; }

    [Column("float_types")]
    [StringLength(100)]
    public string? FloatTypes { get; set; }

    [Column("float_type_set")]
    [StringLength(100)]
    public string? FloatTypeSet { get; set; }

    [Column("due_date")]
    public bool DueDate { get; set; }

    [Column("tag1")]
    [StringLength(15)]
    public string? Tag1 { get; set; }

    [Column("tag2")]
    [StringLength(15)]
    public string? Tag2 { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("authorized_users")]
    [StringLength(1000)]
    public string? AuthorizedUsers { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("float_type_1_id")]
    public int? FloatType1Id { get; set; }

    [Column("float_type_2_id")]
    public int? FloatType2Id { get; set; }

    [Column("float_type_3_id")]
    public int? FloatType3Id { get; set; }

    [Column("float_type_4_id")]
    public int? FloatType4Id { get; set; }

    [Column("float_type_5_id")]
    public int? FloatType5Id { get; set; }

    [Column("float_type_6_id")]
    public int? FloatType6Id { get; set; }

    [Column("float_type_7_id")]
    public int? FloatType7Id { get; set; }

    [Column("float_type_8_id")]
    public int? FloatType8Id { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("master_id")]
    public int MasterId { get; set; }

    [Column("slave_id")]
    public int SlaveId { get; set; }

    [InverseProperty("SlaveCompany")]
    public virtual ICollection<ClosingPatternSlaveCompany> ClosingPatternSlaveCompanies { get; set; } = new List<ClosingPatternSlaveCompany>();

    [ForeignKey("FloatType1Id")]
    [InverseProperty("SlaveAccountCompanyFloatType1s")]
    public virtual FloatAccountType? FloatType1 { get; set; }

    [ForeignKey("FloatType2Id")]
    [InverseProperty("SlaveAccountCompanyFloatType2s")]
    public virtual FloatAccountType? FloatType2 { get; set; }

    [ForeignKey("FloatType3Id")]
    [InverseProperty("SlaveAccountCompanyFloatType3s")]
    public virtual FloatAccountType? FloatType3 { get; set; }

    [ForeignKey("FloatType4Id")]
    [InverseProperty("SlaveAccountCompanyFloatType4s")]
    public virtual FloatAccountType? FloatType4 { get; set; }

    [ForeignKey("FloatType5Id")]
    [InverseProperty("SlaveAccountCompanyFloatType5s")]
    public virtual FloatAccountType? FloatType5 { get; set; }

    [ForeignKey("FloatType6Id")]
    [InverseProperty("SlaveAccountCompanyFloatType6s")]
    public virtual FloatAccountType? FloatType6 { get; set; }

    [ForeignKey("FloatType7Id")]
    [InverseProperty("SlaveAccountCompanyFloatType7s")]
    public virtual FloatAccountType? FloatType7 { get; set; }

    [ForeignKey("FloatType8Id")]
    [InverseProperty("SlaveAccountCompanyFloatType8s")]
    public virtual FloatAccountType? FloatType8 { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("SlaveAccountCompanies")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("MasterId")]
    [InverseProperty("SlaveAccountCompanies")]
    public virtual MasterAccount Master { get; set; } = null!;

    [ForeignKey("SlaveId")]
    [InverseProperty("SlaveAccountCompanies")]
    public virtual SlaveAccount Slave { get; set; } = null!;

    [InverseProperty("SlaveCompany")]
    public virtual ICollection<TrashVoucherItem> TrashVoucherItems { get; set; } = new List<TrashVoucherItem>();

    [InverseProperty("SlaveCompany")]
    public virtual ICollection<VoucherItemLog> VoucherItemLogs { get; set; } = new List<VoucherItemLog>();

    [InverseProperty("SlaveCompany")]
    public virtual ICollection<VoucherItem> VoucherItems { get; set; } = new List<VoucherItem>();
}
