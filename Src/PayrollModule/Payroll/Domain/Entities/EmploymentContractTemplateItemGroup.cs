using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("employment_contract_template_item_groups", Schema = "payroll")]
[Index("CreatorId", Name = "employment_contract_template_item_groups_creator_id_f392451e")]
public partial class EmploymentContractTemplateItemGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [InverseProperty("EctItemGroup")]
    public virtual ICollection<EmploymentContractTemplateItem> EmploymentContractTemplateItems { get; set; } = new List<EmploymentContractTemplateItem>();
}
