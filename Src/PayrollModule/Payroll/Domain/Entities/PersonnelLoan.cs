using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("personnel_loans", Schema = "payroll")]
[Index("CompanyId", Name = "personnel_loans_company_id_b6e3d117")]
[Index("CreatorId", Name = "personnel_loans_creator_id_498fd766")]
[Index("LoanId", Name = "personnel_loans_loan_id_0a1a63ce")]
[Index("PersonId", Name = "personnel_loans_person_id_7bb386a0")]
public partial class PersonnelLoan
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("number")]
    public int? Number { get; set; }

    [Column("amount", TypeName = "numeric(18, 2)")]
    public decimal Amount { get; set; }

    [Column("number_of_installments")]
    public short NumberOfInstallments { get; set; }

    [Column("first_installment_amount", TypeName = "numeric(18, 2)")]
    public decimal? FirstInstallmentAmount { get; set; }

    [Column("installment_amount", TypeName = "numeric(18, 2)")]
    public decimal InstallmentAmount { get; set; }

    [Column("description")]
    [StringLength(1000)]
    public string? Description { get; set; }

    [Column("be_deducted")]
    public bool BeDeducted { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("loan_id")]
    public int LoanId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("LoanId")]
    [InverseProperty("PersonnelLoans")]
    public virtual Loan Loan { get; set; } = null!;
}
