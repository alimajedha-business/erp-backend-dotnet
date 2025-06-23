using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("salary_increases", Schema = "payroll")]
[Index("CompanyId", Name = "salary_increases_company_id_bcf39dba")]
[Index("CreatorId", Name = "salary_increases_creator_id_67e5458c")]
[Index("EmploymentGroupId", Name = "salary_increases_employment_group_id_75fe1456")]
public partial class SalaryIncrease
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("increase_date")]
    public DateOnly IncreaseDate { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("employment_group_id")]
    public int EmploymentGroupId { get; set; }

    [ForeignKey("EmploymentGroupId")]
    [InverseProperty("SalaryIncreases")]
    public virtual EmploymentGroup EmploymentGroup { get; set; } = null!;

    [InverseProperty("SalaryIncrease")]
    public virtual ICollection<SalaryIncreaseFormula> SalaryIncreaseFormulas { get; set; } = new List<SalaryIncreaseFormula>();
}
