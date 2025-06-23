using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("benefit_calculations", Schema = "payroll")]
[Index("BenefitId", Name = "benefit_calculations_benefit_id_12da378e")]
[Index("CompanyId", Name = "benefit_calculations_company_id_efc48197")]
[Index("CreatorId", Name = "benefit_calculations_creator_id_6643d6d3")]
[Index("WorkRecordId", Name = "benefit_calculations_work_record_id_cbcdd8b9")]
public partial class BenefitCalculation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("count", TypeName = "numeric(18, 2)")]
    public decimal Count { get; set; }

    [Column("amount", TypeName = "numeric(18, 2)")]
    public decimal Amount { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("benefit_id")]
    public int BenefitId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("work_record_id")]
    public int WorkRecordId { get; set; }

    [ForeignKey("BenefitId")]
    [InverseProperty("BenefitCalculations")]
    public virtual Benefit Benefit { get; set; } = null!;

    [ForeignKey("WorkRecordId")]
    [InverseProperty("BenefitCalculations")]
    public virtual WorkRecord WorkRecord { get; set; } = null!;
}
