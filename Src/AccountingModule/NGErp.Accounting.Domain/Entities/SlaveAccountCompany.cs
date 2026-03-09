using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.Accounting.Domain.Entities;

[Table("slave_account_companies", Schema = "Accounting")]
public class SlaveAccountCompany
{
    public Guid Id { get; set; }
    public bool NeedTtms { get; private set; }
    public bool IsActive { get; private set; } = true;
    public virtual DateTime DueDate { get; private set; }
    public Guid MasterId { get; private set; }
    public Guid SlaveId { get; private set; }
    public Guid LedgerId { get; private set; }

    public required MasterAccount Master { get; set; }
    public required SlaveAccount Slave { get; set; }
    public required Ledger Ledger { get; set; }
}
