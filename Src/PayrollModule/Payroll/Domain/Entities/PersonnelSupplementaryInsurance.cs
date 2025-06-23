using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("personnel_supplementary_insurances", Schema = "payroll")]
[Index("CompanyId", Name = "personnel_supplementary_insurances_company_id_d47cd62c")]
[Index("CreatorId", Name = "personnel_supplementary_insurances_creator_id_953b1a80")]
[Index("PersonId", Name = "personnel_supplementary_insurances_person_id_87ce5b4f")]
public partial class PersonnelSupplementaryInsurance
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("is_default")]
    public bool IsDefault { get; set; }

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }

    [Column("deduct")]
    public bool Deduct { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }

    [InverseProperty("PersonnelSupplementaryInsurance")]
    public virtual ICollection<PersonnelSupplementaryInsuranceItem> PersonnelSupplementaryInsuranceItems { get; set; } = new List<PersonnelSupplementaryInsuranceItem>();
}
