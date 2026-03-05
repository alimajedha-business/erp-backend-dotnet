using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.Accounting.Domain.Entities;

[Table("periods", Schema = "Accounting")]
public class Period
{
    public Guid Id { get; set; }
    public string Name { get; private set; } = default!;
    public virtual DateTime StartDate { get; private set; }
    public virtual DateTime EndDate { get; private set; }
    public Guid? PreviousPeriodId { get; private set; }

    public Period? PreviousPeriod { get; set; }
}
