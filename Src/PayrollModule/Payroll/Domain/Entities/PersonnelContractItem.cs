using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Domain.Entities;

[Table("personnel_contract_items", Schema = "payroll")]
[Index("EmploymentContractTemplateItemId", Name = "personnel_contract_items_employment_contract_template_item_id_46dfb850")]
[Index("PersonnelContractId", Name = "personnel_contract_items_personnel_contract_id_06379213")]
public partial class PersonnelContractItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("count", TypeName = "numeric(5, 2)")]
    public decimal Count { get; set; }

    [Column("amount", TypeName = "numeric(18, 2)")]
    public decimal Amount { get; set; }

    [Column("employment_contract_template_item_id")]
    public int EmploymentContractTemplateItemId { get; set; }

    [Column("personnel_contract_id")]
    public int PersonnelContractId { get; set; }

    [ForeignKey("EmploymentContractTemplateItemId")]
    [InverseProperty("PersonnelContractItems")]
    public virtual EmploymentContractTemplateItem EmploymentContractTemplateItem { get; set; } = null!;

    [ForeignKey("PersonnelContractId")]
    [InverseProperty("PersonnelContractItems")]
    public virtual PersonnelContract PersonnelContract { get; set; } = null!;
}
