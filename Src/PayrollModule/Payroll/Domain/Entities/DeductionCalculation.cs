using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("deduction_calculations", Schema = "payroll")]
[Index("CompanyId", Name = "deduction_calculations_company_id_998cbcb9")]
[Index("CreatorId", Name = "deduction_calculations_creator_id_72073e3f")]
[Index("DeductionId", Name = "deduction_calculations_deduction_id_7989dbc7")]
[Index("WorkRecordId", Name = "deduction_calculations_work_record_id_c2e19b2c")]
public partial class DeductionCalculation
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

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("deduction_id")]
    public int DeductionId { get; set; }

    [Column("work_record_id")]
    public int WorkRecordId { get; set; }

    [ForeignKey("DeductionId")]
    [InverseProperty("DeductionCalculations")]
    public virtual Deduction Deduction { get; set; } = null!;

    [ForeignKey("WorkRecordId")]
    [InverseProperty("DeductionCalculations")]
    public virtual WorkRecord WorkRecord { get; set; } = null!;
}
