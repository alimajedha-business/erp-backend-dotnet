using System.ComponentModel.DataAnnotations.Schema;

namespace NGErp.Accounting.Domain.Entities;

[Table("ledgers", Schema = "Accounting")]
public class Ledger
{
    public Guid Id { get; set; }
    public int Code { get; private set; }
    public string Name { get; private set; } = default!;
    public bool IsLeading { get; private set; } = false;
}
