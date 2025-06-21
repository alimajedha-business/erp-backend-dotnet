using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Share.Domain.Entities;

[Table("person_companies", Schema = "shared")]
[Index("CompanyId", Name = "person_companies_company_id_8d893c9c")]
[Index("PersonId", Name = "person_companies_person_id_01c9a592")]
public partial class PersonCompany
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("person_id")]
    public int PersonId { get; set; }
}
