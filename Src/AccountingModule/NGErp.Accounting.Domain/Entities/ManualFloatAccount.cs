using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.Accounting.Domain.Entities;

[Table("manual_float_accounts", Schema = "Accounting")]
public class ManualFloatAccount
{
    public Guid Id { get; set; }
    public int Code { get; private set; }
    public string Title { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public string AuthorizedUsers { get; private set; } = default!;
    public Guid FloatAccountTypeId { get; private set; }
    public Guid? ParentId { get; private set; }

    public required FloatAccountType FloatAccountType { get; set; }
    public ManualFloatAccount? Parent { get; set; }
}
