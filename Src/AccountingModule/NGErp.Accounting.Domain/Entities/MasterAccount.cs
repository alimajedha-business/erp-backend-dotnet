using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.Accounting.Domain.Entities;

[Table("master_accounts", Schema = "Accounting")]
public class MasterAccount
{
    public Guid Id { get; set; }
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public string AuthorizedUsers { get; private set; } = default!;
    public Guid? LedgerId { get; private set; }

    public Ledger? Ledger { get; private set; }
}
