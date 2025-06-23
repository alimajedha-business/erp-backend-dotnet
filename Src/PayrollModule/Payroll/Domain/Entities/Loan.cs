using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("loans", Schema = "payroll")]
[Index("CompanyId", Name = "loans_company_id_c7fdb4a2")]
[Index("CreatorId", Name = "loans_creator_id_96eb2361")]
public partial class Loan
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("be_calculated")]
    public bool BeCalculated { get; set; }

    [Column("be_printed")]
    public bool BePrinted { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [InverseProperty("Loan")]
    public virtual ICollection<PersonnelLoan> PersonnelLoans { get; set; } = new List<PersonnelLoan>();
}
