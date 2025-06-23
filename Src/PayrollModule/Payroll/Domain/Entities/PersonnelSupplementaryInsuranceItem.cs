using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("personnel_supplementary_insurance_items", Schema = "payroll")]
[Index("CreatorId", Name = "personnel_supplementary_insurance_items_creator_id_948b6be9")]
[Index("PersonnelSupplementaryInsuranceId", Name = "personnel_supplementary_insurance_items_personnel_supplementary_insurance_id_94a5d15d")]
[Index("SupplementaryInsuranceId", Name = "personnel_supplementary_insurance_items_supplementary_insurance_id_07fa3946")]
public partial class PersonnelSupplementaryInsuranceItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("count")]
    public int? Count { get; set; }

    [Column("amount", TypeName = "numeric(18, 2)")]
    public decimal Amount { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("personnel_supplementary_insurance_id")]
    public int PersonnelSupplementaryInsuranceId { get; set; }

    [Column("supplementary_insurance_id")]
    public int SupplementaryInsuranceId { get; set; }

    [ForeignKey("PersonnelSupplementaryInsuranceId")]
    [InverseProperty("PersonnelSupplementaryInsuranceItems")]
    public virtual PersonnelSupplementaryInsurance PersonnelSupplementaryInsurance { get; set; } = null!;

    [ForeignKey("SupplementaryInsuranceId")]
    [InverseProperty("PersonnelSupplementaryInsuranceItems")]
    public virtual SupplementaryInsurance SupplementaryInsurance { get; set; } = null!;
}
