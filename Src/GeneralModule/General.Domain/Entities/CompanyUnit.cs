using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.General.Domain.Entities;

[Table("company_units", Schema = "Shared")]
public class CompanyUnit
{
    public Guid Id { get; set; }
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
}
