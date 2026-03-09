using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.Accounting.Domain.Entities;

[Table("slave_accounts", Schema = "Accounting")]
public class SlaveAccount
{
    public Guid Id { get; set; }
    public string Title { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public Guid MasterId { get; private set; }
    public Guid LedgerId { get; private set; }
    public Guid? ParentId { get; private set; }

    public required MasterAccount Master { get; set; }
    public required Ledger Ledger { get; set; }
    public SlaveAccount? Parent { get; set; }
}
