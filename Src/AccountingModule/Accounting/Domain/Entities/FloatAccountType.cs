using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("float_account_types", Schema = "accounting")]
[Index("NameFa", Name = "UQ__float_ac__2E0A7AA653BB5215", IsUnique = true)]
[Index("ParentId", Name = "float_account_types_parent_id_4dd8ba99")]
public partial class FloatAccountType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("is_static")]
    public bool IsStatic { get; set; }

    [Column("name_fa")]
    [StringLength(100)]
    public string NameFa { get; set; } = null!;

    [Column("name_en")]
    [StringLength(100)]
    public string? NameEn { get; set; }

    [Column("voucher_item_float_level")]
    public short? VoucherItemFloatLevel { get; set; }

    [Column("extra_1_title")]
    [StringLength(50)]
    public string? Extra1Title { get; set; }

    [Column("extra_1_title2")]
    [StringLength(50)]
    public string? Extra1Title2 { get; set; }

    [Column("extra_1_type")]
    [StringLength(8)]
    public string? Extra1Type { get; set; }

    [Column("extra_1_required")]
    public bool? Extra1Required { get; set; }

    [Column("extra_2_title")]
    [StringLength(50)]
    public string? Extra2Title { get; set; }

    [Column("extra_2_title2")]
    [StringLength(50)]
    public string? Extra2Title2 { get; set; }

    [Column("extra_2_type")]
    [StringLength(8)]
    public string? Extra2Type { get; set; }

    [Column("extra_2_required")]
    public bool? Extra2Required { get; set; }

    [Column("extra_3_title")]
    [StringLength(50)]
    public string? Extra3Title { get; set; }

    [Column("extra_3_title2")]
    [StringLength(50)]
    public string? Extra3Title2 { get; set; }

    [Column("extra_3_type")]
    [StringLength(8)]
    public string? Extra3Type { get; set; }

    [Column("extra_3_required")]
    public bool? Extra3Required { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<FloatAccountType> InverseParent { get; set; } = new List<FloatAccountType>();

    [InverseProperty("FloatAccountType")]
    public virtual ICollection<ManualFloatAccount> ManualFloatAccounts { get; set; } = new List<ManualFloatAccount>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual FloatAccountType? Parent { get; set; }

    [InverseProperty("FloatType1")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanyFloatType1s { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("FloatType2")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanyFloatType2s { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("FloatType3")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanyFloatType3s { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("FloatType4")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanyFloatType4s { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("FloatType5")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanyFloatType5s { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("FloatType6")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanyFloatType6s { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("FloatType7")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanyFloatType7s { get; set; } = new List<SlaveAccountCompany>();

    [InverseProperty("FloatType8")]
    public virtual ICollection<SlaveAccountCompany> SlaveAccountCompanyFloatType8s { get; set; } = new List<SlaveAccountCompany>();
}
